// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph.SimpleGraph
{
    public class DirectedWeightedSimpleGraph<TNode, TEdge, TValue> : SimpleGraphBase<TNode, TEdge, TValue>
                                                                     where TNode : notnull, INode<TValue>
                                                                     where TEdge : notnull, IWeightedSimpleEdge<TNode, TValue>
                                                                     where TValue : notnull
    {
        public void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TEdge>());
        }

        public void AddEdge(TNode startNode, TNode endNode, double weight)
        {
            var edge = (TEdge)(IWeightedSimpleEdge<TNode, TValue>)new WeightedSimpleEdge<TNode, TValue>(startNode, endNode, true, weight);
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            AddEdge(edge);
        }

        public void AddEdge(TEdge edge)
        {
            var startNode = (TNode)edge.StartNode;

            if (AdjacencyList.Values.Any(edgeList => edgeList.Any(e => e.StartNode.Equals(edge.StartNode) && e.EndNode.Equals(edge.EndNode))))
            {
                throw new ArgumentException("Simple graphs can only have one edge between nodes.");
            }

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<TEdge>());
            AdjacencyList[startNode].Add(edge);
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
        }

        public void RemoveEdge(TEdge edge)
        {
            var startNode = (TNode)edge.StartNode;

            AdjacencyList[startNode].Remove(edge);
        }
    }
}