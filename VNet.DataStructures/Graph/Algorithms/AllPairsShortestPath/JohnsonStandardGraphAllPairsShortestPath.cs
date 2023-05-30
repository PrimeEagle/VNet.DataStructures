using VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath;

namespace VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath;
// Johnson's algorithm is useful for sparse graphs (i.e., graphs where the number of edges is much less than the number of nodes squared). It works by
// reweighting the graph using a technique called "potential functions", then applying Dijkstra's algorithm from each vertex. Like Floyd-Warshall, it works
// with negative weights but not negative cycles. Its time complexity is O(n^2 log n + n m), where n is the number of vertices and m is the number of edges.
public class JohnsonStandardGraphAllPairsShortestPath<TNode, TEdge, TValue> : IGraphAllPairsShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                              where TNode : notnull, INode<TValue>
                                                                              where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                              where TValue : notnull, IComparable<TValue>
{
    private readonly Dictionary<TNode, double> _h;
    private readonly Dictionary<TNode, IList<TNode>> _paths;

    public JohnsonStandardGraphAllPairsShortestPath()
    {
        _h = new Dictionary<TNode, double>();
        _paths = new Dictionary<TNode, IList<TNode>>();
    }

    public AllPairsResult<TNode, TValue> FindShortestPath(IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        var bfArgs = new GraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue>(args.Graph, args.StartNode, args.EndNode);
        var bfAlgorithm = new BellmanFordStandardGraphSingleSourceShortestPath<TNode, TEdge, TValue>();
        var h = bfAlgorithm.FindShortestPath(bfArgs).Weights;

        if (h.Count == 0)
        {
            return new AllPairsResult<TNode, TValue>(); // Negative cycle detected
        }

        var reweightedGraph = ReweightEdges(args, h);
        var nodeCount = args.Graph.Nodes.Count;
        var weightMatrix = new double[nodeCount, nodeCount];
        var nextMatrix = new TNode[nodeCount, nodeCount];
        var nodes = args.Graph.Nodes.ToList();

        var dArgs = new GraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue>(reweightedGraph);
        var dAlgorithm = new DijkstraStandardGraphSingleSourceShortestPath<TNode, TEdge, TValue>();

        for (var i = 0; i < nodeCount; i++)
        {
            dArgs.StartNode = nodes[i];
            var shortestPaths = dAlgorithm.FindAllShortestPaths(dArgs);

            for (var j = 0; j < nodeCount; j++)
            {
                var path = shortestPaths[nodes[j]];
                if (path != null)
                {
                    weightMatrix[i, j] = path.TotalWeight;
                    nextMatrix[i, j] = path.Nodes.Count > 1 ? path.Nodes[1] : path.Nodes[0];
                }
                else
                {
                    weightMatrix[i, j] = double.PositiveInfinity;
                    nextMatrix[i, j] = default;
                }
            }
        }

        return new AllPairsResult<TNode, TValue>(weightMatrix, nextMatrix, nodes);
    }


    public IList<TNode> GetShortestPath(TNode startNode, TNode endNode)
    {
        return _paths[startNode];
    }

    private static IGraph<TNode, TEdge, TValue> ReweightEdges(IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args, IReadOnlyDictionary<TNode, double> h)
    {
        var reweightedGraph = args.Graph.Clone(true);
        foreach (var node in reweightedGraph.Nodes)
        {
            foreach (var edge in reweightedGraph[node])
            {
                var weight = 1.0d;
                if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>) edge).Weight;
                var newWeight = weight + h[edge.StartNode] - h[edge.EndNode];
                ((IWeightedStandardEdge<TNode, TValue>)edge).Weight = newWeight;
            }
        }
        return reweightedGraph;
    }

    private static Dictionary<TNode, Path<TNode, TValue>> ReweightPaths(IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args, Dictionary<TNode, Path<TNode, TValue>> paths, IReadOnlyDictionary<TNode, double> h)
    {
        var graph = args.Graph.Clone(true);
        var reweightedPaths = new Dictionary<TNode, Path<TNode, TValue>>();
        foreach (var path in paths)
        {
            var reweightedPath = new Path<TNode, TValue>();
            for (var i = 0; i < path.Value.Nodes.Count - 1; i++)
            {
                var edge = graph[path.Value.Nodes[i]].First(e => e.EndNode.Equals(path.Value.Nodes[i + 1]));

                var weight = 1.0d;
                if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;

                var newWeight = weight + h[edge.EndNode] - h[edge.StartNode];
                ((IWeightedStandardEdge<TNode, TValue>)edge).Weight = newWeight;
                reweightedPath.Nodes.Add(path.Value.Nodes[i]);
            }
            reweightedPath.Nodes.Add(path.Value.Nodes.Last()); // add the last node
            reweightedPaths[path.Key] = reweightedPath;
        }
        return reweightedPaths;
    }
}