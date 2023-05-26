namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IStandardGraphMaximumFlowAlgorithm<TNode, TValue> : IGraphMaximumFlowAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}