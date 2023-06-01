using System.Collections.Concurrent;

namespace VNet.DataStructures.Graph.Algorithms.Traversal;

internal class DepthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue> : IGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                              where TNode : notnull, INode<TValue>
                                                                              where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                              where TValue : notnull, IComparable<TValue>
{
    public void Traverse(IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var visited = new ConcurrentBag<TNode>();
        var parentMap = new ConcurrentDictionary<TNode, TNode>(); // For cycle detection.
        DfsHelper(args.StartNode, visited, parentMap, args);
    }

    private static void DfsHelper(TNode node, ConcurrentBag<TNode> visited, ConcurrentDictionary<TNode, TNode> parentMap, IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        visited.Add(node);
         
        // Call the pre-visit function, if provided.
        args.OnVisitNode?.Invoke(node);

        // If the current node is the endNode, stop the traversal.
        var shouldStop = false;
        if (args.ShouldStop is not null)
        {
            shouldStop = args.ShouldStop(node);
        }
        if ((args.EndNode is not null && node.Equals(args.EndNode)) || shouldStop)
            return;

        foreach (var neighbor in args.Graph[node].Select(edge => edge.EndNode))
        {
            if (!visited.Contains(neighbor))
            {
                parentMap[neighbor] = node; // Mark current node as the parent of its neighbor.
                DfsHelper(neighbor, visited, parentMap, args);
            }
            else if (parentMap[node].Equals(neighbor)) // If the neighbor was visited before and it's not the parent of the current node, then it's a cycle.
            {
                // Handle cycle detection here...
                Console.WriteLine($"Cycle detected: {node} --> {neighbor}");
            }
        }

        // Call the post-visit function, if provided.
        args.OnVisitedNode?.Invoke(node);
    }
}