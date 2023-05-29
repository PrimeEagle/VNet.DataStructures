namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                          where TNode : notnull, INode<TValue>
                                                                          where TEdge : notnull, IEdge<TNode, TValue>
                                                                          where TValue : notnull, IComparable<TValue>
    {
        public TNode StartNode { get; set; }
        public TNode EndNode { get; set; }
        public Action<TNode>? OnVisitNode { get; set; }
        public Action<TNode>? OnVisitedNode { get; set; }
        public Func<TNode, bool>? ShouldStop { get; set; }
    }
}