namespace VNet.DataStructures.Tree
{
    internal class OneDimentionalIntervalTree<T> where T : IComparable
    {
        private readonly Lazy<T> defaultValue;
        private readonly RedBlackTree<OneDimentionalInterval<T>> redBlackTree = new();
        internal OneDimentionalIntervalTree(Lazy<T> defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        internal int Count { get; private set; }

        internal void Insert(OneDimentionalInterval<T> newInterval)
        {
            SortInterval(newInterval);
            var existing = redBlackTree.FindNode(newInterval);
            if (existing != null)
                existing.Value.End.Add(newInterval.End[0]);
            else
                existing = redBlackTree.InsertAndReturnNode(newInterval).Item1;
            UpdateMax(existing);
            Count++;
        }

        internal void Delete(OneDimentionalInterval<T> interval)
        {
            SortInterval(interval);

            var existing = redBlackTree.FindNode(interval);
            if (existing != null && existing.Value.End.Count > 1)
            {
                existing.Value.End.RemoveAt(existing.Value.End.Count - 1);
                UpdateMax(existing);
            }
            else if (existing != null)
            {
                redBlackTree.Delete(interval);
                UpdateMax(existing.Parent);
            }
            else
            {
                throw new ArgumentException("Interval not found in this interval tree.");
            }

            Count--;
        }

        internal OneDimentionalInterval<T> GetOverlap(OneDimentionalInterval<T> searchInterval)
        {
            SortInterval(searchInterval);
            return GetOverlap(redBlackTree.Root, searchInterval);
        }

        internal List<OneDimentionalInterval<T>> GetOverlaps(OneDimentionalInterval<T> searchInterval)
        {
            SortInterval(searchInterval);
            return GetOverlaps(redBlackTree.Root, searchInterval);
        }

        internal bool DoOverlap(OneDimentionalInterval<T> searchInterval)
        {
            SortInterval(searchInterval);
            return GetOverlap(redBlackTree.Root, searchInterval) != null;
        }

        private void SortInterval(OneDimentionalInterval<T> value)
        {
            if (value.Start.CompareTo(value.End[0]) <= 0) return;

            var tmp = value.End[0];
            value.End[0] = value.Start;
            value.Start = tmp;
        }

        private OneDimentionalInterval<T> GetOverlap(RedBlackTreeNode<OneDimentionalInterval<T>> current,
            OneDimentionalInterval<T> searchInterval)
        {
            while (true)
            {
                if (current == null) return null;

                if (DoOverlap(current.Value, searchInterval)) return current.Value;

                if (current.Left != null && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
                {
                    current = current.Left;
                    continue;
                }

                current = current.Right;
            }
        }

        private List<OneDimentionalInterval<T>> GetOverlaps(RedBlackTreeNode<OneDimentionalInterval<T>> current,
            OneDimentionalInterval<T> searchInterval, List<OneDimentionalInterval<T>> result = null)
        {
            if (result == null) result = new List<OneDimentionalInterval<T>>();

            if (current == null) return result;

            if (DoOverlap(current.Value, searchInterval)) result.Add(current.Value);

            if (current.Left != null
                && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
                GetOverlaps(current.Left, searchInterval, result);

            GetOverlaps(current.Right, searchInterval, result);

            return result;
        }

        private bool DoOverlap(OneDimentionalInterval<T> a, OneDimentionalInterval<T> b)
        {
            a.MatchingEndIndex = -1;
            b.MatchingEndIndex = -1;

            for (var i = 0; i < a.End.Count; i++)
                for (var j = 0; j < b.End.Count; j++)
                {
                    if (a.Start.CompareTo(b.End[j]) > 0 || a.End[i].CompareTo(b.Start) < 0) continue;

                    a.MatchingEndIndex = i;
                    b.MatchingEndIndex = j;

                    return true;
                }

            return false;
        }

        private void UpdateMax(RedBlackTreeNode<OneDimentionalInterval<T>> node, T currentMax, bool recurseUp = true)
        {
            while (true)
            {
                if (node == null) return;

                if (node.Left != null && node.Right != null)
                {
                    if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0) currentMax = node.Left.Value.MaxEnd;

                    if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0) currentMax = node.Right.Value.MaxEnd;
                }
                else if (node.Left != null)
                {
                    if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0) currentMax = node.Left.Value.MaxEnd;
                }
                else if (node.Right != null)
                {
                    if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0) currentMax = node.Right.Value.MaxEnd;
                }

                foreach (var v in node.Value.End)
                    if (currentMax.CompareTo(v) < 0)
                        currentMax = v;

                node.Value.MaxEnd = currentMax;


                if (recurseUp)
                {
                    node = node.Parent;
                    continue;
                }


                break;
            }
        }

        private void UpdateMax(RedBlackTreeNode<OneDimentionalInterval<T>> newRoot, bool recurseUp = true)
        {
            if (newRoot == null)
                return;

            newRoot.Value.MaxEnd = defaultValue.Value;

            if (newRoot.Left != null)
            {
                newRoot.Left.Value.MaxEnd = defaultValue.Value;
                UpdateMax(newRoot.Left, newRoot.Left.Value.MaxEnd, recurseUp);
            }

            if (newRoot.Right != null)
            {
                newRoot.Right.Value.MaxEnd = defaultValue.Value;
                UpdateMax(newRoot.Right, newRoot.Right.Value.MaxEnd, recurseUp);
            }

            UpdateMax(newRoot, newRoot.Value.MaxEnd, recurseUp);
        }
    }
}