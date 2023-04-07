using System.Runtime.Serialization;

namespace utils.enumerables
{
    /// <summary>
    /// Interface for making collections have pretty printing.
    /// </summary>
    public interface IPrintable {
        /// <summary>
        /// Separator to be put between elements.
        /// </summary>
        string Separator { get; set; }

        string ToString();
    }

    /// <summary>
    /// <c>List&lt;T&gt;</c> but with implementation of <c>IPrintable</c>.
    /// </summary>
    public class PrintableList<T> : List<T>, IPrintable {
        public PrintableList() {
        }

        public PrintableList(IEnumerable<T> collection) : base(collection) {
        }

        public PrintableList(int capacity) : base(capacity) {
        }

        public string Separator { get; set; } = "; ";

        public override string ToString() => string.Join("; ", this);
    }

    /// <summary>
    /// <c>HashSet&lt;T&gt;</c> but with implementation of <c>IPrintable</c>.
    /// </summary>
    [Serializable]
    public class PrintableHashSet<T> : HashSet<T> {
        public PrintableHashSet() {
        }

        public PrintableHashSet(IEnumerable<T> collection) : base(collection) {
        }

        public PrintableHashSet(IEqualityComparer<T>? comparer) : base(comparer) {
        }

        public PrintableHashSet(int capacity) : base(capacity) {
        }

        public PrintableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer) : base(collection, comparer) {
        }

        public PrintableHashSet(int capacity, IEqualityComparer<T>? comparer) : base(capacity, comparer) {
        }

        protected PrintableHashSet(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        public override string ToString() => string.Join("; ", this);
    }

    /// <summary>
    /// <c>Dictionary&lt;TKey, TValue&gt;</c> but with implementation of <c>IPrintable</c>.
    /// </summary>
    [Serializable]
    public class PrintableDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull {
        public PrintableDictionary() {
        }

        public PrintableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) {
        }

        public PrintableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) : base(dictionary, comparer) {
        }

        public PrintableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) {
        }

        public PrintableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) : base(collection, comparer) {
        }

        public PrintableDictionary(IEqualityComparer<TKey>? comparer) : base(comparer) {
        }

        public PrintableDictionary(int capacity) : base(capacity) {
        }

        public PrintableDictionary(int capacity, IEqualityComparer<TKey>? comparer) : base(capacity, comparer) {
        }

        protected PrintableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        public override string ToString() {
            var query =
                from pair in this
                select $"{pair.Key} = {pair.Value}";

            return string.Join("; ", query);
        }
    }
}
