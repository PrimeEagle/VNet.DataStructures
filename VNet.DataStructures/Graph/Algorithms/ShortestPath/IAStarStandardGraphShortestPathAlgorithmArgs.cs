namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public interface IAStarStandardGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                                          where TNode : notnull, INode<TValue>
                                                                                          where TEdge : notnull, IEdge<TNode, TValue>
                                                                                          where TValue : notnull, IComparable<TValue>
    {
        public Func<TNode, TNode, double> Heuristic { get; init; }
    }
}