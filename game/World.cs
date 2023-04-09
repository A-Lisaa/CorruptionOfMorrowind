using Newtonsoft.Json;

namespace game {
    public static class World {
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
        private sealed class SaveableAttribute : Attribute {
        }

        [Saveable]
        public static Dictionary<string, Room> Rooms { get; } = new();

        [Saveable]
        public static Dictionary<string, Being> Beings { get; } = new();

        [Saveable]
        public static Dictionary<string, Character> Characters { get; } = new();

        public new static string ToString() {
            Type type = typeof(World);

            var fieldsQuery =
                from member in type.GetFields()
                where Attribute.IsDefined(member, typeof(SaveableAttribute))
                select (member.Name, Value: member.GetValue(null));

            var propertiesQuery =
                from member in type.GetProperties()
                where Attribute.IsDefined(member, typeof(SaveableAttribute))
                select (member.Name, Value: member.GetValue(null));

            var result = fieldsQuery.Concat(propertiesQuery);

            return JsonConvert.SerializeObject(
                result.ToDictionary(x => x.Name, x => x.Value),
                Formatting.Indented
            );
        }

        public static void Print() {
            Console.WriteLine(ToString());
        }
    }
}
