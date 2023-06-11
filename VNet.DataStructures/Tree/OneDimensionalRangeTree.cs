namespace VNet.DataStructures.Tree
{
    internal class OneDimentionalRangeTree<T> where T : IComparable
    {
        internal RedBlackTree<RangeTreeNode<T>> Tree = new();
        internal int Count => Tree.Count;

        internal RangeTreeNode<T> Find(T value)
        {
            var result = Tree.FindNode(new RangeTreeNode<T>(value));
            if (result == null) throw new NullReferenceException("Item not found in this tree.");

            return result.Value;
        }

        internal RangeTreeNode<T> Insert(T value)
        {
            var newNode = new RangeTreeNode<T>(value);

            var existing = Tree.FindNode(newNode);
            if (existing != null)
            {
                existing.Value.Values.Add(value);
                return existing.Value;
            }

            Tree.Insert(newNode);
            return newNode;
        }

        internal void Delete(T value)
        {
            var existing = Tree.FindNode(new RangeTreeNode<T>(value));

            if (existing.Value.Values.Count == 1)
            {
                Tree.Delete(new RangeTreeNode<T>(value));
                return;
            }

            //remove last
            existing.Value.Values.RemoveAt(existing.Value.Values.Count - 1);
        }

        internal List<RangeTreeNode<T>> RangeSearch(T start, T end)
        {
            return GetInRange(new List<RangeTreeNode<T>>(),
                new Dictionary<RedBlackTreeNode<RangeTreeNode<T>>, bool>(),
                Tree.Root, start, end);
        }

        private List<RangeTreeNode<T>> GetInRange(List<RangeTreeNode<T>> result,
            Dictionary<RedBlackTreeNode<RangeTreeNode<T>>, bool> visited,
            RedBlackTreeNode<RangeTreeNode<T>> currentNode,
            T start, T end)
        {
            if (currentNode.IsLeaf)
            {
                if (!InRange(currentNode, start, end)) return result;

                result.Add(currentNode.Value);
            }
            else
            {
                if (start.CompareTo(currentNode.Value.Value) <= 0)
                {
                    if (currentNode.Left != null) GetInRange(result, visited, currentNode.Left, start, end);

                    if (!visited.ContainsKey(currentNode)
                        && InRange(currentNode, start, end))
                    {
                        result.Add(currentNode.Value);
                        visited.Add(currentNode, false);
                    }
                }

                if (end.CompareTo(currentNode.Value.Value) < 0) return result;

                {
                    if (currentNode.Right != null) GetInRange(result, visited, currentNode.Right, start, end);

                    if (visited.ContainsKey(currentNode) || !InRange(currentNode, start, end)) return result;

                    result.Add(currentNode.Value);
                    visited.Add(currentNode, false);
                }
            }

            return result;
        }

        private bool InRange(RedBlackTreeNode<RangeTreeNode<T>> currentNode, T start, T end)
        {
            return start.CompareTo(currentNode.Value.Value) <= 0
                   && end.CompareTo(currentNode.Value.Value) >= 0;
        }
    }
}
