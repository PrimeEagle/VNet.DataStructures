namespace VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree
{
    public interface IHyperGraphMinimumSpanningTreeAlgorithm<TNode, TValue> : IGraphMinimumSpanningTreeAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}