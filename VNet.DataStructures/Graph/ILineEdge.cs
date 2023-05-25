namespace VNet.DataStructures.Graph
{
    public interface ILineEdge<THyperEdge, TNode, TValue> : IEdge<TNode, TValue>
                                                            where THyperEdge : notnull, IHyperEdge<TNode, TValue>
                                                            where TNode : notnull, INode<TValue>
                                                            where TValue : notnull
    {
        public THyperEdge StartEdge { get; init; }
        public THyperEdge EndEdge { get; init; }


        public ILineEdge<THyperEdge, TNode, TValue> Reverse();
        public ILineEdge<THyperEdge, TNode, TValue> Clone();
    }
}