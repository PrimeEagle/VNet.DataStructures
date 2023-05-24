namespace VNet.DataStructures.Graph
{
    public abstract class SimpleGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                                                                  where TEdge : notnull, INormalEdge<TValue>
                                                                                                  where TValue : notnull
    {
        public override void RemoveNode(TNode node)
        {
            AdjacencyList.Remove(node);

            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartNode.Equals(node) || e.EndNode.Equals(node));
            }
        }
    }
}