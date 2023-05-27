using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;

namespace VNet.DataStructures.Stack
{
    public class RestrictedObservableConcurrentStack<T> : ObservableSingleTypeCollectionBase<T>,
                                                          IEnumerable<T>
                                                          where T : notnull, IComparable<T>
    {
        private readonly ConcurrentStack<T> _stack;
        private Type? _restrictedType;



        #region Constructors
        public RestrictedObservableConcurrentStack()
        {
            _stack = new ConcurrentStack<T>();
        }

        public RestrictedObservableConcurrentStack(IEnumerable<T> enumerable)
        {
            _stack = new ConcurrentStack<T>(enumerable);
        }

        #endregion Constructors



        public IEnumerator<T> GetEnumerator()
        {
            return _stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_stack).GetEnumerator();
        }

        #region Wrapper Properties
        public bool IsSynchronized => ((ICollection)_stack).IsSynchronized;

        public object SyncRoot => ((ICollection)_stack).SyncRoot;
        public int Count => _stack.Count;
        #endregion Wrapper Properties




        public void Push(T item)
        {
            CheckReentrancy();

            if (_stack.Count == 0) _restrictedType = item.GetType();
            if (item.GetType() != _restrictedType) throw new ArgumentException("All types in a restricted stack must match.");

            _stack.Push(item);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, item));
        }

        public bool TryPop(out T item)
        {
            CheckReentrancy();
            bool result = _stack.TryPop(out item);
            if (result)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, item));
            }

            return result;
        }

        public void Clear()
        {
            CheckReentrancy();
            T[] removedItems = new T[] { };
            _stack.CopyTo(removedItems, 0);

            _stack.Clear();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Reset, new List<T>(removedItems)));
        }

        public void PushRange(T[] items)
        {
            CheckReentrancy();

            if (_stack is { IsEmpty: true }) _restrictedType = items.ElementAt(0).GetType();
            if (items.Any(i => i.GetType() != _restrictedType)) throw new ArgumentException("All types in a restricted stack must match.");

            _stack.PushRange(items);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, items));
        }

        public void PushRange(T[] items, int startIndex, int count)
        {
            CheckReentrancy();

            if (_stack is { IsEmpty: true }) _restrictedType = items.ElementAt(0).GetType();
            if (items.Any(i => i.GetType() != _restrictedType)) throw new ArgumentException("All types in a restricted stack must match.");

            _stack.PushRange(items, startIndex, count);
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Add, items));
        }

        public int TryPopRange(T[] items)
        {
            CheckReentrancy();
            int result = _stack.TryPopRange(items);
            if (result > 0)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, items));
            }

            return result;
        }

        public int TryPopRange(T[] items, int startIndex, int count)
        {
            CheckReentrancy();
            int result = _stack.TryPopRange(items, startIndex, count);
            if (result > 0)
            {
                OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, items));
            }

            return result;
        }

        #region Wrapper Methods
        public bool Contains(T item)
        {
            return _stack.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _stack.CopyTo(array, arrayIndex);
        }

        public T[] ToArray()
        {
            return _stack.ToArray();
        }

        public bool TryPeek(out T result)
        {
            return _stack.TryPeek(out result);
        }
        #endregion Wrapper Methods
    }
}