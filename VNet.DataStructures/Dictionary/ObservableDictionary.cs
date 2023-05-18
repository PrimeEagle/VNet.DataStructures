using System.Collections;
using System.Runtime.Serialization;

namespace VNet.DataStructures.Dictionary
{
    public class ObservableDictionary<TKey, TValue> : ObservableDictionaryBase<TKey, TValue>,
                                                            IEnumerable<KeyValuePair<TKey, TValue>>
                                                            where TKey : notnull
                                                            where TValue : notnull
    {
        private readonly Dictionary<TKey, TValue> _dictionary;



        public TValue this[TKey key]
        {
            get => ((IDictionary<TKey, TValue>)_dictionary)[key];
            set
            {
                CheckReentrancy();
                TValue oldValue = _dictionary[key];
                _dictionary[key] = value;
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Replace, _dictionary[key], oldValue));

            }
        }

        #region Constructors
        public ObservableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            _dictionary = new Dictionary<TKey, TValue>(collection);
        }

        public ObservableDictionary(IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(collection, comparer);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }
        #endregion Constructors

        public void Add(TKey key, TValue value)
        {
            CheckReentrancy();
            _dictionary.Add(key, value);
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, value));
        }

        public bool TryAdd(TKey key, TValue value)
        {
            bool result = false;

            CheckReentrancy();
            if (_dictionary.TryAdd(key, value))
            {
                result = true;
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, value));
            }

            return result;
        }

        public bool Remove(TKey key)
        {
            CheckReentrancy();
            var oldValue = _dictionary[key];
            bool result = _dictionary.Remove(key);
            if(result)
            {
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Remove, oldValue));
            }

            return result;
        }

        public bool Remove(TKey key, out TValue value)
        {
            CheckReentrancy();
            bool result = _dictionary.Remove(key, out value);
            if(result)
            {
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Remove, value));
            }

            return result;
        }

        public void Clear()
        {
            CheckReentrancy();
            _dictionary.Clear();
        }

        #region Wrapper Properties
        public bool IsSynchronized => ((ICollection) _dictionary).IsSynchronized;

        public object SyncRoot => ((ICollection) _dictionary).SyncRoot;

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool IsFixedSize => ((IDictionary) _dictionary).IsFixedSize;

        public bool IsReadOnly => ((IDictionary) _dictionary).IsReadOnly;

        public ICollection<TKey> Keys => ((IDictionary<TKey, TValue>) _dictionary).Keys;

        public ICollection<TValue> Values => ((IDictionary<TKey, TValue>) _dictionary).Values;

        public IEqualityComparer<TKey> Comparer => _dictionary.Comparer;

        public int Count => _dictionary.Count;
        #endregion Wrapper Properties

        #region Wrapper Methods
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return _dictionary.ContainsValue(value);
        }

        public int EnsureCapacity(int capacity)
        {
            return _dictionary.EnsureCapacity(capacity);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _dictionary.GetObjectData(info, context);
        }

        public void OnDeserialization(object? sender)
        {
            _dictionary.OnDeserialization(sender);
        }

        public void TrimExcess()
        {
            _dictionary.TrimExcess();
        }

        public void TrimExcess(int capacity)
        {
            _dictionary.TrimExcess(capacity);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
        #endregion Wrapper Methods
    }
}