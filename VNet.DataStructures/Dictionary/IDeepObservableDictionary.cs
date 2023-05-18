using System.ComponentModel;

namespace VNet.DataStructures.Dictionary
{
    public interface IDeepObservableDictionary<TKey, TValue> : IObservableDictionary<TKey, TValue>, 
                                                               INotifyPropertyChanging, INotifyPropertyChanged,  
                                                               INotifyExtendedPropertyChanging<TValue>, INotifyExtendedPropertyChanged<TValue>
    {
    }
}