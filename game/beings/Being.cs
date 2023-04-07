namespace game.beings {
    public abstract record Being {
        public abstract string Name { get; init; }
        public abstract int SoulSize { get; }
    }
}
