using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VNet.DataStructures
{
    public abstract class ObservableDoubleTypeCollectionBase<TItem, TPriority> : IObservableDoubleTypeCollection<TItem, TPriority>,
                                                                                 IReentrancyMonitorClient<ObservableDoubleTypeCollectionBase<TItem, TPriority>>
                                                                                 where TItem : notnull
                                                                                 where TPriority : notnull
    {
        public int BlockReentrancyCount { get; set; }
        [AllowNull]
        public ReentrancyMonitor<ObservableDoubleTypeCollectionBase<TItem, TPriority>> Monitor { get; set; }


        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event NotifyExtendedDoubleTypeCollectionChangedEventHandler<TItem, TPriority>? ExtendedCollectionChanged;


        public IDisposable BlockReentrancy()
        {
            BlockReentrancyCount++;
            return EnsureMonitorInitialized();
        }

        public ReentrancyMonitor<ObservableDoubleTypeCollectionBase<TItem, TPriority>> EnsureMonitorInitialized()
        {
            return Monitor = Monitor ?? new ReentrancyMonitor<ObservableDoubleTypeCollectionBase<TItem, TPriority>>(this);
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
        protected virtual void OnExtendedCollectionChanged(NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority> e)
        {
            BlockReentrancy();

            ExtendedCollectionChanged?.Invoke(this, e);
            CollectionChanged?.Invoke(this, e);
        }
    }
}