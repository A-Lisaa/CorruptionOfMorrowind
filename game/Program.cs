using Serilog.Events;

using utils.logger;

namespace game {
    public static class Program {
        // TODO: Rewrite all the ToString's, so they have nice output with class name and public properties, should have tabs
        // TODO: Delegates shouldn't be serialized, maybe just make a class (partial?) with all the events
        // (and maybe dict with their names or some other way to get them through serialized string or something)
        // TODO: Saving of objects
        public static void Register() {
            Character mc = new() { Name = "Alice", Gender = Character.Genders.Female };
            mc.Register("mc");
            Character kas = new() { Name = "Kasyrra", Gender = Character.Genders.Herm };
            kas.Race.Nouns.Add("demon");
            kas.Race.Adjectives.Add("demonic");
            kas.Register("kas");
            Room room1 = new() {
                Name = "Room 1"
            };
            room1.Register("room1");
            Room room2 = new() {
                Name = "Room 2"
            };
            room2.Register("room2");
        }

        public static void Unregister() {
            World.Characters["mc"].Unregister();
            World.Rooms["room1"].Unregister();
        }

        public static void Subscribe() {
            World.Rooms["room1"].Sleep.Subscribe("change_invokers_name_to_Succubus", (invoker) => invoker.Name = "Succubus");
            World.Rooms["room1"].Sleep.Subscribe("add_100_hp_to_kasyrras_maximum", (_) => World.Characters["kas"].MaxHealth += 100);
        }

        public static void Unsubscribe() {
            World.Rooms["room1"].Sleep.Unsubscribe("change_invokers_name_to_Succubus");
        }

        public static void DoInvokes() {
            World.Rooms["room1"].Sleep.Invoke(World.Characters["mc"]);
            Unsubscribe();
            World.Rooms["room1"].Sleep.Invoke(World.Characters["kas"]);
        }

        private static void Main(string[] args) {
            Log.Logger = AliceLog.GetLogger(args.Contains("debug"));

            if (Log.IsEnabled(LogEventLevel.Debug)) {
                Log.Information("Debug enabled");
            }

            Register();
            Subscribe();

            World.Print();

            DoInvokes();

            World.Print();

            Unregister();

            World.Print();
        }
    }
}