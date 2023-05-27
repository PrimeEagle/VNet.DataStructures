namespace VNet.DataStructures.Graph.Algorithms.Partitioning
{
    public interface IStandardGraphPartitioningAlgorithm<TNode, TValue> : IGraphPartitioningAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}