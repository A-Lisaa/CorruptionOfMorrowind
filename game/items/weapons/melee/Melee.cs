using game.beings.npcs;
using game.items.interfaces;

namespace game.items.weapons {

    public abstract class Melee : Item, IDurability {
        //public abstract Character.CharacterSkills.Skill Governor { get; }

        public abstract int Chop { get; }
        public abstract int Thrust { get; }
        public abstract int Slash { get; }

        //public Enchantment? Enchantment { get; set; }

        public int CurrentDurability { get; set; }
        public abstract int MaximumDurability { get; }
        public double DurabilityP { get => CurrentDurability / MaximumDurability; }

        //public override List<object> Metadata {
        //    get {
        //        var meta = base.Metadata;

        //        meta.AddRange(
        //            new List<object>() { $"{CurrentDurability} / {MaximumDurability}" }
        //        );

        //        return meta;
        //    }
        //}

        protected Melee(int? currentDurability = null) {
            if (currentDurability != null) {
                CurrentDurability = MaximumDurability;
            }
        }
    }
}