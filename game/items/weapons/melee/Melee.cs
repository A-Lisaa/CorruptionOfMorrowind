namespace game.items.weapons {

    public abstract class Melee {
        public abstract Character.CharacterSkills.Skill Governor { get; }

        public abstract int Chop { get; }
        public abstract int Thrust { get; }
        public abstract int Slash { get; }

        //public Enchantment? Enchantment { get; set; }

        public int CurrentCondition { get; set; }
        public abstract int MaximumCondition { get; }
        public int ConditionP { get => CurrentCondition / MaximumCondition; }

        protected Melee() {
            CurrentCondition = MaximumCondition;
        }
    }
}