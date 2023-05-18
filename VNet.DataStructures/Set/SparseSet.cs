using System.Collections;

namespace VNet.DataStructures.Set
{
    public class SparseSet : IEnumerable<int>
    {
        private readonly int[] dense;
        private readonly int[] sparse;

        public SparseSet(int maxVal, int capacity)
        {
            sparse = Enumerable.Repeat(-1, maxVal + 1).ToArray();
            dense = Enumerable.Repeat(-1, capacity).ToArray();
        }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return dense.Take(Count).GetEnumerator();
        }

        public void Add(int value)
        {
            if (value < 0) throw new Exception("Negative values not supported.");
            if (value >= sparse.Length) throw new Exception("Item is greater than max value.");
            if (Count >= dense.Length) throw new Exception("Set reached its capacity.");

            sparse[value] = Count;
            dense[Count] = value;
            Count++;
        }

        public void Remove(int value)
        {
            if (value < 0) throw new Exception("Negative values not supported.");
            if (value >= sparse.Length) throw new Exception("Item is greater than max value.");
            if (HasItem(value) == false) throw new Exception("Item do not exist.");

            var index = sparse[value];
            sparse[value] = -1;

            var lastVal = dense[Count - 1];
            dense[index] = lastVal;
            dense[Count - 1] = -1;

            sparse[lastVal] = index;

            Count--;
        }

        public bool HasItem(int value)
        {
            var index = sparse[value];
            return index != -1 && dense[index] == value;
        }

        public void Clear()
        {
            Count = 0;
        }
    }
}