﻿namespace VNet.DataStructures.Graph
{
    public abstract class MultiGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                                                                 where TEdge : notnull, INormalEdge<TValue>
                                                                                                 where TValue : notnull
    {
    }
}