namespace VNet.DataStructures.Queue
{
    public class AsyncQueue<T>
    {
        private readonly Queue<TaskCompletionSource<T>> consumerQueue = new();

        private readonly Queue<T> queue = new();
        private readonly SemaphoreSlim consumerQueueLock = new(1);
        public int Count => queue.Count;

        public async Task EnqueueAsync(T value, int millisecondsTimeout = int.MaxValue,
            CancellationToken taskCancellationToken = default)
        {
            await consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

            if (consumerQueue.Count > 0)
            {
                var consumer = consumerQueue.Dequeue();
                consumer.TrySetResult(value);
            }
            else
            {
                queue.Enqueue(value);
            }

            consumerQueueLock.Release();
        }

        public async Task<T> DequeueAsync(int millisecondsTimeout = int.MaxValue,
            CancellationToken taskCancellationToken = default)
        {
            await consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

            TaskCompletionSource<T> consumer;

            try
            {
                if (queue.Count > 0)
                {
                    var result = queue.Dequeue();
                    return result;
                }

                consumer = new TaskCompletionSource<T>();
                taskCancellationToken.Register(() => consumer.TrySetCanceled());
                consumerQueue.Enqueue(consumer);
            }
            finally
            {
                consumerQueueLock.Release();
            }

            return await consumer.Task;
        }
    }
}