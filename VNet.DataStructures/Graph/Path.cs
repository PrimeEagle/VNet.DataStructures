namespace VNet.DataStructures.Graph
{
    public class Path<TNode, TValue> where TNode : notnull, INode<TValue>
                                     where TValue : notnull, IComparable<TValue>
    {
        private readonly List<TNode> _list;
        private int _index;
        public Dictionary<TNode, double> Weights { get; init; }
        public IList<TNode> Nodes => _list;
        public double TotalWeight => Weights.Values.Sum();



        public Path()
        {
            _list = new List<TNode>();
            Weights = new Dictionary<TNode, double>();
            _index = 0;
        }

        public Path(IEnumerable<TNode> list)
        {
            _list = new List<TNode>(list);
            Weights = new Dictionary<TNode, double>();
            _index = 0;
        }

        public Path(IEnumerable<TNode> list, IDictionary<TNode, double> weights)
        {
            _list = new List<TNode>(list);
            Weights = (Dictionary<TNode, double>)weights;
            _index = 0;
        }

        public TNode this[int index] => _list[index];

        public TNode? First()
        {
            if (_list.Count == 0) return default;

            _index = 0;
            return _list[_index];
        }

        public TNode? Last()
        {
            if (_list.Count == 0) return default;

            _index = _list.Count - 1;
            return _list[_index];
        }

        public TNode? Next()
        {
            if (_list.Count == 0 || _index >= _list.Count - 1) return default;

            return _list[++_index];
        }

        public TNode? Previous()
        {
            if (_list.Count == 0 || _index <= 0) return default;

            return _list[--_index];
        }
    }
}