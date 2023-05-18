namespace VNet.DataStructures
{
    public interface INotifyExtendedDoubleTypeCollectionChanged<TItem, TPriority>
    {
        public event NotifyExtendedDoubleTypeCollectionChangedEventHandler<TItem, TPriority>? ExtendedCollectionChanged;
    }
}