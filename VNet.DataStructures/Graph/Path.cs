namespace VNet.DataStructures.Graph
{
    public class Path<TNode>
    {
        private readonly List<TNode> _list;
        private int _index;

        public Path()
        {
            _list = new List<TNode>();
            _index = 0;
        }

        public Path(IEnumerable<TNode> list)
        {
            _list = new List<TNode>(list);
            _index = 0;
        }

        TNode this[int index] => _list[index];

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