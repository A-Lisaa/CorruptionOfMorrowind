namespace game {
    public abstract record Unique<T> where T : Unique<T> {
        private string _id = "";

        public abstract Dictionary<string, T> ContainingDict();

        public void Register(string id) {
            if (id is "") {
                Log.Error("Can't register an object using empty id");
                return;
            }
            _id = id;
            bool result = ContainingDict().TryAdd(_id, (T)this);
            if (!result) {
                Log.Error($"Couldn't register {this}");
            }
        }

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
