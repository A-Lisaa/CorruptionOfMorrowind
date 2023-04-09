using game.interfaces;

namespace game {
    public record Being : Unique<Being>, ICreature {
        public required string Name { get; set; } = "";

        public override Dictionary<string, Being> ContainingDict() => World.Beings;
    }
}
