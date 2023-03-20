namespace game.items.weapons.melee.generic.axes.oneHand {
    public record AxeOneHandEbony : OneHand {
        public override int Chop => 19;

        public override int Thrust => 3;

        public override int Slash => 10;

        public override int MaximumDurability => 2400;

        public override string Name => "Ebony War Axe";

        public override double Weight => 48;

        public override int Value => 15_000;

        public AxeOneHandEbony() : base() {
        }
    }
}
