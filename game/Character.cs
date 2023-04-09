using game.items;
using game.interfaces;
using utils.extensions;

namespace game {
    public abstract record Measurement {
        private int @base;
        private int bonus;

        public int Base { get => @base; set => @base = (value > 0 ? value : 0); }
        public int Bonus { get => bonus; set => bonus = (value > 0 ? value : 0); }
        public int Full { get => Base + Bonus; }
    }

    public sealed record CharacterAttr : Measurement {
        public CharacterAttr() {
            Base = 20;
        }
    }

    public sealed record CharacterSkill : Measurement {
        public enum Specializations {
            COMBAT,
            STEALTH,
            MAGIC
        }

        public CharacterAttr Governor { get; init; }
        public Specializations Specialization { get; init; }

        public CharacterSkill(CharacterAttr governor, Specializations specialization) {
            Governor = governor;
            Specialization = specialization;
            Base = 5;
        }
    }

    public sealed record Descriptor {
#pragma warning disable CA1002
        public List<string> Nouns { get; } = new List<string>();
        public List<string> Adjectives { get; } = new List<string>();
#pragma warning restore CA1002
    }

    public class ItemComparer : Comparer<Item> {
        public override int Compare(Item? x, Item? y) {
            //if (x is not null && y is not null) {
            //    return x.Name.CompareTo(y.Name);
            //}
            //else if (x is null && y is null) {
            //    return 0;
            //}
            //else if (x is null && y is not null) {
            //    return -1;
            //}
            //else if (x is not null && y is null) {
            //    return 1;
            //}
            //Log.Error($"ItemComparer somehow fucked up null checks for x={x}, y={y}");
            //return 0;
            return string.CompareOrdinal(x?.Name, y?.Name);
        }
    }

    public sealed record CharacterInventory {
        private readonly SortedDictionary<Item, int> _items = new(new ItemComparer());

        public void AddItem(Item item) {
#pragma warning disable CA1854
            if (_items.ContainsKey(item)) {
#pragma warning restore CA1854
                _items[item]++;
            }
            else {
                _items[item] = 1;
            }
        }

        public void RemoveItem(Item item) {
            if (!_items.ContainsKey(item)) {
                return;
            }

            _items[item]--;

            if (_items[item] == 0) {
                _items.Remove(item);
            }
        }
    }

    public sealed record Character : Unique<Character>, ISoulfulCreature {
        public enum Genders {
            Male,
            Female,
            Herm
        }

        #region Attributes
        private double currentHealth;
        private double maxHealth = 25;

        private double currentMagicka;
        private double maxMagicka = 100;
        private double magickaModifier = 1;

        private double currentFatigue;

        private double currentEncumbrance;

        private double reputation;
        private int bounty;
        private int slaves;

        // Primary Attributes

        public CharacterAttr Strength { get; } = new();
        public CharacterAttr Intelligence { get; } = new();
        public CharacterAttr Willpower { get; } = new();
        public CharacterAttr Agility { get; } = new();
        public CharacterAttr Speed { get; } = new();
        public CharacterAttr Endurance { get; } = new();
        public CharacterAttr Personality { get; } = new();
        public CharacterAttr Luck { get; } = new();

        // Derived Attributes

        public double CurrentHealth { get => currentHealth; set => currentHealth = (value > 0 ? value : 0); }
        public double MaxHealth { get => maxHealth; set => maxHealth = (value > 0 ? value : 0); }

        public double CurrentMagicka { get => currentMagicka; set => currentMagicka = (value > 0 ? value : 0); }
        public double MaxMagicka { get => maxMagicka * MagickaModifier; set => maxMagicka = (value > 0 ? value : 0); }
        public double MagickaModifier { get => magickaModifier; set => magickaModifier = (value > 0 ? value : 0); }

        public double CurrentFatigue { get => currentFatigue; set => currentFatigue = (value > 0 ? value : 0); }
        public double MaxFatigue {
            get {
                double result = Strength.Base + Willpower.Base + Agility.Base + Endurance.Base + MaxFatigueBonus;
                return result > 0 ? result : 0;
            }
        }
        public double MaxFatigueBonus { get; set; }
        public double FatigueP { get => CurrentFatigue / MaxFatigue; }

        public double CurrentEncumbrance { get => currentEncumbrance; set => currentEncumbrance = (value > 0 ? value : 0); }
        public double MaxEncumbrance {
            get {
                double result = (Strength.Base * 5) + MaxEncumbranceBonus;
                return result > 0 ? result : 0;
            }
        }
        public double MaxEncumbranceBonus { get; set; }
        public double EncumbranceP { get => CurrentEncumbrance / MaxEncumbrance; }

        // Other Attributes

        public double Reputation { get => reputation; set => reputation = (value > 0 ? value : 0); }
        public int Bounty { get => bounty; set => bounty = (value > 0 ? value : 0); }
        public int Slaves { get => slaves; set => slaves = (value > 0 ? value : 0); }
        #endregion

