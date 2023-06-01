using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.Connectivity;

// Depth first traversal, also known as depth first search or DFS, is an algorithm for traversing or searching tree or graph data structures.
// One starts at the root (selecting some arbitrary node as the root in the case of a graph) and explores as far as possible along each branch before backtracking.
public class DepthFirstStandardGraphConnectivity<TNode, TEdge, TValue> : IGraphConnectivityAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
{
    public bool IsConnected(IGraphConnectivityAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var bfsTraversal = new DepthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue>();
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