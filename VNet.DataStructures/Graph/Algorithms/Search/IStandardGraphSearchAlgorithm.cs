namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IStandardGraphSearchAlgorithm<TNode, TValue> : IGraphSearchAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}