        #region Skills
        public CharacterSkill HeavyArmor { get; init; }
        public CharacterSkill MediumArmor { get; init; }
        public CharacterSkill Spear { get; init; }

        // Strength

        public CharacterSkill Acrobatics { get; init; }
        public CharacterSkill Armorer { get; init; }
        public CharacterSkill Axe { get; init; }
        public CharacterSkill BluntWeapon { get; init; }
        public CharacterSkill LongBlade { get; init; }

        // Agility

        public CharacterSkill Block { get; init; }
        public CharacterSkill LightArmor { get; init; }
        public CharacterSkill Marksman { get; init; }
        public CharacterSkill Sneak { get; init; }

        // Speed

        public CharacterSkill Athletics { get; init; }
        public CharacterSkill HandToHand { get; init; }
        public CharacterSkill ShortBlade { get; init; }
        public CharacterSkill Unarmored { get; init; }

        // Personality

        public CharacterSkill Illusion { get; init; }
        public CharacterSkill Mercantile { get; init; }
        public CharacterSkill Speechcraft { get; init; }

        // Intelligence

        public CharacterSkill Alchemy { get; init; }
        public CharacterSkill Conjuration { get; init; }
        public CharacterSkill Enchant { get; init; }
        public CharacterSkill Security { get; init; }

        // Willpower

        public CharacterSkill Alteration { get; init; }
        public CharacterSkill Destruction { get; init; }
        public CharacterSkill Mysticism { get; init; }
        public CharacterSkill Restoration { get; init; }
        #endregion

        #region Appearance
        public Descriptor Race { get; set; } = new();
        public Descriptor Head { get; set; } = new();
        #endregion

        public required string Name { get; set; }
        public required Genders Gender { get; init; }
        public int SoulSize { get; } = 100;

        public CharacterInventory Inventory { get; } = new();

        private static readonly Room _WaitRoom = new() {
            Name = "(!Technical!) Wait Room"
        };
        public Room CurrentRoom { get; private set; } = _WaitRoom;

        public override Dictionary<string, Character> ContainingDict() => World.Characters;

        public Character() {
            #region Attributes Setting
            CurrentHealth = MaxHealth;
            CurrentMagicka = MaxMagicka;
            CurrentFatigue = MaxFatigue;
            #endregion
            #region Skills Construction
            // Endurance

            HeavyArmor = new(Endurance, CharacterSkill.Specializations.COMBAT);
            MediumArmor = new(Endurance, CharacterSkill.Specializations.COMBAT);
            Spear = new(Endurance, CharacterSkill.Specializations.COMBAT);

            // Strength

            Acrobatics = new(Strength, CharacterSkill.Specializations.STEALTH);
            Armorer = new(Strength, CharacterSkill.Specializations.COMBAT);
            Axe = new(Strength, CharacterSkill.Specializations.COMBAT);
            BluntWeapon = new(Strength, CharacterSkill.Specializations.COMBAT);
            LongBlade = new(Strength, CharacterSkill.Specializations.COMBAT);

            // Agility

            Block = new(Agility, CharacterSkill.Specializations.COMBAT);
            LightArmor = new(Agility, CharacterSkill.Specializations.STEALTH);
            Marksman = new(Agility, CharacterSkill.Specializations.STEALTH);
            Sneak = new(Agility, CharacterSkill.Specializations.STEALTH);

            // Speed

            Athletics = new(Speed, CharacterSkill.Specializations.COMBAT);
            HandToHand = new(Speed, CharacterSkill.Specializations.STEALTH);
            ShortBlade = new(Speed, CharacterSkill.Specializations.STEALTH);
            Unarmored = new(Speed, CharacterSkill.Specializations.MAGIC);

            // Personality

            Illusion = new(Personality, CharacterSkill.Specializations.MAGIC);
            Mercantile = new(Personality, CharacterSkill.Specializations.STEALTH);
            Speechcraft = new(Personality, CharacterSkill.Specializations.STEALTH);

            // Intelligence

            Alchemy = new(Intelligence, CharacterSkill.Specializations.MAGIC);
            Conjuration = new(Intelligence, CharacterSkill.Specializations.MAGIC);
            Enchant = new(Intelligence, CharacterSkill.Specializations.MAGIC);
            Security = new(Intelligence, CharacterSkill.Specializations.STEALTH);

            // Willpower

            Alteration = new(Willpower, CharacterSkill.Specializations.MAGIC);
            Destruction = new(Willpower, CharacterSkill.Specializations.MAGIC);
            Mysticism = new(Willpower, CharacterSkill.Specializations.MAGIC);
            Restoration = new(Willpower, CharacterSkill.Specializations.MAGIC);
            #endregion
        }

        public void Move(Room destination) {
            CurrentRoom.Leave.Invoke(this);
            CurrentRoom = destination;
            CurrentRoom.Enter.Invoke(this);
        }

        public void TakeDamage(double damage) {
            CurrentHealth -= damage;
        }

        public bool IsDead {
            get => CurrentHealth <= 0;
        }

        public void SetDead() {
            CurrentHealth = 0;
        }
    }
}