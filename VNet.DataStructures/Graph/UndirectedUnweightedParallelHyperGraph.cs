﻿namespace VNet.DataStructures.Graph
{
    public class UndirectedUnweightedParallelHyperGraph<TNode, TValue> : ParallelHyperGraphBase<TNode, IUnweightedHyperEdge<TValue>, TValue>
        where TNode : notnull, INode<TValue>
        where TValue : notnull
    {
        public override void RemoveNode(TNode node)
        {
            throw new NotImplementedException();
        }

        public override void AddEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEdge(TNode startNode, TNode endNode)
        {
            throw new NotImplementedException();
        }

        public override GraphBase<TNode, IUnweightedHyperEdge<TValue>, TValue> Clone()
        {
            throw new NotImplementedException();
        }
    }
}