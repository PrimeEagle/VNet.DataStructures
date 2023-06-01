namespace VNet.DataStructures.Graph.Algorithms.Traversal;

public class UniformCostSearchWeightedStandardGraphTraversal<TNode, TEdge, TValue> : IStandardGraphTraversalAlgorithm<TNode, TEdge, TValue>
                                                                                     where TNode : notnull, INode<TValue>
                                                                                     where TEdge : notnull, IWeightedStandardEdge<TNode, TValue>
                                                                                     where TValue : notnull, IComparable<TValue>
{
    public void Traverse(IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var visited = new HashSet<TNode>();
        var priorityQueue = new PriorityQueue<TNode, double>();

        priorityQueue.Enqueue(args.StartNode, 0);

        while (priorityQueue.Count != 0)
        {
            var currentNode = priorityQueue.Dequeue();

            if (visited.Contains(currentNode))
                continue;

            visited.Add(currentNode);
            args.OnVisitNode?.Invoke(currentNode);

            var shouldStop = false;
            if (args.ShouldStop is not null) shouldStop = args.ShouldStop(currentNode);

            if ((args.EndNode is not null && currentNode.Equals(args.EndNode)) || shouldStop)
                return;

            foreach (var edge in args.Graph[currentNode].Where(edge => !visited.Contains(edge.EndNode)))
            {
                var cost = edge is IWeightedStandardEdge<TNode, TValue> standardEdge
                    ? standardEdge.Weight
                    : 1.0d;
                priorityQueue.Enqueue(edge.EndNode, cost);
            }

            args.OnVisitedNode?.Invoke(currentNode);
        }

        return;
    }
}