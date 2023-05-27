using System.Collections;
using System.Collections.Specialized;

namespace VNet.DataStructures.Queue
{
    public class DeepObservableQueue<T> : DeepObservableSingleTypeCollectionBase<T>,
                                          IEnumerable<T>
                                          where T : notnull, IComparable<T>
    {
        private readonly Queue<T> _queue;



        #region Constructors
        public DeepObservableQueue()
        {
            _queue = new Queue<T>();
        }

        public DeepObservableQueue(IEnumerable<T> enumerable)
        {
            _queue = new Queue<T>(enumerable);
        }

        public DeepObservableQueue(int capacity)
        {
            _queue = new Queue<T>(capacity);
        }
        #endregion Constructors



        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_queue).GetEnumerator();
        }

        #region Wrapper Properties
        public bool IsSynchronized => ((ICollection)_queue).IsSynchronized;

        public object SyncRoot => ((ICollection)_queue).SyncRoot;
        public int Count => _queue.Count;
        #endregion Wrapper Properties

        public T Dequeue()
        {
            CheckReentrancy();
            var result = _queue.Dequeue();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, result));

            return result;
        }

        public void Enqueue(T item)
        {
            CheckReentrancy();
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

        public void Clear()
        {
            CheckReentrancy();
            T[] removedItems = new T[] { };
            _queue.CopyTo(removedItems, 0);

            _queue.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset, new List<T>(removedItems)));
        }

        #region Wrapper Methods
        public bool Contains(T item)
        {
            return _queue.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _queue.CopyTo(array, arrayIndex);
        }

        public T Peek()
        {
            return _queue.Peek();
        }

        public T[] ToArray()
        {
            return _queue.ToArray();
        }

        public void TrimExcess()
        {
            _queue.TrimExcess();
        }

        public int EnsureCapacity(int capacity)
        {
            return _queue.EnsureCapacity(capacity);
        }

        public bool TryPeek(out T result)
        {
            return _queue.TryPeek(out result);
        }
        #endregion Wrapper Methods
    }
}