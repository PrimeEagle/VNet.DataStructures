namespace VNet.DataStructures.Graph.HyperGraph
{
    public class DirectedUnweightedHyperGraph<TNode, TValue> : HyperGraphBase<TNode, IUnweightedHyperEdge<TValue>, TValue>
        where TNode : notnull, INode<TValue>
        where TValue : notnull
    {
        public override void RemoveNode(TNode node)
        {
            throw new NotImplementedException();
        }

        public override void AddEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }
    }
}