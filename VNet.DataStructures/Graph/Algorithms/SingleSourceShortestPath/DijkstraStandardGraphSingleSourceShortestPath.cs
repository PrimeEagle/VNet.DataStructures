namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath;
// Dijkstra's algorithm, also known as single-source shortest paths, solves the problem of finding the shortest path from a point in a graph (the source) to a destination.
// It is a greedy algorithm and similar to Prim's algorithm. Algorithm starts at the source vertex, s, it grows a tree, T, that ultimately spans all vertices reachable
// from S. Vertices are added to T in order of distance i.e., first S, then the vertex closest to S, then the next closest, and so on.

public class DijkstraStandardGraphSingleSourceShortestPath<TNode, TEdge, TValue> : IGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                       where TValue : notnull, IComparable<TValue>
{
    public Path<TNode> FindShortestPath(IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        if (!args.Graph.IsStandardGraph) throw new ArgumentException("This shortest path algorithm only works for standard graphs.");
        if (!args.Graph.HasNegativeWeights) throw new ArgumentException("This shortest path finding only works for standard graphs with positive weights.");

        var predecessors = new Dictionary<TNode, TNode>();
        var distances = new Dictionary<TNode, double>();
        var priorityQueue = new SortedDictionary<double, List<TNode>>();

        foreach (var node in args.Graph.Nodes) distances[node] = double.PositiveInfinity;
        distances[args.StartNode] = 0;

        priorityQueue[0] = new List<TNode> { args.StartNode };

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, nodesAtCurrentDistance) = priorityQueue.First();
            var currentNode = nodesAtCurrentDistance.First();
            nodesAtCurrentDistance.Remove(currentNode);
            if (nodesAtCurrentDistance.Count == 0) priorityQueue.Remove(currentDistance);

            var weight = 1.0d;
            foreach (var edge in args.Graph[currentNode])
            {
                if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;

                var neighbor = edge.EndNode;
                var newDistance = currentDistance + weight;
                if (!(newDistance < distances[neighbor])) continue;
                distances[neighbor] = newDistance;
                predecessors[neighbor] = currentNode;

                if (!priorityQueue.TryGetValue(newDistance, out var nodesAtNewDistance))
                {
                    nodesAtNewDistance = new List<TNode>();
                    priorityQueue[newDistance] = nodesAtNewDistance;
                }

                nodesAtNewDistance.Add(neighbor);
            }

            if (currentNode.Equals(args.EndNode)) break;
        }

        if (!predecessors.ContainsKey(args.EndNode)) return new Path<TNode>();

        var path = new List<TNode> { args.EndNode };
        while (!path.Last().Equals(args.StartNode)) path.Add(predecessors[path.Last()]);
        path.Reverse(); // Reverse the path to get it from start to end.
        return new Path<TNode>(path);
    }
}