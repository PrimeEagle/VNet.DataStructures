using System.Collections;
using VNet.Mathematics.Geometry;

namespace VNet.DataStructures.Tree
{
    internal class QuadTreeEnumerator<T> : IEnumerator<Tuple<Point, T>>
    {
        private readonly QuadTreeNode<T> root;

        private QuadTreeNode<T> current;
        private Stack<QuadTreeNode<T>> progress;

        internal QuadTreeEnumerator(QuadTreeNode<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null) return false;

            if (progress == null)
            {
                progress = new Stack<QuadTreeNode<T>>(new[] { root.Ne, root.Nw, root.Se, root.Sw }.Where(x => x != null));
                current = root;
                return true;
            }

            if (progress.Count > 0)
            {
                var next = progress.Pop();
                current = next;

                foreach (var child in new[] { next.Ne, next.Nw, next.Se, next.Sw }.Where(x => x != null))
                    progress.Push(child);

                return true;
            }

            return false;
        }

        public void Reset()
        {
            progress = null;
            current = null;
        }

        object IEnumerator.Current => Current;

        public Tuple<Point, T> Current => new(current.Point, current.Value);

        public void Dispose()
        {
            progress = null;
        }
    }
}