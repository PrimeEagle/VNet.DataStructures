using System.Collections;
using System.Collections.Concurrent;

namespace VNet.DataStructures.Map
{
    public class ConcurrentMultimap<TKey, TVal> where TKey : notnull where TVal : notnull
    {
        private readonly ConcurrentDictionary<TKey, IList<TVal>> _dictionary;



        public IEnumerable<TKey> Keys => _dictionary.Keys;

        public IEnumerable<IList<TVal>> Values => _dictionary.Values;

        public IList<TVal> this[TKey key] => _dictionary[key];

        public bool IsEmpty => _dictionary.IsEmpty;




        protected ConcurrentMultimap()
        {
            _dictionary = new ConcurrentDictionary<TKey, IList<TVal>>();
        }

        public IEnumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public TVal? AddOrUpdate(TKey key, TVal addValue, Func<TKey, TVal, TVal> updateValueFactory)
        {
            bool result = true;

            if(!_dictionary.ContainsKey(key))
            {
                result = _dictionary.TryAdd(key, new List<TVal>());
            }

            if(result)
            {
                _dictionary[key].Add(addValue);
                return addValue;
            }
            else
            {
                return default;
            }
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

        public bool TryAdd(TKey key, TVal value)
        {
            bool result = true;

            if(!_dictionary.ContainsKey(key))
            {
                result = _dictionary.TryAdd(key, new List<TVal>());
            }

            if(result)
            {
                _dictionary[key].Add(value);
            }

            return result;
        }

        public bool TryRemove(TKey key, TVal value)
        {
            bool result = true;

            if (!_dictionary.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            _dictionary[key].Remove(value);
            if (_dictionary[key].Count == 0)
            {
                result = _dictionary.TryRemove(key, out _);
            }

            return result;
        }
    }
}