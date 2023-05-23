namespace VNet.DataStructures.Graph
{
    public class DirectedUnweightedGraph<TNode, TValue> : NormalGraphBase<TNode, IUnweightedNormalEdge<TValue>, TValue>
                                                          where TNode : notnull, INode<TValue>
                                                          where TValue : notnull
    {
        public override void AddEdge(TNode startNode, TNode endNode)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedNormalEdge<TValue>>());

            AdjacencyList[startNode].Add(new UnweightedNormalEdge<TValue>(startNode, endNode, true));
        }

        public void AddEdge(IUnweightedNormalEdge<TValue> edge)
        {
            if (!edge.Directed) throw new ArgumentException("Edge must be directed.");

            var startNode = (TNode)edge.StartNode;

            if(!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedNormalEdge<TValue>>());

            AdjacencyList[startNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(startNode) && e.EndNode.Equals(endNode));
            }
        }

        public override DirectedUnweightedGraph<TNode, TValue> Clone()
        {
            var result = new DirectedUnweightedGraph<TNode, TValue>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}