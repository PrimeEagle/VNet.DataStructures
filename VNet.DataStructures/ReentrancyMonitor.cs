namespace VNet.DataStructures
{
    [Serializable]
    public sealed class ReentrancyMonitor<T> : IDisposable where T : IReentrancyMonitorClient<T>
    {
        [NonSerialized]
        private T _internalDataStructure;

        public ReentrancyMonitor(T collection)
        {
            _internalDataStructure = collection;
        }

        public void Dispose() => _internalDataStructure.BlockReentrancyCount--;
    }
}