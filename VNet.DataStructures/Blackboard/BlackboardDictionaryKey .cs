using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.Blackboard
{
    public struct BlackboardDictionaryKey : IEquatable<BlackboardDictionaryKey>
    {
        public Type ItemType { get; set; }
        [AllowNull]
        public string Category { get; set; }



        public BlackboardDictionaryKey(Type itemType)
        {
            this.ItemType = itemType;
        }

        public bool Equals(BlackboardDictionaryKey other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.ItemType.GetHashCode();
        }
    }
}