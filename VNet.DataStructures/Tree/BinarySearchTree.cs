using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(IEnumerable<T> sortedCollection) : this()
        {
            Util.ValidateSortedCollection(sortedCollection);
            var nodes = sortedCollection.Select(x => new BinarySearchTreeNode<T>(null, x)).ToArray();
            Root = (BinarySearchTreeNode<T>)Util.ToBst(nodes);
            Util.AssignCount(Root);
        }

        internal BinarySearchTreeNode<T> Root { get; set; }

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
            return GetHeight(Root);
        }

        private int GetHeight(BinarySearchTreeNode<T> node)
        {
            if (node == null) return -1;

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }


        internal BinarySearchTreeNode<T> InsertAndReturnNewNode(T value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeNode<T>(null, value);
                return Root;
            }

            var newNode = Insert(Root, value);
            return newNode;
        }

        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeNode<T>(null, value);
                return;
            }

            var newNode = Insert(Root, value);
            newNode.UpdateCounts(true);
        }

        private BinarySearchTreeNode<T> Insert(BinarySearchTreeNode<T> currentNode, T newNodeValue)
        {
            while (true)
            {
                var compareResult = currentNode.Value.CompareTo(newNodeValue);

                if (compareResult < 0)
                {
                    if (currentNode.Right != null)
                    {
                        currentNode = currentNode.Right;
                        continue;
                    }

                    currentNode.Right = new BinarySearchTreeNode<T>(currentNode, newNodeValue);
                    return currentNode.Right;
                }

                if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = new BinarySearchTreeNode<T>(currentNode, newNodeValue);
                        return currentNode.Left;
                    }

                    currentNode = currentNode.Left;
                }
                else
                {
                    throw new Exception("Item exists");
                }
            }
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
            if (Root == null) throw new Exception("Empty BST");

            var deleted = Delete(Root, value);
            deleted.UpdateCounts(true);
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentException("index");

            var nodeToDelete = Root.KthSmallest(index) as BinarySearchTreeNode<T>;

            var deleted = Delete(nodeToDelete, nodeToDelete.Value);
            deleted.UpdateCounts(true);

            return nodeToDelete.Value;
        }

        private BinarySearchTreeNode<T> Delete(BinarySearchTreeNode<T> node, T value)
        {
            while (true)
            {
                if (node != null)
                {
                    var compareResult = node.Value.CompareTo(value);

                    if (compareResult < 0)
                    {
                        node = node.Right ?? throw new Exception("Item do not exist");
                        continue;
                    }

                    if (compareResult > 0)
                    {
                        node = node.Left ?? throw new Exception("Item do not exist");
                        continue;
                    }
                }

                if (node == null) return null;

                if (node.IsLeaf)
                {
                    DeleteLeaf(node);
                    return node;
                }

                if (node.Left != null && node.Right == null)
                {
                    DeleteLeftNode(node);
                    return node;
                }

                if (node.Right != null && node.Left == null)
                {
                    DeleteRightNode(node);
                    return node;
                }

                var maxLeftNode = FindMax(node.Left);

                node.Value = maxLeftNode.Value;

                node = node.Left;
                value = maxLeftNode.Value;
            }
        }

        private void DeleteLeaf(BinarySearchTreeNode<T> node)
        {
            if (node.Parent == null)
                Root = null;
            else if (node.IsLeftChild)
                node.Parent.Left = null;
            else
                node.Parent.Right = null;
        }

        private void DeleteRightNode(BinarySearchTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
            }
            else
            {
                if (node.IsLeftChild)
                    node.Parent.Left = node.Right;
                else
                    node.Parent.Right = node.Right;

                node.Right.Parent = node.Parent;
            }
        }

        private void DeleteLeftNode(BinarySearchTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;
            }
            else
            {
                if (node.IsLeftChild)
                    node.Parent.Left = node.Left;
                else
                    node.Parent.Right = node.Left;

                node.Left.Parent = node.Parent;
            }
        }

        public T FindMax()
        {
            return FindMax(Root).Value;
        }

        private BinarySearchTreeNode<T> FindMax(BinarySearchTreeNode<T> node)
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

        private BinarySearchTreeNode<T> FindMin(BinarySearchTreeNode<T> node)
        {
            while (true)
            {
                if (node.Left == null) return node;
                node = node.Left;
            }
        }

        internal BinarySearchTreeNode<T> FindNode(T value)
        {
            return Find(Root, value);
        }

        private BinarySearchTreeNode<T> Find(BinarySearchTreeNode<T> parent, T value)
        {
            while (true)
            {
                if (parent == null) return null;

                if (parent.Value.CompareTo(value) == 0) return parent;

                var left = Find(parent.Left, value);

                if (left != null) return left;

                parent = parent.Right;
            }
        }

        private BinarySearchTreeNodeBase<T> Find(T value)
        {
            return Root.Find(value).Item1;
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
