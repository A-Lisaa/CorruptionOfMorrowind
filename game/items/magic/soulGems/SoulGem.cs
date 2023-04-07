using game.beings;


namespace game.items.magic.soulGems {
    public abstract record SoulGem : Item {
        protected abstract string BaseName { get; }
        public override string Name { get => SoulName is not null ? $"{BaseName} ({SoulName})" : BaseName; }

        protected abstract int BaseValue { get; }
        public override int Value { get => SoulSize is not null ? BaseValue + (int)SoulSize * 5 : BaseValue; }

        public abstract int Capacity { get; }
        public string? SoulName { get; protected set; }
        public int? SoulSize { get; protected set; }

        private void CheckEmptiness() {
            if (SoulName is not null || SoulSize is not null) {
                throw new InvalidOperationException(
                    "Soul gem is not empty"
                );
            }
        }

        private void CheckCapacity(Being creature) {
            if (creature.SoulSize > Capacity) {
                throw new ArgumentOutOfRangeException(
                    nameof(creature),
                    "Creature's soul must be less or equal to soul gem's capacity"
                );
            }
        }

        protected SoulGem(Being? creature = null) {
            if (creature is null) {
                SoulName = null;
                SoulSize = null;
                return;
            }
            CheckCapacity(creature);

            SoulName = creature.Name;
            SoulSize = creature.SoulSize;
        }

        public void AddSoul(Being creature) {
            CheckEmptiness();
            CheckCapacity(creature);

            SoulName = creature.Name;
            SoulSize = creature.SoulSize;
        }

        public void TryAddSoul(Being creature) {
            try {
                AddSoul(creature);
            }
            catch (InvalidOperationException) {
                Console.WriteLine("Soul gem is not empty, aborting operation");
            }
        }
    }
}
