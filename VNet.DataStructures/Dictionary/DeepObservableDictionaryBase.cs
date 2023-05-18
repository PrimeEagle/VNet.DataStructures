using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VNet.DataStructures.Dictionary
{
    public abstract class DeepObservableDictionaryBase<TKey, TValue> : ObservableDictionaryBase<TKey, TValue>,
                                                                       IDeepObservableDictionary<TKey, TValue>
                                                                       where TKey : notnull
                                                                       where TValue : notnull
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ExtendedPropertyChangingEventHandler<TValue>? ExtendedPropertyChanging;
        public event ExtendedPropertyChangedEventHandler<TValue>? ExtendedPropertyChanged;



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnDictionaryChanged(NotifyExtendedDictionaryChangedEventArgs<TKey, TValue> e)
        {
            BlockReentrancy();

            if (e.NewItems is not null)
            {
                foreach (var item in e.NewItems)
                {
                    ((INotifyExtendedPropertyChanging<TValue>)item).ExtendedPropertyChanging -= OnItemInDictionaryChanging;
                    ((INotifyExtendedPropertyChanged<TValue>)item).ExtendedPropertyChanged -= OnItemInDictionaryChanged;
                }
            }

            if (e.OldItems is not null)
            {
                foreach (var item in e.OldItems)
                {
                    ((INotifyExtendedPropertyChanging<TValue>)item).ExtendedPropertyChanging += OnItemInDictionaryChanging;
                    ((INotifyExtendedPropertyChanged<TValue>)item).ExtendedPropertyChanged += OnItemInDictionaryChanged;
                }
            }

            base.OnDictionaryChanged(e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnPropertyChanging(string propertyName, TValue oldValue, TValue newValue) => PropertyChanging?.Invoke(this, new ExtendedPropertyChangingEventArgs<TValue>(propertyName, oldValue, newValue));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnPropertyChanged(string propertyName, TValue oldValue, TValue newValue) => PropertyChanged?.Invoke(this, new ExtendedPropertyChangedEventArgs<TValue>(propertyName, oldValue, newValue));

        protected void OnItemInDictionaryChanging(object sender, ExtendedPropertyChangingEventArgs<TValue> e)
        {
            OnPropertyChanging("name", default, default);
        }

        protected void OnItemInDictionaryChanged(object sender, ExtendedPropertyChangedEventArgs<TValue> e)
        {
            OnPropertyChanged("name", default, default);
        }

    }
}