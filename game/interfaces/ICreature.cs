namespace game.interfaces {
    public interface ICreature {
        string Name { get; set; }
    }

    public interface ISoulfulCreature : ICreature {
        int SoulSize { get; }
    }
}
