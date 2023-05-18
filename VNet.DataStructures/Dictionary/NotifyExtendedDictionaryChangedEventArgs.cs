using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.Dictionary
{
    public class NotifyExtendedDictionaryChangedEventArgs<TKey, TValue> : NotifyCollectionChangedEventArgs, INotifyExtendedDictionaryChangedEventArgs<TKey, TValue>
    {
        public new NotifyExtendedDictionaryChangedAction Action { get; init; }
        [AllowNull]
        public ICollection<TKey> OldKeys { get; init; }
        [AllowNull]
        public ICollection<TKey> NewKeys { get; init; }
        [AllowNull]
        public ICollection<TValue> OldValues { get; init; }
        [AllowNull]
        public ICollection<TValue> NewValues { get; init; }
        [AllowNull]
        public TValue OldItem { get; init; }
        [AllowNull]
        public TValue NewItem { get; init; }
        [AllowNull]
        public int OldIndex { get; init; }
        [AllowNull]
        public int NewIndex { get; init; }


        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action))
        {
            Action = action;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, TValue? changedItem)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), changedItem)
        {
            Action = action;
            NewItem = changedItem;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, IList<TValue>? changedItems)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), changedItems)
        {
            Action = action;
            NewValues = changedItems;
        }
        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, TValue? changedItem, int index)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), changedItem, index)
        {
            Action = action;
            NewItem = changedItem;
            NewIndex = index;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, TValue? newItem, TValue? oldItem)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), newItem, oldItem)
        {
            Action = action;
            NewItem = newItem;
            OldItem = oldItem;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, IList<TValue> newItems, IList<TValue> oldItems)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), newItems, oldItems)
        {
            Action = action;
            NewValues = newItems;
            OldValues = oldItems;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, IList<TValue> changedItems, int index)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), changedItems, index)
        {
            Action = action;
            NewValues = changedItems;
            NewIndex = index;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, TValue? changedItem, int index, int oldIndex)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), changedItem, index, oldIndex)
        {
            Action = action;
            NewItem = changedItem;
            NewIndex = index;
            OldIndex = oldIndex;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, TValue? newItem, TValue? oldItem, int index)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), newItem, oldItem, index)
        {
            Action = action;
            NewItem = newItem;
            OldItem = oldItem;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, IList<TValue> newItems, IList<TValue> oldItems, int startingIndex)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), newItems, oldItems, startingIndex)
        {
            Action = action;
            NewValues = newItems;
            OldValues = oldItems;
            NewIndex = startingIndex;
        }

        public NotifyExtendedDictionaryChangedEventArgs(NotifyExtendedDictionaryChangedAction action, IList<TValue> changedItems, int index, int oldIndex)
            : base(ExtendedDictionaryActionToCollectionActionConverter.Convert(action), changedItems, index, oldIndex)
        {
            Action = action;
            OldIndex = oldIndex;
            NewIndex = index;
        }

        public event NotifyExtendedDictionaryChangedEventHandler<TKey, TValue>? ExtendedDictionaryChanged;
    }
}