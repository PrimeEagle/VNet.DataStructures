using System.ComponentModel;

namespace VNet.DataStructures
{
    public interface IExtendedPropertyChangedEventArgs<T> : INotifyPropertyChanged
    {
        public T OldValue { get; init; }
        public T NewValue { get; init; }
    }
}