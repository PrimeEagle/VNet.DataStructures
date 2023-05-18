namespace VNet.DataStructures.Algorithms.Pathfinding
{
    // Dijkstra's algorithm, also known as single-source shortest paths, solves the problem of finding the shortest path from a point in a graph (the source) to a destination.
    // It is a greedy algorithm and similar to Prim's algorithm. Algorithm starts at the source vertex, s, it grows a tree, T, that ultimately spans all vertices reachable
    // from S. Vertices are added to T in order of distance i.e., first S, then the vertex closest to S, then the next closest, and so on.

    public class DijkstraPathfinding : IPathfindingAlgorithm
    {
        private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public int[] Find(Graph.Graph graph, int source = 0)
        {
            int[] distance = new int[graph.VertexCount];
            bool[] shortestPathTreeSet = new bool[graph.VertexCount];

            for (int i = 0; i < graph.VertexCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < graph.VertexCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, graph.VertexCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < graph.VertexCount; ++v)
                {
                    var edge = graph.Edges.Where(e => e.Source == u && e.Destination == v).FirstOrDefault();

                    if (edge != null && !shortestPathTreeSet[v] && (edge.Weight > 0 ? true : false) && distance[u] != int.MaxValue && distance[u] + edge.Weight < distance[v])
                    {
                        distance[v] = distance[u] + edge.Weight;
                    }
                }
            }

            return distance;
        }
    }
}