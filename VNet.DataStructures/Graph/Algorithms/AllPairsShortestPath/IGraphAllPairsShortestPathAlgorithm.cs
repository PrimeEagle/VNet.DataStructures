namespace VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath
{
    public interface IGraphAllPairsShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
    {
        public Path<TNode> FindShortestPath(IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}