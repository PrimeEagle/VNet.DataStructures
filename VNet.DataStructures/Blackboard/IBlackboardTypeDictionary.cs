namespace VNet.DataStructures.Blackboard
{
    public interface IBlackboardTypeDictionary
    {

    }

    public interface IBlackboardTypeDictionary<T> : IBlackboardTypeDictionary
    {
        public T AddOrUpdate(BlackboardItemKey key, T item);
        public bool Remove<T>(BlackboardItemKey key, out T item);
    }
}
