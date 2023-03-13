using game.creatures;

namespace game.items.magic
{
    public abstract class SoulGem : Item {

        protected abstract string BaseName { get; }
        public override string Name { get => (SoulName != null) ? $"{BaseName} ({SoulName})" : BaseName; }

        protected abstract int BaseValue { get; }
        public override int Value { get => (SoulSize != null) ? (BaseValue + (int)SoulSize * 5) : BaseValue; }

        public abstract int Capacity { get; }
        public string? SoulName { get; private set; }
        public int? SoulSize { get; private set; }

        private void CheckEmptiness() {
            if (SoulName != null || SoulSize != null) {
                throw new InvalidOperationException(
                    "Soul gem is not empty"
                );
            }
        }

        private void CheckCapacity(Creature creature) {
            if (creature.SoulSize > Capacity) {
                throw new ArgumentOutOfRangeException(
                    nameof(creature),
                    "Creature's soul must be less or equal to soul gem's capacity"
                );
            }
        }

        protected SoulGem() {
            SoulName = null;
            SoulSize = null;
        }

        protected SoulGem(Creature creature) {
            CheckCapacity(creature);

            SoulName = creature.Name;
            SoulSize = creature.SoulSize;
        }

        public void AddSoul(Creature creature) {
            CheckEmptiness();
            CheckCapacity(creature);

            SoulName = creature.Name;
            SoulSize = creature.SoulSize;
        }
    }

    public class PettySoulGem : SoulGem {
        protected override string BaseName { get; } = "Petty Soul Gem";
        protected override int BaseValue { get; } = 10;
        public override double Weight { get; } = 0.2;
        public override int Capacity { get; } = 30;

        public PettySoulGem() : base() {
        }

        public PettySoulGem(Creature creature) : base(creature) {
        }
    }

    public class LesserSoulGem : SoulGem {
        protected override string BaseName { get; } = "Lesser Soul Gem";
        protected override int BaseValue { get; } = 20;
        public override double Weight { get; } = 0.5;
        public override int Capacity { get; } = 60;

        public LesserSoulGem() : base() {
        }

        public LesserSoulGem(Creature creature) : base(creature) {
        }
    }

    public class CommonSoulGem : SoulGem {
        protected override string BaseName { get; } = "Common Soul Gem";
        protected override int BaseValue { get; } = 40;
        public override double Weight { get; } = 1;
        public override int Capacity { get; } = 120;

        public CommonSoulGem() : base() {
        }

        public CommonSoulGem(Creature creature) : base(creature) {
        }
    }

    public class GreaterSoulGem : SoulGem {
        protected override string BaseName { get; } = "Greater Soul Gem";
        protected override int BaseValue { get; } = 60;
        public override double Weight { get; } = 1.5;
        public override int Capacity { get; } = 180;

        public GreaterSoulGem() : base() {
        }

        public GreaterSoulGem(Creature creature) : base(creature) {
        }
    }

    public class GrandSoulGem : SoulGem {
        protected override string BaseName { get; } = "Grand Soul Gem";
        protected override int BaseValue { get; } = 200;
        public override double Weight { get; } = 2;
        public override int Capacity { get; } = 600;

        public GrandSoulGem() : base() {
        }

        public GrandSoulGem(Creature creature) : base(creature) {
        }
    }

    public class AzuraStarSoulGem : SoulGem {
        protected override string BaseName { get; } = "Azura's Star";
        protected override int BaseValue { get; } = 5000;
        public override double Weight { get; } = 2;
        public override int Capacity { get; } = 15000;

        public AzuraStarSoulGem() : base() {
        }

        public AzuraStarSoulGem(Creature creature) : base(creature) {
        }
    }
}
