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
    }
}