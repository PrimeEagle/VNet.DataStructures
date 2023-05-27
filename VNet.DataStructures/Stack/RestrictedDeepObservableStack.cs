using System.Collections;
using System.Collections.Specialized;

namespace VNet.DataStructures.Stack
{
    public class RestrictedDeepObservableStack<T> : DeepObservableSingleTypeCollectionBase<T>,
                                                    IEnumerable<T>
                                                    where T : notnull, IComparable<T>
    {
        private readonly Stack<T> _stack;
        private Type? _restrictedType;



        #region Constructors
        public RestrictedDeepObservableStack()
        {
            _stack = new Stack<T>();
        }

        public RestrictedDeepObservableStack(IEnumerable<T> enumerable)
        {
            _stack = new Stack<T>(enumerable);
        }

        public RestrictedDeepObservableStack(int capacity)
        {
            _stack = new Stack<T>(capacity);
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




        public T Pop()
        {
            CheckReentrancy();
            var result = _stack.Pop();
            OnExtendedCollectionChanged(new NotifyExtendedSingleTypeCollectionChangedEventArgs<T>(NotifyCollectionChangedAction.Remove, result));

            return result;
        }

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


        #region Wrapper Methods
        public bool Contains(T item)
        {
            return _stack.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _stack.CopyTo(array, arrayIndex);
        }

        public T Peek()
        {
            return _stack.Peek();
        }

        public int EnsureCapacity(int capacity)
        {
            return _stack.EnsureCapacity(capacity);
        }

        public T[] ToArray()
        {
            return _stack.ToArray();
        }

        public void TrimExcess()
        {
            _stack.TrimExcess();
        }

        public bool TryPeek(out T result)
        {
            return _stack.TryPeek(out result);
        }
        #endregion Wrapper Methods
    }
}