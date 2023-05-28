namespace VNet.DataStructures.Graph.Algorithms.Matching
{
    public class GraphMatchingAlgorithmArgs<TNode, TEdge, TValue> : IGraphMatchingAlgorithmArgs<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IEdge<TNode, TValue>
                                                                    where TValue : notnull
    {
        public GraphMatchingAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}