using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures
{
    public class NotifyExtendedSingleTypeCollectionChangedEventArgs<T> : NotifyCollectionChangedEventArgs, INotifyExtendedSingleTypeCollectionChangedEventArgs<T>
    {
        public new NotifyCollectionChangedAction Action { get; init; }
        [AllowNull]
        public new IEnumerable<T> OldItems { get; init; }
        [AllowNull]
        public new IEnumerable<T> NewItems { get; init; }
        [AllowNull]
        public T OldItem { get; init; }
        [AllowNull]
        public T NewItem { get; init; }
        [AllowNull]
        public int OldIndex { get; init; }
        [AllowNull]
        public int NewIndex { get; init; }


        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action)
            : base(action)
        {
            Action = action;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, T? changedItem)
            : base(action, changedItem)
        {
            Action = action;
            NewItem = changedItem;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<T>? changedItems)
            : base(action, changedItems)
        {
            Action = action;
            NewItems = changedItems;
        }
        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<T>? changedItems)
            : base(action, changedItems)
        {
            Action = action;
            NewItems = changedItems;
        }
        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, T? changedItem, int index)
            : base(action, changedItem, index)
        {
            Action = action;
            NewItem = changedItem;
            NewIndex = index;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, T? newItem, T? oldItem)
            : base(action, newItem, oldItem)
        {
            Action = action;
            NewItem = newItem;
            OldItem = oldItem;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<T> newItems, IList<T> oldItems)
            : base(action, newItems, oldItems)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<T> newItems, IEnumerable<T> oldItems)
            : base(action, newItems, oldItems)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<T> changedItems, int index)
            : base(action, changedItems, index)
        {
            Action = action;
            NewItems = changedItems;
            NewIndex = index;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<T> changedItems, int index)
            : base(action, changedItems, index)
        {
            Action = action;
            NewItems = changedItems;
            NewIndex = index;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, T? changedItem, int index, int oldIndex)
            : base(action, changedItem, index, oldIndex)
        {
            Action = action;
            NewItem = changedItem;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, T? newItem, T? oldItem, int index)
            : base(action, newItem, oldItem, index)
        {
            Action = action;
            NewItem = newItem;
            OldItem = oldItem;
            NewIndex = index;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<T> newItems, IList<T> oldItems, int startingIndex)
            : base(action, newItems, oldItems, startingIndex)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            NewIndex = startingIndex;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<T> newItems, IEnumerable<T> oldItems, int startingIndex)
            : base(action, newItems, oldItems, startingIndex)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            NewIndex = startingIndex;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<T> changedItems, int index, int oldIndex)
            : base(action, changedItems, index, oldIndex)
        {
            Action = action;
            NewItems = changedItems;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public NotifyExtendedSingleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<T> changedItems, int index, int oldIndex)
            : base(action, changedItems, index, oldIndex)
        {
            Action = action;
            NewItems = changedItems;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public event NotifyExtendedSingleTypeCollectionChangedEventHandler<T>? ExtendedCollectionChanged;
    }
}