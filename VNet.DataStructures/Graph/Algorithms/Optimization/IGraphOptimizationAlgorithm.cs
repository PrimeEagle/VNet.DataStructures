namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public interface IGraphOptimizationAlgorithm<TNode, TEdge, TValue, out TSolution> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                                        where TNode : notnull, INode<TValue>
                                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                                        where TValue : notnull, IComparable<TValue>
                                                                                        where TSolution : notnull
    {
        public TSolution Solve(IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}