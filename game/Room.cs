using System.Drawing;

namespace game {
#pragma warning disable S2094
    public class RoomEventArgs : GameEventArgs {
    }
#pragma warning restore S2094

    public static partial class RoomEvents {
        public static void ChangeInvokerNameToSuccubus(object? sender, RoomEventArgs args) {
            args.Invoker.Name = "Succubus";
        }

        public static void Add100HpToKasyrrasMaximum(object? _, RoomEventArgs args) {
            World.Characters["kas"].MaxHealth += 100;
        }
    }

    public static partial class RoomEvents {
        public static void SetInvokerIntelliigenceTo100(object? sender, RoomEventArgs args) {
            args.Invoker.Intelligence.Base = 100;
        }
    }

    public record RoomGroup {
        public required string Name { get; set; }
    }

    public record Passage {
        public required Room Destination { get; set; }
        public required int Time { get; set; }

        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnEnter;

        public void Enter(Character invoker) {
            OnEnter?.Invoke(this, new RoomEventArgs() { Invoker = invoker });
        }
    }

    public record Room : Unique<Room> {
        public required string Name { get; set; }
        public string Description { get; set; } = "";
#pragma warning disable CA1002
        public List<RoomGroup> Groups { get; } = new();
#pragma warning restore CA1002

        #region Passages
        public Passage? North { get; set; }
        public Passage? NorthEast { get; set; }
        public Passage? East { get; set; }
        public Passage? SouthEast { get; set; }
        public Passage? South { get; set; }
        public Passage? SouthWest { get; set; }
        public Passage? West { get; set; }
        public Passage? NorthWest { get; set; }
        #endregion

        // TODO: Joint events (in Group?)
        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnEnter;
        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnLeave;
        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnSleep;

        public override Dictionary<string, Room> ContainingDict() => World.Rooms;

        public void Enter(Character invoker) {
            OnEnter?.Invoke(this, new RoomEventArgs() { Invoker = invoker });
        }

        public void Leave(Character invoker) {
            OnLeave?.Invoke(this, new RoomEventArgs() { Invoker = invoker });
        }

        public void Sleep(Character invoker) {
            OnSleep?.Invoke(this, new RoomEventArgs() { Invoker = invoker });
        }
    }
}
