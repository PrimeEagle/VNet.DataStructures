// ReSharper disable MemberCanBePrivate.Global

namespace VNet.DataStructures.Graph.SimpleGraph
{
    public class DirectedWeightedSimpleGraph<TNode, TValue> : GraphBase<TNode, IWeightedSimpleEdge<TValue>, TValue>
                                                        where TNode : notnull, INode<TValue>
                                                        where TValue : notnull
    {
        public override void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<IWeightedSimpleEdge<TValue>>());
        }

        public override void RemoveNode(TNode node)
        {
            AdjacencyList.Remove(node);

            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(node) || e.EndNode.Equals(node));
            }
        }

        public override void AddEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(TNode startNode, TNode endNode, double weight)
        {
            AddEdge(new WeightedSimpleEdge<TValue>(startNode, endNode, true, weight));
        }

        public override void AddEdge(IWeightedSimpleEdge<TValue> edge)
        {
            var startNode = (TNode)edge.StartNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedSimpleEdge<TValue>>());
            AdjacencyList[startNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            AdjacencyList[startNode].RemoveAll(e => e.EndNode.Equals(endNode));
        }

        public override void RemoveEdge(IWeightedSimpleEdge<TValue> edge)
        {
            var startNode = (TNode)edge.StartNode;

            AdjacencyList[startNode].Remove(edge);
        }
    }
}