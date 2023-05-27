namespace VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree
{
    public interface IGraphMinimumSpanningTreeAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                                where TNode : notnull, INode<TValue>
                                                                                where TEdge : notnull, IEdge<TNode, TValue>
                                                                                where TValue : notnull
    {
    }
}