using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.Blackboard
{
    public struct BlackboardItemKey : IEquatable<BlackboardItemKey>
    {
        public string Key { get; set; }
        public int HashCode { get; set; }
        [AllowNull]
        public Type ItemType { get; set; }
        [AllowNull]
        public string Category { get; set; }



        public BlackboardItemKey(string key)
        {
            this.Key = key;
            this.HashCode = (int)Util.FarmHash.Hash64(this.Key);
        }

        public bool Equals(BlackboardItemKey other)
        {
            return this.HashCode == other.HashCode;
        }

        public override int GetHashCode()
        {
            return this.HashCode;
        }
    }
}