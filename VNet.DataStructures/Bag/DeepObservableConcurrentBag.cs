using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;

namespace VNet.DataStructures.Bag
{
    public class DeepObservableConcurrentBag<T> : DeepObservableSingleTypeCollectionBase<T> , IEnumerable<T>
    {
        private readonly ConcurrentBag<T> _bag;


        #region Constructors
        public DeepObservableConcurrentBag()
        {
            _bag = new ConcurrentBag<T>();
        }

        public DeepObservableConcurrentBag(IEnumerable<T> enumerable)
        {
            _bag = new ConcurrentBag<T>(enumerable);
        }
        #endregion Constructors


        public bool TryTake(out T item)
        {
            CheckReentrancy();
            bool result = _bag.TryTake(out item);
            if (result)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, item));
            }

            return result;
        }

        public void Add(T item)
        {
            CheckReentrancy();

            _bag.Add(item);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            CheckReentrancy();

            _bag.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset));
        }

        #region Wraper Properties
        public int Count => _bag.Count;

        public bool IsEmpty => _bag.IsEmpty;

        public bool IsSynchronized => ((ICollection)_bag).IsSynchronized;

        public object SyncRoot => ((ICollection)_bag).SyncRoot;
        #endregion Wrapper Properties


        #region Wrapper Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_bag).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _bag.GetEnumerator();
        }

        public void CopyTo(T[] array, int index)
        {
            _bag.CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return _bag.ToArray();
        }

        public bool TryPeek(out T result)
        {
            return _bag.TryPeek(out result);
        }
        #endregion Wrapper Methods
    }
}