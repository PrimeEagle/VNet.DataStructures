namespace VNet.DataStructures.Graph.Algorithms.Matching
{
    public interface IStandardGraphMatchingAlgorithm<TNode, TValue> : IGraphMatchingAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}