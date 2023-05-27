namespace VNet.DataStructures.Graph.Algorithms.Matching
{
    public interface IHyperGraphMatchingAlgorithm<TNode, TValue> : IGraphMatchingAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}