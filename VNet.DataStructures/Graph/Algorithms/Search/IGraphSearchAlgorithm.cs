namespace VNet.DataStructures.Graph.Algorithms.Search;

public interface IGraphSearchAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                               where TNode : notnull, INode<TValue>
                                                               where TEdge : notnull, IEdge<TNode, TValue>
                                                               where TValue : notnull, IComparable<TValue>
{
    public IEnumerable<TNode> Search(IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> args);
}