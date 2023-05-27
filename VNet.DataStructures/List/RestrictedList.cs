using System.Collections;
using System.Collections.ObjectModel;

namespace VNet.DataStructures.List
{
    public class RestrictedList<T> : ListBase<T>,
                                     IEnumerable<T> 
                                     where T : notnull, IComparable<T>
    {
        private Type? _restrictedType;


        public RestrictedList()
        {
            _list = new List<T>();
        }

        public RestrictedList(IEnumerable<T> enumerable)
        {
            _list = new List<T>(enumerable);
        }

        public RestrictedList(int capacity)
        {
            _list = new List<T>(capacity);
        }



        public bool IsSynchronized => ((ICollection) _list).IsSynchronized;

        public object SyncRoot => ((ICollection) _list).SyncRoot;

        public bool Contains(T value)
        {
            return _list.Contains(value);
        }

        public int IndexOf(T value)
        {
            return _list.IndexOf(value);
        }

        public void Remove(T value)
        {
            _list.Remove(value);
        }

        public bool IsFixedSize => ((IList) _list).IsFixedSize;

        public bool IsReadOnly => ((IList) _list).IsReadOnly;

        public T this[int index]
        {
            get => (T)((IList)_list)[index];
            set => ((IList) _list)[index] = value;
        }

        public void Add(T item)
        {
            if (_list.Count == 0) _restrictedType = item.GetType();
            if (item.GetType() != _restrictedType) throw new ArgumentException("All types in a restricted list must match.");

            _list.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            var enumerable = collection.ToList();
            if (_list.Count == 0) _restrictedType = enumerable.ElementAt(0).GetType();
            if (enumerable.Any(i => i.GetType() != _restrictedType)) throw new ArgumentException("All types in a restricted list must match.");

            _list.AddRange(enumerable);
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

        public void Clear()
        {
            _list.Clear();
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

        public void ForEach(Action<T> action)
        {
            _list.ForEach(action);
        }

        public List<T> GetRange(int index, int count)
        {
            return _list.GetRange(index, count);
        }

        public int IndexOf(T item, int index)
        {
            return _list.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return _list.IndexOf(item, index, count);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            _list.InsertRange(index, collection);
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

        public int RemoveAll(Predicate<T> match)
        {
            return _list.RemoveAll(match);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public void RemoveRange(int index, int count)
        {
            _list.RemoveRange(index, count);
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

        public int Capacity
        {
            get => _list.Capacity;
            set => _list.Capacity = value;
        }

        public int Count => _list.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}