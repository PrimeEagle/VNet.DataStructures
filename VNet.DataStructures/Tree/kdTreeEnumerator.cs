using System.Collections;

namespace VNet.DataStructures.Tree
{
    internal class kdTreeEnumerator<T> : IEnumerator<T[]> where T : IComparable
    {
        private readonly kdTreeNode<T> root;
        private Stack<kdTreeNode<T>> progress;

        internal kdTreeEnumerator(kdTreeNode<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null) return false;

            if (progress == null)
            {
                progress = new Stack<kdTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
                Current = root.Points;
                return true;
            }

            if (progress.Count > 0)
            {
                var next = progress.Pop();
                Current = next.Points;

                foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null)) progress.Push(node);

                return true;
            }

            return false;
        }

        public void Reset()
        {
            progress = null;
            Current = null;
        }

        public T[] Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            progress = null;
        }
    }
}
