namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public interface IAStarStandardGraphShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                                      where TNode : notnull, INode<TValue>
                                                                                      where TEdge : notnull, IEdge<TNode, TValue>
                                                                                      where TValue : notnull, IComparable<TValue>
    {
        public Path<TNode> FindShortestPath(IAStarStandardGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}