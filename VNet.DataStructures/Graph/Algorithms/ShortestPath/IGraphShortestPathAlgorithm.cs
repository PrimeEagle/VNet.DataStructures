﻿namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public interface IGraphShortestPathAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                         where TNode : notnull, INode<TValue>
                                                                         where TEdge : notnull, IEdge<TNode, TValue>
                                                                         where TValue : notnull, IComparable<TValue>
    {
        public Path<TNode> FindShortestPath(IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}