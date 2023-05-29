namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public interface IAStarStandardGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                                          where TNode : notnull, INode<TValue>
                                                                                          where TEdge : notnull, IEdge<TNode, TValue>
                                                                                          where TValue : notnull, IComparable<TValue>
    {
        public Func<TNode, TNode, double> Heuristic { get; init; }
    }
}