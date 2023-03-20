namespace game.items.interfaces {
    public interface IDurability {
        public abstract int CurrentDurability { get; set; }
        public abstract int MaximumDurability { get; }
    }

    public static class IDurabilityExtensions {
        public static double DurabilityP(this IDurability durability) {
            return (double)durability.CurrentDurability / durability.MaximumDurability;
        }
    }
}
