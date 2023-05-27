namespace VNet.DataStructures.Graph.Algorithms.Search;

public interface IGraphSearchAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
    where TNode : notnull, INode<TValue>
    where TValue : notnull
{
    public bool Search(TNode node);
    public TNode? SearchByValue(TValue value);
    public TNode? SearchByValue(string value, bool hasWildcards);
}