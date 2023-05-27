namespace VNet.DataStructures.Graph.Algorithms.Matching
{
    public interface IHyperGraphMatchingAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                          where TNode : notnull, INode<TValue>
                                                                          where TEdge : notnull, IEdge<TNode, TValue>
                                                                          where TValue : notnull
    {
    }
}