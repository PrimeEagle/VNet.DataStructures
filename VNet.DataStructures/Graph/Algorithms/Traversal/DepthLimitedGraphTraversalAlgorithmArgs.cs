namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class DepthLimitedGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> : IDepthLimitedGraphTraversalAlgorithmArgs<TNode, TEdge, TValue>
                                                                             where TNode : notnull, INode<TValue>
                                                                             where TEdge : notnull, IEdge<TNode, TValue>
                                                                             where TValue : notnull, IComparable<TValue>
{
    public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    public TNode StartNode { get; set; }
    public TNode EndNode { get; set; }
    public Action<TNode>? OnVisitNode { get; set; }
    public Action<TNode>? OnVisitedNode { get; set; }
    public Func<TNode, bool>? ShouldStop { get; set; }
    public int MaxDepth { get; set; }



    public DepthLimitedGraphTraversalAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode, int maxDepth)
    {
        Graph = graph;
        StartNode = startNode;
        EndNode = endNode;
        MaxDepth = maxDepth;
    }
}