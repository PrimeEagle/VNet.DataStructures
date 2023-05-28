namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public class GraphSearchAlgorithmArgs<TNode, TEdge, TValue> : IGraphSearchAlgorithmArgs<TNode, TEdge, TValue>
                                                                  where TNode : notnull, INode<TValue>
                                                                  where TEdge : notnull, IEdge<TNode, TValue>
                                                                  where TValue : notnull
    {
        public GraphSearchAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}