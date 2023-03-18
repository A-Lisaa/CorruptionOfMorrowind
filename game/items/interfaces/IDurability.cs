namespace game.items.interfaces {
    public interface IDurability {
        public abstract int CurrentDurability { get; set; }
        public abstract int MaximumDurability { get; }
        public abstract double DurabilityP { get; }
    }
}
