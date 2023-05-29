namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IDepthLimitedGraphTraversalAlgorithm<TNode, TEdge, TValue> : IGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                                  where TNode : notnull, INode<TValue>
                                                                                  where TEdge : notnull, IEdge<TNode, TValue>
                                                                                  where TValue : notnull, IComparable<TValue>
    {
        public void Traverse(IDepthLimitedGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}