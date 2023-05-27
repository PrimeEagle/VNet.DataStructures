namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public interface IHyperGraphSearchAlgorithm<TNode, TValue> : IGraphSearchAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}