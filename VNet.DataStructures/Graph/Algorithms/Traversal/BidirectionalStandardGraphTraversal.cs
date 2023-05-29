namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class BidirectionalStandardGraphTraversal<TNode, TEdge, TValue> : IGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
{
    public void Traverse(IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        var visitedFromStart = new Dictionary<TNode, bool>();
        var visitedFromEnd = new Dictionary<TNode, bool>();

        var queueFromStart = new Queue<TNode>();
        var queueFromEnd = new Queue<TNode>();

        queueFromStart.Enqueue(args.StartNode);
        visitedFromStart[args.StartNode] = true;

        queueFromEnd.Enqueue(args.EndNode);
        visitedFromEnd[args.EndNode] = true;
        
        var shouldStop = false;
        if (args.ShouldStop is not null)
        {
            shouldStop = args.ShouldStop(queueFromStart.Peek()) || args.ShouldStop(queueFromEnd.Peek());
        }

        while ((queueFromStart.Count > 0 && queueFromEnd.Count > 0) || !shouldStop)
        {
            // Search from the start
            if (SearchLevel(queueFromStart, visitedFromStart, visitedFromEnd, args))
                return;

            // Search from the end
            if (SearchLevel(queueFromEnd, visitedFromEnd, visitedFromStart, args))
                return;

            if (args.ShouldStop is not null)
            {
                shouldStop = args.ShouldStop(queueFromStart.Peek()) || args.ShouldStop(queueFromEnd.Peek());
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