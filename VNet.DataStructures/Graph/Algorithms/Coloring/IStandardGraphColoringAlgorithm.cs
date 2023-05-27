namespace VNet.DataStructures.Graph.Algorithms.Coloring
{
    public interface IStandardGraphColoringAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                             where TNode : notnull, INode<TValue>
                                                                             where TEdge : notnull, IEdge<TNode, TValue>
                                                                             where TValue : notnull  
    {
    }
}