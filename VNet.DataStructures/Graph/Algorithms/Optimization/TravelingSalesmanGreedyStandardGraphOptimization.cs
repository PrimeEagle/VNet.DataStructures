namespace VNet.DataStructures.Graph.Algorithms.Optimization;

public class TravelingSalesmanGreedyStandardGraphOptimization<TNode, TEdge, TValue> : IGraphOptimizationAlgorithm<TNode, TEdge, TValue, Path<TNode, TValue>>
                                                                                       where TNode : notnull, INode<TValue>
                                                                                       where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                       where TValue : notnull, IComparable<TValue>
{
    public Path<TNode, TValue> Solve(IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var visited = new HashSet<TNode>();
        var nodes = args.Graph.Nodes.ToList();
        var currentNode = nodes.First();
        var path = new List<TNode> {currentNode};
        visited.Add(currentNode);

        var minEdge = default(TEdge);
        while (visited.Count < nodes.Count)
        {
            var minDist = double.MinValue;

            foreach (var edge in args.Graph[currentNode])
            {
                if (visited.Contains(edge.EndNode)) continue;

                var weight = 1.0d;
                if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>) edge).Weight;

                if (minEdge != null && weight.CompareTo(minDist) >= 0) continue;
                minEdge = edge;
                minDist = weight;
            }

            if (minEdge == null)
                throw new InvalidOperationException("Graph is not connected.");

            currentNode = minEdge.EndNode;
            path.Add(currentNode);
            visited.Add(currentNode);
        }

        return new Path<TNode, TValue>(path);
    }
}