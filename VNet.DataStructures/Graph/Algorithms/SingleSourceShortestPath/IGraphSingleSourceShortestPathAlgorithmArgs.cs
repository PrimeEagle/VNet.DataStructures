﻿namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public interface IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                             where TNode : notnull, INode<TValue>
                                                                             where TEdge : notnull, IEdge<TNode, TValue>
                                                                             where TValue : notnull, IComparable<TValue>
    {
        public TNode? StartNode { get; set; }
        public TNode? EndNode { get; set; }
    }
}