namespace VNet.DataStructures.Graph.Algorithms.CycleDetection
{
    public interface IStandardGraphCycleDetectionAlgorithm<TNode, TValue> : IGraphCycleDetectionAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}