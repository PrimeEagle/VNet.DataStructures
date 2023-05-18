using VNet.DataStructures.Graph;

namespace VNet.DataStructures.Algorithms.Pathfinding
{
    // Bellman–Ford algorithm, also known as Bellman–Ford–Moore algorithm, is an algorithm that computes shortest paths from a single source vertex to all
    // of the other vertices in a weighted digraph. It is slower than Dijkstra's algorithm for the same problem, but more versatile, as it is capable of
    // handling graphs in which some of the edge weights are negative numbers..

    public class BellmanFordPathfinding : IPathfindingAlgorithm
    {
        public int[] Find(Graph.Graph graph, int source = 0)
        {
            int vertices = graph.VertexCount;
            int[] distances = new int[vertices];
            for (int i = 0; i < vertices; ++i)
            {
                distances[i] = int.MaxValue;
            }
            distances[0] = 0;

            for (int i = 1; i < vertices; ++i)
            {
                foreach (Edge edge in graph.Edges)
                {
                    int u = edge.Source;
                    int v = edge.Destination;
                    int weight = edge.Weight;
                    if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight;
                    }
                }
            }

            // Check for negative-weight cycles
            bool hasNegativeWeightCycle = false;
            foreach (Edge edge in graph.Edges)
            {
                int u = edge.Source;
                int v = edge.Destination;
                int weight = edge.Weight;
                if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                {
                    hasNegativeWeightCycle = true;
                    break;
                }
            }

            if (hasNegativeWeightCycle)
            {
                Console.WriteLine("Graph contains a negative-weight cycle");
                return null;
            }

            return distances;
        }
    }
}