namespace VNet.DataStructures.Graph.Algorithms.PathFinding;

// Depth first traversal, also known as depth first search or DFS, is an algorithm for traversing or searching tree or graph data structures.
// One starts at the root (selecting some arbitrary node as the root in the case of a graph) and explores as far as possible along each branch before backtracking.
public class DepthFirst
{
    private int GetAdjacentUnvisitedVertex(Basic.Graph graph, int vertexIndex)
    {
        for (var i = 0; i < graph.VertexCount; ++i)
        {
            var edge = graph.Edges.Where(e => e.Source == vertexIndex && e.Destination == i).FirstOrDefault();
            if (edge != null && edge.Weight == 1 && graph.Vertices[i].Visited == false) return i;
        }

        return -1;
    }

    public int[] Find(Basic.Graph graph, int source = 0)
    {
        var result = new Stack<int>();

        graph.Vertices[0].Visited = true;
        result.Push(0);

        while (result.Count > 0)
        {
            var unvisitedVertex = GetAdjacentUnvisitedVertex(graph, result.Peek());

            if (unvisitedVertex == -1)
            {
                result.Pop();
            }
            else
            {
                graph.Vertices.ElementAt(unvisitedVertex).Visited = true;
                result.Push(unvisitedVertex);
            }
        }

        return result.ToArray();
    }
}