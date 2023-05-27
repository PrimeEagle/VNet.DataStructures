namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IStandardGraphPathFindingAlgorithm<TNode, TValue> : IGraphPathFindingAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}