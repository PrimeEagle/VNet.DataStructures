using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class SegmentTree<T> : IEnumerable<T>
    {
        private readonly Func<T> defaultValue;
        private readonly T[] input;
        private readonly int length;
        private readonly Func<T, T, T> operation;
        private readonly T[] segmentTree;

        public SegmentTree(T[] input, Func<T, T, T> operation, Func<T> defaultValue)
        {
            if (input == null || operation == null) throw new ArgumentNullException();

            this.input = input.Clone() as T[];

            var maxHeight = Math.Ceiling(Math.Log(input.Length, 2));
            var maxTreeNodes = 2 * (int)Math.Pow(2, maxHeight) - 1;
            segmentTree = new T[maxTreeNodes];
            this.operation = operation;
            this.defaultValue = defaultValue;

            length = input.Length;

            Construct(input, 0, input.Length - 1, 0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return input.Select(x => x).GetEnumerator();
        }

        private T Construct(T[] input, int left, int right, int currentIndex)
        {
            if (left == right)
            {
                segmentTree[currentIndex] = input[left];
                return segmentTree[currentIndex];
            }

            var midIndex = GetMidIndex(left, right);

            segmentTree[currentIndex] = operation(Construct(input, left, midIndex, 2 * currentIndex + 1),
                Construct(input, midIndex + 1, right, 2 * currentIndex + 2));

            return segmentTree[currentIndex];
        }

        public T RangeResult(int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > length - 1
                               || endIndex < startIndex)
                throw new ArgumentException();

            return GetRangeResult(startIndex, endIndex, 0, length - 1, 0);
        }

        private T GetRangeResult(int start, int end, int left, int right, int currentIndex)
        {
            if (left >= start && right <= end) return segmentTree[currentIndex];

            if (right < start || left > end) return defaultValue();

            var midIndex = GetMidIndex(left, right);
            return operation(GetRangeResult(start, end, left, midIndex, 2 * currentIndex + 1),
                GetRangeResult(start, end, midIndex + 1, right, 2 * currentIndex + 2));
        }

        private int GetMidIndex(int left, int right)
        {
            return left + (right - left) / 2;
        }
    }
}