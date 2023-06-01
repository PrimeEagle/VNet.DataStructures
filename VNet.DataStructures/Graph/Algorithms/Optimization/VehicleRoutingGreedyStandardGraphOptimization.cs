namespace VNet.DataStructures.Graph.Algorithms.Optimization;

public class VehicleRoutingGreedyStandardGraphOptimization<TNode, TEdge, TValue> : IGraphOptimizationAlgorithm<TNode, TEdge, TValue, List<Path<TNode, TValue>>>
                                                                                   where TNode : notnull, INode<TValue>
                                                                                   where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                   where TValue : notnull, IComparable<TValue>
{
    public List<Path<TNode, TValue>> Solve(IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        throw new NotImplementedException("Use the overload Solve(IGraphVehicleRoutingOptimizationAlgorithmArgs<TNode, TEdge, TValue> args) instead.");
    }

    public List<Path<TNode, TValue>> Solve(IGraphVehicleRoutingOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        if (args.VehicleCount <= 0)
            throw new ArgumentException("Vehicle count must be greater than zero.");

        var visited = new HashSet<TNode> { args.Depot };
        var routes = new List<Path<TNode, TValue>>();

        while (visited.Count < args.Graph.Nodes.Count)
        {
            var route = new List<TNode>();
            var currentNode = args.Depot;

            while (route.Count < args.Graph.Nodes.Count / args.VehicleCount)
            {
                var minEdge = default(TEdge);
                var minDistance = double.MinValue;

                foreach (var edge in args.Graph[currentNode])
                {
                    if (visited.Contains(edge.EndNode)) continue;

                    var weight = 1.0d;
                    if(args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;
                    var distance = weight;

                    if (minEdge != null && distance.CompareTo(minDistance) >= 0) continue;
                    minEdge = edge;
                    minDistance = distance;
                }

                if (minEdge == null)
                    throw new InvalidOperationException("Graph is not connected.");

                currentNode = minEdge.EndNode;
                route.Add(currentNode);
                visited.Add(currentNode);
            }

            routes.Add(new Path<TNode, TValue>(route));
        }

        return routes;
    }
}