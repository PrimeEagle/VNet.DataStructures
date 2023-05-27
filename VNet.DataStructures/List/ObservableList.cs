using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace VNet.DataStructures.List
{
    public class ObservableList<T> : ObservableSingleTypeCollectionBase<T>,
                                     IEnumerable<T>
                                     where T : notnull, IComparable<T>
    {
        public T this[int index]
        {
            get => _list[index];
            set
            {
                CheckReentrancy();
                T oldValue = _list[index];
                _list[index] = value;
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Replace, _list[index], oldValue));
            }
        }

        #region Constructors
        public ObservableList()
        {
            _list = new List<T>();
        }

        public ObservableList(IEnumerable<T> enumerable)
        {
            _list = new List<T>(enumerable);
        }

        public ObservableList(int capacity)
        {
            _list = new List<T>(capacity);
        }
        #endregion Constructors

        public void Add(T item)
        {
            CheckReentrancy();
            _list.Add(item);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item));
        }

        public void AddRange(IEnumerable<T> collection)
        {
            CheckReentrancy();
            _list.AddRange(collection);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, collection));
        }

        public void Insert(int index, T item)
        {
            CheckReentrancy();
            _list.Insert(index, item);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item, index));
        }
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            CheckReentrancy();
            _list.InsertRange(index, collection);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, collection, index));
        }

        public bool Remove(T item)
        {
            CheckReentrancy();
            var result = _list.Remove(item);
            if (result)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, item));
            }

            return result;
        }

        public int RemoveAll(Predicate<T> match)
        {
            CheckReentrancy();
            var func = new Func<T, bool>(match);
            var removedItems = _list.Where(func).ToArray();
            var result = _list.RemoveAll(match);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, removedItems));

            return result;
        }

        public void RemoveAt(int index)
        {
            CheckReentrancy();
            var removedItem = _list[index];
            _list.RemoveAt(index);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, removedItem, index));
        }

        public void RemoveRange(int index, int count)
        {
            CheckReentrancy();
            var list = _list.GetRange(index, count);
            _list.RemoveRange(index, count);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, list, index));
        }

        public void Clear()
        {
            CheckReentrancy();
            T[] removedItems = new T[] { };
            _list.CopyTo(removedItems);

            _list.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset, new List<T>(removedItems)));
        }

        public void ForEach(Action<T> action)
        {
            CheckReentrancy();
            _list.ForEach(action);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset, _list, _list));
        }

        #region Wrapper Properties
        public bool IsSynchronized => ((ICollection)_list).IsSynchronized;

        public object SyncRoot => ((ICollection)_list).SyncRoot;

        public bool IsReadOnly => ((IList)_list).IsReadOnly;



        public int Capacity
        {
            get => _list.Capacity;
            set => _list.Capacity = value;
        }

        public int Count => _list.Count;
        #endregion Wrapper Properties

        #region Wrapper Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return _list.AsReadOnly();
        }

        public int BinarySearch(int index, int count, T item, IComparer<T>? comparer)
        {
            return _list.BinarySearch(index, count, item, comparer);
        }

        public int BinarySearch(T item)
        {
            return _list.BinarySearch(item);
        }

        public int BinarySearch(T item, IComparer<T>? comparer)
        {
            return _list.BinarySearch(item, comparer);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return _list.ConvertAll(converter);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            _list.CopyTo(index, array, arrayIndex, count);
        }

        public void CopyTo(T[] array)
        {
            _list.CopyTo(array);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int EnsureCapacity(int capacity)
        {
            return _list.EnsureCapacity(capacity);
        }

        public bool Exists(Predicate<T> match)
        {
            return _list.Exists(match);
        }

        public T? Find(Predicate<T> match)
        {
            return _list.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return _list.FindAll(match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return _list.FindIndex(startIndex, count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return _list.FindIndex(startIndex, match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return _list.FindIndex(match);
        }

        public T? FindLast(Predicate<T> match)
        {
            return _list.FindLast(match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return _list.FindLastIndex(startIndex, count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return _list.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return _list.FindLastIndex(match);
        }

        public List<T> GetRange(int index, int count)
        {
            return _list.GetRange(index, count);
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public int IndexOf(T item, int index)
        {
            return _list.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return _list.IndexOf(item, index, count);
        }

        public int LastIndexOf(T item)
        {
            return _list.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return _list.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return _list.LastIndexOf(item, index, count);
        }

        public void Reverse()
        {
            _list.Reverse();
        }

        public void Reverse(int index, int count)
        {
            _list.Reverse(index, count);
        }

        public void Sort()
        {
            _list.Sort();
        }

        public void Sort(IComparer<T>? comparer)
        {
            _list.Sort(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            _list.Sort(comparison);
        }

        public void Sort(int index, int count, IComparer<T>? comparer)
        {
            _list.Sort(index, count, comparer);
        }

        public T[] ToArray()
        {
            return _list.ToArray();
        }

        public void TrimExcess()
        {
            _list.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return _list.TrueForAll(match);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_list).GetEnumerator();
        }
        #endregion Wrapper Methods
    }
}