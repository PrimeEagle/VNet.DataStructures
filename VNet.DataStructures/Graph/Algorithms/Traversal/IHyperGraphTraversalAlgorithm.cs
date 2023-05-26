﻿namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IHyperGraphTraversalAlgorithm<TNode, TValue> : IGraphTraversalAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
        public void Traverse(TNode starTNode, Action<TNode> preVisit, Action<TNode> postVisit);
    }
}