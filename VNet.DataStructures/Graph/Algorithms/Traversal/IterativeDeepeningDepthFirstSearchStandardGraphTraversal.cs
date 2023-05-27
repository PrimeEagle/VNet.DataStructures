using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public class IterativeDeepeningDepthFirstSearchStandardGraphTraversal<TNode, TEdge, TValue> : IStandardGraphSearchAlgorithm<TNode, TEdge, TValue> 
                                                                                                  where TNode : notnull, INode<TValue>
                                                                                                  where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                                                  where TValue : notnull
    {
        IGraph<TNode, TEdge, TValue> _graph;

        public IterativeDeepeningDepthFirstSearchStandardGraphTraversal(IGraph<TNode, TEdge, TValue> graph)
        {
            _graph = graph;
        }

        public bool Traverse(TNode start, TNode end, int maxDepth, Action<TNode>? onVisit = null)
        {
            // DLS for each depth from 0 to maxDepth
            for (int depth = 0; depth <= maxDepth; depth++)
            {
                var visited = new Dictionary<TNode, bool>();

                if (DLS(start, end, depth, visited, onVisit))
                    return true;
            }
            return false;
        }

        private bool DLS(TNode node, TNode end, int depth, Dictionary<TNode, bool> visited, Action<TNode>? onVisit)
        {
            if (depth < 0)
                return false;

            visited[node] = true;
            onVisit?.Invoke(node);

            if (node.Equals(end))
                return true;

            if (depth > 0)
            {
                foreach (var edge in _graph[node])
                {
                    var adjacentNode = edge.EndNode;

                    if (!visited.ContainsKey(adjacentNode))
                    {
                        if (DLS(adjacentNode, end, depth - 1, visited, onVisit))
                            return true;
                    }
                }
            }
            return false;
        }

        public bool Search(TNode node)
        {
            throw new NotImplementedException();
        }

        public TNode? SearchByValue(TValue value)
        {
            throw new NotImplementedException();
        }

        public TNode? SearchByValue(string value, bool hasWildcards)
        {
            throw new NotImplementedException();
        }
    }
}