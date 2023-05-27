namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                          where TNode : notnull, INode<TValue>
                                                                          where TEdge : notnull, IEdge<TNode, TValue>
                                                                          where TValue : notnull
    {
        
    }
}