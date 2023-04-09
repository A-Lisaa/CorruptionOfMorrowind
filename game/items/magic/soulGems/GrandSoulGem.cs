namespace game.items.magic.soulGems {
    public record GrandSoulGem : SoulGem {
        protected override string BaseName { get; } = "Grand Soul Gem";
        protected override int BaseValue { get; } = 200;
        public override double Weight { get; } = 2;
        public override int Capacity { get; } = 600;
    }
}
