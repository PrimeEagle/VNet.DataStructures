namespace VNet.DataStructures.Graph.Algorithms.TopologicalSorting
{
    public interface IGraphTopologicalSortAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}