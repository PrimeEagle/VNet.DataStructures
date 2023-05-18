namespace VNet.DataStructures.Tree
{
    internal class AvlTreeNode<T> : BinarySearchTreeNodeBase<T> where T : IComparable
    {
        internal AvlTreeNode(AvlTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            Height = 0;
        }

        internal new AvlTreeNode<T> Parent
        {
            get => (AvlTreeNode<T>)base.Parent;
            set => base.Parent = value;
        }

        internal new AvlTreeNode<T> Left
        {
            get => (AvlTreeNode<T>)base.Left;
            set => base.Left = value;
        }

        internal new AvlTreeNode<T> Right
        {
            get => (AvlTreeNode<T>)base.Right;
            set => base.Right = value;
        }

        internal int Height { get; set; }
    }
}