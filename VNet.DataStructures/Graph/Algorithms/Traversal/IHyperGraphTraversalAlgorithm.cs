namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphTraversalAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                           where TNode : notnull, INode<TValue>
                                                                           where TEdge : notnull, IEdge<TNode, TValue>
                                                                           where TValue : notnull, IComparable<TValue>
    {
    }
}