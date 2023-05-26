using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph
{
    public abstract class StandardGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>, IStandardGraph<TNode, TEdge, TValue>
                                                                  where TNode : notnull, INode<TValue>
                                                                  where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                  where TValue : notnull
    {
        public TValue Search(IStandardGraphTraversalAlgorithm<TNode, TValue> searchAlgorithm, TValue value)
        {
            return searchAlgorithm.Traverse(this, value);
        }
    }
}