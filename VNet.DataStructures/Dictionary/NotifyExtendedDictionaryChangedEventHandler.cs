namespace VNet.DataStructures.Dictionary
{
    public delegate void NotifyExtendedDictionaryChangedEventHandler<TKey, TValue>(object? sender, NotifyExtendedDictionaryChangedEventArgs<TKey, TValue> e);
}