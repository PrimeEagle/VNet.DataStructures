namespace VNet.DataStructures.Graph.Algorithms.MaximumFlow
{
    public interface IGraphMaximumFlowAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}