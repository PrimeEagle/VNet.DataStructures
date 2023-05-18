
namespace VNet.DataStructures.Algorithms.Pathfinding
{
    // Breadth first traversal, also known as breadth first search or BFS, is an algorithm for traversing or searching tree or graph data structures.
    // It starts at the tree root (or some arbitrary node of a graph, sometimes referred to as a 'search key') and explores the neighbor nodes first,
    // before moving to the next level neighbors.
    public class BreadthFirstPathfinding : IPathfindingAlgorithm
    {
        private static int _rear = -1;
        private static int _front = 0;
        private static int _queueItemCount = 0;
        private static int _vertexCount = 0;

        private static void Insert(List<int> queue, int data)
        {
            queue[++_rear] = data;
            _queueItemCount++;
        }

        private static int RemoveData(List<int> queue)
        {
            _queueItemCount--;
            return queue[_front++];
        }

        private static bool IsQueueEmpty()
        {
            return _queueItemCount == 0;
        }

        private int GetAdjacentUnvisitedVertex(Graph.Graph graph, int vertexIndex)
        {
            int i;

            for (i = 0; i < graph.VertexCount; i++)
            {
                var edge = graph.Edges.Where(e => e.Source == vertexIndex && e.Destination == i).FirstOrDefault();
                if (edge != null && edge.Weight == 1 && graph.Vertices.ElementAt(i).Visited == false)
                {
                    return i;
                }
            }

            return -1;
        }

        public int[] Find(Graph.Graph graph, int source = 0)
        {
            int i;

            var result = new List<int>();
            for (int r = 0; r < graph.VertexCount; r++)
            {
                result.Add(0);
            }

            graph.Vertices[0].Visited = true;
            Insert(result, 0);

            int unvisitedVertex;

            while (!IsQueueEmpty())
            {
                int tempVertex = RemoveData(result);

                while ((unvisitedVertex = GetAdjacentUnvisitedVertex(graph, tempVertex)) != -1)
                {
                    graph.Vertices[unvisitedVertex].Visited = true;
                    Insert(result, unvisitedVertex);
                }
            }

            for (i = 0; i < graph.VertexCount; i++)
            {
                graph.Vertices[i].Visited = false;
            }

            return result.ToArray();
        }
    }
}