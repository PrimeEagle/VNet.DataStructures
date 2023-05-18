using System.Collections;
using System.Collections.Concurrent;

namespace VNet.DataStructures.Dictionary
{
    public class DeepObservableConcurrentDictionary<TKey, TValue> : DeepObservableDictionaryBase<TKey, TValue>,
                                                                    IEnumerable<KeyValuePair<TKey, TValue>>
                                                                    where TKey : notnull
                                                                    where TValue : notnull
    {
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary;

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
        public DeepObservableConcurrentDictionary()
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        public DeepObservableConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>(collection);
        }

        public DeepObservableConcurrentDictionary(IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>(comparer);
        }

        public DeepObservableConcurrentDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>(dictionary);
        }

        public DeepObservableConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>(collection, comparer);
        }

        public DeepObservableConcurrentDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>(dictionary, comparer);
        }
        #endregion Constructors

        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            CheckReentrancy();
            var value = _dictionary.AddOrUpdate(key, addValueFactory, updateValueFactory);
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, value));

            return value;
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            CheckReentrancy();
            var value = _dictionary.AddOrUpdate(key, addValue, updateValueFactory);
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, value));

            return value;
        }

        public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
        {
            CheckReentrancy();
            var value = _dictionary.AddOrUpdate(key, addValueFactory, updateValueFactory, factoryArgument);
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, value));

            return value;
        }

        public bool TryAdd(TKey key, TValue value)
        {
            CheckReentrancy();
            bool result = false;

            CheckReentrancy();
            if (_dictionary.TryAdd(key, value))
            {
                result = true;
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, value));
            }

            return result;
        }

        public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
        {
            CheckReentrancy();
            bool result = _dictionary.TryUpdate(key, newValue, comparisonValue);
            if (result)
            {
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Update, newValue));
            }

            return result;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            CheckReentrancy();
            bool result = _dictionary.TryRemove(key, out value);
            if (result)
            {
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Remove, value));
            }

            return result;
        }

        public bool TryRemove(KeyValuePair<TKey, TValue> item)
        {
            CheckReentrancy();
            bool result = _dictionary.TryRemove(item);
            if (result)
            {
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Remove, item.Value));
            }

            return result;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            CheckReentrancy();
            return _dictionary.TryGetValue(key, out value);
        }

        public bool TryGetNonEnumeratedCount(out int count)
        {
            CheckReentrancy();
            return _dictionary.TryGetNonEnumeratedCount(out count);
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            CheckReentrancy();
            return _dictionary.GetOrAdd(key, value);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            CheckReentrancy();
            TValue result = _dictionary.GetOrAdd(key, valueFactory);
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, result));

            return result;
        }

        public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
        {
            CheckReentrancy();
            TValue result = _dictionary.GetOrAdd<TArg>(key, valueFactory, factoryArgument);
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Add, result));

            return result;
        }

        public bool Remove(TKey key, out TValue value)
        {
            CheckReentrancy();
            bool result = _dictionary.Remove(key, out value);
            if (result)
            {
                OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Remove, value));
            }

            return result;
        }

        public void Clear()
        {
            CheckReentrancy();
            _dictionary.Clear();
            OnDictionaryChanged(new NotifyExtendedDictionaryChangedEventArgs<TKey, TValue>(NotifyExtendedDictionaryChangedAction.Reset));
        }

        #region Wrapper Properties
        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return _dictionary.Comparer;
            }
        }
        public bool IsSynchronized => ((ICollection)_dictionary).IsSynchronized;

        public object SyncRoot => ((ICollection)_dictionary).SyncRoot;

        public bool IsFixedSize => ((IDictionary)_dictionary).IsFixedSize;

        public bool IsReadOnly => ((IDictionary)_dictionary).IsReadOnly;

        public ICollection<TKey> Keys => ((IDictionary<TKey, TValue>)_dictionary).Keys;

        public ICollection<TValue> Values => ((IDictionary<TKey, TValue>)_dictionary).Values;

        public int Count => _dictionary.Count;
        #endregion Wrapper Properties

        #region Wrapper Methods
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
        #endregion Wrapper Methods
    }
}