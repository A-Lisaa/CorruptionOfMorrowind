using System.Drawing;

namespace game {
    public static partial class RoomEvents {
        public static void ChangeInvokerNameToSuccubus(Character invoker) {
            if (invoker is null) {
                throw new ArgumentNullException(nameof(invoker));
            }
            invoker.Name = "Succubus";
        }

        public static void Add100HpToKasyrrasMaximum(Character _) {
            World.Characters["kas"].MaxHealth += 100;
        }
    }

    public static partial class RoomEvents {
        public static void SetInvokerIntelliigenceTo100(Character invoker) {
            if (invoker is null) {
                throw new ArgumentNullException(nameof(invoker));
            }
            invoker.Intelligence.Base = 100;
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

        public override Dictionary<string, Room> ContainingDict() => World.Rooms;

        // TODO: Joint events (maybe, static?)
        public event Action<Character>? OnEnter;
        public event Action<Character>? OnLeave;
        public event Action<Character>? OnSleep;

        public void Enter(Character invoker) {
            OnEnter?.Invoke(invoker);
        }

        public void Leave(Character invoker) {
            OnLeave?.Invoke(invoker);
        }

        public void Sleep(Character invoker) {
            OnSleep?.Invoke(invoker);
        }
    }
}
