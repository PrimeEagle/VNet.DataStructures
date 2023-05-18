namespace VNet.DataStructures.Queue
{
    public class CircularQueue<T>
    {
        private int end;
        private readonly T[] queue;

        private int start;

        public CircularQueue(int size)
        {
            queue = new T[size];
        }

        public int Count { get; private set; }


        public T Enqueue(T data)
        {
            var deleted = default(T);

            if (end > queue.Length - 1)
            {
                end = 0;

                if (start == 0)
                {
                    deleted = queue[start];
                    start++;
                }
            }

            if (end == start && Count > 1)
            {
                deleted = queue[start];
                start++;
            }

            queue[end] = data;
            end++;

            if (Count < queue.Length) Count++;

            return deleted;
        }

        public IEnumerable<T> Enqueue(T[] bulk)
        {
            return bulk.Select(item => Enqueue(item))
                .Where(deleted => !deleted.Equals(default(T))).ToList();
        }

        public T Dequeue()
        {
            if (Count == 0) throw new Exception("Empty queue.");

            var element = queue[start];
            start++;

            if (start > queue.Length - 1)
            {
                start = 0;

                if (end == 0) end++;
            }

            Count--;

            if (start == end && Count > 1) end++;
            if (Count == 0) start = end = 0;

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