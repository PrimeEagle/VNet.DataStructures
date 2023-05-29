namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public interface IHyperGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                              where TNode : notnull, INode<TValue>
                                                                              where TEdge : notnull, IEdge<TNode, TValue>
                                                                              where TValue : notnull, IComparable<TValue>
    {
    }
}