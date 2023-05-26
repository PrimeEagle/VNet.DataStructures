namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IGraphTransversalAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}