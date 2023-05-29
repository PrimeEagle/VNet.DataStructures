namespace VNet.DataStructures.Graph.Algorithms.ShortestPath;
// Bellman–Ford algorithm, also known as Bellman–Ford–Moore algorithm, is an algorithm that computes shortest paths from a single source vertex to all
// of the other vertices in a weighted digraph. It is slower than Dijkstra's algorithm for the same problem, but more versatile, as it is capable of
// handling graphs in which some of the edge weights are negative numbers.

public class BellmanFordStandardGraphShortestPath<TNode, TEdge, TValue> : IGraphShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                          where TNode : notnull, INode<TValue>
                                                                          where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                          where TValue : notnull, IComparable<TValue>
{
    public Path<TNode> FindShortestPath(IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        if (!args.Graph.IsStandardGraph) throw new ArgumentException("This shortest path algorithm only works for standard graphs.");

        var distances = new Dictionary<TNode, double>();
        var predecessors = new Dictionary<TNode, TNode>();

        foreach (var node in args.Graph.Nodes) distances[node] = double.PositiveInfinity;
        distances[args.StartNode] = 0;

        // Relax edges |V| - 1 times.
        for (var i = 0; i < args.Graph.Count - 1; i++)
            foreach (var node in args.Graph.Nodes)
            {
                var weight = 1.0d;
                foreach (var edge in args.Graph[node])
                {
                    if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>) edge).Weight;

                    var neighbor = edge.EndNode;
                    var potentialDistance = distances[node] + weight;

                    if (potentialDistance < distances[neighbor])
                    {
                        distances[neighbor] = potentialDistance;
                        predecessors[neighbor] = node;
                    }
                }
            }

        // Check for negative weight cycles.
        foreach (var node in args.Graph.Nodes)
        {
            var weight = 1.0d;
            foreach (var edge in args.Graph[node])
            {
                if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>) edge).Weight;

                var neighbor = edge.EndNode;
                if (distances[node] + weight < distances[neighbor]) throw new Exception("Graph contains a negative weight cycle");
            }
        }

        // Build the shortest path.
        var path = new List<TNode>();
        var current = args.EndNode;
        while (!Equals(current, default(TNode)))
        {
            path.Insert(0, current);
            predecessors.TryGetValue(current, out current);
        }

        // Check if we found a path.
        if (path.Count == 0 || !Equals(path[0], args.StartNode)) return null; // No path found.

        return new Path<TNode>(path);
    }
}