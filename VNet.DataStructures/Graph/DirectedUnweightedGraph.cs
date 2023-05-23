namespace VNet.DataStructures.Graph
{
    public class DirectedUnweightedGraph<TNode> : GraphBase<TNode, IUnweightedNormalEdge> where TNode : notnull, INode
    {
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
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedNormalEdge>());

            AdjacencyList[startNode].Add(new UnweightedNormalEdge(startNode, endNode, true));
        }

        public void AddEdge(IUnweightedNormalEdge edge)
        {
            if (!edge.Directed) throw new ArgumentException("Edge must be directed.");

            var startNode = (TNode)edge.StartNode;

            if(!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedNormalEdge>());

            AdjacencyList[startNode].Add(edge);
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            if(AdjacencyList.TryGetValue(startNode, out var value)) value.Clear();

            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(endNode) && e.EndNode.Equals(startNode));
            }
        }

        public void RemoveEdge(IUnweightedNormalEdge edge)
        {
            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode)edge.EndNode;

            if (AdjacencyList.TryGetValue(startNode, out var value)) value.RemoveAll(e => e.EndNode.Equals(endNode));
        }

        public override DirectedUnweightedGraph<TNode> Clone()
        {
            var result = new DirectedUnweightedGraph<TNode>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}