using game.items.interfaces;

namespace game.items.weapons {

    public abstract record Melee : Item, IDurability {
        //public abstract Character.CharacterSkills.Skill Governor { get; }

        public abstract int Chop { get; }
        public abstract int Thrust { get; }
        public abstract int Slash { get; }

        //public Enchantment? Enchantment { get; set; }

        public int CurrentDurability { get; set; }
        public abstract int MaximumDurability { get; }

        protected Melee() {
            CurrentDurability = MaximumDurability;
        }

        public virtual void OnAttack() {
        }
    }
}