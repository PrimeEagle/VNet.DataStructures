namespace VNet.DataStructures.Graph.Algorithms.Search;

public interface IGraphSearchAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                               where TNode : notnull, INode<TValue>
                                                               where TEdge : notnull, IEdge<TNode, TValue>
                                                               where TValue : notnull
{
    public bool Search(TNode node);
    public TNode? SearchByValue(TValue value);
    public TNode? SearchByValue(string value, bool hasWildcards);
}