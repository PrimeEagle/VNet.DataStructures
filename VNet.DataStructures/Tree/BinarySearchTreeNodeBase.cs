namespace VNet.DataStructures.Tree
{
    internal abstract class BinarySearchTreeNodeBase<T> where T : IComparable
    {
        internal int Count { get; set; } = 1;
        internal virtual BinarySearchTreeNodeBase<T> Parent { get; set; }
        internal virtual BinarySearchTreeNodeBase<T> Left { get; set; }
        internal virtual BinarySearchTreeNodeBase<T> Right { get; set; }
        internal T Value { get; set; }

        internal bool IsLeftChild => Parent.Left == this;
        internal bool IsRightChild => Parent.Right == this;
        internal bool IsLeaf => Left == null && Right == null;
    }
}