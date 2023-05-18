namespace VNet.DataStructures
{
    public interface INotifyExtendedSingleTypeCollectionChangedEventArgs<T>
    {
        public IEnumerable<T> OldItems { get; init; }
        public IEnumerable<T> NewItems { get; init; }
        public T OldItem { get; init; }
        public T NewItem { get; init; }
        public int OldIndex { get; init; }
        public int NewIndex { get; init; }
    }
}