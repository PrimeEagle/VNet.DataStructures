using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class BinaryIndexedTree<T> : IEnumerable<T>
    {
        private readonly T[] input;
        private readonly Func<T, T, T> sumOperation;
        private T[] tree;

        public BinaryIndexedTree(T[] input, Func<T, T, T> sumOperation)
        {
            if (input == null || sumOperation == null) throw new ArgumentNullException();

            this.input = input.Clone() as T[];

            this.sumOperation = sumOperation;
            Construct(input);
        }

        private int Length => tree.Length - 1;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return input.Select(x => x).GetEnumerator();
        }

        private void Construct(T[] input)
        {
            tree = new T[input.Length + 1];

            for (var i = 0; i < input.Length; i++)
            {
                var j = i + 1;
                while (j < input.Length)
                {
                    tree[j] = sumOperation(tree[j], input[i]);
                    j = GetNextIndex(j);
                }
            }
        }

        public T PrefixSum(int endIndex)
        {
            if (endIndex < 0 || endIndex > Length - 1) throw new ArgumentException();

            var sum = default(T);

            var currentIndex = endIndex + 1;

            while (currentIndex > 0)
            {
                sum = sumOperation(sum, tree[currentIndex]);
                currentIndex = GetParentIndex(currentIndex);
            }

            return sum;
        }

        private int GetNextIndex(int currentIndex)
        {
            return currentIndex + (currentIndex & -currentIndex);
        }

        private int GetParentIndex(int currentIndex)
        {
            return currentIndex - (currentIndex & -currentIndex);
        }
    }
}