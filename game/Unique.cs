using utils.logger;

namespace game {
    public abstract record Unique<T> where T : Unique<T> {
        private string _id = "";

        /// <summary>
        /// Gets <c>Dictionary</c> in World containing objects of T type.
        /// </summary>
        /// <returns>Dictionary in World</returns>
        public abstract Dictionary<string, T> ContainingDict();

        /// <summary>
        /// Adds the object to <c>ContaningDict</c> with <paramref name="id"/> as the key.
        /// </summary>
        /// <param name="id">id to be used for the object in <c>ContainingDict</c></param>
        public void Register(string id) {
            if (id is "") {
                Log.Error(new LogMessage("Can't register the [object]", "Empty ID", ("object", this)));
                return;
            }
            _id = id;
            bool result = ContainingDict().TryAdd(_id, (T)this);
            if (!result) {
                //Log.Error($"Couldn't register {this}, likely reason: object with this id is already registered");
                Log.Error(
                    new LogMessage(
                        "Can't register [object]",
                        "Object of the same type with [id] already registered",
                        ("object", this),
                        ("id", id)
                    )
                );
            }
        }

        /// <summary>
        /// Removes the object from <c>ContaningDict</c>.
        /// </summary>
        public void Unregister() {
            if (_id is "") {
                Log.Error("Can't unregister an object that hasn't been registered");
                return;
            }
            bool result = ContainingDict().Remove(_id);
            if (!result) {
                Log.Error($"Couldn't unregister {this}");
            }
        }
    }
}
