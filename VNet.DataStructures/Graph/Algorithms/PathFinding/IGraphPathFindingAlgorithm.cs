using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IGraphPathFindingAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull
    {
    }
}