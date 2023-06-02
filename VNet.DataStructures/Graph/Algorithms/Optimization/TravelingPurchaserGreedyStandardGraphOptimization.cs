using System.Numerics;
using VNet.System.Conversion;

namespace VNet.DataStructures.Graph.Algorithms.Optimization;

public class TravelingPurchaserGreedyStandardGraphOptimization<TNode, TEdge, TValue> : IGraphOptimizationAlgorithm<TNode, TEdge, TValue, Path<TNode, TValue>>
                                                                                        where TNode : notnull, INode<TValue>
                                                                                        where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                        where TValue : notnull, IComparable<TValue>, INumber<TValue>
{
    public Path<TNode, TValue> Solve(IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        throw new NotImplementedException("Use the overload Solve(IGraphTravelingPurchaserOptimizationAlgorithmArgs<TNode, TEdge, TValue> args) instead.");
    }

    public Path<TNode, TValue> Solve(IGraphTravelingPurchaserOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        if (args.PurchaseCosts == null || args.PurchaseCosts.Count != args.Graph.Nodes.Count)
            throw new ArgumentException("Purchase costs dictionary must have a value for each args.Graph node.");

        var visited = new HashSet<TNode>();
        var nodes = args.Graph.Nodes.ToList();
        var currentNode = nodes.First();
        var path = new List<TNode> {currentNode};
        visited.Add(currentNode);

        while (visited.Count < nodes.Count)
        {
            var minEdge = default(TEdge);
            var minTotalCost = double.MinValue;

            foreach (var edge in args.Graph[currentNode].Where(edge => !visited.Contains(edge.EndNode)))
            {
                var weight = 1.0d;
                if(args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;

                var totalCost = GenericNumber<TValue>.ToDouble(args.PurchaseCosts[currentNode]) + weight;

                if (minEdge != null && totalCost.CompareTo(minTotalCost) >= 0) continue;
                minEdge = edge;
                minTotalCost = totalCost;
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