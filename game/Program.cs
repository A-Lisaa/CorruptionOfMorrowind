using Serilog.Events;

using utils.logger;

namespace game {
    public static class Program {
        // TODO: Delegates shouldn't be serialized, maybe just make a class (partial?) with all the events
        // (and maybe dict with their names or some other way to get them through serialized string or something)
        // TODO: Saving of objects
        public static void Register() {
            Character mc = new() { Name = "Alice", Gender = Character.Genders.Female };
            mc.Register("mc");
            mc.Register("mc");
            Character kas = new() { Name = "Kasyrra", Gender = Character.Genders.Herm };
            kas.Register("kas");
            Room room1 = new() {
                Name = "Room 1"
            };
            room1.Register("room1");
            Room room2 = new() {
                Name = "Room 2"
            };
            room2.Register("");
        }

        public static void Unregister() {
            World.Characters["mc"].Unregister();
            World.Rooms["room1"].Unregister();
        }

        public static void Subscribe() {
            World.Rooms["room1"].OnSleep += RoomEvents.ChangeInvokerNameToSuccubus;
            World.Rooms["room1"].OnSleep += RoomEvents.Add100HpToKasyrrasMaximum;
        }

        public static void Unsubscribe() {
            World.Rooms["room1"].OnSleep -= RoomEvents.ChangeInvokerNameToSuccubus;
            World.Rooms["room1"].OnSleep += RoomEvents.SetInvokerIntelliigenceTo100;
        }

        public static void DoInvokes() {
            World.Rooms["room1"].Sleep(World.Characters["mc"]);
            Unsubscribe();
            World.Rooms["room1"].Sleep(World.Characters["kas"]);
        }

        private static void Main(string[] args) {
            Log.Logger = AliceLog.GetLogger(args.Contains("debug"));

            if (Log.IsEnabled(LogEventLevel.Debug)) {
                Log.Information("Debug enabled");
            }

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => Log.Error((Exception)eventArgs.ExceptionObject, "Unhandled Exception");

            Register();
            Subscribe();

            DoInvokes();

            Console.WriteLine(World.Characters["mc"].Name);
            Console.WriteLine(World.Characters["kas"].Name);
            Console.WriteLine(World.Characters["kas"].MaxHealth);

            Unregister();
        }
    }
}