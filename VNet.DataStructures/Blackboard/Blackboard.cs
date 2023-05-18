using System.Collections.Concurrent;

namespace VNet.DataStructures.Blackboard
{
    public class Blackboard : IBlackboard
    {
        private readonly ConcurrentDictionary<BlackboardDictionaryKey, IBlackboardTypeDictionary> _typeDictionaries;



        private IBlackboardTypeDictionary<T> GetTypeDictionary<T>(BlackboardDictionaryKey key)
        {
            IBlackboardTypeDictionary dictionary;

            if (_typeDictionaries.ContainsKey(key))
            {
                dictionary = _typeDictionaries[key];
            }
            else
            { 
                dictionary = CreateTypeDictionary<T>(key);
            }

            return (IBlackboardTypeDictionary<T>)dictionary;
        }
            
        private IBlackboardTypeDictionary<T> CreateTypeDictionary<T>(BlackboardDictionaryKey key)
        {
            IBlackboardTypeDictionary dictionary;

            if (!_typeDictionaries.ContainsKey(key) && _typeDictionaries.TryAdd(key, new BlackboardTypeDictionary<T>()))
            {
                dictionary = _typeDictionaries[key];
            }
            else
            {
                throw new InvalidOperationException($"Could not create BlackboardTypeDictionary for {key.ItemType}, {key.Category}>.");
            }

            return (IBlackboardTypeDictionary<T>)dictionary;
        }

        public Blackboard()
        {
            _typeDictionaries = new ConcurrentDictionary<BlackboardDictionaryKey, IBlackboardTypeDictionary>();
        }

        public T AddOrUpdate<T>(string keyName, object item, string category)
        {
            BlackboardComboKey ck = new(keyName, category, typeof(T));
            var typeDictionary = (IBlackboardTypeDictionary<T>)GetTypeDictionary<T>(ck.DictionaryKey);

            typeDictionary.AddOrUpdate(ck.ItemKey, (T)item);

            return (T)item;
        }

        public bool Remove<T>(string keyName, string category, out T item)
        {
            BlackboardComboKey ck = new(keyName, category, typeof(T));
            var typeDictionary = (IBlackboardTypeDictionary<T>)GetTypeDictionary<T>(ck.DictionaryKey);

            return typeDictionary.Remove<T>(ck.ItemKey, out item);
        }
    }
}