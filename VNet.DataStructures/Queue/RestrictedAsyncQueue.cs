namespace VNet.DataStructures.Queue
{
    public class RestrictedAsyncQueue<T>
    {
        private readonly Queue<TaskCompletionSource<T>> _consumerQueue = new();

        private readonly Queue<T> _queue = new();
        private readonly SemaphoreSlim _consumerQueueLock = new(1);
        private Type? _restrictedType;

        public int Count => _queue.Count;

        public async Task EnqueueAsync(T value, int millisecondsTimeout = int.MaxValue,
            CancellationToken taskCancellationToken = default)
        {
            await _consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

            if (_queue.Count == 0) _restrictedType = value?.GetType();
            if (value?.GetType() != _restrictedType) throw new ArgumentException("All types in a restricted queue must match.");

            if (_consumerQueue.Count > 0)
            {
                var consumer = _consumerQueue.Dequeue();
                consumer.TrySetResult(value);
            }
            else
            {
                _queue.Enqueue(value);
            }

            _consumerQueueLock.Release();
        }

        public async Task<T> DequeueAsync(int millisecondsTimeout = int.MaxValue,
            CancellationToken taskCancellationToken = default)
        {
            await _consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

            TaskCompletionSource<T> consumer;

            try
            {
                if (_queue.Count > 0)
                {
                    var result = _queue.Dequeue();
                    return result;
                }

                consumer = new TaskCompletionSource<T>();
                taskCancellationToken.Register(() => consumer.TrySetCanceled());
                _consumerQueue.Enqueue(consumer);
            }
            finally
            {
                _consumerQueueLock.Release();
            }

            return await consumer.Task;
        }
    }
}