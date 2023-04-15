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
        public Color Color { get; set; } = Color.White;
    }

    public record Room : Unique<Room> {
        public required string Name { get; set; }
#pragma warning disable CA1002
        public List<RoomGroup> Groups { get; } = new();
#pragma warning restore CA1002

        // TODO: Passages to other Rooms and, maybe, to non-Room locations (or Room locations not in Location)

        public override Dictionary<string, Room> ContainingDict() => World.Rooms;

        // TODO: Joint events (in Group?)
        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnEnter;
        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnLeave;
        [GameEvent]
        public event EventHandler<RoomEventArgs>? OnSleep;

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
