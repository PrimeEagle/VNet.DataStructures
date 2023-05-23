namespace VNet.DataStructures.Graph
{
    public class DirectedUnweightedMultiGraph<TNode, TValue> : MultiGraphBase<TNode, IUnweightedNormalEdge<TValue>, TValue>
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

        public override GraphBase<TNode, IUnweightedNormalEdge<TValue>, TValue> Clone()
        {
            throw new NotImplementedException();
        }
    }
}