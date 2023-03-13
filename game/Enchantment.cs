namespace game {

    public abstract class Enchantment {
        public List<BaseEffect> Effects { get; init; }

        public int Cost {
            get {
                int i = Effects.Count;
                int cost = 0;
                foreach (BaseEffect effect in Effects) {
                    //cost += effect.Cost * i;
                    i--;
                }
                return cost;
            }
        }

        public int CurrentCharge { get; set; }
        public int MaxCharge { get; init; }

        public int Size { get; init; }

        protected Enchantment(List<BaseEffect> effects, int charge, int size) {
            Effects = effects;
            MaxCharge = charge;
            Size = size;

            CurrentCharge = MaxCharge;
        }
    }

    public class OnUseEnchantment : Enchantment {

        public OnUseEnchantment(List<BaseEffect> effects, int charge, int size) : base(effects, charge, size) {
        }
    }

    public class OnStrikeEnchantment : Enchantment {

        public OnStrikeEnchantment(List<BaseEffect> effects, int charge, int size) : base(effects, charge, size) {
        }
    }

    public class ConstantEnchantment : Enchantment {
        private const int MIN_CHARGE = 400;

        public ConstantEnchantment(List<BaseEffect> effects, int charge, int size) : base(effects, charge, size) {
            if (charge < MIN_CHARGE) {
                throw new ArgumentOutOfRangeException(
                    nameof(charge),
                    $"[charge] must be more or equal to [MIN_CHARGE] ({MIN_CHARGE})"
                );
            }
        }
    }
}