using System.Collections;

namespace VNet.DataStructures.Map
{
    public class Multimap<TKey, TVal> where TKey : notnull where TVal : notnull
    {
        private readonly Dictionary<TKey, IList<TVal>> _dictionary;



        public IEnumerable<TKey> Keys => _dictionary.Keys;

        public IEnumerable<IList<TVal>> Values => _dictionary.Values;

        public IList<TVal> this[TKey key] => _dictionary[key];



        protected Multimap()
        {
            _dictionary = new Dictionary<TKey, IList<TVal>>();
        }

        public IEnumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public void Add(TKey key, TVal item)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, new List<TVal>());
            }

            _dictionary[key].Add((item));
        }
        
        public void Clear()
        {
            _dictionary.Clear();
        }

        public void Clear(TKey key)
        {
            _dictionary[key].Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Contains(TKey key, TVal item)
        {
            bool result;

            if (!_dictionary.ContainsKey(key))
            {
                result = false;
            }
            else
            {
                result = _dictionary[key].Contains((item));
            }

            return result;
        }

        public bool Remove(TKey key, TVal item)
        {
            bool result;

            if (!_dictionary.ContainsKey(key))
            {
                result = false;
            }
            else
            {
                result = _dictionary[key].Remove(item);
            }

            return result;
        }

        public int Count()
        {
            return _dictionary.Count;
        }

        public int Count(TKey key)
        {
            int result = 0;

            if (_dictionary.TryGetValue(key, out var value))
            {
                result = value.Count;
            }

            return result;
        }
    }
}