using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public class BidirectionalStandardGraphTraversal<TNode, TEdge, TValue> : IStandardGraphSearchAlgorithm<TNode, TEdge, TValue> 
                                                                             where TNode : notnull, INode<TValue>
                                                                             where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                             where TValue : notnull
    {
        private readonly IGraph<TNode, TEdge, TValue> _graph;

        public BidirectionalStandardGraphTraversal(IGraph<TNode, TEdge, TValue> graph)
        {
            _graph = graph;
        }

        public bool Traverse(TNode start, TNode end, Action<TNode>? onStartVisit = null, Action<TNode>? onEndVisit = null)
        {
            var visitedFromStart = new Dictionary<TNode, bool>();
            var visitedFromEnd = new Dictionary<TNode, bool>();

            var queueFromStart = new Queue<TNode>();
            var queueFromEnd = new Queue<TNode>();

            queueFromStart.Enqueue(start);
            visitedFromStart[start] = true;

            queueFromEnd.Enqueue(end);
            visitedFromEnd[end] = true;

            while (queueFromStart.Count > 0 && queueFromEnd.Count > 0)
            {
                // Search from the start
                if (SearchLevel(queueFromStart, visitedFromStart, visitedFromEnd, onStartVisit))
                    return true;

                // Search from the end
                if (SearchLevel(queueFromEnd, visitedFromEnd, visitedFromStart, onEndVisit))
                    return true;
            }

            return false;
        }

        private bool SearchLevel(Queue<TNode> queue, Dictionary<TNode, bool> visitedFromThisSide, Dictionary<TNode, bool> visitedFromThatSide, Action<TNode> onVisit)
        {
            TNode node = queue.Dequeue();

            onVisit?.Invoke(node);

            foreach (var edge in _graph[node])
            {
                var adjacentNode = edge.EndNode;

                if (visitedFromThatSide.ContainsKey(adjacentNode))
                {
                    // We found the connection point between the two BFSs
                    return true;
                }

                if (!visitedFromThisSide.ContainsKey(adjacentNode))
                {
                    visitedFromThisSide[adjacentNode] = true;
                    queue.Enqueue(adjacentNode);
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