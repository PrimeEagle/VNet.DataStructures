using System.Collections;
using System.Reflection;

namespace VNet.DataStructures.Tree
{
    public class RedBlackTree<T> : IEnumerable<T> where T : IComparable
    {
        internal readonly Dictionary<T, BinarySearchTreeNodeBase<T>> NodeLookUp;
        public RedBlackTree(bool enableNodeLookUp = false, IEqualityComparer<T> equalityComparer = null)
        {
            if (enableNodeLookUp)
            {
                if (!typeof(T).GetTypeInfo().IsValueType && equalityComparer == null)
                    throw new ArgumentException(
                        "equalityComparer parameter is required when node lookup us enabled and T is not a value type.");

                NodeLookUp = new Dictionary<T, BinarySearchTreeNodeBase<T>>(equalityComparer ?? EqualityComparer<T>.Default);
            }
        }

        public RedBlackTree(IEnumerable<T> sortedCollection, bool enableNodeLookUp = false,
            IEqualityComparer<T> equalityComparer = null)
        {
            Util.ValidateSortedCollection(sortedCollection);
            var nodes = sortedCollection.Select(x => new RedBlackTreeNode<T>(null, x)).ToArray();
            Root = (RedBlackTreeNode<T>)Util.ToBst(nodes);
            AssignColors(Root);
            Util.AssignCount(Root);

            if (enableNodeLookUp)
            {
                if (!typeof(T).GetTypeInfo().IsValueType && equalityComparer == null)
                    throw new ArgumentException(
                        "equalityComparer parameter is required when node lookup us enabled and T is not a value type.");

                NodeLookUp = nodes.ToDictionary(x => x.Value, x => x as BinarySearchTreeNodeBase<T>,
                    equalityComparer ?? EqualityComparer<T>.Default);
            }
        }

        internal RedBlackTreeNode<T> Root { get; set; }

        public int Count => Root == null ? 0 : Root.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BinarySearchTreeEnumerator<T>(Root);
        }

        public bool HasItem(T value)
        {
            if (Root == null) return false;

            if (NodeLookUp != null) return NodeLookUp.ContainsKey(value);

            return Find(value).Item1 != null;
        }

        internal void Clear()
        {
            Root = null;
        }

        public T Max()
        {
            var max = Root.FindMax();
            return max == null ? default : max.Value;
        }

        private RedBlackTreeNode<T> FindMax(RedBlackTreeNode<T> node)
        {
            return node.FindMax() as RedBlackTreeNode<T>;
        }

        public T Min()
        {
            var min = Root.FindMin();
            return min == null ? default : min.Value;
        }

        public int IndexOf(T item)
        {
            return Root.Position(item);
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentNullException("index");

            return Root.KthSmallest(index).Value;
        }

        internal RedBlackTreeNode<T> FindNode(T value)
        {
            return Root == null ? null : Find(value).Item1;
        }

        internal bool Exists(T value)
        {
            return FindNode(value) != null;
        }

        internal (RedBlackTreeNode<T>, int) Find(T value)
        {
            if (NodeLookUp != null)
            {
                if (NodeLookUp.ContainsKey(value))
                {
                    var node = NodeLookUp[value] as RedBlackTreeNode<T>;
                    return (node, Root.Position(value));
                }

                return (null, -1);
            }

            var result = Root.Find(value);
            return (result.Item1 as RedBlackTreeNode<T>, result.Item2);
        }

        public int Insert(T value)
        {
            var node = InsertAndReturnNode(value);
            return node.Item2;
        }

        internal (RedBlackTreeNode<T>, int) InsertAndReturnNode(T value)
        {
            if (Root == null)
            {
                Root = new RedBlackTreeNode<T>(null, value) { NodeColor = RedBlackTreeNodeColor.Black };
                if (NodeLookUp != null) NodeLookUp[value] = Root;

                return (Root, 0);
            }

            var newNode = Insert(Root, value);

            if (NodeLookUp != null) NodeLookUp[value] = newNode.Item1;

            return newNode;
        }

