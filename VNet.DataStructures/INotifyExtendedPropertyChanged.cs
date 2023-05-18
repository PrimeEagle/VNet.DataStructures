using System.ComponentModel;

namespace VNet.DataStructures
{
    public interface INotifyExtendedPropertyChanged<T> : INotifyPropertyChanged
    {
        event ExtendedPropertyChangedEventHandler<T>? ExtendedPropertyChanged;
    }
}