﻿namespace VNet.DataStructures.Graph.Algorithms.MaximumFlow
{
    public interface IStandardGraphMaximumFlowAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                                where TNode : notnull, INode<TValue>
                                                                                where TEdge : notnull, IEdge<TNode, TValue>
                                                                                where TValue : notnull, IComparable<TValue>
    {
    }
}