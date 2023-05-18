namespace VNet.DataStructures
{
    public interface IReentrancyMonitorClient<T> where T : IReentrancyMonitorClient<T>
    {
        public int BlockReentrancyCount { get; set; }
        public ReentrancyMonitor<T> Monitor { get; set; }
        protected IDisposable BlockReentrancy();
        protected ReentrancyMonitor<T> EnsureMonitorInitialized();
        protected void CheckReentrancy();
    }
}