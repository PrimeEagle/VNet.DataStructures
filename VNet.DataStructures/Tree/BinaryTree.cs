using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        private BinaryTreeNode<T> Root { get; set; }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator<T>(Root);
        }

        public bool HasItem(T value)
        {
            if (Root == null) return false;

            return Find(Root, value) != null;
        }

        public int GetHeight()
        {
            return GetHeight(Root);
        }

        public void Insert(T parent, T child)
        {
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(null, child);
                Count++;
                return;
            }

            var parentNode = Find(parent);

            if (parentNode == null) throw new ArgumentNullException("parent is null.");

            var exists = Find(Root, child) != null;

            if (exists) throw new InvalidOperationException("value already exists.");

            switch (parentNode.Left)
            {
                case null when parentNode.Right == null:
                    parentNode.Left = new BinaryTreeNode<T>(parentNode, child);
                    break;
                case null:
                    parentNode.Left = new BinaryTreeNode<T>(parentNode, child);
                    break;
                default:
                    if (parentNode.Right == null)
                        parentNode.Right = new BinaryTreeNode<T>(parentNode, child);
                    else
                        throw new InvalidOperationException("Cannot delete two child node.");
                    break;
            }

            Count++;
        }

        public void Delete(T value)
        {
            var node = Find(value);

            if (node == null) throw new ArgumentNullException("value is null.");

            switch (node.Left)
            {
                case null when node.Right == null:
                    if (node.Parent == null)
                    {
                        Root = null;
                    }
                    else
                    {
                        if (node.Parent.Left == node)
                            node.Parent.Left = null;
                        else
                            node.Parent.Right = null;
                    }

                    break;
                case null when node.Right != null:
                    node.Right.Parent = node.Parent;

                    if (node.Parent.Left == node)
                        node.Parent.Left = node.Right;
                    else
                        node.Parent.Right = node.Right;

                    break;
                default:
                    if (node.Right == null && node.Left != null)
                    {
                        node.Left.Parent = node.Parent;

                        if (node.Parent.Left == node)
                            node.Parent.Left = node.Left;
                        else
                            node.Parent.Right = node.Left;
                    }
                    else
                    {
                        throw new InvalidOperationException("Cannot delete two child node.");
                    }

                    break;
            }

            Count--;
        }

        public IEnumerable<T> Children(T value)
        {
            var node = Find(value);

            if (node != null) return new[] { node.Left, node.Right }.Where(x => x != null).Select(x => x.Value);

            return null;
        }

        private int GetHeight(BinaryTreeNode<T> node)
        {
            if (node == null) return -1;

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        private BinaryTreeNode<T> Find(T value)
        {
            return Root == null ? null : Find(Root, value);
        }

        private BinaryTreeNode<T> Find(BinaryTreeNode<T> parent, T value)
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
    }
}