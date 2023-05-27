namespace VNet.DataStructures.Graph.Algorithms.TopologicalSorting
{
    public interface IHyperGraphTopologicalSortAlgorithm<TNode, TValue> : IGraphTopologicalSortAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    { 
    }
}