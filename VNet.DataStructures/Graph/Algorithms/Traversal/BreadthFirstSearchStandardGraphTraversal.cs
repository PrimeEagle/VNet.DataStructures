namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public class BreadthFirstSearchSimpleAlgorithm<TNode, TEdge, TValue> : IStandardGraphSearchAlgorithm<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                                   where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                                                   where TValue : notnull
    {
        IGraph<TNode, TEdge, TValue> _graph;

        public BreadthFirstSearchSimpleAlgorithm(IGraph<TNode, TEdge, TValue> graph)
        {
            _graph = graph;
        }

        public void Traverse(TNode starTNode, Action<TNode> visitAction, Action<TNode> dummyAction)
        {
            if (starTNode == null)
                throw new ArgumentNullException(nameof(starTNode));

            if (visitAction == null)
                throw new ArgumentNullException(nameof(visitAction));

            HashSet<TNode> visited = new HashSet<TNode>();
            Queue<TNode> queue = new Queue<TNode>();

            visited.Add(starTNode);
            queue.Enqueue(starTNode);

            while (queue.Count > 0)
            {
                TNode vertex = queue.Dequeue();

                // Call the visit function.
                visitAction(vertex);

                foreach (TEdge edge in _graph[vertex])
                {
                    TNode neighbor = edge.EndNode;

                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
    }
}