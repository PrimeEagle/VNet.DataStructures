namespace VNet.DataStructures.Graph
{
    public class UnweightedSimpleEdge<TNode, TValue> : IUnweightedSimpleEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                            where TValue : notnull
    {
        public bool Directed { get; init; }
        public TNode StartNode { get; init; }
        public TNode EndNode { get; init; }


        public UnweightedSimpleEdge(TNode startNode, TNode endNode, bool directed)
        {
            Directed = directed;
            StartNode = startNode;
            EndNode = endNode;
        }

        public IUnweightedSimpleEdge<TNode, TValue> Clone()
        {
            return new UnweightedSimpleEdge<TNode, TValue>(StartNode, EndNode, Directed);
        }

        public IUnweightedSimpleEdge<TNode, TValue> Reverse()
        {
            return new UnweightedSimpleEdge<TNode, TValue>(EndNode, StartNode, Directed);
        }
    }
}