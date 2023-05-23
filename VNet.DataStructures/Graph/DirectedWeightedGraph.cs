// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph
{
    public class DirectedWeightedGraph<TNode, TValue> : NormalGraphBase<TNode, IWeightedNormalEdge<TValue>, TValue>
                                                        where TNode : notnull, INode<TValue>
                                                        where TValue : notnull
    {
        public override void AddEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(TNode startNode, TNode endNode, double weight)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedNormalEdge<TValue>>());

            AdjacencyList[startNode].Add(new WeightedNormalEdge<TValue>(startNode, endNode, true, weight));
        }

        public void AddEdge(IWeightedNormalEdge<TValue> edge)
        {
            if (!edge.Directed) throw new ArgumentException("Edge must be directed.");

            var startNode = (TNode)edge.StartNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedNormalEdge<TValue>>());

            AdjacencyList[startNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(startNode) && e.EndNode.Equals(endNode));
            }
        }

        public override DirectedWeightedGraph<TNode, TValue> Clone()
        {
            var result = new DirectedWeightedGraph<TNode, TValue>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}