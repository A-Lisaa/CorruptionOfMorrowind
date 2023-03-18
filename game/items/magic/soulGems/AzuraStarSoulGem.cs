using game.beings;
using game.items.magic.soulGems.interfaces;

namespace game.items.magic.soulGems {
    public class AzuraStarSoulGem : SoulGem, IIndestructibleSoulGem {
        protected override string BaseName { get; } = "Azura's Star";
        protected override int BaseValue { get; } = 5000;
        public override double Weight { get; } = 2;
        public override int Capacity { get; } = 15000;

        public AzuraStarSoulGem(Being? creature = null) : base(creature) {
        }

        public void RemoveSoul() {
            SoulName = null;
            SoulSize = null;
        }
    }
}
