using game.beings;

namespace game.items.magic.soulGems {
    public record LesserSoulGem : SoulGem {
        protected override string BaseName { get; } = "Lesser Soul Gem";
        protected override int BaseValue { get; } = 20;
        public override double Weight { get; } = 0.5;
        public override int Capacity { get; } = 60;

        public LesserSoulGem(Being? creature = null) : base(creature) {
        }
    }
}
