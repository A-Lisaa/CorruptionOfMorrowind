namespace game {
    public record Location : Unique<Location> {
        public required string Name { get; set; }

        public override Dictionary<string, Location> ContainingDict() => World.Locations;
    }
}
