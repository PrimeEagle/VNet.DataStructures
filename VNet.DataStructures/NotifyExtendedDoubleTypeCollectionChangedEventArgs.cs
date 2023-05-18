using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures
{
    public class NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority> : NotifyCollectionChangedEventArgs, INotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>
    {
        public new NotifyCollectionChangedAction Action { get; init; }
        [AllowNull]
        public new IEnumerable<TItem> OldItems { get; init; }
        [AllowNull]
        public new IEnumerable<TItem> NewItems { get; init; }
        [AllowNull]
        public TItem OldItem { get; init; }
        [AllowNull]
        public TItem NewItem { get; init; }
        [AllowNull]
        public IEnumerable<TPriority> OldPriorities { get; init; }
        [AllowNull]
        public IEnumerable<TPriority> NewPriorities { get; init; }
        [AllowNull]
        public TPriority OldPriority { get; init; }
        [AllowNull]
        public TPriority NewPriority { get; init; }
        [AllowNull]
        public int OldIndex { get; init; }
        [AllowNull]
        public int NewIndex { get; init; }


        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action)
            : base(action)
        {
            Action = action;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, TItem? changedItem, TPriority? changedPriority)
            : base(action, changedItem)
        {
            Action = action;
            NewItem = changedItem;
            NewPriority = changedPriority;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<TItem>? changedItems, IList<TPriority>? changedPriorities)
            : base(action, changedItems)
        {
            Action = action;
            NewItems = changedItems;
            NewPriorities = changedPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<TItem>? changedItems, IEnumerable<TPriority>? changedPriorities)
            : base(action, changedItems)
        {
            Action = action;
            NewItems = changedItems;
            NewPriorities = changedPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<TItem>? changedItems, TPriority? changedPriority)
            : base(action, changedItems)
        {
            Action = action;
            NewItems = changedItems;
            NewPriority = changedPriority;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, TItem? changedItem, int index, TPriority? changedPriority)
            : base(action, changedItem, index)
        {
            Action = action;
            NewItem = changedItem;
            NewIndex = index;
            NewPriority = changedPriority;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, TItem? newItem, TPriority? newPriority, TItem? oldItem, TPriority? oldPriority)
            : base(action, newItem, oldItem)
        {
            Action = action;
            NewItem = newItem;
            OldItem = oldItem;
            NewPriority = newPriority;
            OldPriority = oldPriority;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<TItem> newItems, IList<TPriority> newPriorities, IList<TItem> oldItems, IList<TPriority> oldPriorities)
            : base(action, newItems, oldItems)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            NewPriorities = newPriorities;
            OldPriorities = oldPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<TItem> newItems, IEnumerable<TPriority> newPriorities, IEnumerable<TItem> oldItems, IEnumerable<TPriority> oldPriorities)
            : base(action, newItems, oldItems)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            NewPriorities = newPriorities;
            OldPriorities = oldPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<TItem> changedItems, IList<TPriority> changedPriorities, int index)
            : base(action, changedItems, index)
        {
            Action = action;
            NewItems = changedItems;
            NewIndex = index;
            NewPriorities = changedPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<TItem> changedItems, IEnumerable<TPriority> changedPriorities, int index)
            : base(action, changedItems, index)
        {
            Action = action;
            NewItems = changedItems;
            NewIndex = index;
            NewPriorities = changedPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, TItem? changedItem, TPriority changedPriority, int index, int oldIndex)
            : base(action, changedItem, index, oldIndex)
        {
            Action = action;
            NewItem = changedItem;
            NewPriority = changedPriority;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, TItem? newItem, TPriority? newPriority,  TItem? oldItem, TPriority? oldPriority, int index)
            : base(action, newItem, oldItem, index)
        {
            Action = action;
            NewItem = newItem;
            NewPriority = newPriority;
            OldItem = oldItem;
            OldPriority = oldPriority;
            NewIndex = index;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<TItem> newItems, IList<TPriority> newPriorities, IList<TItem> oldItems, IList<TPriority> oldPriorities, int startingIndex)
            : base(action, newItems, oldItems, startingIndex)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            NewIndex = startingIndex;
            NewPriorities = newPriorities;
            OldPriorities = oldPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<TItem> newItems, IEnumerable<TPriority> newPriorities, IEnumerable<TItem> oldItems, IEnumerable<TPriority> oldPriorities, int startingIndex)
            : base(action, newItems, oldItems, startingIndex)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
            NewIndex = startingIndex;
            NewPriorities = newPriorities;
            OldPriorities = oldPriorities;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<TItem> changedItems, IList<TPriority> changedPriorities, int index, int oldIndex)
            : base(action, changedItems, index, oldIndex)
        {
            Action = action;
            NewItems = changedItems;
            NewPriorities = changedPriorities;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public NotifyExtendedDoubleTypeCollectionChangedEventArgs(NotifyCollectionChangedAction action, IEnumerable<TItem> changedItems, IEnumerable<TPriority> changedPriorities, int index, int oldIndex)
            : base(action, changedItems, index, oldIndex)
        {
            Action = action;
            NewItems = changedItems;
            NewPriorities = changedPriorities;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public event NotifyExtendedDoubleTypeCollectionChangedEventHandler<TItem, TPriority>? ExtendedCollectionChanged;
    }
}