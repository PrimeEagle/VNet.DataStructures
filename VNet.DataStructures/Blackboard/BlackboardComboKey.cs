namespace VNet.DataStructures.Blackboard
{
    public class BlackboardComboKey
    {
        public BlackboardDictionaryKey DictionaryKey { get; set; }
        public BlackboardItemKey ItemKey { get; set; }


        public BlackboardComboKey(string keyName, string category, Type itemType)
        {
            DictionaryKey = new BlackboardDictionaryKey(itemType)
            {
                Category = category,
            };

            ItemKey = new BlackboardItemKey(keyName)
            {
                Category= category,
                ItemType = itemType
            };
        }
    }
}
