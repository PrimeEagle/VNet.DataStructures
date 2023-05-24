namespace VNet.DataStructures.Graph
{
    public class UnweightedSimpleEdge<T> : IUnweightedSimpleEdge<T> where T : notnull
    {
        public bool Directed { get; init; }
        public INode<T> StartNode { get; init; }
        public INode<T> EndNode { get; init; }


        public UnweightedSimpleEdge(INode<T> startNode, INode<T> endNode, bool directed)
        {
            Directed = directed;
            StartNode = startNode;
            EndNode = endNode;
        }

        public new IUnweightedSimpleEdge<T> Clone()
        {
            return new UnweightedSimpleEdge<T>(StartNode, EndNode, Directed);
        }

        public new IUnweightedSimpleEdge<T> Reverse()
        {
            return new UnweightedSimpleEdge<T>(EndNode, StartNode, Directed);
        }

        ISimpleEdge<T> ISimpleEdge<T>.Reverse()
        {
            return Clone();
        }

        ISimpleEdge<T> ISimpleEdge<T>.Clone()
        {
            return Reverse();
        }

        IEdge<T> IEdge<T>.Reverse()
        {
            return Reverse();
        }

        IEdge<T> IEdge<T>.Clone()
        {
            return Clone();
        }
    }
}