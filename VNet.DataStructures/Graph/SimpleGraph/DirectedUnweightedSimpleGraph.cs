namespace VNet.DataStructures.Graph.SimpleGraph
{
    public class DirectedUnweightedSimpleGraph<TNode, TValue> : GraphBase<TNode, IUnweightedSimpleEdge<TValue>, TValue>
                                                          where TNode : notnull, INode<TValue>
                                                          where TValue : notnull
    {
        public override void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<IUnweightedSimpleEdge<TValue>>());
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
            AddEdge(new UnweightedSimpleEdge<TValue>(startNode, endNode, true));
        }

        public override void AddEdge(IUnweightedSimpleEdge<TValue> edge)
        {
            var startNode = (TNode)edge.StartNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());
            AdjacencyList[startNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            AdjacencyList[startNode].RemoveAll(e => e.EndNode.Equals(endNode));
        }

        public override void RemoveEdge(IUnweightedSimpleEdge<TValue> edge)
        {
            var startNode = (TNode)edge.StartNode;

            AdjacencyList[startNode].Remove(edge);
        }
    }
}