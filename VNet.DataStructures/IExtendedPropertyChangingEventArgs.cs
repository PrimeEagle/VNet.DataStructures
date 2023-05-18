using System.ComponentModel;

namespace VNet.DataStructures
{
    public interface IExtendedPropertyChangingEventArgs<T> : INotifyPropertyChanging
    {
        public T OldValue { get; init; }
        public T NewValue { get; init; }
    }
}