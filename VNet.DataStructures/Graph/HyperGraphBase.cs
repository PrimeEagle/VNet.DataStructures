﻿namespace VNet.DataStructures.Graph
{
    public abstract class HyperGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                                                                 where TEdge : notnull, IHyperEdge<TValue>
                                                                                                 where TValue : notnull
    {
    }
}