namespace game.items.junk {
    public record Glass : Item {
        public override string Name { get; } = "Glass";
        public override double Weight { get; } = 0.1;
        public override int Value { get; } = 5;
    }
}
