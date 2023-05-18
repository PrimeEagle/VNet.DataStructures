using VNet.DataStructures.LinkedList;

namespace VNet.DataStructures.Tree
{
    internal class TreeNode<T> : IComparable where T : IComparable
    {
        internal TreeNode(TreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;

            Children = new SingleLinkedList<TreeNode<T>>();
        }

        internal T Value { get; set; }

        internal TreeNode<T> Parent { get; set; }
        internal SingleLinkedList<TreeNode<T>> Children { get; set; }

        internal bool IsLeaf => Children.Count() == 0;

        public int CompareTo(object obj)
        {
            return Value.CompareTo(obj as TreeNode<T>);
        }
    }
}
