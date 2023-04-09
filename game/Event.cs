namespace game {
    // All this events stuff is a huge fucking kludge, it SHOULD be rewritten to work with all kinds of delegates.
    public class GameEvent<T> {
        public required Action<T> Del { get; init; }
        public int Priority { get; init; }

        public void Invoke(T obj) {
            Del.Invoke(obj);
        }
    }

    public class EventDict<T> {
        private Dictionary<string, GameEvent<T>> _subscribers = new();

        private void Sort() {
            _subscribers = new(_subscribers.OrderBy(x => x.Value.Priority));
        }

        public void Subscribe(string id, Action<T> del, int priority = 0) {
            GameEvent<T> gameEvent = new() { Del = del, Priority = priority };
            bool result = _subscribers.TryAdd(id, gameEvent);
            if (!result) {
                Log.Error($"Couldn't subscribe {del}");
            }
            Sort();
        }

        public void Unsubscribe(string id) {
            bool result = _subscribers.Remove(id);
            if (!result) {
                Log.Error($"Couldn't unsubscribe subscriber with id = {id}");
            }
        }

        public void Invoke(T obj) {
            foreach (var del in _subscribers.Values) {
                del.Invoke(obj);
            }
        }
    }
}