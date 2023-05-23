namespace VNet.DataStructures.Graph
{
    public class UndirectedUnweightedGraph<TNode, TValue> : NormalGraphBase<TNode, IUnweightedNormalEdge<TValue>, TValue>
                                                            where TNode : notnull, INode<TValue>
                                                            where TValue : notnull
    {
        public override void AddEdge(TNode startNode, TNode endNode)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedNormalEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IUnweightedNormalEdge<TValue>>());

            AdjacencyList[startNode].Add(new UnweightedNormalEdge<TValue>(startNode, endNode, false));
            AdjacencyList[endNode].Add(new UnweightedNormalEdge<TValue>(endNode, startNode, false));
        }

        public void AddEdge(IUnweightedNormalEdge<TValue> edge)
        {
            if (edge.Directed) throw new ArgumentException("Edge must be undirected.");

            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode) edge.EndNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedNormalEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IUnweightedNormalEdge<TValue>>());

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

        public override UndirectedUnweightedGraph<TNode, TValue> Clone()
        {
            var result = new UndirectedUnweightedGraph<TNode, TValue>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}