namespace VNet.DataStructures.Dictionary
{
    public interface INotifyExtendedDictionaryChangedEventArgs<TKey, TValue>
    {
        public ICollection<TKey> OldKeys { get; init; }
        public ICollection<TKey> NewKeys { get; init; }
        public ICollection<TValue> OldValues { get; init; }
        public ICollection<TValue> NewValues { get; init; }
        public TValue OldItem { get; init; }
        public TValue NewItem { get; init; }
    }
}