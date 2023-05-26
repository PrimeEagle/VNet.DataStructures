namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphColoringAlgorithm<TNode, TValue> : IGraphColoringAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}