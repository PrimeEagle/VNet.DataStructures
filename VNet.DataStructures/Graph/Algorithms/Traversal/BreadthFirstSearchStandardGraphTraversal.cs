namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class BreadthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue> : IGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                              where TNode : notnull, INode<TValue>
                                                                              where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                              where TValue : notnull, IComparable<TValue>
{
    public void Traverse(IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        if (args.StartNode == null)
            throw new ArgumentNullException(nameof(args.StartNode));

        var visited = new HashSet<TNode>();
        var queue = new Queue<TNode>();

        visited.Add(args.StartNode);
        queue.Enqueue(args.StartNode);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();

            // Call the visit function.
            args.OnVisitNode?.Invoke(currentNode);

            var shouldStop = false;
            if (args.ShouldStop is not null)
            {
                shouldStop = args.ShouldStop(currentNode);
            }

            // If the current node is the endNode, stop the traversal.
            if (((args.EndNode is not null && currentNode.Equals(args.EndNode)) || shouldStop))
            {
                args.EndNode = currentNode;
                return;
            }

            foreach (var neighbor in args.Graph[currentNode].Select(edge => edge.EndNode).Where(neighbor => !visited.Contains(neighbor)))
            {
                visited.Add(neighbor);
                queue.Enqueue(neighbor);
            }

            args.OnVisitedNode?.Invoke(currentNode);
        }
    }
}