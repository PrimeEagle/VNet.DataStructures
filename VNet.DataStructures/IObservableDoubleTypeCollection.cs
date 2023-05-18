using System.Collections.Specialized;

namespace VNet.DataStructures
{
    public interface IObservableDoubleTypeCollection<TItem, TPriority> : INotifyCollectionChanged, INotifyExtendedDoubleTypeCollectionChanged<TItem, TPriority>

    {
    }
}