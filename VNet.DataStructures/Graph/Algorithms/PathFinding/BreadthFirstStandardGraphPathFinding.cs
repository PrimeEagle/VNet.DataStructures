using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.PathFinding;

// Breadth first traversal, also known as breadth first search or BFS, is an algorithm for traversing or searching tree or graph data structures.
// It starts at the tree root (or some arbitrary node of a graph, sometimes referred to as a 'search key') and explores the neighbor nodes first,
// before moving to the next level neighbors.
public class BreadthFirstStandardGraphPathFinding<TNode, TEdge, TValue> : IGraphPathFindingAlgorithm<TNode, TEdge, TValue>
                                                                          where TNode : notnull, INode<TValue>
                                                                          where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                          where TValue : notnull, IComparable<TValue>
{
    public IEnumerable<TNode> FindPath(IGraphPathFindingAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        var predecessors = new Dictionary<TNode, TNode>();
        var found = false;

        var traverseArgs = new GraphTraversalAlgorithmArgs<TNode, TEdge, TValue>(args.Graph, args.StartNode, args.EndNode)
        {
            OnVisitNode = node =>
            {
                if (node.Equals(args.EndNode))
                {
                    found = true;
                }
            },
            OnVisitedNode = node =>
            {
                foreach (var neighbor in args.Graph[node].Select(edge => edge.EndNode))
                {
                    predecessors.TryAdd(neighbor, node);
                }
            },
            ShouldStop = node => found
        };
        var traverseAlgorithm = new BreadthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue>();
        traverseAlgorithm.Traverse(traverseArgs);

        if (!found)
        {
            return null;  // Return null or an empty list if there's no path.
        }

        var path = new List<TNode> { args.EndNode };
        while (!path.Last().Equals(args.StartNode))
        {
            path.Add(predecessors[path.Last()]);
        }

        path.Reverse();  // Reverse the path to get it from start to end.
        return path;
    }
}