        private (RedBlackTreeNode<T>, int) Insert(RedBlackTreeNode<T> currentNode, T newNodeValue)
        {
            var insertionPosition = 0;

            while (true)
            {
                var compareResult = currentNode.Value.CompareTo(newNodeValue);

                if (compareResult < 0)
                {
                    insertionPosition += (currentNode.Left != null ? currentNode.Left.Count : 0) + 1;

                    if (currentNode.Right == null)
                    {
                        var node = currentNode.Right = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                        BalanceInsertion(currentNode.Right);
                        return (node, insertionPosition);
                    }

                    currentNode = currentNode.Right;
                }
                else if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        var node = currentNode.Left = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                        BalanceInsertion(currentNode.Left);
                        return (node, insertionPosition);
                    }

                    currentNode = currentNode.Left;
                }
                else
                {
                    throw new InvalidOperationException("Item with same key exists");
                }
            }
        }

        private void BalanceInsertion(RedBlackTreeNode<T> nodeToBalance)
        {
            while (true)
            {
                if (nodeToBalance == Root)
                {
                    nodeToBalance.NodeColor = RedBlackTreeNodeColor.Black;
                    break;
                }

                if (nodeToBalance.NodeColor == RedBlackTreeNodeColor.Red)
                    if (nodeToBalance.Parent.NodeColor == RedBlackTreeNodeColor.Red)
                    {
                        if (nodeToBalance.Parent.Sibling != null &&
                            nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Red)
                        {
                            nodeToBalance.Parent.Sibling.NodeColor = RedBlackTreeNodeColor.Black;
                            nodeToBalance.Parent.NodeColor = RedBlackTreeNodeColor.Black;

                            if (nodeToBalance.Parent.Parent != Root)
                                nodeToBalance.Parent.Parent.NodeColor = RedBlackTreeNodeColor.Red;

                            nodeToBalance.UpdateCounts();
                            nodeToBalance.Parent.UpdateCounts();
                            nodeToBalance = nodeToBalance.Parent.Parent;
                        }
                        else if (nodeToBalance.Parent.Sibling == null ||
                                 nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Black)
                        {
                            if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsLeftChild)
                            {
                                var newRoot = nodeToBalance.Parent;
                                SwapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                                RightRotate(nodeToBalance.Parent.Parent);

                                if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsRightChild)
                            {
                                RightRotate(nodeToBalance.Parent);

                                var newRoot = nodeToBalance;

                                SwapColors(nodeToBalance.Parent, nodeToBalance);
                                LeftRotate(nodeToBalance.Parent);

                                if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsRightChild)
                            {
                                var newRoot = nodeToBalance.Parent;
                                SwapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                                LeftRotate(nodeToBalance.Parent.Parent);

                                if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsLeftChild)
                            {
                                LeftRotate(nodeToBalance.Parent);

                                var newRoot = nodeToBalance;

                                SwapColors(nodeToBalance.Parent, nodeToBalance);
                                RightRotate(nodeToBalance.Parent);

                                if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                        }
                    }

                if (nodeToBalance.Parent != null)
                {
                    nodeToBalance.UpdateCounts();
                    nodeToBalance = nodeToBalance.Parent;
                    continue;
                }

                break;
            }

            nodeToBalance.UpdateCounts(true);
        }

        private void SwapColors(RedBlackTreeNode<T> node1, RedBlackTreeNode<T> node2)
        {
            var tmpColor = node2.NodeColor;
            node2.NodeColor = node1.NodeColor;
            node1.NodeColor = tmpColor;
        }

        public int Delete(T value)
        {
            if (Root == null) return -1;

            var node = Find(value);

            if (node.Item1 == null) return -1;

            var position = node.Item2;

            Delete(node.Item1);

            if (NodeLookUp != null) NodeLookUp.Remove(value);

            return position;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentException("index");

            var node = Root.KthSmallest(index) as RedBlackTreeNode<T>;

            var deletedValue = node.Value;

            Delete(node);

            if (NodeLookUp != null) NodeLookUp.Remove(deletedValue);

            return node.Value;
        }

        private void Delete(RedBlackTreeNode<T> node)
        {
            if (node.IsLeaf)
            {
                if (node.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    DeleteLeaf(node);
                    node.Parent?.UpdateCounts(true);
                    return;
                }

                DeleteLeaf(node);
                BalanceNode(node.Parent);
            }
            else
            {
                if (node.Left != null && node.Right == null)
                {
                    DeleteLeftNode(node);
                    BalanceNode(node.Left);
                }
                else if (node.Right != null && node.Left == null)
                {
                    DeleteRightNode(node);
                    BalanceNode(node.Right);
                }
                else
                {
                    var maxLeftNode = FindMax(node.Left);

                    if (NodeLookUp != null)
                    {
                        NodeLookUp[node.Value] = maxLeftNode;
                        NodeLookUp[maxLeftNode.Value] = node;
                    }

                    node.Value = maxLeftNode.Value;

                    Delete(maxLeftNode);
                }
            }
        }

        private void BalanceNode(RedBlackTreeNode<T> nodeToBalance)
        {
            //handle six cases
            while (nodeToBalance != null)
            {
                nodeToBalance.UpdateCounts();
                nodeToBalance = HandleDoubleBlack(nodeToBalance);
            }
        }

        private void DeleteLeaf(RedBlackTreeNode<T> node)
        {
            if (node.Parent == null)
                Root = null;
            else if (node.IsLeftChild)
                node.Parent.Left = null;
            else
                node.Parent.Right = null;
        }

        private void DeleteRightNode(RedBlackTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                return;
            }

            if (node.IsLeftChild)
                node.Parent.Left = node.Right;
            else
                node.Parent.Right = node.Right;

            node.Right.Parent = node.Parent;

            if (node.Right.NodeColor != RedBlackTreeNodeColor.Red) return;

            node.Right.NodeColor = RedBlackTreeNodeColor.Black;
        }

        private void DeleteLeftNode(RedBlackTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                return;
            }

            if (node.IsLeftChild)
                node.Parent.Left = node.Left;
            else
                node.Parent.Right = node.Left;

            node.Left.Parent = node.Parent;

            if (node.Left.NodeColor != RedBlackTreeNodeColor.Red) return;

            node.Left.NodeColor = RedBlackTreeNodeColor.Black;
        }

        private void RightRotate(RedBlackTreeNode<T> node)
        {
            var prevRoot = node;
            var leftRightChild = prevRoot.Left.Right;

            var newRoot = node.Left;

            prevRoot.Left.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                    prevRoot.Parent.Left = prevRoot.Left;
                else
                    prevRoot.Parent.Right = prevRoot.Left;
            }
            newRoot.Right = prevRoot;
            prevRoot.Parent = newRoot;

            newRoot.Right.Left = leftRightChild;
            if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

            if (prevRoot == Root) Root = newRoot;

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();
        }

        private void LeftRotate(RedBlackTreeNode<T> node)
        {
            var prevRoot = node;
            var rightLeftChild = prevRoot.Right.Left;

            var newRoot = node.Right;

            prevRoot.Right.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                    prevRoot.Parent.Left = prevRoot.Right;
                else
                    prevRoot.Parent.Right = prevRoot.Right;
            }

            newRoot.Left = prevRoot;
            prevRoot.Parent = newRoot;

            newRoot.Left.Right = rightLeftChild;
            if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

            if (prevRoot == Root) Root = newRoot;

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();
        }

        private RedBlackTreeNode<T> HandleDoubleBlack(RedBlackTreeNode<T> node)
        {
            if (node == Root)
            {
                node.NodeColor = RedBlackTreeNodeColor.Black;
                return null;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Red
                && (node.Sibling.Left == null && node.Sibling.Right == null
                    || node.Sibling.Left != null && node.Sibling.Right != null
                                                 && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                                 && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
            {
                node.Parent.NodeColor = RedBlackTreeNodeColor.Red;
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Black;

                if (node.Sibling.IsRightChild)
                    LeftRotate(node.Parent);
                else
                    RightRotate(node.Parent);

                return node;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && (node.Sibling.Left == null && node.Sibling.Right == null
                    || node.Sibling.Left != null && node.Sibling.Right != null
                                                 && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                                 && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
            {
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;

                return node.Parent;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Red
                && node.Sibling != null
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && (node.Sibling.Left == null && node.Sibling.Right == null
                    || node.Sibling.Left != null && node.Sibling.Right != null
                                                 && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                                 && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
            {
                node.Parent.NodeColor = RedBlackTreeNodeColor.Black;
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
                node.UpdateCounts(true);
                return null;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.IsRightChild
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Left != null
                && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Red
                && node.Sibling.Right != null
                && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black)
            {
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
                node.Sibling.Left.NodeColor = RedBlackTreeNodeColor.Black;
                RightRotate(node.Sibling);

                return node;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.IsLeftChild
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Left != null
                && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Right != null
                && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Red)
            {
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
                node.Sibling.Right.NodeColor = RedBlackTreeNodeColor.Black;
                LeftRotate(node.Sibling);

                return node;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.IsRightChild
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Right != null
                && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Red)
            {
                node.Sibling.Right.NodeColor = RedBlackTreeNodeColor.Black;
                LeftRotate(node.Parent);
                node.UpdateCounts(true);
                return null;
            }

            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.IsLeftChild
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Left != null
                && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Red)
            {
                node.Sibling.Left.NodeColor = RedBlackTreeNodeColor.Black;
                RightRotate(node.Parent);
                node.UpdateCounts(true);
                return null;
            }

            node.UpdateCounts(true);
            return null;
        }

        private void AssignColors(RedBlackTreeNode<T> current)
        {
            if (current == null) return;

            AssignColors(current.Left);
            AssignColors(current.Right);

            if (current.IsLeaf)
                current.NodeColor = RedBlackTreeNodeColor.Red;
            else
                current.NodeColor = RedBlackTreeNodeColor.Black;
        }

        public T NextLower(T value)
        {
            var node = FindNode(value);
            if (node == null) return default;

            var next = node.NextLower();
            return next != null ? next.Value : default;
        }

        public T NextHigher(T value)
        {
            var node = FindNode(value);
            if (node == null) return default;

            var next = node.NextHigher();
            return next != null ? next.Value : default;
        }

        public IEnumerable<T> AsEnumerableDesc()
        {
            return GetEnumeratorDesc().AsEnumerable();
        }

        public IEnumerator<T> GetEnumeratorDesc()
        {
            return new BinarySearchTreeEnumerator<T>(Root, false);
        }
    }
}