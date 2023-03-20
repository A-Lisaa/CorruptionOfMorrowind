using game.beings;

namespace game.items.magic.soulGems {
    public record PettySoulGem : SoulGem {
        protected override string BaseName { get; } = "Petty Soul Gem";
        protected override int BaseValue { get; } = 10;
        public override double Weight { get; } = 0.2;
        public override int Capacity { get; } = 30;

        public PettySoulGem(Being? creature = null) : base(creature) {
        }
    }
}
