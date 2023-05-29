namespace VNet.DataStructures.Graph.Algorithms.ShortestPath;
// A* is primarily designed for finding the shortest path from a starting node to a goal node. It's not just for finding any path, but for finding the most cost-effective (shortest) one based on a given cost function.
// The primary characteristic that sets A* apart from other shortest path algorithms is its use of a heuristic function to estimate the cost from the current node to the goal.
// This heuristic helps guide the algorithm's search towards the goal more efficiently, potentially reducing the number of nodes that have to be explored compared to algorithms like Dijkstra's or Bellman-Ford.

public class AStarStandardGraphShortestPath<TNode, TEdge, TValue> : IAStarStandardGraphShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                    where TValue : notnull, IComparable<TValue>
{
    public Path<TNode> FindShortestPath(IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        throw new NotImplementedException();
    }

    public Path<TNode> FindShortestPath(IAStarStandardGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        if (!args.Graph.IsStandardGraph) throw new ArgumentException("This shortest path algorithm only works for standard graphs.");
        if (!args.Graph.HasNegativeWeights) throw new ArgumentException("This shortest path finding only works for standard graphs with positive weights.");

        var openSet = new HashSet<TNode>();
        var closedSet = new HashSet<TNode>();

        var gScore = new Dictionary<TNode, double>();  // Cost from start along best known path.
        var fScore = new Dictionary<TNode, double>();  // Estimated total cost from start to end through y.

        var cameFrom = new Dictionary<TNode, TNode>();  // For path reconstruction.

        gScore[args.StartNode] = 0;
        fScore[args.StartNode] = args.Heuristic(args.StartNode, args.EndNode);

        openSet.Add(args.StartNode);

        while (openSet.Count > 0)
        {
            // Node in openSet having the lowest fScore[] value.
            var currentNode = openSet.OrderBy(node => fScore[node]).First();

            if (currentNode.Equals(args.EndNode))
            {
                // We have found the shortest path, reconstruct and return it.
                var path = new List<TNode>();
                while (cameFrom.ContainsKey(currentNode))
                {
                    path.Add(currentNode);
                    cameFrom.TryGetValue(currentNode, out currentNode);
                }
                path.Add(args.StartNode);
                path.Reverse();
                return new Path<TNode>(path);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            
            var weight = 1.0d;
            foreach (var neighbor in args.Graph[currentNode].Select(edge => edge.EndNode))
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                var edge = args.Graph[currentNode].Where(e => e.EndNode.Equals(neighbor)).FirstOrDefault();
                if (args.Graph.IsWeighted && edge is not null) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;
                var tentativeGScore = gScore[currentNode] + weight;  // Assumes GetEdgeWeight method is defined.

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                // This path is the best until now. Record it!
                cameFrom[neighbor] = currentNode;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + args.Heuristic(neighbor, args.EndNode);
            }
        }

        return new Path<TNode>();  // No path was found.
    }
}