namespace VNet.DataStructures.Graph.Algorithms.MaximumFlow
{
    public class GraphMaximumFlowAlgorithmArgs<TNode, TEdge, TValue> : IGraphMaximumFlowAlgorithmArgs<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IEdge<TNode, TValue>
                                                                       where TValue : notnull, IComparable<TValue>
    {
        public GraphMaximumFlowAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}