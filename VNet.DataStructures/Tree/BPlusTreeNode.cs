namespace VNet.DataStructures.Tree
{
    internal class BPlusTreeNode<T> : BNodeBase<T> where T : IComparable
    {
        internal BPlusTreeNode(int maxKeysPerNode, BPlusTreeNode<T> parent)
            : base(maxKeysPerNode)
        {
            Parent = parent;
            Children = new BPlusTreeNode<T>[maxKeysPerNode + 1];
        }

        internal BPlusTreeNode<T> Parent { get; set; }
        internal BPlusTreeNode<T>[] Children { get; set; }
        internal bool IsLeaf => Children[0] == null;
        public BPlusTreeNode<T> Prev { get; set; }
        public BPlusTreeNode<T> Next { get; set; }


        internal override BNodeBase<T> GetParent()
        {
            return Parent;
        }

        internal override BNodeBase<T>[] GetChildren()
        {
            return Children;
        }
    }
}
