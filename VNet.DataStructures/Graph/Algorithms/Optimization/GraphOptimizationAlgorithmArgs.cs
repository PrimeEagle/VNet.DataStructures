namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public class GraphOptimizationAlgorithmArgs<TNode, TEdge, TValue, TSolution> : IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue>
                                                                                   where TNode : notnull, INode<TValue>
                                                                                   where TEdge : notnull, IEdge<TNode, TValue>
                                                                                   where TValue : notnull, IComparable<TValue>
                                                                                   where TSolution : notnull
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }



        public GraphOptimizationAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }
    }
}