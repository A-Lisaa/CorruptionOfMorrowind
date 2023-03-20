using game.beings;

namespace game.items.magic.soulGems {
    public record CommonSoulGem : SoulGem {
        protected override string BaseName { get; } = "Common Soul Gem";
        protected override int BaseValue { get; } = 40;
        public override double Weight { get; } = 1;
        public override int Capacity { get; } = 120;

        public CommonSoulGem() : base() {
        }

        public CommonSoulGem(Being creature) : base(creature) {
        }
    }
}
