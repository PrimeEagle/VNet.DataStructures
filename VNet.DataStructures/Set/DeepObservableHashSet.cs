using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace VNet.DataStructures.Set
{
    public class DeepObservableHashSet<T> : DeepObservableSingleTypeCollectionBase<T>,
                                            IEnumerable<T>
                                            where T : notnull, IComparable<T>
    {
        private readonly HashSet<T> _set;



        #region Constructors
        public DeepObservableHashSet()
        {
            _set = new HashSet<T>();
        }

        public DeepObservableHashSet(IEnumerable<T> enumerable)
        {
            _set = new HashSet<T>(enumerable);
        }
        #endregion Constructors

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_set).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_set).GetEnumerator();
        }


        public bool Add(T item)
        {
            CheckReentrancy();
            bool result = _set.Add(item);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item));

            return result;
        }

        public void Clear()
        {
            CheckReentrancy();
            T[] removedItems = new T[] { };
            _set.CopyTo(removedItems);
            _set.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset, new List<T>(removedItems)));
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            CheckReentrancy();
            var removedItems = _set.Where(x => other.Contains(x)).ToArray();
            _set.ExceptWith(other);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, removedItems));
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            CheckReentrancy();
            var removedItems = _set.Where(x => !other.Contains(x)).ToArray();
            _set.IntersectWith(other);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, removedItems));
        }
        public bool Remove(T item)
        {
            CheckReentrancy();
            bool result = _set.Remove(item);
            if (result)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, item));
            }

            return result;
        }

        public int RemoveWhere(Predicate<T> match)
        {
            CheckReentrancy();
            var func = new Func<T, bool>(match);
            var removedItems = _set.Where(func).ToArray();
            var result = _set.RemoveWhere(match);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, removedItems));

            return result;
        }
        public void UnionWith(IEnumerable<T> other)
        {
            CheckReentrancy();
            _set.UnionWith(other);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, other));
        }


        #region Wrapper Properties
        public IEqualityComparer<T> Comparer => _set.Comparer;

        public int Count => _set.Count;
        #endregion Wrapper Properties

        #region Wrapper Methods
        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        public void CopyTo(T[] array)
        {
            _set.CopyTo(array);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            _set.CopyTo(array, arrayIndex, count);
        }

        public int EnsureCapacity(int capacity)
        {
            return _set.EnsureCapacity(capacity);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _set.GetObjectData(info, context);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _set.IsSupersetOf(other);
        }

        public void OnDeserialization(object? sender)
        {
            _set.OnDeserialization(sender);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _set.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _set.SymmetricExceptWith(other);
        }

        public void TrimExcess()
        {
            _set.TrimExcess();
        }

        public bool TryGetValue(T equalValue, out T actualValue)
        {
            return _set.TryGetValue(equalValue, out actualValue);
        }
        #endregion Wrapper Methods
    }
}