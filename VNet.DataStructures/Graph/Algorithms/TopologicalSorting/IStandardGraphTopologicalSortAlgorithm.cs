namespace VNet.DataStructures.Graph.Algorithms.TopologicalSorting
{
    public interface IStandardGraphTopologicalSortAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                                    where TNode : notnull, INode<TValue>
                                                                                    where TEdge : notnull, IEdge<TNode, TValue>
                                                                                    where TValue : notnull, IComparable<TValue>
    {
    }
}