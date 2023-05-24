using VNet.DataStructures.Graph.Basic;

namespace VNet.DataStructures.Graph.MultiGraph
{
    public class UndirectedUnweightedMultiGraph<TNode, TValue> : GraphBase<TNode, IUnweightedSimpleEdge<TValue>, TValue>
                                                                 where TNode : notnull, INode<TValue>
                                                                 where TValue : notnull
    {
        public override void AddEdge(IUnweightedSimpleEdge<TValue> edge)
        {
            var startNode = (TNode)edge.StartNode;
            var endNode = (TNode)edge.EndNode;

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IUnweightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(edge);
            AdjacencyList[endNode].Add( new UnweightedSimpleEdge<TValue>(endNode, startNode, false));
        }

        public override void AddEdge(TNode startNode, TNode endNode)
        {
            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<IUnweightedSimpleEdge<TValue>>());
            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<IUnweightedSimpleEdge<TValue>>());

            AdjacencyList[startNode].Add(new UnweightedSimpleEdge<TValue>(startNode, endNode, false));
            AdjacencyList[endNode].Add(new UnweightedSimpleEdge<TValue>(endNode, startNode, false));
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(startNode) && e.EndNode.Equals(endNode));
                edgeList.RemoveAll(e => e.StartNode.Equals(endNode) && e.EndNode.Equals(startNode));
            }
        }
    }
}