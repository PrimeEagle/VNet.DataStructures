namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IGraphMinimumSpanningTreeAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}