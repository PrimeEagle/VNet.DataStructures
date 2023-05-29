namespace VNet.DataStructures.Graph
{
    public abstract class LineGraphBase<TLineNode, TLineEdge, TNode, TValue> : ILineGraph<TLineNode, TLineEdge, TNode, TValue>
                                                                               where TLineNode : notnull, IHyperEdge<TNode, TValue>
                                                                               where TLineEdge : notnull, ILineEdge<TLineNode, TNode, TValue>
                                                                               where TNode : notnull, INode<TValue>
                                                                               where TValue : notnull
    {
        public bool IsStandardGraph => false;
        public bool IsHyperGraph => false;
        public bool IsLineGraph => true;
        public bool IsMultiOrParallelGraph => false;

        List<TLineEdge> ILineGraph<TLineNode, TLineEdge, TNode, TValue>.this[TLineNode node]
        {
            get => AdjacencyList[node];
            set => AdjacencyList[node] = value;
        }

        public virtual Dictionary<TLineNode, List<TLineEdge>> AdjacencyList { get; init; } = new();

        public virtual ILineGraph<TLineNode, TLineEdge, TNode, TValue> Clone()
        {
            var d1 = this.GetType();
            var typeArgs = new[] { typeof(TLineNode), typeof(TLineEdge), typeof(TNode), typeof(TValue) };
            var constructed = d1.MakeGenericType(typeArgs);
            var result = Activator.CreateInstance(constructed) as ILineGraph<TLineNode, TLineEdge, TNode, TValue>;

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result?.AdjacencyList.Add(key, edgeList);
            }

            return result ?? throw new InvalidOperationException();
        }
    }
}