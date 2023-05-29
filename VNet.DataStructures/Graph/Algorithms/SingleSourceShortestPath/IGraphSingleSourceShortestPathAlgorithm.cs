namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public interface IGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
    {
        public Path<TNode> FindShortestPath(IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}