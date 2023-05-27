namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public interface IGraphShortestPathAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}