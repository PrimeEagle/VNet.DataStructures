using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VNet.DataStructures
{
    public abstract class DeepObservableDoubleTypeCollectionBase<TItem, TPriority> : ObservableDoubleTypeCollectionBase<TItem, TPriority>,
                                                                                     IDeepObservableDoubleTypeCollection<TItem, TPriority>
                                                                                     where TItem : notnull
                                                                                     where TPriority : notnull
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ExtendedPropertyChangingEventHandler<TItem>? ExtendedPropertyChanging;
        public event ExtendedPropertyChangedEventHandler<TItem>? ExtendedPropertyChanged;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnExtendedCollectionChanged(NotifyExtendedDoubleTypeCollectionChangedEventArgs<TItem, TPriority> e)
        {
            BlockReentrancy();

            if (e.OldItem is not null)
            {
                ((INotifyExtendedPropertyChanging<TItem>)e.OldItem).ExtendedPropertyChanging += OnItemInCollectionChanging;
                ((INotifyExtendedPropertyChanged<TItem>)e.OldItem).ExtendedPropertyChanged += OnItemInCollectionChanged;
            }

            foreach (var item in e.OldItems)
            {
                if (item is null) continue;
                ((INotifyExtendedPropertyChanging<TItem>)item).ExtendedPropertyChanging += OnItemInCollectionChanging;
                ((INotifyExtendedPropertyChanged<TItem>)item).ExtendedPropertyChanged += OnItemInCollectionChanged;
            }

            if (e.NewItem is not null)
            {
                ((INotifyExtendedPropertyChanging<TItem>)e.NewItem).ExtendedPropertyChanging += OnItemInCollectionChanging;
                ((INotifyExtendedPropertyChanged<TItem>)e.NewItem).ExtendedPropertyChanged += OnItemInCollectionChanged;
            }

            foreach (var item in e.NewItems)
            {
                if (item is null) continue;
                ((INotifyExtendedPropertyChanging<TItem>)item).ExtendedPropertyChanging -= OnItemInCollectionChanging;
                ((INotifyExtendedPropertyChanged<TItem>)item).ExtendedPropertyChanged -= OnItemInCollectionChanged;
            }

            base.OnExtendedCollectionChanged(e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnItemInCollectionChanging(object? sender, ExtendedPropertyChangingEventArgs<TItem> e)
        {
            OnExtendedPropertyChanging(e.PropertyName, e.OldValue, e.NewValue);
            OnPropertyChanging(e.PropertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnItemInCollectionChanged(object? sender, ExtendedPropertyChangedEventArgs<TItem> e)
        {
            OnExtendedPropertyChanged(e.PropertyName, e.OldValue, e.NewValue);
            OnPropertyChanged(e.PropertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnPropertyChanging(string propertyName)
        {
            BlockReentrancy();

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnPropertyChanged(string propertyName)
        {
            BlockReentrancy();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnExtendedPropertyChanging(string propertyName, TItem oldValue, TItem newValue)
        {
            BlockReentrancy();

            ExtendedPropertyChanging?.Invoke(this, new ExtendedPropertyChangingEventArgs<TItem>(propertyName, oldValue, newValue));
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnExtendedPropertyChanged(string propertyName, TItem oldValue, TItem newValue)
        {
            BlockReentrancy();

            ExtendedPropertyChanged?.Invoke(this, new ExtendedPropertyChangedEventArgs<TItem>(propertyName, oldValue, newValue));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}