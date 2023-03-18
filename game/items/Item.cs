namespace game.items {

    public abstract class Item {
        public readonly ref struct MetadataEntry {
            private readonly ref object _value;

            public ref object Value {
                get {
                    return ref _value;
                }
            }

            public MetadataEntry(object value) {
                _value = value;
            }
        }

        public abstract string Name { get; }
        public abstract double Weight { get; }
        public abstract int Value { get; }
        public virtual string Kind { get; } = "Other";

        //public virtual List<MetadataEntry> Metadata {
        //    get => new() {
        //        new MetadataEntry(Name),
        //        new MetadataEntry(Weight),
        //        new MetadataEntry(Value)
        //    };
        //}

        //public override string ToString() {

        //    return string.Join(
        //        "; ",
        //        Metadata.ConvertAll(x => $"{nameof(x.Value)} = {x.Value}")
        //    );
        //}
    }
}