using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VNet.DataStructures.Dictionary
{
    public abstract class ObservableDictionaryBase<TKey, TValue> : IObservableDictionary<TKey, TValue>,
                                                                   IReentrancyMonitorClient<ObservableDictionaryBase<TKey, TValue>>
                                                                   where TKey : notnull
                                                                   where TValue : notnull
    {
        [AllowNull]
        public ReentrancyMonitor<ObservableDictionaryBase<TKey, TValue>> Monitor { get; set; }
        public int BlockReentrancyCount { get; set; }
        public event NotifyExtendedDictionaryChangedEventHandler<TKey, TValue>? ExtendedDictionaryChanged;


        public IDisposable BlockReentrancy()
        {
            BlockReentrancyCount++;
            return EnsureMonitorInitialized();
        }

        public ReentrancyMonitor<ObservableDictionaryBase<TKey, TValue>> EnsureMonitorInitialized()
        {
            return Monitor = Monitor ?? new ReentrancyMonitor<ObservableDictionaryBase<TKey, TValue>>(this);
        }

        public void CheckReentrancy()
        {
            if (BlockReentrancyCount > 0)
            {
                if (ExtendedDictionaryChanged?.GetInvocationList().Length > 1)
                    throw new InvalidOperationException("Reentrancy not allowed.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDictionaryChanged(NotifyExtendedDictionaryChangedEventArgs<TKey, TValue> e)
        {
            BlockReentrancy();

            ExtendedDictionaryChanged?.Invoke(this, e);
        }
    }
}