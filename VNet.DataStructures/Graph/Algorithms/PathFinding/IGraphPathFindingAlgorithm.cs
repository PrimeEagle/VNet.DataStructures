using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IGraphPathFindingAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                                      where TValue : notnull
    {
    }
}