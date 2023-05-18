using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class ArithmeticTreeInOrderTraversal<T> : IEnumerator<ArithmeticTreeNode<T>>
    {
        private ArithmeticTreeNode<T> _root;
        private ArithmeticTreeNode<T> _next;

        #region Implementation of IEnumerator
        public ArithmeticTreeNode<T> Current { get; private set; }

        public ArithmeticTreeInOrderTraversal(IBinaryTree<T> tree)
        {
            _root = tree.GetRoot();
            _next = tree.GetRoot();

            if (_next == null)
                return;

            while (_next.LeftNode != null)
                _next = _next.LeftNode;
        }

        public bool MoveNext()
        {
            if (_next == null)
                return false;

            Current = _next;

            if (_next.RightNode != null)
            {
                _next = _next.RightNode;

                while (_next.LeftNode != null)
                    _next = _next.LeftNode;

                return true;
            }
            else while (true)
                {
                    if (_next.ParentNode == null)
                    {
                        _next = null;
                        return true;
                    }

                    if (_next.ParentNode.LeftNode == _next)
                    {
                        _next = _next.ParentNode;
                        return true;
                    }
                    _next = _next.ParentNode;
                }
        }

        public void Reset()
        {
            _next = _root;

            if (_next == null)
                return;

            while (_next.LeftNode != null)
                _next = _next.LeftNode;
        }

        object IEnumerator.Current => Current;
        #endregion

        #region Implementation of IDisposable
        public void Dispose()
        {
        }

        #endregion
    }

    public class InOrderTraversalTree<T> : IEnumerable<ArithmeticTreeNode<T>>
    {
        private IBinaryTree<T> _tree;

        public InOrderTraversalTree(IBinaryTree<T> pTree)
        {
            _tree = pTree;
        }
        #region Implementation of IEnumerable

        public IEnumerator<ArithmeticTreeNode<T>> GetEnumerator()
        {
            return new ArithmeticTreeInOrderTraversal<T>(_tree);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ArithmeticTreeInOrderTraversal<T>(_tree);
        }

        #endregion
    }
}