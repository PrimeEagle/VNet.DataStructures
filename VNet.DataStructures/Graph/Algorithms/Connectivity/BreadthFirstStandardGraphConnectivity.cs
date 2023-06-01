using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.Connectivity;

// Breadth first traversal, also known as breadth first search or BFS, is an algorithm for traversing or searching tree or graph data structures.
// It starts at the tree root (or some arbitrary node of a graph, sometimes referred to as a 'search key') and explores the neighbor nodes first,
// before moving to the next level neighbors.
public class BreadthFirstStandardGraphConnectivity<TNode, TEdge, TValue> : IGraphConnectivityAlgorithm<TNode, TEdge, TValue>
                                                                           where TNode : notnull, INode<TValue>
                                                                           where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                           where TValue : notnull, IComparable<TValue>
{
    public bool IsConnected(IGraphConnectivityAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var bfsTraversal = new BreadthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue>();
        var visited = new HashSet<TNode>();

        Action<TNode> onVisitNode = node =>
        {
            visited.Add(node);
        };
        Action<TNode> onVisitedNode = node => { };
        Func<TNode, bool>? shouldStop = node => false;

        var bfsArgs = new GraphTraversalAlgorithmArgs<TNode, TEdge, TValue>(args.Graph)
        {
            ShouldStop = shouldStop,
            OnVisitNode = onVisitNode,
            OnVisitedNode = onVisitedNode,
        };
        bfsTraversal.Traverse(bfsArgs);


        return visited.Count == args.Graph.Count;
    }
}