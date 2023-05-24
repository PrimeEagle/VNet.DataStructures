namespace VNet.DataStructures.Graph.MultiGraph
{
    public class UndirectedWeightedMultiGraph<TNode, TValue> : GraphBase<TNode, IWeightedSimpleEdge<TValue>, TValue>
                                                               where TNode : notnull, INode<TValue>
                                                               where TValue : notnull
    {
        public override void AddEdge(IWeightedSimpleEdge<TValue> edge)
        {
            var startNode = (TNode)edge.StartNode;

            if(!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(edge);
        }

        public override void AddEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(TNode startNode, TNode endNode, double weight)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IWeightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(new WeightedSimpleEdge<TValue>(startNode, endNode, false, weight));
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            foreach(var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(startNode) && e.EndNode.Equals(endNode));
                edgeList.RemoveAll(e => e.StartNode.Equals(endNode) && e.EndNode.Equals(startNode));
            }
        }
    }
}