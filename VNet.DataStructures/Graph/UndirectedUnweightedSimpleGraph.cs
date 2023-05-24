namespace VNet.DataStructures.Graph
{
    public class UndirectedUnweightedSimpleGraph<TNode, TValue> : SimpleGraphBase<TNode, IUnweightedSimpleEdge<TValue>, TValue>
                                                            where TNode : notnull, INode<TValue>
                                                            where TValue : notnull
    {
        public override void AddEdge(TNode startNode, TNode endNode)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IUnweightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(new UnweightedSimpleEdge<TValue>(startNode, endNode, false));
            AdjacencyList[endNode].Add(new UnweightedSimpleEdge<TValue>(endNode, startNode, false));
        }

        public void AddEdge(IUnweightedSimpleEdge<TValue> edge)
        {
            if (edge.Directed) throw new ArgumentException("Edge must be undirected.");

            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode) edge.EndNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IUnweightedSimpleEdge<TValue>>());

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

        public override UndirectedUnweightedSimpleGraph<TNode, TValue> Clone()
        {
            var result = new UndirectedUnweightedSimpleGraph<TNode, TValue>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}