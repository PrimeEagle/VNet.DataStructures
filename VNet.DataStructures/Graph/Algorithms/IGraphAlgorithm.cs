using VNet.DataStructures.Algorithms;

namespace VNet.DataStructures.Graph.Algorithms
{
    public interface IGraphAlgorithm<TNode, TEdge, TValue> : IDataStructureAlgorithm
                                                             where TNode : notnull, INode<TValue>
                                                             where TEdge : notnull, IEdge<TNode, TValue>
                                                             where TValue : notnull
    {
    }
}