using System.ComponentModel;

namespace VNet.DataStructures
{
    public interface IDeepObservableSingleTypeCollection<T> : IObservableSingleTypeCollection<T>,
                                                    INotifyPropertyChanging, INotifyPropertyChanged,
                                                    INotifyExtendedPropertyChanging<T>, INotifyExtendedPropertyChanged<T>
    {
    }
}