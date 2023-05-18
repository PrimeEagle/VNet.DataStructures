namespace VNet.DataStructures.Tree
{
    internal class BinaryTreeNode<T> : IComparable where T : IComparable
    {
        internal BinaryTreeNode(BinaryTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        internal T Value { get; set; }

        internal BinaryTreeNode<T> Parent { get; set; }

        internal BinaryTreeNode<T> Left { get; set; }
        internal BinaryTreeNode<T> Right { get; set; }

        internal bool IsLeaf => Left == null && Right == null;

        public int CompareTo(object obj)
        {
            return Value.CompareTo(obj as BinaryTreeNode<T>);
        }
    }
}
