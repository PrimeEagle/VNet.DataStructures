namespace VNet.DataStructures.Graph.MultiGraph
{
    public class DirectedWeightedMultiGraph<TNode, TValue> : GraphBase<TNode, IWeightedSimpleEdge<TValue>, TValue>
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