using PostSharp.Aspects;
using PostSharp.Serialization;

namespace game {
    public class GameEventArgs : EventArgs {
        public required Character Invoker { get; init; }
        public bool IsCancelled { get; private set; }

        public void Cancel() {
            IsCancelled = true;
        }
    }

    [PSerializable]
    [AttributeUsage(AttributeTargets.Event)]
    public sealed class GameEventAttribute : EventInterceptionAspect {
        public override void OnInvokeHandler(EventInterceptionArgs args) {
            if (args is null) {
                throw new ArgumentNullException(nameof(args));
            }
            RoomEventArgs eventArgs = (RoomEventArgs)args.Arguments[1];
            if (eventArgs?.IsCancelled == false) {
                args.ProceedInvokeHandler();
            }
        }
    }
}
