using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VNet.DataStructures
{
    public abstract class ObservableSingleTypeCollectionBase<T> : IObservableSingleTypeCollection<T>,
                                                                  IReentrancyMonitorClient<ObservableSingleTypeCollectionBase<T>>
                                                                  where T : notnull
    {
        public int BlockReentrancyCount { get; set; }
        [AllowNull]
        public ReentrancyMonitor<ObservableSingleTypeCollectionBase<T>> Monitor { get; set; }


        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event NotifyExtendedSingleTypeCollectionChangedEventHandler<T>? ExtendedCollectionChanged;


        public IDisposable BlockReentrancy()
        {
            BlockReentrancyCount++;
            return EnsureMonitorInitialized();
        }

        public ReentrancyMonitor<ObservableSingleTypeCollectionBase<T>> EnsureMonitorInitialized()
        {
            return Monitor = Monitor ?? new ReentrancyMonitor<ObservableSingleTypeCollectionBase<T>>(this);
        }

        public void CheckReentrancy()
        {
            if (BlockReentrancyCount > 0)
            {
                if (CollectionChanged?.GetInvocationList().Length > 1)
                    throw new InvalidOperationException("Reentrancy not allowed.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            BlockReentrancy();

            CollectionChanged?.Invoke(this, e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnExtendedCollectionChanged(NotifyExtendedSingleTypeCollectionChangedEventArgs<T> e)
        {
            BlockReentrancy();

            ExtendedCollectionChanged?.Invoke(this, e);
            CollectionChanged?.Invoke(this, e);
        }
    }
}