using System.Reflection;

using utils.enumerables;

namespace test
{
    // TODO: Rewrite all the ToString's, so they have nice output with class name and public properties, should have tabs
    // All this events stuff is a huge fucking kludge, it SHOULD be rewritten to work with all kinds of delegates.
    public class GameEvent<T> {
        public required Action<T> Del { get; init; }
        public int Priority { get; init; }

        public override string ToString() {
            return $"Del = {Del}; Priority = {Priority}";
        }

        public void Invoke(T obj) {
            Del.Invoke(obj);
        }
    }

    public class EventDict<T> : IPrintable {
        private PrintableDictionary<string, GameEvent<T>> _subscribers = new();

        public string Separator { get; set; } = "; ";

        public override string ToString() {
            var query =
                from pair in _subscribers
                select $"{pair.Key} = {pair.Value}";

            return string.Join("; ", query);
        }

        private void Sort() {
            _subscribers = new(_subscribers.OrderBy(x => x.Value.Priority));
        }

        public void Subscribe(string id, Action<T> del, int priority = 0) {
            GameEvent<T> gameEvent = new() { Del = del, Priority = priority };
            bool result = _subscribers.TryAdd(id, gameEvent);
            if (!result) {
                //Log.Error($"Couldn't subscribe {del}");
            }
            Sort();
        }

        public void Unsubscribe(string id) {
            bool result = _subscribers.Remove(id);
            if (!result) {
                //Log.Error($"Couldn't unsubscribe {del}");
            }
        }

        public void Invoke(T obj) {
            foreach (var del in _subscribers.Values) {
                del.Invoke(obj);
            }
        }
    }

    public abstract record Unique<T> where T : Unique<T> {
        public string Id { get; private set; } = "";

        public abstract Dictionary<string, T> ContainingDict();

        public void Register(string id) {
            if (id is "") {
                //Log.Error("Can't register an object using empty id");
                return;
            }
            Id = id;
            bool result = ContainingDict().TryAdd(Id, (T)this);
            if (!result) {
                //Log.Error($"Couldn't register {this}");
            }
        }

        public void Unregister() {
            if (Id is "") {
                //Log.Error($"Can't unregister an object that hasn't been registered")
                return;
            }
            bool result = ContainingDict().Remove(Id);
            if (!result) {
                //Log.Error($"Couldn't unregister {this}");
            }
        }
    }

    public record Room : Unique<Room> {
        public required string Name { get; set; }

        public override Dictionary<string, Room> ContainingDict() => World.Rooms;

        // TODO: Joint events (maybe, static?)
        public EventDict<Character> Enter { get; } = new();
        public EventDict<Character> Leave { get; } = new();
        public EventDict<Character> Sleep { get; } = new();
    }

    public record Character : Unique<Character> {
        private static readonly Room _WaitRoom = new() {
            Name = "(!Technical!) Wait Room"
        };
        public Room CurrentRoom { get; private set; } = _WaitRoom;

        public required string Name { get; set; }
        public required int MaxHealth { get; set; }

        public override Dictionary<string, Character> ContainingDict() => World.Characters;

        public void Move(Room destination) {
            CurrentRoom.Leave.Invoke(this);
            CurrentRoom = destination;
            CurrentRoom.Enter.Invoke(this);
        }
    }

    // static can't be sealed you doofus, stop setting it to sealed
    public static class World {
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
        private sealed class SaveableAttribute : Attribute {
        }

        [Saveable]
        public static PrintableDictionary<string, Room> Rooms { get; } = new();

        [Saveable]
        public static PrintableDictionary<string, Character> Characters { get; } = new();

        private static List<object> GetSaveables() {
            Type type = typeof(World);

            var fieldsQuery =
                from member in type.GetFields()
                from attribute in member.GetCustomAttributes()
                where attribute is SaveableAttribute
                select member.GetValue(null);

            var propertiesQuery =
                from member in type.GetProperties()
                from attribute in member.GetCustomAttributes()
                where attribute is SaveableAttribute
                select member.GetValue(null);

            return new(fieldsQuery.Concat(propertiesQuery));
        }

        public new static string ToString() {
            return string.Join("\n", GetSaveables()) + "\n";
        }

        public static void Print() {
            Console.WriteLine(ToString());
        }
    }

    public static class Program {
        public static void Register() {
            Character mc = new() { Name = "Alice", MaxHealth = 25 };
            mc.Register("mc");
            Character kas = new() { Name = "Kasyrra", MaxHealth = 100 };
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

        public static void Main() {
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