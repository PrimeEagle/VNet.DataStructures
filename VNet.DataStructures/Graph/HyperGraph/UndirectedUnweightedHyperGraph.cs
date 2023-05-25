// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph.HyperGraph
{
    public class UndirectedUnweightedHyperGraph<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>
                                                                 where TNode : notnull, INode<TValue>
                                                                 where TEdge : notnull, IUnweightedHyperEdge<TNode, TValue>
                                                                 where TValue : notnull
    {
        public void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TEdge>());
        }

        public void AddEdge(List<TNode> startNode, List<TNode> endNode)
        {
            var edge = (TEdge)(IUnweightedHyperEdge<TNode, TValue>)new UnweightedHyperEdge<TNode, TValue>(startNode, endNode, false);
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            AddEdge(edge);
        }

        public void AddEdge(TEdge edge)
        {
            if (AdjacencyList.Values.Any(v => v.Contains(edge)))
            {
                throw new ArgumentException("HyperGraphs can only have one edge between nodes.");
            }

            var startNodes = edge.StartNodes;

            foreach (var sn in startNodes)
            {
                if (!AdjacencyList.ContainsKey(sn)) AdjacencyList.Add(sn, new List<TEdge>());

                AdjacencyList[sn].Add(edge);
            }

            var endNodes = edge.EndNodes;
            var edgeReverse = (TEdge)edge.Reverse();

            foreach (var en in endNodes)
            {
                if (!AdjacencyList.ContainsKey(en)) AdjacencyList.Add(en, new List<TEdge>());

                AdjacencyList[en].Add(edgeReverse);
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