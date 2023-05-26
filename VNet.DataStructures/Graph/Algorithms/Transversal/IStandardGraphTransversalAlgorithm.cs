namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IStandardGraphTransversalAlgorithm<TNode, TValue> : IGraphTransversalAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}