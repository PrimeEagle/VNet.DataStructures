namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public interface IStandardGraphShortestPathAlgorithm<TNode, TValue> : IGraphShortestPathAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}