namespace VNet.DataStructures.Graph.Algorithms.TopologicalSorting
{
    public interface IStandardGraphTopologicalSortAlgorithm<TNode, TValue> : IGraphTopologicalSortAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}