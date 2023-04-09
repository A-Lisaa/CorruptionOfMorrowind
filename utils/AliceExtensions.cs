namespace utils.extensions {
    public static class ListExtensions {
        private readonly static Random random = new();

        public static T GetRandomElement<T>(this List<T> list) {
            if (list is null) {
                throw new ArgumentNullException(nameof(list));
            }
#pragma warning disable CA5394
            return list[random.Next(0, list.Count)];
#pragma warning restore CA5394
        }
    }
}
