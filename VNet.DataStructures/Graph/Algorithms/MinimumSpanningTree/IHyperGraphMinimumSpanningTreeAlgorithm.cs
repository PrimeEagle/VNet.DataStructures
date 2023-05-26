namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphMinimumSpanningTreeAlgorithm<TNode, TValue> : IGraphMinimumSpanningTreeAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}