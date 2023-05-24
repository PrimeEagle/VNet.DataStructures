namespace VNet.DataStructures.Graph.Algorithms.PathFinding;

// Floyd–Warshall algorithm, also known as Floyd's algorithm, the Roy–Warshall algorithm, the Roy–Floyd algorithm, or the WFI algorithm, is an algorithm for finding shortest paths
// in a weighted graph with positive or negative edge weights (but with no negative cycles). A single execution of the algorithm will find the lengths (summed weights) of the
// shortest paths between all pairs of vertices, though it does not return details of the paths themselves. Versions of the algorithm can also be used for finding the
// transitive closure of a relation R, or (in connection with the Schulze voting system) widest paths between all pairs of vertices in a weighted graph.
public class FloydWarshall
{
    public const int INF = 99999;

    public int[,] FindOld(int[,] graph, int verticesCount)
    {
        var distance = new int[verticesCount, verticesCount];

        for (var i = 0; i < verticesCount; ++i)
        for (var j = 0; j < verticesCount; ++j)
            distance[i, j] = graph[i, j];

        for (var k = 0; k < verticesCount; ++k)
        for (var i = 0; i < verticesCount; ++i)
        for (var j = 0; j < verticesCount; ++j)
            if (distance[i, k] + distance[k, j] < distance[i, j])
                distance[i, j] = distance[i, k] + distance[k, j];

        return distance;
    }

    public int[,] Find(Basic.Graph graph, int source = 0)
    {
        var distance = new int[graph.VertexCount, graph.VertexCount];

        for (var i = 0; i < graph.VertexCount; ++i)
        for (var j = 0; j < graph.VertexCount; ++j)
        {
            var edge = graph.Edges.Where(e => e.Source == i && e.Destination == j).FirstOrDefault();
            distance[i, j] = edge == null ? 0 : edge.Weight;
        }

        for (var k = 0; k < graph.VertexCount; ++k)
        for (var i = 0; i < graph.VertexCount; ++i)
        for (var j = 0; j < graph.VertexCount; ++j)
            if (distance[i, k] + distance[k, j] < distance[i, j])
                distance[i, j] = distance[i, k] + distance[k, j];

        return distance;
    }
}