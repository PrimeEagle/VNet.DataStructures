using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class AvlTree<T> : IEnumerable<T> where T : IComparable
    {
        private readonly Dictionary<T, BinarySearchTreeNodeBase<T>> nodeLookUp;


        public AvlTree(bool enableNodeLookUp = false)
        {
            if (enableNodeLookUp) nodeLookUp = new Dictionary<T, BinarySearchTreeNodeBase<T>>();
        }

        public AvlTree(IEnumerable<T> sortedCollection, bool enableNodeLookUp = false)
        {
            Util.ValidateSortedCollection(sortedCollection);
            var nodes = sortedCollection.Select(x => new AvlTreeNode<T>(null, x)).ToArray();
            Root = (AvlTreeNode<T>)Util.ToBst(nodes);
            RecomputeHeight(Root);
            Util.AssignCount(Root);

            if (enableNodeLookUp) nodeLookUp = nodes.ToDictionary(x => x.Value, x => x as BinarySearchTreeNodeBase<T>);
        }

        internal AvlTreeNode<T> Root { get; set; }

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

            return Find(Root, value) != null;
        }

        internal int GetHeight()
        {
            if (Root == null)
                return -1;

            return Root.Height;
        }

        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new AvlTreeNode<T>(null, value);
                if (nodeLookUp != null) nodeLookUp[value] = Root;

                return;
            }

            Insert(Root, value);
        }

        private void Insert(AvlTreeNode<T> node, T value)
        {
            var compareResult = node.Value.CompareTo(value);

            if (compareResult < 0)
            {
                if (node.Right == null)
                {
                    node.Right = new AvlTreeNode<T>(node, value);
                    if (nodeLookUp != null) nodeLookUp[value] = node.Right;
                }
                else
                {
                    Insert(node.Right, value);
                }
            }
            else if (compareResult > 0)
            {
                if (node.Left == null)
                {
                    node.Left = new AvlTreeNode<T>(node, value);
                    if (nodeLookUp != null) nodeLookUp[value] = node.Left;
                }
                else
                {
                    Insert(node.Left, value);
                }
            }
            else
            {
                throw new InvalidOperationException("Item alrady exists.");
            }

            UpdateHeight(node);
            Balance(node);

            node.UpdateCounts();
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

        public void Delete(T value)
        {
            if (Root == null) throw new NullReferenceException("Root is null.");

            Delete(Root, value);

            if (nodeLookUp != null) nodeLookUp.Remove(value);
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentException("index");

            var nodeToDelete = Root.KthSmallest(index) as AvlTreeNode<T>;
            var nodeToBalance = Delete(nodeToDelete, nodeToDelete.Value);

            while (nodeToBalance != null)
            {
                nodeToBalance.UpdateCounts();
                UpdateHeight(nodeToBalance);
                Balance(nodeToBalance);

                nodeToBalance = nodeToBalance.Parent;
            }

            if (nodeLookUp != null) nodeLookUp.Remove(nodeToDelete.Value);

            return nodeToDelete.Value;
        }

        private AvlTreeNode<T> Delete(AvlTreeNode<T> node, T value)
        {
            var baseCase = false;

            var compareResult = node.Value.CompareTo(value);

            if (compareResult < 0)
            {
                if (node.Right == null) throw new NullReferenceException("node.Right does not exist.");

                Delete(node.Right, value);
            }
            else if (compareResult > 0)
            {
                if (node.Left == null) throw new NullReferenceException("node.Left does not exist.");

                Delete(node.Left, value);
            }
            else
            {
                if (node.IsLeaf)
                {
                    if (node.Parent == null)
                        Root = null;
                    else if (node.Parent.Left == node)
                        node.Parent.Left = null;
                    else
                        node.Parent.Right = null;

                    baseCase = true;
                }
                else
                {
                    if (node.Left != null && node.Right == null)
                    {
                        if (node.Parent == null)
                        {
                            Root.Left.Parent = null;
                            Root = Root.Left;
                        }
                        else
                        {
                            if (node.Parent.Left == node)
                                node.Parent.Left = node.Left;
                            else
                                node.Parent.Right = node.Left;

                            node.Left.Parent = node.Parent;
                        }

                        baseCase = true;
                    }
                    else if (node.Right != null && node.Left == null)
                    {
                        if (node.Parent == null)
                        {
                            Root.Right.Parent = null;
                            Root = Root.Right;
                        }
                        else
                        { 
                            if (node.Parent.Left == node)
                                node.Parent.Left = node.Right;
                            else
                                node.Parent.Right = node.Right;

                            node.Right.Parent = node.Parent;
                        }

                        baseCase = true;
                    }
                    else
                    {
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        if (nodeLookUp != null) nodeLookUp[node.Value] = node;

                        Delete(node.Left, maxLeftNode.Value);
                    }
                }
            }

            if (baseCase)
            {
                node.Parent.UpdateCounts();
                UpdateHeight(node.Parent);
                Balance(node.Parent);
                return node.Parent;
            }

            node.UpdateCounts();
            UpdateHeight(node);
            Balance(node);
            return node;
        }

        public T FindMax()
        {
            return FindMax(Root).Value;
        }

        private AvlTreeNode<T> FindMax(AvlTreeNode<T> node)
        {
            while (true)
            {
                if (node.Right == null) return node;

                node = node.Right;
            }
        }

        public T FindMin()
        {
            return FindMin(Root).Value;
        }

        private AvlTreeNode<T> FindMin(AvlTreeNode<T> node)
        {
            while (true)
            {
                if (node.Left == null) return node;

                node = node.Left;
            }
        }

        public bool Contains(T value)
        {
            if (Root == null) return false;

            return Find(Root, value) != null;
        }

        private AvlTreeNode<T> Find(T value)
        {
            if (nodeLookUp != null) return nodeLookUp[value] as AvlTreeNode<T>;

            return Root.Find(value).Item1 as AvlTreeNode<T>;
        }

        private AvlTreeNode<T> Find(AvlTreeNode<T> parent, T value)
        {
            if (parent == null) return null;

            if (parent.Value.CompareTo(value) == 0) return parent;

            var left = Find(parent.Left, value);

            if (left != null) return left;

            var right = Find(parent.Right, value);

            return right;
        }

        private void Balance(AvlTreeNode<T> node)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
                return;

            var leftHeight = node.Left?.Height + 1 ?? 0;
            var rightHeight = node.Right?.Height + 1 ?? 0;

            var balanceFactor = leftHeight - rightHeight;

            if (balanceFactor >= 2)
            {
                leftHeight = node.Left?.Left?.Height + 1 ?? 0;
                rightHeight = node.Left?.Right?.Height + 1 ?? 0;


                if (leftHeight > rightHeight)
                {
                    RightRotate(node);
                }
                else
                {
                    LeftRotate(node.Left);
                    RightRotate(node);
                }
            }
            else if (balanceFactor <= -2)
            {
                leftHeight = node.Right?.Left?.Height + 1 ?? 0;
                rightHeight = node.Right?.Right?.Height + 1 ?? 0;

                if (rightHeight > leftHeight)
                {
                    LeftRotate(node);
                }
                else
                {
                    RightRotate(node.Right);
                    LeftRotate(node);
                }
            }
        }

        private void RightRotate(AvlTreeNode<T> node)
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

            UpdateHeight(newRoot);

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();

            if (prevRoot == Root) Root = newRoot;
        }

        private void LeftRotate(AvlTreeNode<T> node)
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

            UpdateHeight(newRoot);

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();

            if (prevRoot == Root) Root = newRoot;
        }

        private void UpdateHeight(AvlTreeNode<T> node)
        {
            if (node == null) return;

            if (node.Left != null)
                node.Left.Height = Math.Max(node.Left.Left?.Height + 1 ?? 0,
                    node.Left.Right?.Height + 1 ?? 0);

            if (node.Right != null)
                node.Right.Height = Math.Max(node.Right.Left?.Height + 1 ?? 0,
                    node.Right.Right?.Height + 1 ?? 0);

            node.Height = Math.Max(node.Left?.Height + 1 ?? 0,
                node.Right?.Height + 1 ?? 0);
        }

        private void RecomputeHeight(AvlTreeNode<T> node)
        {
            if (node == null) return;

            RecomputeHeight(node.Left);
            RecomputeHeight(node.Right);

            UpdateHeight(node);
        }

        public T NextLower(T value)
        {
            var node = Find(value);
            if (node == null) return default;

            var next = node.NextLower();
            return next != null ? next.Value : default;
        }

        public T NextHigher(T value)
        {
            var node = Find(value);
            if (node == null) return default;

            var next = node.NextHigher();
            return next != null ? next.Value : default;
        }

        internal void Swap(T value1, T value2)
        {
            var node1 = Find(value1);
            var node2 = Find(value2);

            if (node1 == null) throw new ArgumentNullException("value1 was not found.");
            if (node2 == null) throw new ArgumentNullException("value2 was not found.");

            var tmp = node1.Value;
            node1.Value = node2.Value;
            node2.Value = tmp;

            if (nodeLookUp != null)
            {
                nodeLookUp[node1.Value] = node1;
                nodeLookUp[node2.Value] = node2;
            }
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