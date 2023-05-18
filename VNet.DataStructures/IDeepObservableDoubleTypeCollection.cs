using System.ComponentModel;

namespace VNet.DataStructures
{
    public interface IDeepObservableDoubleTypeCollection<TItem, TPriority> : IObservableDoubleTypeCollection<TItem, TPriority>,
                                                    INotifyPropertyChanging, INotifyPropertyChanged,
                                                    INotifyExtendedPropertyChanging<TItem>, INotifyExtendedPropertyChanged<TItem>
    {
    }
}