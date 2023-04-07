namespace game.beings.creatures {
    public record Creeper : Being {
        public override string Name { get; init; } = "Creeper";
        public override int SoulSize { get; } = 10;
    }
}
