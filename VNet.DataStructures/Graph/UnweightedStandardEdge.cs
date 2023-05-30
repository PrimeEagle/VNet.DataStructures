namespace VNet.DataStructures.Graph
{
    public class UnweightedStandardEdge<TNode, TValue> : IUnweightedStandardEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                            where TValue : notnull
    {
        public bool Directed { get; init; }
        public TNode StartNode { get; init; }
        public TNode EndNode { get; init; }


        public UnweightedStandardEdge(TNode startNode, TNode endNode, bool directed)
        {
            Directed = directed;
            StartNode = startNode;
            EndNode = endNode;
        }

        public IEdge<TNode, TValue> Clone(bool deep = false)
        {
            return (IEdge<TNode, TValue>)new UnweightedStandardEdge<TNode, TValue>(StartNode, EndNode, Directed);
        }

        public IUnweightedStandardEdge<TNode, TValue> Reverse()
        {
            return new UnweightedStandardEdge<TNode, TValue>(EndNode, StartNode, Directed);
        }
    }
}