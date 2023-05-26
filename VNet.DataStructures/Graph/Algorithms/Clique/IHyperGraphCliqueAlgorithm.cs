namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphCliqueAlgorithm<TNode, TValue> : IGraphCliqueAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}