namespace VNet.DataStructures.Graph.Algorithms.Partitioning
{
    public interface IGraphPartitioningAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}