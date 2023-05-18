using System.Collections;

namespace VNet.DataStructures.Tree
{
    internal class BinaryTreeEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly BinaryTreeNode<T> root;
        private Stack<BinaryTreeNode<T>> progress;

        internal BinaryTreeEnumerator(BinaryTreeNode<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null) return false;

            if (progress == null)
            {
                progress = new Stack<BinaryTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
                Current = root.Value;
                return true;
            }

            if (progress.Count > 0)
            {
                var next = progress.Pop();
                Current = next.Value;

                foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null)) progress.Push(node);

                return true;
            }

            return false;
        }

        public void Reset()
        {
            progress = null;
            Current = default;
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            progress = null;
        }
    }
}
