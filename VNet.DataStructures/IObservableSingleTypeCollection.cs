using System.Collections.Specialized;

namespace VNet.DataStructures
{
    public interface IObservableSingleTypeCollection<T> : INotifyCollectionChanged, INotifyExtendedSingleTypeCollectionChanged<T>

    {
    }
}