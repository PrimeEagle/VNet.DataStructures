namespace VNet.DataStructures.Graph
{
    public abstract class SimpleGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>, ISimpleGraph<TNode, TEdge, TValue>
                                                                  where TNode : notnull, INode<TValue>
                                                                  where TEdge : notnull, ISimpleEdge<TNode, TValue>
                                                                  where TValue : notnull
    {
       
    }
}