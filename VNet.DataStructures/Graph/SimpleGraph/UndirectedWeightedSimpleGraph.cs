// ReSharper disable MemberCanBePrivate.Global

namespace VNet.DataStructures.Graph.SimpleGraph
{
    public class UndirectedWeightedSimpleGraph<TNode, TValue> : SimpleGraphBase<TNode, IWeightedSimpleEdge<TValue>, TValue>
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
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IWeightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(new WeightedSimpleEdge<TValue>(startNode, endNode, false, weight));
            AdjacencyList[endNode].Add(new WeightedSimpleEdge<TValue>(endNode, startNode, false, weight));
        }

        public void AddEdge(IWeightedSimpleEdge<TValue> edge)
        {
            if (edge.Directed) throw new ArgumentException("Edge must be undirected.");

            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode)edge.EndNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedSimpleEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IWeightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(edge);
            AdjacencyList[endNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(endNode) && e.EndNode.Equals(startNode));
                edgeList.RemoveAll(e => e.StartNode.Equals(startNode) && e.EndNode.Equals(endNode));
            }
        }
    }
}