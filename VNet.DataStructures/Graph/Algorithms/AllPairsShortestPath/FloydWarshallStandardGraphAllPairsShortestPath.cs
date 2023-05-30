namespace VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath;
// This is one of the most popular APSP algorithms. It is a dynamic programming algorithm that works for both directed and undirected graphs, and handles positive and
// negative weights (but not negative cycles). It has a time complexity of O(n^3), which makes it practical for small to moderately-sized graphs.
public class FloydWarshallStandardGraphAllPairsShortestPath<TNode, TEdge, TValue> : IGraphAllPairsShortestPathAlgorithm<TNode, TEdge, TValue>
                                                                                    where TNode : notnull, INode<TValue>
                                                                                    where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                    where TValue : notnull, IComparable<TValue>
{
    public AllPairsResult<TNode, TValue> FindShortestPath(IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        if (!args.Graph.IsStandardGraph) throw new ArgumentException("This shortest path algorithm only works for standard graphs.");
        if (!args.Graph.HasNegativeWeights) throw new ArgumentException("This shortest path finding only works for standard graphs with positive weights.");

        var nodes = args.Graph.Nodes;
        var size = args.Graph.Count;

        var distanceMatrix = new double[size, size];
        var nextMatrix = new TNode[size, size];

        // Initialize distance and next matrices
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                if (i == j)
                {
                    distanceMatrix[i, j] = 0;
                }
                else
                {
                    if (args.Graph[nodes[i]].Any(edge => edge.EndNode.Equals(nodes[j])))
                    {
                        var edge = args.Graph[nodes[i]].First(edge => edge.EndNode.Equals(nodes[j]));
                        var weight = 1.0d;
                        if(args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;
                        distanceMatrix[i, j] = weight;
                        nextMatrix[i, j] = nodes[j];
                    }
                    else
                    {
                        distanceMatrix[i, j] = double.PositiveInfinity;
                    }
                }
            }
        }

        // Floyd-Warshall
        for (var k = 0; k < size; k++)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (distanceMatrix[i, k] + distanceMatrix[k, j] < distanceMatrix[i, j])
                    {
                        distanceMatrix[i, j] = distanceMatrix[i, k] + distanceMatrix[k, j];
                        nextMatrix[i, j] = nextMatrix[i, k];
                    }
                }
            }
        }

        // Check for negative cycles
        for (var i = 0; i < size; i++)
        {
            if (distanceMatrix[i, i] < 0)
            {
                // Negative cycle detected
                throw new InvalidOperationException("Graph contains a negative cycle");
            }
        }

        return new AllPairsResult<TNode, TValue>(distanceMatrix, nextMatrix, nodes);
    }
}