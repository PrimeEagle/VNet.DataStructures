using VNet.DataStructures.Graph.Algorithms.PathFinding;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphPathFindingAlgorithm<TNode, TValue> : IGraphPathFindingAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}