namespace game.items {

    public abstract class Item {

        public abstract string Name { get; }
        public abstract double Weight { get; }
        public abstract int Value { get; }
        public virtual string Kind { get; } = "Other";
    }
}