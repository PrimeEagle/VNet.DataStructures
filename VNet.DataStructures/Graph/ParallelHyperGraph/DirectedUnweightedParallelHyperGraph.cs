namespace VNet.DataStructures.Graph.ParallelHyperGraph
{
    public class DirectedUnweightedParallelHyperGraph<TNode, TEdge, TValue> : HyperGraphBase<TNode, TEdge, TValue>
                                                                              where TNode : notnull, INode<TValue>
                                                                              where TEdge : notnull, IUnweightedHyperEdge<TNode, TValue>
                                                                              where TValue : notnull
    {
        public void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TEdge>());
        }

        public void AddEdge(HashSet<TNode> startNode, HashSet<TNode> endNode)
        {
            var edge = (TEdge)(IUnweightedHyperEdge<TNode, TValue>)new UnweightedHyperEdge<TNode, TValue>(startNode, endNode, false);
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            AddEdge(edge);
        }

        public void AddEdge(TEdge edge)
        {
            var startNodes = edge.StartNodes;

            foreach (var sn in startNodes)
            {
                if (!AdjacencyList.ContainsKey(sn)) AdjacencyList.Add(sn, new List<TEdge>());

                AdjacencyList[sn].Add(edge);
            }
        }

        public void RemoveNodeFromEdges(TNode node)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                foreach (var edge in edgeList)
                {
                    edge.StartNodes.Remove(node);
                    edge.EndNodes.Remove(node);
                }
            }
        }

        public void RemoveEdge(TEdge edge)
        {
            foreach (var node in edge.StartNodes)
            {
                if (AdjacencyList.TryGetValue(node, out var edgeList))
                {
                    edgeList.Remove(edge);
                }
            }
        }

        public void RemoveEdgesByNode(TNode node)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(edge => edge.StartNodes.Contains(node) || edge.EndNodes.Contains(node));
            }
        }
    }
}