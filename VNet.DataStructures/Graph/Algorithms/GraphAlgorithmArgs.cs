namespace VNet.DataStructures.Graph.Algorithms
{
    public class GraphAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                            where TNode : notnull, INode<TValue>
                                                            where TEdge : notnull, IEdge<TNode, TValue>
                                                            where TValue : notnull, IComparable<TValue>
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }

        public GraphAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }
    }
}