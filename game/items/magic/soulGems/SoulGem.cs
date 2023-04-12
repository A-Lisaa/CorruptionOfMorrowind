using game.interfaces;

namespace game.items.magic.soulGems {
    public abstract record SoulGem : Item {
        protected abstract string BaseName { get; }
        public override string Name { get => Soul is not null ? $"{BaseName} ({Soul.Name})" : BaseName; }

        protected abstract int BaseValue { get; }
        public override int Value { get => Soul is not null ? BaseValue + (Soul.SoulSize * 5) : BaseValue; }

        public abstract int Capacity { get; }
        public ISoulfulCreature? Soul { get; protected set; }

        /// <summary>
        /// Traps <paramref name="creature"/> in the soul gem.
        /// </summary>
        /// <param name="creature">Creature to be trapped in the soul gem.</param>
        /// <returns>true if succesful, otherwise false</returns>
        public bool AddSoul(ISoulfulCreature creature) {
            if (creature is null) {
                return false;
            }
            if (Soul is not null) {
                Log.Error($"Soul gem {this} is not empty");
                return false;
            }
            if (creature.SoulSize > Capacity) {
                Log.Error($"Soul of {creature} is larger than soul gem {this} capacity");
                return false;
            }

            Soul = creature;
            return true;
        }
    }
}
