namespace VNet.DataStructures.Graph.Algorithms.Connectivity
{
    public interface IGraphConnectivityAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
    {
        public bool IsConnected(IGraphConnectivityAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}