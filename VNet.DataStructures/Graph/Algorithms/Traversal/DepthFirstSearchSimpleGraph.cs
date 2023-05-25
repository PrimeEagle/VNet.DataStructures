using System.Collections.Concurrent;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    internal class DepthFirstSearchSimpleGraph<TNode, TEdge, TValue> : IGraphTraversalAlgorithm<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                               where TEdge : notnull, ISimpleEdge<TNode, TValue>
                                                                                                               where TValue : notnull
    {
        IGraph<TNode, TEdge, TValue> _graph;

        public DepthFirstSearchSimpleGraph(IGraph<TNode, TEdge, TValue> graph)
        {
            _graph = graph;
        }

        public void Traverse(TNode starTNode, Action<TNode> preVisit, Action<TNode> postVisit)
        {
            ConcurrentBag<TNode> visited = new ConcurrentBag<TNode>();
            ConcurrentDictionary<TNode, TNode> parentMap = new ConcurrentDictionary<TNode, TNode>(); // For cycle detection.
            DFSHelper(starTNode, visited, parentMap, preVisit, postVisit);
        }

        private void DFSHelper(TNode node, ConcurrentBag<TNode> visited, ConcurrentDictionary<TNode, TNode> parentMap, Action<TNode> preVisit, Action<TNode> postVisit)
        {
            visited.Add(node);

            // Call the pre-visit function, if provided.
            preVisit?.Invoke(node);
            foreach (TEdge edge in _graph[node])
            {
                TNode neighbor = edge.EndNode;

                if (!visited.Contains(neighbor))
                {
                    parentMap[neighbor] = node; // Mark current node as the parent of its neighbor.
                    DFSHelper(neighbor, visited, parentMap, preVisit, postVisit);
                }
                else if (parentMap[node].Equals(neighbor)) // If the neighbor was visited before and it's not the parent of the current node, then it's a cycle.
                {
                    // Handle cycle detection here...
                    // You could throw an exception, print a message, call a provided callback function, etc.
                    Console.WriteLine($"Cycle detected: {node} --> {neighbor}");
                }
            }

            // Call the post-visit function, if provided.
            postVisit?.Invoke(node);
        }
    }
}