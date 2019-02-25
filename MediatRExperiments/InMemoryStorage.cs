using System.Collections.Generic;

namespace MediatRExperiments
{
    public class InMemoryStorage
    {
        private readonly Dictionary<string, string> _keyValueStorage = new Dictionary<string, string>();


        public int Count { get; private set; } = 0;

        public void AddValue(string key, string value)
        {
            _keyValueStorage[key] = value;
        }

        public string GetValue(string key)
        {
            return _keyValueStorage.ContainsKey(key) ? _keyValueStorage[key] : null;
        }

        public void Increment()
        {
            Count++;
        }
    }
}