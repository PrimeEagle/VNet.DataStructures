namespace VNet.DataStructures.Graph.Algorithms.CycleDetection
{
    public interface IGraphCycleDetectionAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}