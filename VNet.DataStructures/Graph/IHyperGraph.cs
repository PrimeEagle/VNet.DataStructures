namespace VNet.DataStructures.Graph
{
    public interface IHyperGraph<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                        where TValue : notnull
    {
        public Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; }
        public List<TEdge> this[TNode node] { get; set; }
    }
}