using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.PathFinding;

// Depth first traversal, also known as depth first search or DFS, is an algorithm for traversing or searching tree or graph data structures.
// One starts at the root (selecting some arbitrary node as the root in the case of a graph) and explores as far as possible along each branch before backtracking.
public class DepthFirstStandardGraphPathFinding<TNode, TEdge, TValue> : IGraphPathFindingAlgorithm<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
{
    public Path<TNode, TValue> FindPath(IGraphPathFindingAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true, CannotBeWeighted = true });


        var predecessors = new Dictionary<TNode, TNode>();
        var found = false;

        var traverseArgs = new GraphTraversalAlgorithmArgs<TNode, TEdge, TValue>(args.Graph, args.StartNode, args.EndNode)
        {
            OnVisitNode = node =>
            {
                if (node.Equals(args.EndNode)) found = true;
            },
            OnVisitedNode = node =>
            {
                foreach (var neighbor in args.Graph[node].Select(edge => edge.EndNode)) predecessors.TryAdd(neighbor, node);
            },
            ShouldStop = node => found
        };
        var traverseAlgorithm = new DepthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue>();
        traverseAlgorithm.Traverse(traverseArgs);

        if (!found) return null; // Return null or an empty list if there's no path.

        var path = new List<TNode> {args.EndNode};
        while (!path.Last().Equals(args.StartNode)) path.Add(predecessors[path.Last()]);

        path.Reverse(); // Reverse the path to get it from start to end.
        return new Path<TNode, TValue>(path);
    }
}