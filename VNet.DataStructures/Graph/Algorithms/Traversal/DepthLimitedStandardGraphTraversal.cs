namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class DepthLimitedStandardGraphTraversal<TNode, TEdge, TValue> : IDepthLimitedGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
{
    public void Traverse(IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        throw new NotImplementedException();
    }

    public void Traverse(IDepthLimitedGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        var visited = new Dictionary<TNode, bool>();
        Dls(visited, args);
    }

    private static bool Dls(IDictionary<TNode, bool> visited, IDepthLimitedGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        if (args.MaxDepth < 0)
            return false;

        visited[args.StartNode] = true;
        args.OnVisitNode?.Invoke(args.StartNode);

        var shouldStop = false;
        if (args.ShouldStop is not null)
        {
            shouldStop = args.ShouldStop(args.StartNode);
        }

        if (args.StartNode.Equals(args.EndNode) || shouldStop)
            return true;

        if (args.MaxDepth <= 0) return false;
        foreach (var adjacentNode in args.Graph[args.StartNode].Select(edge => edge.EndNode).Where(adjacentNode => !visited.ContainsKey(adjacentNode)))
        {
            args.OnVisitedNode?.Invoke(args.StartNode);

            args.StartNode = adjacentNode;
            args.MaxDepth -= 1;
            if (Dls(visited, args))
                return true;
        }

        return false;
    }
}