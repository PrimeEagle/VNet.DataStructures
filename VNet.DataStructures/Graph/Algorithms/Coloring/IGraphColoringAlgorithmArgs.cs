namespace VNet.DataStructures.Graph.Algorithms.Coloring
{
    public interface IGraphColoringAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
    {

    }
}