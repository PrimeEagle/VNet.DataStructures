using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public class UniformCostSearchStandardGraphTraversal<TNode, TEdge, TValue> : IStandardGraphSearchAlgorithm<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                                   where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                                                   where TValue : notnull
    {
        IGraph<TNode, TEdge, TValue> _graph;

        public UniformCostSearchStandardGraphTraversal(IGraph<TNode, TEdge, TValue> graph)
        {
            _graph = graph;
        }

        public bool Traverse(TNode startNode, TNode endNode, Action<TNode>? onVisit = null)
        {
            var visited = new HashSet<TNode>();
            var priorityQueue = new PriorityQueue<TNode, double>();  // This is a priority queue where the nodes with the smallest associated value (i.e., the cost) have the highest priority.

            priorityQueue.Enqueue(startNode, 0);

            while (priorityQueue.Count != 0)
            {
                var currentNode = priorityQueue.Dequeue();

                if (visited.Contains(currentNode))
                    continue;

                visited.Add(currentNode);
                onVisit?.Invoke(currentNode);

                if (currentNode.Equals(endNode))
                    return true;

                foreach (var edge in _graph[currentNode])
                {
                    if (!visited.Contains(edge.EndNode))
                    {
                        double cost = (edge is IWeightedStandardEdge<TNode, TValue>) ? ((IWeightedStandardEdge<TNode, TValue>)edge).Weight : 1.0d;
                        priorityQueue.Enqueue(edge.EndNode, cost);
                    }
                }
            }

            return false;
        }
    }
}