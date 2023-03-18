using game.items;


namespace game.beings.npcs {

    public class Character : Being {

        public abstract class Measurement {
            public int Base { get; set; }
            public int Bonus { get; set; }
            public int Full { get => Base + Bonus; }

            public override string ToString() {
                return $"Base={Base}; Bonus={Bonus}; Full={Full}";
            }
        }

        public class CharacterAttributes {

            public class Attribute : Measurement {

                public Attribute() {
                    Base = 20;
                }

                public static Attribute operator +(Attribute a, int b) {
                    return new Attribute {
                        Base = a.Base,
                        Bonus = a.Bonus + b,
                    };
                }
            }

            // Primary Attributes

            public Attribute Strength { get; set; } = new Attribute();
            public Attribute Intelligence { get; set; } = new Attribute();
            public Attribute Willpower { get; set; } = new Attribute();
            public Attribute Agility { get; set; } = new Attribute();
            public Attribute Speed { get; set; } = new Attribute();
            public Attribute Endurance { get; set; } = new Attribute();
            public Attribute Personality { get; set; } = new Attribute();
            public Attribute Luck { get; set; } = new Attribute();

            // Derived Attributes

            public double MaxHealth { get; set; }
            public double CurrentHealth { get; set; }

            public double MagickaModifier { get; set; } = 1;
            public double MaxMagicka { get; set; }
            public double CurrentMagicka { get; set; }

            public int MaxFatigue { get => Strength.Base + Willpower.Base + Agility.Base + Endurance.Base; }
            public double CurrentFatigue { get; set; }
            public double FatigueP { get => 0.75 + CurrentFatigue / MaxFatigue; }

            public int MaxEncumbrance { get => Strength.Base * 5; }
            public double CurrentEncumbrance { get; set; }
            public double EncumbranceP { get => CurrentEncumbrance / MaxEncumbrance; }

            // Other Attributes

            public double Reputation { get; set; }
            public int Bounty { get; set; }
            public int Slaves { get; set; }

            public CharacterAttributes() {
                MaxHealth = (double)(Strength.Base + Endurance.Base) / 2;
                CurrentHealth = MaxHealth;
                MaxMagicka = Intelligence.Base * MagickaModifier;
                CurrentMagicka = MaxMagicka;
            }
        }

        public class CharacterSkills {

            public class Skill : Measurement {

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

                public static Skill operator +(Skill a, int b) {
                    return new Skill(a.Governor, a.Specialization) {
                        Base = a.Base,
                        Bonus = a.Bonus + b,
                    };
                }
            }

            // Endurance

            public Skill HeavyArmor { get; set; }
            public Skill MediumArmor { get; set; }
            public Skill Spear { get; set; }

            // Strength

            public Skill Acrobatics { get; set; }
            public Skill Armorer { get; set; }
            public Skill Axe { get; set; }
            public Skill BluntWeapon { get; set; }
            public Skill LongBlade { get; set; }

            // Agility

            public Skill Block { get; set; }
            public Skill LightArmor { get; set; }
            public Skill Marksman { get; set; }
            public Skill Sneak { get; set; }

            // Speed

            public Skill Athletics { get; set; }
            public Skill HandToHand { get; set; }
            public Skill ShortBlade { get; set; }
            public Skill Unarmored { get; set; }

            // Personality

            public Skill Illusion { get; set; }
            public Skill Mercantile { get; set; }
            public Skill Speechcraft { get; set; }

            // Intelligence

            public Skill Alchemy { get; set; }
            public Skill Conjuration { get; set; }
            public Skill Enchant { get; set; }
            public Skill Security { get; set; }

            // Willpower

            public Skill Alteration { get; set; }
            public Skill Destruction { get; set; }
            public Skill Mysticism { get; set; }
            public Skill Restoration { get; set; }

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

        public class CharacterInventory {
            private Dictionary<Item, int> _items = new();

            public void AddItem(Item item) {

            }
        }

        public override string Name { get; init; }
        public override int SoulSize { get; } = 100;
        public CharacterAttributes Attributes { get; } = new();
        public CharacterSkills Skills { get; init; }
        protected CharacterInventory Inventory { get; } = new();

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