namespace VNet.DataStructures.Tree
{
    internal static class Util
    {
        internal static void ValidateSortedCollection<T>(IEnumerable<T> sortedCollection) where T : IComparable
        {
            if (!IsSorted(sortedCollection))
                throw new ArgumentException("Initial collection should have unique keys and be in sorted order.");
        }

        internal static BinarySearchTreeNodeBase<T> ToBst<T>(BinarySearchTreeNodeBase<T>[] sortedNodes) where T : IComparable
        {
            return ToBst(sortedNodes, 0, sortedNodes.Length - 1);
        }

        internal static int AssignCount<T>(BinarySearchTreeNodeBase<T> node) where T : IComparable
        {
            if (node == null) return 0;

            node.Count = AssignCount(node.Left) + AssignCount(node.Right) + 1;

            return node.Count;
        }

        private static BinarySearchTreeNodeBase<T> ToBst<T>(BinarySearchTreeNodeBase<T>[] sortedNodes, int start, int end) where T : IComparable
        {
            if (start > end)
                return null;

            var mid = (start + end) / 2;
            var root = sortedNodes[mid];

            root.Left = ToBst(sortedNodes, start, mid - 1);
            if (root.Left != null) root.Left.Parent = root;

            root.Right = ToBst(sortedNodes, mid + 1, end);
            if (root.Right != null) root.Right.Parent = root;

            return root;
        }

        private static bool IsSorted<T>(IEnumerable<T> collection) where T : IComparable
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext()) return true;

            var previous = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.CompareTo(previous) <= 0) return false;
            }

            return true;
        }

        internal static (BinarySearchTreeNodeBase<T>, int) Find<T>(this BinarySearchTreeNodeBase<T> current, T value) where T : IComparable
        {
            var position = 0;

            while (true)
            {
                if (current == null) return (null, -1);

                var compareResult = current.Value.CompareTo(value);

                if (compareResult == 0)
                {
                    position += current.Left != null ? current.Left.Count : 0;
                    return (current, position);
                }

                if (compareResult > 0)
                {
                    current = current.Left;
                }
                else
                {
                    position += (current.Left != null ? current.Left.Count : 0) + 1;
                    current = current.Right;
                }
            }
        }

        internal static BinarySearchTreeNodeBase<T> FindMax<T>(this BinarySearchTreeNodeBase<T> node) where T : IComparable
        {
            if (node == null) return null;

            while (true)
            {
                if (node.Right == null) return node;
                node = node.Right;
            }
        }

        internal static BinarySearchTreeNodeBase<T> FindMin<T>(this BinarySearchTreeNodeBase<T> node) where T : IComparable
        {
            if (node == null) return null;

            while (true)
            {
                if (node.Left == null) return node;
                node = node.Left;
            }
        }

        internal static BinarySearchTreeNodeBase<T> NextLower<T>(this BinarySearchTreeNodeBase<T> node) where T : IComparable
        {
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Left != null)
                {
                    node = node.Left;

                    while (node.Right != null) node = node.Right;

                    return node;
                }

                while (node.Parent != null && node.IsLeftChild) node = node.Parent;

                return node?.Parent;
            }

            if (node.Left != null)
            {
                node = node.Left;

                while (node.Right != null) node = node.Right;

                return node;
            }

            return node.Parent;
        }

        internal static BinarySearchTreeNodeBase<T> NextHigher<T>(this BinarySearchTreeNodeBase<T> node) where T : IComparable
        {
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Right != null)
                {
                    node = node.Right;

                    while (node.Left != null) node = node.Left;

                    return node;
                }

                return node?.Parent;
            }

            if (node.Right != null)
            {
                node = node.Right;

                while (node.Left != null) node = node.Left;

                return node;
            }

            while (node.Parent != null && node.IsRightChild) node = node.Parent;

            return node?.Parent;
        }
        internal static void UpdateCounts<T>(this BinarySearchTreeNodeBase<T> node, bool spiralUp = false) where T : IComparable
        {
            while (node != null)
            {
                var leftCount = node.Left?.Count ?? 0;
                var rightCount = node.Right?.Count ?? 0;

                node.Count = leftCount + rightCount + 1;

                node = node.Parent;

                if (!spiralUp) break;
            }
        }

        internal static BinarySearchTreeNodeBase<T> KthSmallest<T>(this BinarySearchTreeNodeBase<T> node, int k) where T : IComparable
        {
            var leftCount = node.Left != null ? node.Left.Count : 0;

            if (k == leftCount) return node;

            if (k < leftCount) return KthSmallest(node.Left, k);

            return KthSmallest(node.Right, k - leftCount - 1);
        }

        internal static int Position<T>(this BinarySearchTreeNodeBase<T> node, T item) where T : IComparable
        {
            if (node == null) return -1;

            var leftCount = node.Left != null ? node.Left.Count : 0;

            if (node.Value.CompareTo(item) == 0) return leftCount;

            if (item.CompareTo(node.Value) < 0) return Position(node.Left, item);

            var position = Position(node.Right, item);

            return position < 0 ? position : position + leftCount + 1;
        }

        internal static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> e)
        {
            while (e.MoveNext()) yield return e.Current;
        }
    }
}