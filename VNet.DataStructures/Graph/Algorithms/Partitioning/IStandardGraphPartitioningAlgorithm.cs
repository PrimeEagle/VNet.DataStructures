namespace VNet.DataStructures.Graph.Algorithms.Partitioning
{
    public interface IStandardGraphPartitioningAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                                 where TNode : notnull, INode<TValue>
                                                                                 where TEdge : notnull, IEdge<TNode, TValue>
                                                                                 where TValue : notnull
    {
    }
}