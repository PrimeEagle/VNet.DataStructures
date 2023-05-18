namespace VNet.DataStructures.Dictionary
{
    public interface INotifyExtendedDictionaryChanged<TKey, TValue>
    {
        public event NotifyExtendedDictionaryChangedEventHandler<TKey, TValue> ExtendedDictionaryChanged;
    }
}