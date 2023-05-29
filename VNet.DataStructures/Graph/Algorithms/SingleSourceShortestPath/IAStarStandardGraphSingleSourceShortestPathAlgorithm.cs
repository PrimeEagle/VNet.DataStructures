namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public interface IAStarStandardGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                                      where TNode : notnull, INode<TValue>
                                                                                      where TEdge : notnull, IEdge<TNode, TValue>
                                                                                      where TValue : notnull, IComparable<TValue>
    {
        public Path<TNode> FindShortestPath(IAStarStandardGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}