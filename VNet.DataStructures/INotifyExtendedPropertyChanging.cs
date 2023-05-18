using System.ComponentModel;

namespace VNet.DataStructures
{
    public interface INotifyExtendedPropertyChanging<T> : INotifyPropertyChanging
    {
        event ExtendedPropertyChangingEventHandler<T>? ExtendedPropertyChanging;
    }
}