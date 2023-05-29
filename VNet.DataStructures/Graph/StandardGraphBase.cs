namespace VNet.DataStructures.Graph
{
    public abstract class StandardGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>, IStandardGraph<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                    where TValue : notnull, IComparable<TValue>
    {
        public override bool HasNegativeWeights
        {
            get
            {
                return IsWeighted && Edges.Any(e => (e.GetType() == typeof(IWeightedStandardEdge<TNode, TValue>) && ((IWeightedStandardEdge<TNode, TValue>)e).Weight < 0));
            }
        }
    }
}