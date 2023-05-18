namespace VNet.DataStructures.Tree
{
    internal class kdTreeNode<T> where T : IComparable
    {
        internal kdTreeNode(int dimensions, kdTreeNode<T> parent)
        {
            Points = new T[dimensions];
            Parent = parent;
        }

        internal T[] Points { get; set; }

        internal kdTreeNode<T> Left { get; set; }
        internal kdTreeNode<T> Right { get; set; }
        internal bool IsLeaf => Left == null && Right == null;

        internal kdTreeNode<T> Parent { get; set; }
        internal bool IsLeftChild => Parent.Left == this;
    }
}
