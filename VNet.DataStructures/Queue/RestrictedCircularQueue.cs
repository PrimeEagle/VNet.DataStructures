namespace VNet.DataStructures.Queue
{
    public class RestrictedCircularQueue<T>
    {
        private int _end;
        private readonly T[] _queue;

        private int _start;
        private Type? _restrictedType;


        public RestrictedCircularQueue(int size)
        {
            _queue = new T[size];
        }

        public int Count { get; private set; }


        public T Enqueue(T data)
        {
            var deleted = default(T);

            if (_end > _queue.Length - 1)
            {
                _end = 0;

                if (_start == 0)
                {
                    deleted = _queue[_start];
                    _start++;
                }
            }

            if (_end == _start && Count > 1)
            {
                deleted = _queue[_start];
                _start++;
            }

            _queue[_end] = data;
            _end++;

            if (Count < _queue.Length) Count++;

            return deleted;
        }

        public IEnumerable<T> Enqueue(T[] items)
        {
            if (_queue.Length == 0) _restrictedType = items[0]?.GetType();
            if (items.Any(e => e.GetType() != _restrictedType)) throw new ArgumentException("All types in a restricted queue must match.");


            return items.Select(item => Enqueue(item))
                .Where(deleted => !deleted.Equals(default(T))).ToList();
        }

        public T Dequeue()
        {
            if (Count == 0) throw new Exception("Empty queue.");

            var element = _queue[_start];
            _start++;

            if (_start > _queue.Length - 1)
            {
                _start = 0;

                if (_end == 0) _end++;
            }

            Count--;

            if (_start == _end && Count > 1) _end++;
            if (Count == 0) _start = _end = 0;

            return element;
        }

        public IEnumerable<T> Dequeue(int bulkNumber)
        {
            var deletedList = new List<T>();
            while (bulkNumber > 0 && Count > 0)
            {
                var deleted = Dequeue();

                if (!deleted.Equals(default(T))) deletedList.Add(deleted);

                bulkNumber--;
            }

            return deletedList;
        }
    }
}