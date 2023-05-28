namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public class GraphCliqueAlgorithmArgs<TNode, TEdge, TValue> : IGraphCliqueAlgorithmArgs<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IEdge<TNode, TValue>
                                                                       where TValue : notnull
    {
        public GraphCliqueAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}