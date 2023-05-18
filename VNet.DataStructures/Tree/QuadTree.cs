using System.Collections;
using VNet.Mathematics.Geometry;

namespace VNet.DataStructures.Tree
{
    public class QuadTree<T> : IEnumerable<Tuple<Point, T>>
    {
        private int deletionCount;
        private QuadTreeNode<T> root;
        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tuple<Point, T>> GetEnumerator()
        {
            return new QuadTreeEnumerator<T>(root);
        }

        public void Insert(Point point, T value = default)
        {
            root = Insert(root, point, value);
            Count++;
        }

        private QuadTreeNode<T> Insert(QuadTreeNode<T> current, Point point, T value)
        {
            if (current == null) return new QuadTreeNode<T>(point, value);

            if (point.X < current.Point.X && point.Y < current.Point.Y)
                current.Sw = Insert(current.Sw, point, value);
            else if (point.X < current.Point.X && point.Y >= current.Point.Y)
                current.Nw = Insert(current.Nw, point, value);
            else if (point.X > current.Point.X && point.Y >= current.Point.Y)
                current.Ne = Insert(current.Ne, point, value);
            else if (point.X > current.Point.X && point.Y < current.Point.Y) current.Se = Insert(current.Se, point, value);

            return current;
        }

        public List<Tuple<Point, T>> RangeSearch(Rectangle searchWindow)
        {
            return RangeSearch(root, searchWindow, new List<Tuple<Point, T>>());
        }

        private List<Tuple<Point, T>> RangeSearch(QuadTreeNode<T> current, Rectangle searchWindow,
            List<Tuple<Point, T>> result)
        {
            if (current == null) return result;

            if (current.Point.X >= searchWindow.LeftTop.X
                && current.Point.X <= searchWindow.RightBottom.X
                && current.Point.Y <= searchWindow.LeftTop.Y
                && current.Point.Y >= searchWindow.RightBottom.Y
                && !current.IsDeleted)
                result.Add(new Tuple<Point, T>(current.Point, current.Value));

            if (searchWindow.LeftTop.X < current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
                RangeSearch(current.Sw, searchWindow, result);
            if (searchWindow.LeftTop.X < current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
                RangeSearch(current.Nw, searchWindow, result);
            if (searchWindow.RightBottom.X > current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
                RangeSearch(current.Ne, searchWindow, result);
            if (searchWindow.RightBottom.X > current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
                RangeSearch(current.Se, searchWindow, result);

            return result;
        }

        public void Delete(Point p)
        {
            var point = Find(root, p);

            if (point == null || point.IsDeleted) throw new Exception("Point not found.");

            point.IsDeleted = true;
            Count--;

            if (deletionCount == Count)
            {
                Reconstruct();
                deletionCount = 0;
            }
            else
            {
                deletionCount++;
            }
        }

        private void Reconstruct()
        {
            QuadTreeNode<T> newRoot = null;

            foreach (var exisiting in this) newRoot = Insert(newRoot, exisiting.Item1, exisiting.Item2);

            root = newRoot;
        }

        private QuadTreeNode<T> Find(QuadTreeNode<T> current, Point point)
        {
            if (current == null) return null;

            if (current.Point.X == point.X && current.Point.Y == point.Y) return current;


            if (point.X < current.Point.X && point.Y < current.Point.Y)
                return Find(current.Sw, point);
            if (point.X < current.Point.X && point.Y >= current.Point.Y)
                return Find(current.Nw, point);
            if (point.X > current.Point.X && point.Y >= current.Point.Y)
                return Find(current.Ne, point);
            if (point.X > current.Point.X && point.Y < current.Point.Y) return Find(current.Se, point);

            return null;
        }
    }
}
