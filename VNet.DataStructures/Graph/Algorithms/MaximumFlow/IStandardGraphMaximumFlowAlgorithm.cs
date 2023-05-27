namespace VNet.DataStructures.Graph.Algorithms.MaximumFlow
{
    public interface IStandardGraphMaximumFlowAlgorithm<TNode, TValue> : IGraphMaximumFlowAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}