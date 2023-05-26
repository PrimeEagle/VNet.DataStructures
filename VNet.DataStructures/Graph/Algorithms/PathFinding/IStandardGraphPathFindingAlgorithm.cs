using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Basic;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IStandardGraphPathFindingAlgorithm<TNode, TValue> : IGraphPathFindingAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}