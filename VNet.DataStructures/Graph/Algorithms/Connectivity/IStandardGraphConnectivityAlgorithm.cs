namespace VNet.DataStructures.Graph.Algorithms.Connectivity
{
    public interface IStandardGraphConnectivityAlgorithm<TNode, TValue> : IGraphConnectivityAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}