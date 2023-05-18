namespace VNet.DataStructures.Dictionary
{
    public interface IObservableDictionary<TKey, TValue> : INotifyExtendedDictionaryChanged<TKey, TValue>
    {
    }
}