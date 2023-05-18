using System.Collections.Specialized;

namespace VNet.DataStructures.Dictionary
{
    public static class ExtendedDictionaryActionToCollectionActionConverter
    {
        public static NotifyCollectionChangedAction Convert(NotifyExtendedDictionaryChangedAction action)
        {
            NotifyCollectionChangedAction result;

            switch (action)
            {
                case NotifyExtendedDictionaryChangedAction.Add:
                case NotifyExtendedDictionaryChangedAction.AddOrUpdate:
                case NotifyExtendedDictionaryChangedAction.GetOrAdd:
                    result = NotifyCollectionChangedAction.Add;
                    break;
                case NotifyExtendedDictionaryChangedAction.Remove:
                    result = NotifyCollectionChangedAction.Remove;
                    break;
                case NotifyExtendedDictionaryChangedAction.Reset:
                    result = NotifyCollectionChangedAction.Reset;
                    break; ;
                case NotifyExtendedDictionaryChangedAction.Replace:
                case NotifyExtendedDictionaryChangedAction.Update:
                    result = NotifyCollectionChangedAction.Replace;
                    break;
                default:
                    result = NotifyCollectionChangedAction.Add;
                    break;
            }

            return result;
        }
    }
}