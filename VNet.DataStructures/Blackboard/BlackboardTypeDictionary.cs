using System.Collections.Concurrent;

namespace VNet.DataStructures.Blackboard
{
    public class BlackboardTypeDictionary<T> : ConcurrentDictionary<BlackboardItemKey, T>, IBlackboardTypeDictionary
    {
        public void test()
        {
            ConcurrentDictionary<BlackboardItemKey, T> m_test = new ConcurrentDictionary<BlackboardItemKey, T>();
        }
    }
}