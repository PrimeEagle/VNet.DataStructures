namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public interface IGraphCliqueAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IEdge<TNode, TValue>
                                                                       where TValue : notnull, IComparable<TValue>
    {

    }
}