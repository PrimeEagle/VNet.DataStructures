namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class BidirectionalStandardGraphTraversal<TNode, TEdge, TValue> : IGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
{
    public void Traverse(IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var visitedFromStart = new Dictionary<TNode, bool>();
        var visitedFromEnd = new Dictionary<TNode, bool>();

        var queueFromStart = new Queue<TNode>();
        var queueFromEnd = new Queue<TNode>();

        queueFromStart.Enqueue(args.StartNode);
        visitedFromStart[args.StartNode] = true;

        queueFromEnd.Enqueue(args.EndNode);
        visitedFromEnd[args.EndNode] = true;
        
        var shouldStop = false;
        while ((queueFromStart.Count > 0 && queueFromEnd.Count > 0) || !shouldStop)
        {
            // Search from the start
            var currentNode = queueFromStart.Peek();
            if (SearchLevel(queueFromStart, visitedFromStart, visitedFromEnd, args))
                return;

            if (args.EndNode is not null) shouldStop = currentNode.Equals(args.EndNode);
            if (args.ShouldStop is not null)
            {
                shouldStop &= args.ShouldStop(queueFromStart.Peek());
            }

            if (shouldStop) continue;


            // Search from the end
            currentNode = queueFromEnd.Peek();
            if (SearchLevel(queueFromEnd, visitedFromEnd, visitedFromStart, args))
                return;

            if (args.EndNode is not null) shouldStop = currentNode.Equals(args.EndNode);
            if (args.ShouldStop is not null)
            {
                shouldStop &= args.ShouldStop(queueFromEnd.Peek());
            }
        }

        return;
    }

    private static bool SearchLevel(Queue<TNode> queue, IDictionary<TNode, bool> visitedFromThisSide, IReadOnlyDictionary<TNode, bool> visitedFromThatSide, IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        var node = queue.Dequeue();

        args.OnVisitNode?.Invoke(node);

        foreach (var adjacentNode in args.Graph[node].Select(edge => edge.EndNode))
        {
            if (visitedFromThatSide.ContainsKey(adjacentNode))
                // We found the connection point between the two BFSs
                return true;

            if (visitedFromThisSide.ContainsKey(adjacentNode)) continue;
            visitedFromThisSide[adjacentNode] = true;
            queue.Enqueue(adjacentNode);
        }

        args.OnVisitedNode?.Invoke(node);

        return false;
    }
}