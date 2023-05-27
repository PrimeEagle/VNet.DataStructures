using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.List
{
    public class RestrictedDeepObservableList<T> : DeepObservableSingleTypeCollectionBase<T> , 
                                                   IEnumerable<T> 
                                                   where T : notnull, IComparable<T>
    {
        [AllowNull]
        private Type? _restrictedType;

        public T this[int index]
        {
            get => _list[index];
            set
            {
                CheckReentrancy();
                var oldValue = _list[index];
                _list[index] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldValue, index));
            }
        }


        public RestrictedDeepObservableList()
        {
            _list = new List<T>();
        }

        public RestrictedDeepObservableList(IEnumerable<T> enumerable)
        {
            _list = new List<T>(enumerable);
        }

        public void Add(T item)
        {
            CheckReentrancy();

            if (_list.Count == 0) _restrictedType = item.GetType();
            if (item.GetType() != _restrictedType) throw new ArgumentException("All types in a restricted list must match.");

            _list.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void AddRange(IEnumerable<T> collection)
        {
            CheckReentrancy();

            var enumerable = collection.ToList();
            if (_list.Count == 0) _restrictedType = enumerable.ElementAt(0).GetType();
            if (enumerable.Any(i => i.GetType() != _restrictedType)) throw new ArgumentException("All types in a restricted list must match.");

            _list.AddRange(enumerable);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection));
        }

        public void Insert(int index, T item)
        {
            CheckReentrancy();
            _list.Insert(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            CheckReentrancy();
            _list.InsertRange(index, collection);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection, index));
        }

        public bool Remove(T item)
        {
            CheckReentrancy();
            var result = _list.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));

            return result;
        }

        public int RemoveAll(Predicate<T> match)
        {
            CheckReentrancy();
            var func = new Func<T, bool>(match);
            var list = _list.Where(func);
            var result = _list.RemoveAll(match);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, list));

            return result;
        }

        public void RemoveAt(int index)
        {
            CheckReentrancy();
            var removedItem = _list[index];
            _list.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
        }

        public void RemoveRange(int index, int count)
        {
            CheckReentrancy();
            var list = _list.GetRange(index, count);
            _list.RemoveRange(index, count);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, list, index));
        }

        public void Clear()
        {
            CheckReentrancy();
            _list.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, _list));
        }

        public void ForEach(Action<T> action)
        {
            CheckReentrancy();
            _list.ForEach(action);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, _list));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }
    }
}