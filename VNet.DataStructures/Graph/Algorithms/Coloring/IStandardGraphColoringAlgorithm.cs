namespace VNet.DataStructures.Graph.Algorithms.Coloring
{
    public interface IStandardGraphColoringAlgorithm<TNode, TValue> : IGraphColoringAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}