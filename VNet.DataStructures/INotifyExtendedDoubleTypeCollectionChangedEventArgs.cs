namespace VNet.DataStructures
{
    public interface INotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority>
    {
        public IEnumerable<TItem> OldItems { get; init; }
        public IEnumerable<TItem> NewItems { get; init; }
        public TItem OldItem { get; init; }
        public TItem NewItem { get; init; }
        public IEnumerable<TPriority> OldPriorities { get; init; }
        public IEnumerable<TPriority> NewPriorities { get; init; }
        public TPriority OldPriority { get; init; }
        public TPriority NewPriority { get; init; }
        public int OldIndex { get; init; }
        public int NewIndex { get; init; }
    }
}