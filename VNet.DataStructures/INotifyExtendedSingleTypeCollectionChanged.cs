namespace VNet.DataStructures
{
    public interface INotifyExtendedSingleTypeCollectionChanged<T>
    {
        public event NotifyExtendedSingleTypeCollectionChangedEventHandler<T>? ExtendedCollectionChanged;
    }
}