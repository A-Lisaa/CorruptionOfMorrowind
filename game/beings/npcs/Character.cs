using game.items;


namespace game.beings.npcs {

    public class Character : Being {

        public abstract record Measurement {
            private int @base;
            private int bonus;

            public int Base { get => @base; set => @base = (value > 0 ? value : 0); }
            public int Bonus { get => bonus; set => bonus = (value > 0 ? value : 0); }
            public int Full { get => Base + Bonus; }
        }

        public record CharacterAttributes {
            private double maxHealth;
            private double currentHealth;
            private double magickaModifier = 1;
            private double maxMagicka;
            private double currentMagicka;
            private double currentFatigue;
            private double currentEncumbrance;
            private double reputation;
            private int bounty;
            private int slaves;

            public record Attribute : Measurement {

                public Attribute() {
                    Base = 20;
                }
            }

            // Primary Attributes

            public Attribute Strength { get; } = new();
            public Attribute Intelligence { get; } = new();
            public Attribute Willpower { get; } = new();
            public Attribute Agility { get; } = new();
            public Attribute Speed { get; } = new();
            public Attribute Endurance { get; } = new();
            public Attribute Personality { get; } = new();
            public Attribute Luck { get; } = new();

            // Derived Attributes

            public double MaxHealth { get => maxHealth; set => maxHealth = (value > 0 ? value : 0); }
            public double CurrentHealth { get => currentHealth; set => currentHealth = (value > 0 ? value : 0); }

            public double MagickaModifier { get => magickaModifier; set => magickaModifier = (value > 0 ? value : 0); }
            public double MaxMagicka { get => maxMagicka; set => maxMagicka = (value > 0 ? value : 0); }
            public double CurrentMagicka { get => currentMagicka; set => currentMagicka = (value > 0 ? value : 0); }

            public int MaxFatigue { get => Strength.Base + Willpower.Base + Agility.Base + Endurance.Base; }
            public double CurrentFatigue { get => currentFatigue; set => currentFatigue = (value > 0 ? value : 0); }
            public double FatigueP { get => CurrentFatigue / MaxFatigue; }
            public double FatiguePFull { get => 0.75 + FatigueP; }

            public int MaxEncumbrance { get => Strength.Base * 5; }
            public double CurrentEncumbrance { get => currentEncumbrance; set => currentEncumbrance = (value > 0 ? value : 0); }
            public double EncumbranceP { get => CurrentEncumbrance / MaxEncumbrance; }

            // Other Attributes

            public double Reputation { get => reputation; set => reputation = (value > 0 ? value : 0); }
            public int Bounty { get => bounty; set => bounty = (value > 0 ? value : 0); }
            public int Slaves { get => slaves; set => slaves = (value > 0 ? value : 0); }

            public CharacterAttributes() {
                MaxHealth = (double)(Strength.Base + Endurance.Base) / 2;
                CurrentHealth = MaxHealth;
                MaxMagicka = Intelligence.Base * MagickaModifier;
                CurrentMagicka = MaxMagicka;
            }
        }

        public record CharacterSkills {

            public record Skill : Measurement {

                public enum Specializations {
                    COMBAT,
                    STEALTH,
                    MAGIC
                }

                public CharacterAttributes.Attribute Governor { get; init; }
                public Specializations Specialization { get; init; }

                public Skill(CharacterAttributes.Attribute governor, Specializations specialization) {
                    Governor = governor;
                    Specialization = specialization;
                    Base = 5;
                }
            }

            // Endurance

            public Skill HeavyArmor { get; }
            public Skill MediumArmor { get; }
            public Skill Spear { get; }

            // Strength

            public Skill Acrobatics { get; }
            public Skill Armorer { get; }
            public Skill Axe { get; }
            public Skill BluntWeapon { get; }
            public Skill LongBlade { get; }

            // Agility

            public Skill Block { get; }
            public Skill LightArmor { get; }
            public Skill Marksman { get; }
            public Skill Sneak { get; }

            // Speed

            public Skill Athletics { get; }
            public Skill HandToHand { get; }
            public Skill ShortBlade { get; }
            public Skill Unarmored { get; }

            // Personality

            public Skill Illusion { get; }
            public Skill Mercantile { get; }
            public Skill Speechcraft { get; }

            // Intelligence

            public Skill Alchemy { get; }
            public Skill Conjuration { get; }
            public Skill Enchant { get; }
            public Skill Security { get; }

            // Willpower

            public Skill Alteration { get; }
            public Skill Destruction { get; }
            public Skill Mysticism { get; }
            public Skill Restoration { get; }

