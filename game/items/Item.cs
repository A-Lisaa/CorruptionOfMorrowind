namespace game.items {

    public abstract record Item {
        public abstract string Name { get; }
        public abstract double Weight { get; }
        public abstract int Value { get; }
        public virtual string Kind { get; } = "Other";

        //public virtual List<MetadataEntry> Metadata {
        //    get => new() {
        //        new(nameof(Name), Name),
        //        new(nameof(Weight), Weight),
        //        new(nameof(Value), Value),
        //    };
        //}

        //public override string ToString() {
        //    return GetType() + string.Join(
        //        "; ",
        //        Metadata.ConvertAll(x => $"{x.Name}: {x.Value}")
        //    );
        //}
    }
}