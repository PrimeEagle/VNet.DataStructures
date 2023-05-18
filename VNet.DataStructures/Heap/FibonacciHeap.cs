namespace VNet.DataStructures.Heap
{
    public sealed class FibonacciHeap<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        private readonly List<HeapNode<TKey, TValue>> _root = new();
        private int _count;
        private HeapNode<TKey, TValue> _min;

        public FibonacciHeap(HeapNode<TKey, TValue> min)
        {
            _min = min;
        }

        public void Push(TKey key, TValue value)
        {
            Insert(new HeapNode<TKey, TValue>
            {
                Key = key,
                Value = value
            });
        }

        public KeyValuePair<TKey, TValue> Peek()
        {
            if (_min == null)
                throw new InvalidOperationException();
            return new KeyValuePair<TKey, TValue>(_min.Key, _min.Value);
        }

        public KeyValuePair<TKey, TValue> Pop()
        {
            if (_min == null)
                throw new InvalidOperationException();
            var min = ExtractMin();
            return new KeyValuePair<TKey, TValue>(min.Key, min.Value);
        }

        private void Insert(HeapNode<TKey, TValue> node)
        {
            _count++;
            _root.Add(node);
            if (_min == null)
            {
                _min = node;
            }
            else if (Comparer<TKey>.Default.Compare(node.Key, _min.Key) < 0)
            {
                _min = node;
            }
        }

        private HeapNode<TKey, TValue> ExtractMin()
        {
            var result = _min;
            if (result == null)
                return null;
            foreach (var child in result.Children)
            {
                child.Parent = null;
                _root.Add(child);
            }
            _root.Remove(result);
            if (_root.Count == 0)
            {
                _min = null;
            }
            else
            {
                _min = _root[0];
                Consolidate();
            }
            _count--;
            return result;
        }

        private void Consolidate()
        {
            var a = new HeapNode<TKey, TValue>[UpperBound()];
            for (var i = 0; i < _root.Count; i++)
            {
                var x = _root[i];
                var d = x.Children.Count;
                while (true)
                {
                    var y = a[d];
                    if (y == null)
                        break;
                    if (Comparer<TKey>.Default.Compare(x.Key, y.Key) > 0)
                    {
                        (x, y) = (y, x);
                    }
                    _root.Remove(y);
                    i--;
                    x.AddChild(y);
                    y.Mark = false;
                    a[d] = null;
                    d++;
                }
                a[d] = x;
            }
            _min = null;
            foreach (var n in a)
            {
                if (n == null)
                    continue;
                if (_min == null)
                {
                    _root.Clear();
                    _min = n;
                }
                else
                {
                    if (Comparer<TKey>.Default.Compare(n.Key, _min.Key) < 0)
                    {
                        _min = n;
                    }
                }
                _root.Add(n);
            }
        }

        private int UpperBound()
        {
            return (int)Math.Floor(Math.Log(_count, (1.0 + Math.Sqrt(5)) / 2.0)) + 1;
        }
    }
}