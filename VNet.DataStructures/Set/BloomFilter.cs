using System.Collections;

namespace VNet.DataStructures.Set
{
    public class BloomFilter<T>
    {
        private readonly BitArray filter;
        private readonly int numberOfHashFunctions;

        public BloomFilter(int size, int numberOfHashFunctions = 2)
        {
            if (size <= numberOfHashFunctions)
                throw new ArgumentException("size cannot be less than or equal to numberOfHashFunctions.");

            this.numberOfHashFunctions = numberOfHashFunctions;
            filter = new BitArray(size);
        }

        public void AddKey(T key)
        {
            foreach (var hash in GetHashes(key)) filter[hash % filter.Length] = true;
        }

        public bool KeyExists(T key)
        {
            foreach (var hash in GetHashes(key))
                if (filter[hash % filter.Length] == false)
                    return false;

            return true;
        }

        private IEnumerable<int> GetHashes(T key)
        {
            for (var i = 1; i <= numberOfHashFunctions; i++)
            {
                var obj = new { Key = key, InitialValue = i };
                yield return Math.Abs(obj.GetHashCode());
            }
        }
    }
}