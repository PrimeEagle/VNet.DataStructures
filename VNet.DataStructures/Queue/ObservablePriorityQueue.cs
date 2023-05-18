using System.Collections.Specialized;

namespace VNet.DataStructures.Queue
{
    public class ObservablePriorityQueue<TItem, TPriority> : ObservableDoubleTypeCollectionBase<TItem, TPriority>
    {
        private readonly PriorityQueue<TItem, TPriority> _queue;

        
        #region Constructors
        public ObservablePriorityQueue()
        {
            _queue = new PriorityQueue<TItem, TPriority>();
        }

        public ObservablePriorityQueue(IComparer<TPriority>? comparer)
        {
            _queue = new PriorityQueue<TItem, TPriority>(comparer);
        }

        public ObservablePriorityQueue(IEnumerable<(TItem Element, TPriority Priority)> items)
        {
            _queue = new PriorityQueue<TItem, TPriority>(items);
        }

        public ObservablePriorityQueue(IEnumerable<(TItem Element, TPriority Priority)> items,
            IComparer<TPriority>? comparer)
        {
            _queue = new PriorityQueue<TItem, TPriority>(items, comparer);
        }

        public ObservablePriorityQueue(int initialCapacity)
        {
            _queue = new PriorityQueue<TItem, TPriority>(initialCapacity);
        }

        public ObservablePriorityQueue(int initialCapacity, IComparer<TPriority>? comparer)
        {
            _queue = new PriorityQueue<TItem, TPriority>(initialCapacity, comparer);
        }
        #endregion Constructors

        #region Wrapper Properties
        public IComparer<TPriority> Comparer => _queue.Comparer;

        public int Count => _queue.Count;

        public PriorityQueue<TItem, TPriority>.UnorderedItemsCollection UnorderedItems => _queue.UnorderedItems;
        #endregion Wrapper Properties
        
        public void Clear()
        {
            CheckReentrancy();
            var removed = _queue.UnorderedItems;
            TItem[] removedItems = removed.Select(r => r.Element).ToArray();
            TPriority[] removedPriorities = removed.Select(r => r.Priority).ToArray();

            _queue.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Reset, new List<TItem>(removedItems), new List<TPriority>(removedPriorities)));
        }

        public TItem Dequeue()
        {
            CheckReentrancy();
            
            TItem item;
            TPriority priority;
            if (_queue.TryPeek(out item, out priority))
            {
                item = _queue.Dequeue();
                OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Remove, item, priority));
            }

            return item;
        }

        public void Enqueue(TItem item, TPriority priority)
        {
            CheckReentrancy();
            _queue.Enqueue(item, priority);
            OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Add, item, priority));
        }

        public TItem EnqueueDequeue(TItem item, TPriority priority)
        {
            CheckReentrancy();
            if (_queue.TryPeek(out var oldItem, out var oldPriority))
            {
                oldItem = _queue.EnqueueDequeue(item, priority);
                OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Add, item, priority));
                OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Remove, oldItem, oldPriority));
            }

            return oldItem;
        }

        public void EnqueueRange(IEnumerable<(TItem Element, TPriority Priority)> items)
        {
            CheckReentrancy();
            var valueTuples = items as (TItem Element, TPriority Priority)[] ?? items.ToArray();
            _queue.EnqueueRange(valueTuples);
            OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Add,
                valueTuples.Select(i => i.Element).ToArray(), valueTuples.Select(i => i.Priority).ToArray()));
        }

        public void EnqueueRange(IEnumerable<TItem> items, TPriority priority)
        {
            CheckReentrancy();

            _queue.EnqueueRange(items, priority);
            OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Add,
                items, priority));
        }

        public bool TryDequeue(out TItem item, out TPriority priority)
        {
            CheckReentrancy();

            bool result = false;
            item = default;
            priority = default;
            TItem tempItem;
            TPriority tempPriority;
            if (_queue.TryPeek(out tempItem, out tempPriority))
            {
                result = _queue.TryDequeue(out item, out priority);
                if (result)
                {
                    OnExtendedCollectionChanged(new NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>(NotifyCollectionChangedAction.Remove, item, priority));
                }
            }

            return result;
        }

        #region Wrapper Methods
        public int EnsureCapacity(int capacity)
        {
            return _queue.EnsureCapacity(capacity);
        }

        public TItem Peek()
        {
            return _queue.Peek();
        }

        public void TrimExcess()
        {
            _queue.TrimExcess();
        }

        public bool TryPeek(out TItem element, out TPriority priority)
        {
            return _queue.TryPeek(out element, out priority);
        }
        #endregion Wrapper Methods
    }
}