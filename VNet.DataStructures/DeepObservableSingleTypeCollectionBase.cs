using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VNet.DataStructures
{
    public abstract class DeepObservableSingleTypeCollectionBase<T> : ObservableSingleTypeCollectionBase<T>,
                                                                      IDeepObservableSingleTypeCollection<T>
                                                                      where T : notnull
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ExtendedPropertyChangingEventHandler<T>? ExtendedPropertyChanging;
        public event ExtendedPropertyChangedEventHandler<T>? ExtendedPropertyChanged;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnExtendedCollectionChanged(NotifyExtendedSingleTypeCollectionChangedEventArgs<T> e)
        {
            BlockReentrancy();

            if (e.OldItem is not null)
            {
                ((INotifyExtendedPropertyChanging<T>)e.OldItem).ExtendedPropertyChanging += OnItemInCollectionChanging;
                ((INotifyExtendedPropertyChanged<T>)e.OldItem).ExtendedPropertyChanged += OnItemInCollectionChanged;
            }

            if (e.OldItems is not null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is not null)
                    {
                        ((INotifyExtendedPropertyChanging<T>)item).ExtendedPropertyChanging += OnItemInCollectionChanging;
                        ((INotifyExtendedPropertyChanged<T>)item).ExtendedPropertyChanged += OnItemInCollectionChanged;
                    }
                }
            }

            if (e.NewItem is not null)
            {
                ((INotifyExtendedPropertyChanging<T>)e.NewItem).ExtendedPropertyChanging += OnItemInCollectionChanging;
                ((INotifyExtendedPropertyChanged<T>)e.NewItem).ExtendedPropertyChanged += OnItemInCollectionChanged;
            }

            if (e.NewItems is not null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is not null)
                    {
                        ((INotifyExtendedPropertyChanging<T>)item).ExtendedPropertyChanging -= OnItemInCollectionChanging;
                        ((INotifyExtendedPropertyChanged<T>)item).ExtendedPropertyChanged -= OnItemInCollectionChanged;
                    }
                }
            }

            base.OnExtendedCollectionChanged(e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnItemInCollectionChanging(object? sender, ExtendedPropertyChangingEventArgs<T> e)
        {
            OnExtendedPropertyChanging(e.PropertyName, e.OldValue, e.NewValue);
            OnPropertyChanging(e.PropertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnItemInCollectionChanged(object? sender, ExtendedPropertyChangedEventArgs<T> e)
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
        protected void OnExtendedPropertyChanging(string propertyName, T oldValue, T newValue)
        {
            BlockReentrancy();

            ExtendedPropertyChanging?.Invoke(this, new ExtendedPropertyChangingEventArgs<T>(propertyName, oldValue, newValue));
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnExtendedPropertyChanged(string propertyName, T oldValue, T newValue)
        {
            BlockReentrancy();

            ExtendedPropertyChanged?.Invoke(this, new ExtendedPropertyChangedEventArgs<T>(propertyName, oldValue, newValue));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}