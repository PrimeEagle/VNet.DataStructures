using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;

namespace VNet.DataStructures.Queue
{
    public class RestrictedObservableConcurrentQueue<T> : ObservableSingleTypeCollectionBase<T>, IEnumerable<T>
    {
        private readonly ConcurrentQueue<T> _queue;
        private Type? _restrictedType;



        #region Constructors
        public RestrictedObservableConcurrentQueue()
        {
            _queue = new ConcurrentQueue<T>();
        }

        public RestrictedObservableConcurrentQueue(IEnumerable<T> enumerable)
        {
            _queue = new ConcurrentQueue<T>(enumerable);
        }
        #endregion Constructors


        #region Wrapper Properties
        public bool IsSynchronized => ((ICollection)_queue).IsSynchronized;

        public object SyncRoot => ((ICollection)_queue).SyncRoot;

        public int Count => _queue.Count;

        public bool IsEmpty => _queue.IsEmpty;
        #endregion Wrapper Properties




        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_queue).GetEnumerator();
        }



        public void Clear()
        {
            CheckReentrancy();
            T[] removedItems = new T[] { };
            _queue.CopyTo(removedItems, 0);

            _queue.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset, new List<T>(removedItems)));
        }

        public void Enqueue(T item)
        {
            CheckReentrancy();

            if (_queue.Count == 0) _restrictedType = item?.GetType();
            if (item?.GetType() != _restrictedType) throw new ArgumentException("All types in a restricted queue must match.");

            _queue.Enqueue(item);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item));
        }

        public bool TryDequeue(out T item)
        {
            CheckReentrancy();
            bool result = _queue.TryDequeue(out item);
            if (result)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, item));
            }

            return result;
        }

        

        #region Wrapper Methods
        public void CopyTo(T[] array, int index)
        {
            _queue.CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return _queue.ToArray();
        }

        public bool TryPeek(out T result)
        {
            return _queue.TryPeek(out result);
        }
        #endregion Wrapper Methods
    }
}