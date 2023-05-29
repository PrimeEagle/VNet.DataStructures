namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IDepthLimitedGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> : IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue>
                                                                                      where TNode : notnull, INode<TValue>
                                                                                      where TEdge : notnull, IEdge<TNode, TValue>
                                                                                      where TValue : notnull, IComparable<TValue>
    {
        public int MaxDepth { get; set; }
    }
}