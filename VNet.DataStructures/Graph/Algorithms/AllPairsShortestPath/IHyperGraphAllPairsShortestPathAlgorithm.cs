namespace VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath
{
    public interface IHyperGraphAllPairsShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                              where TNode : notnull, INode<TValue>
                                                                              where TEdge : notnull, IEdge<TNode, TValue>
                                                                              where TValue : notnull, IComparable<TValue>
    {
    }
}