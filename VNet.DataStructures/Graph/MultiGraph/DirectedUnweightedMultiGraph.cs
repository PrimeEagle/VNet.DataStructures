namespace VNet.DataStructures.Graph.MultiGraph
{
    public class DirectedUnweightedMultiGraph<TNode, TValue> : GraphBase<TNode, IUnweightedSimpleEdge<TValue>, TValue>
                                                               where TNode : notnull, INode<TValue>
                                                               where TValue : notnull
    {
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