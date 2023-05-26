namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphConnectivityAlgorithm<TNode, TValue> : IGraphConnectivityAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}