using game.beings;

namespace game.items.magic.soulGems {
    public class GreaterSoulGem : SoulGem {
        protected override string BaseName { get; } = "Greater Soul Gem";
        protected override int BaseValue { get; } = 60;
        public override double Weight { get; } = 1.5;
        public override int Capacity { get; } = 180;

        public GreaterSoulGem(Being? creature = null) : base(creature) {
        }
    }
}
