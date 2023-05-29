namespace VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath
{
    public interface IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                             where TNode : notnull, INode<TValue>
                                                                             where TEdge : notnull, IEdge<TNode, TValue>
                                                                             where TValue : notnull, IComparable<TValue>
    {
        public TNode StartNode { get; init; }
        public TNode EndNode { get; init; }
    }
}