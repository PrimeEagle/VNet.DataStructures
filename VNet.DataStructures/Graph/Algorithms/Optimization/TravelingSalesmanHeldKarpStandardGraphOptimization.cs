namespace VNet.DataStructures.Graph.Algorithms.Optimization;

public class TravelingSalesmanHeldKarpStandardGraphOptimization<TNode, TEdge, TValue> : IGraphOptimizationAlgorithm<TNode, TEdge, TValue, Path<TNode, TValue>>
                                                                                         where TNode : notnull, INode<TValue>
                                                                                         where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                         where TValue : notnull, IComparable<TValue>
{
    public Path<TNode, TValue> Solve(IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var nodes = args.Graph.Nodes.ToList();
        var size = nodes.Count;
        var memo = new Dictionary<(int, int), (double, int)>();
        var nodeIndices = nodes.Select((node, index) => new {node, index}).ToDictionary(x => x.node, x => x.index);

        for (var end = 0; end < size; end++)
            if (end != 0)
                memo[(1 << end, end)] = (args.Graph.GetEdgeWeight(nodes[0], nodes[end]), 0);

        for (var r = 2; r <= size; r++)
            foreach (var subset in Combinations(size, r))
            {
                if (!subset.Contains(0)) continue; // 0 must always be in the subset
                foreach (var next in subset.Where(e => e != 0))
                {
                    var subsetWithoutNext = new HashSet<int>(subset);
                    subsetWithoutNext.Remove(next);
                    var minDist = double.PositiveInfinity;
                    var minPrev = 0;
                    foreach (var end in subset.Where(e => e != next))
                    {
                        var subsetKey = ValueTuple.Create(subsetWithoutNext.GetHashCode(), end);
                        var dist = memo[subsetKey].Item1 + args.Graph.GetEdgeWeight(nodes[end], nodes[next]);
                        if (!(dist < minDist)) continue;
                        minDist = dist;
                        minPrev = end;
                    }

                    memo[ValueTuple.Create(new HashSet<int>(subset).GetHashCode(), next)] = (minDist, minPrev);
                }
            }

        var minTourCost = double.PositiveInfinity;
        var lastNode = -1;
        foreach (var node in Enumerable.Range(1, size - 1))
        {
            var tourCost = memo[((1 << size) - 1, node)].Item1 + args.Graph.GetEdgeWeight(nodes[node], nodes[0]);
            if (!(tourCost < minTourCost)) continue;
            minTourCost = tourCost;
            lastNode = node;
        }

        // Unroll the path
        var path = new List<TNode>();
        var state = (1 << size) - 1;
        for (var i = 0; i < size; i++)
        {
            path.Insert(0, nodes[lastNode]);
            var newState = state ^ (1 << lastNode);
            lastNode = memo[(state, lastNode)].Item2;
            state = newState;
        }

        path.Insert(0, nodes[0]);

        return new Path<TNode, TValue>(path);
    }

    private static IEnumerable<List<int>> Combinations(int n, int k)
    {
        var combinations = new List<int>();
        var results = new List<List<int>>();
        CombinationsHelper(0, 1, n, k, combinations, results);
        return results;
    }

    private static void CombinationsHelper(int startIndex, int currentValue, int n, int k, List<int> combinations, List<List<int>> results)
    {
        if (k == 0)
        {
            results.Add(new List<int>(combinations));
            return;
        }

        for (var i = startIndex; i <= n - k; i++)
        {
            combinations.Add(currentValue + i);
            CombinationsHelper(i + 1, currentValue + i + 1, n, k - 1, combinations, results);
            combinations.RemoveAt(combinations.Count - 1);
        }
    }
}