namespace VNet.DataStructures.Graph.Algorithms.Transversal
{
    public class GraphTransversalAlgorithmArgs<TNode, TEdge, TValue> : IGraphTransversalAlgorithmArgs<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IEdge<TNode, TValue>
                                                                       where TValue : notnull, IComparable<TValue>
    {
        public GraphTransversalAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}