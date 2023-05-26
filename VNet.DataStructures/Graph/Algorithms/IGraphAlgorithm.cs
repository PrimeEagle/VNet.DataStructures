namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IGraphAlgorithm<TNode, TValue> where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}