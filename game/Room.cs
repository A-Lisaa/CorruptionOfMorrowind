namespace game {
    public record Room : Unique<Room> {
        public required string Name { get; set; }

        public override Dictionary<string, Room> ContainingDict() => World.Rooms;

        // TODO: Joint events (maybe, static?)
        public EventDict<Character> Enter { get; } = new();
        public EventDict<Character> Leave { get; } = new();
        public EventDict<Character> Sleep { get; } = new();
    }
}
