// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph.MultiGraph
{
    public class UndirectedUnweightedMultiGraph<TNode, TEdge, TValue> : StandardGraphBase<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IUnweightedStandardEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
    {
        public void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TEdge>());
        }

        public void AddEdge(TNode startNode, TNode endNode)
        {
            var edge = (TEdge)(IUnweightedStandardEdge<TNode, TValue>)new UnweightedStandardEdge<TNode, TValue>(startNode, endNode, false);
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            AddEdge(edge);
        }

        public void AddEdge(TEdge edge)
        {
            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode)edge.EndNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<TEdge>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<TEdge>());
            AdjacencyList[startNode].Add(edge);
            AdjacencyList[endNode].Add((TEdge)edge.Reverse());
        }

        public void RemoveNode(TNode node)
        {
            AdjacencyList.Remove(node);

            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(node) || e.EndNode.Equals(node));
            }
        }

        public void RemoveEdge(TNode startNode, TNode endNode)
        {
            AdjacencyList[startNode].RemoveAll(e => e.EndNode.Equals(endNode));
            AdjacencyList[endNode].RemoveAll(e => e.EndNode.Equals(startNode));
        }

        public void RemoveEdge(TEdge edge)
        {
            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode)edge.EndNode;

            AdjacencyList[startNode].Remove(edge);
            AdjacencyList[endNode].Remove((TEdge)edge.Reverse());
        }
    }
}