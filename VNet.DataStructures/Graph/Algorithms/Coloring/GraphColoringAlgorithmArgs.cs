namespace VNet.DataStructures.Graph.Algorithms.Coloring
{
    public class GraphColoringAlgorithmArgs<TNode, TEdge, TValue> : IGraphColoringAlgorithmArgs<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IEdge<TNode, TValue>
                                                                    where TValue : notnull, IComparable<TValue>
    {
        public GraphColoringAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}