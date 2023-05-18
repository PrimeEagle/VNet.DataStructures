namespace VNet.DataStructures.Tree
{
    public interface IBinaryTree<T> : IEnumerable<ArithmeticTreeNode<string>>
    {
        ArithmeticTreeNode<T> GetElement(int index);
        ArithmeticTreeNode<T> GetRoot();
        bool IsEmpty();
        int Size();
        int Height();
        int NumberOfLeaves();
        IBinaryTree<T> Clone();
        ArithmeticTreeInOrderTraversal<T> GetInOrderTraversalEnumerator();
    }
}