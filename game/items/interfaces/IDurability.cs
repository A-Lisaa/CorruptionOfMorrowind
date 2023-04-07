namespace game.items.interfaces {
    public interface IDurability {
        int CurrentDurability { get; set; }
        int MaximumDurability { get; }
        double DurabilityP { get => CurrentDurability / MaximumDurability; }
    }
}
