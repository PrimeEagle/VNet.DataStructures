namespace VNet.DataStructures.Graph
{
    public interface IGraph<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                  where TEdge : notnull, IEdge<TNode, TValue>
                                                  where TValue : notnull
    {
        public IGraph<TNode, TEdge, TValue> Clone();

        List<TEdge> this[TNode node] { get; set; }

        public Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; }
    }
}