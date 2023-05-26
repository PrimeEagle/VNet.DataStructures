namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IStandardGraphCycleDetectionAlgorithm<TNode, TValue> : IGraphCycleDetectionAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}