namespace VNet.DataStructures.Tree
{
    internal class SplayTreeNode<T> : BinarySearchTreeNodeBase<T> where T : IComparable
    {
        internal SplayTreeNode(SplayTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        internal new SplayTreeNode<T> Parent
        {
            get => (SplayTreeNode<T>)base.Parent;
            set => base.Parent = value;
        }

        internal new SplayTreeNode<T> Left
        {
            get => (SplayTreeNode<T>)base.Left;
            set => base.Left = value;
        }

        internal new SplayTreeNode<T> Right
        {
            get => (SplayTreeNode<T>)base.Right;
            set => base.Right = value;
        }
    }
}