            public CharacterSkills(CharacterAttributes attributes) {
                /* Skills */

                // Endurance

                HeavyArmor = new(attributes.Endurance, Skill.Specializations.COMBAT);
                MediumArmor = new(attributes.Endurance, Skill.Specializations.COMBAT);
                Spear = new(attributes.Endurance, Skill.Specializations.COMBAT);

                // Strength

                Acrobatics = new(attributes.Strength, Skill.Specializations.STEALTH);
                Armorer = new(attributes.Strength, Skill.Specializations.COMBAT);
                Axe = new(attributes.Strength, Skill.Specializations.COMBAT);
                BluntWeapon = new(attributes.Strength, Skill.Specializations.COMBAT);
                LongBlade = new(attributes.Strength, Skill.Specializations.COMBAT);

                // Agility

                Block = new(attributes.Agility, Skill.Specializations.COMBAT);
                LightArmor = new(attributes.Agility, Skill.Specializations.STEALTH);
                Marksman = new(attributes.Agility, Skill.Specializations.STEALTH);
                Sneak = new(attributes.Agility, Skill.Specializations.STEALTH);

                // Speed

                Athletics = new(attributes.Speed, Skill.Specializations.COMBAT);
                HandToHand = new(attributes.Speed, Skill.Specializations.STEALTH);
                ShortBlade = new(attributes.Speed, Skill.Specializations.STEALTH);
                Unarmored = new(attributes.Speed, Skill.Specializations.MAGIC);

                // Personality

                Illusion = new(attributes.Personality, Skill.Specializations.MAGIC);
                Mercantile = new(attributes.Personality, Skill.Specializations.STEALTH);
                Speechcraft = new(attributes.Personality, Skill.Specializations.STEALTH);

                // Intelligence

                Alchemy = new(attributes.Intelligence, Skill.Specializations.MAGIC);
                Conjuration = new(attributes.Intelligence, Skill.Specializations.MAGIC);
                Enchant = new(attributes.Intelligence, Skill.Specializations.MAGIC);
                Security = new(attributes.Intelligence, Skill.Specializations.STEALTH);

                // Willpower

                Alteration = new(attributes.Willpower, Skill.Specializations.MAGIC);
                Destruction = new(attributes.Willpower, Skill.Specializations.MAGIC);
                Mysticism = new(attributes.Willpower, Skill.Specializations.MAGIC);
                Restoration = new(attributes.Willpower, Skill.Specializations.MAGIC);
            }
        }

        public record CharacterInventory {
            private readonly Dictionary<Item, int> _items = new();

            public override string ToString() {
                return string.Join(
                    "\n",
                    _items.Select(x => $"{x.Key} = {x.Value}")
                );
            }

            private IEnumerable<Item> ItemLookup(Item item) {
                var query =
                    from elem in _items
                    where elem.Key == item
                    select elem.Key;

                return query;
            }

            public void AddItem(Item item) {
                var query = ItemLookup(item);

                if (!query.Any()) {
                    _items[item] = 1;
                }
                else {
                    _items[item] += 1;
                }
            }

            public void RemoveItem(Item item) {
                var query = ItemLookup(item);

                if (!query.Any()) {
                    return;
                }

                _items[item] -= 1;

                if (_items[item] == 0) {
                    _items.Remove(item);
                }
            }

            public void RemoveItemByIndex(int index) {
                Item item  = _items.ElementAtOrDefault(index).Key;

                if (item != default) {
                    _items[item] -= 1;

                    if (_items[item] == 0) {
                        _items.Remove(item);
                    }
                }
            }
        }

        public override string Name { get; init; }
        public override int SoulSize { get; } = 100;
        public CharacterAttributes Attributes { get; } = new();
        public CharacterSkills Skills { get; init; }
        public CharacterInventory Inventory { get; } = new();

        public Character(string name) {
            Skills = new(Attributes);

            Name = name;
        }

        public void MeleeAttack(Character target) {
            var rand = new Random();

            double hitRate = (
                Skills.BluntWeapon.Full +
                Attributes.Agility.Full / 5 +
                Attributes.Luck.Full / 10
            ) * Attributes.FatigueP;
            double evasion = (
                target.Attributes.Agility.Full / 5 +
                target.Attributes.Luck.Full / 10
            ) * target.Attributes.FatigueP;
            double hitChance = (hitRate - evasion) / 100;

            if (rand.NextDouble() < hitChance) {
                double dmg = 20;//Change

                target.DealDamage(dmg);

                Console.WriteLine($"{Name} hit {target.Name} with chance {hitChance} for {dmg}");
            }
            else {
                Console.WriteLine($"{Name} did not hit {target.Name} with chance {hitChance}");
            }
        }

        public void DealDamage(double damage) {
            Attributes.CurrentHealth -= damage;
        }


        public bool IsDead {
            get => Attributes.CurrentHealth <= 0;
        }

        public void SetDead() {
            Attributes.CurrentHealth = 0;
        }
    }
}