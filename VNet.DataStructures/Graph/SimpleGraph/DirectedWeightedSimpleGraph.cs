// ReSharper disable MemberCanBePrivate.Global

namespace VNet.DataStructures.Graph.SimpleGraph
{
    public class DirectedWeightedSimpleGraph<TNode, TValue> : SimpleGraphBase<TNode, IWeightedSimpleEdge<TValue>, TValue>
                                                        where TNode : notnull, INode<TValue>
                                                        where TValue : notnull
    {
        public override void AddEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(TNode startNode, TNode endNode, double weight)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(new WeightedSimpleEdge<TValue>(startNode, endNode, true, weight));
        }

        public void AddEdge(IWeightedSimpleEdge<TValue> edge)
        {
            if (!edge.Directed) throw new ArgumentException("Edge must be directed.");

            var startNode = (TNode)edge.StartNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(startNode) && e.EndNode.Equals(endNode));
            }
        }
    }
}