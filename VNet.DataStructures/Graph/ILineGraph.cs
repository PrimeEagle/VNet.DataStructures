namespace VNet.DataStructures.Graph
{
    public interface ILineGraph<TLineNode, TLineEdge, TNode, TValue> where TLineNode : notnull, IHyperEdge<TNode, TValue>
                                                                     where TLineEdge : notnull, ILineEdge<TLineNode, TNode, TValue>
                                                                     where TNode : notnull, INode<TValue>
                                                                     where TValue : notnull
    {
        public Dictionary<TLineNode, List<TLineEdge>> AdjacencyList { get; init; }
        public List<TLineEdge> this[TLineNode node] { get; set; }
    }
}