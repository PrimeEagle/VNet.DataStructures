namespace VNet.DataStructures.Tree
{
    internal class BinarySearchTreeNode<T> : BinarySearchTreeNodeBase<T> where T : IComparable
    {
        internal BinarySearchTreeNode(BinarySearchTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        internal new BinarySearchTreeNode<T> Parent
        {
            get => (BinarySearchTreeNode<T>)base.Parent;
            set => base.Parent = value;
        }

        internal new BinarySearchTreeNode<T> Left
        {
            get => (BinarySearchTreeNode<T>)base.Left;
            set => base.Left = value;
        }

        internal new BinarySearchTreeNode<T> Right
        {
            get => (BinarySearchTreeNode<T>)base.Right;
            set => base.Right = value;
        }
    }
}
