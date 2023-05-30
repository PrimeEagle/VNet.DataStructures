namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class GraphTraversalAlgorithmArgs<TNode, TEdge, TValue> : IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue>
                                                                 where TNode : notnull, INode<TValue>
                                                                 where TEdge : notnull, IEdge<TNode, TValue>
                                                                 where TValue : notnull, IComparable<TValue>
{
    public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    public TNode StartNode { get; set; }
    public TNode? EndNode { get; set; }
    public Action<TNode>? OnVisitNode { get; set; }
    public Action<TNode>? OnVisitedNode { get; set; }
    public Func<TNode, bool>? ShouldStop { get; set; }



    public GraphTraversalAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode)
    {
        Graph = graph;
        StartNode = startNode;
        EndNode = endNode;
    }

    public GraphTraversalAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode)
    {
        Graph = graph;
        StartNode = startNode;
    }

    public GraphTraversalAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
    {
        Graph = graph;
        StartNode = graph.Nodes.First();
    }
}