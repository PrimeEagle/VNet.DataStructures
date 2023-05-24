namespace VNet.DataStructures.Graph.SimpleGraph
{
    public class DirectedUnweightedSimpleGraph<TNode, TValue> : SimpleGraphBase<TNode, IUnweightedSimpleEdge<TValue>, TValue>
                                                          where TNode : notnull, INode<TValue>
                                                          where TValue : notnull
    {
        public override void AddEdge(TNode startNode, TNode endNode)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(new UnweightedSimpleEdge<TValue>(startNode, endNode, true));
        }

        public void AddEdge(IUnweightedSimpleEdge<TValue> edge)
        {
            if (!edge.Directed) throw new ArgumentException("Edge must be directed.");

            var startNode = (TNode)edge.StartNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());

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