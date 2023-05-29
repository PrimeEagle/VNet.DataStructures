// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph.MultiGraph
{
    public class DirectedWeightedMultiGraph<TNode, TEdge, TValue> : StandardGraphBase<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IWeightedStandardEdge<TNode, TValue>
                                                                    where TValue : notnull, IComparable<TValue>
    {
        public override bool IsStandardGraph => true;
        public override bool IsHyperGraph => false;
        public override bool IsLineGraph => false;
        public override bool IsMultiOrParallelGraph => true;
        public override bool IsDirected => true;
        public override bool IsWeighted => true;


        public void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TEdge>());
        }

        public void AddEdge(TNode startNode, TNode endNode, double weight)
        {
            var edge = (TEdge)(IWeightedStandardEdge<TNode, TValue>)new WeightedStandardEdge<TNode, TValue>(startNode, endNode, true, weight);
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            AddEdge(edge);
        }

        public void AddEdge(TEdge edge)
        {
            var startNode = (TNode)edge.StartNode;

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