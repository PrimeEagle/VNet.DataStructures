﻿using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph
{
    public abstract class GraphBase<TNode, TEdge, TValue> : IGraph<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                                                         where TEdge : notnull, IEdge<TValue>
                                                                                         where TValue : notnull
    {
        public virtual Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; } = new();


        public abstract void AddNode(TNode node);
        public abstract void RemoveNode(TNode node);
        public abstract void AddEdge(TEdge edge);
        public abstract void AddEdge(TNode startNode, TNode endNode);
        public abstract void RemoveEdge(TEdge edge);
        public abstract void RemoveEdge(TNode startNode, TNode endNode);



        public virtual IGraph<TNode, TEdge, TValue> Clone()
        {
            var d1 = this.GetType();
            var typeArgs = new[] { typeof(TNode), typeof(TEdge), typeof(TValue) };
            var constructed = d1.MakeGenericType(typeArgs);
            var result = Activator.CreateInstance(constructed) as IGraph<TNode, TEdge, TValue>;

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result?.AdjacencyList.Add(key, edgeList);
            }

            return result ?? throw new InvalidOperationException();
        }
        
        public TValue Search(IGraphSearchAlgorithm<TNode, TEdge, TValue> searchAlgorithm, TValue value)
        {
            return searchAlgorithm.Search(this, value);
        }
        
        public List<TNode> FindPath(IGraphPathFindingAlgorithm<TNode, TEdge, TValue> pathFindingAlgorithm, INode<TValue> start, INode<TValue> end)
        {
            var results = pathFindingAlgorithm.Find(this, start, end);

            return results;
        }
    }
}