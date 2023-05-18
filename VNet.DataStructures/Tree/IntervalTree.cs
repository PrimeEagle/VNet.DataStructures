using System.Collections;
using System.Reflection;

namespace VNet.DataStructures.Tree
{
    public class IntervalTree<T> : IEnumerable<Tuple<T[], T[]>> where T : IComparable
    {
        private readonly Lazy<T> defaultValue = new(() =>
        {
            var s = typeof(T);

            bool isValueType;

#if NET40
            isValueType = s.IsValueType;
#else
            isValueType = s.GetTypeInfo().IsValueType;
#endif

            if (isValueType) return (T)Convert.ChangeType(int.MinValue, s);

            return default;
        });

        private readonly int dimensions;
        private readonly OneDimentionalIntervalTree<T> tree;
        private readonly HashSet<Tuple<T[], T[]>> items = new(new IntervalComparer<T>());


        public IntervalTree(int dimension)
        {
            if (dimension <= 0) throw new ArgumentOutOfRangeException("Dimension should be greater than 0.");

            dimensions = dimension;
            tree = new OneDimentionalIntervalTree<T>(defaultValue);
        }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tuple<T[], T[]>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public void Insert(T[] start, T[] end)
        {
            ValidateDimensions(start, end);

            if (items.Contains(new Tuple<T[], T[]>(start, end))) throw new ArgumentOutOfRangeException("Inteval already exists.");

            var currentTrees = new List<OneDimentionalIntervalTree<T>> { tree };

            for (var i = 0; i < dimensions; i++)
            {
                var allOverlaps = new List<OneDimentionalIntervalTree<T>>();

                foreach (var tree in currentTrees)
                {
                    tree.Insert(new OneDimentionalInterval<T>(start[i], end[i], defaultValue));

                    var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[i], end[i], defaultValue));
                    foreach (var overlap in overlaps) allOverlaps.Add(overlap.NextDimensionIntervals);
                }

                currentTrees = allOverlaps;
            }

            items.Add(new Tuple<T[], T[]>(start, end));

            Count++;
        }

        public void Delete(T[] start, T[] end)
        {
            if (!items.Contains(new Tuple<T[], T[]>(start, end))) throw new ArgumentOutOfRangeException("Inteval does not exist.");

            ValidateDimensions(start, end);

            var allOverlaps = new List<OneDimentionalIntervalTree<T>>();
            var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[0], end[0], defaultValue));

            foreach (var overlap in overlaps) allOverlaps.Add(overlap.NextDimensionIntervals);

            DeleteOverlaps(allOverlaps, start, end, 1);
            tree.Delete(new OneDimentionalInterval<T>(start[0], end[0], defaultValue));

            items.Remove(new Tuple<T[], T[]>(start, end));
            Count--;
        }

        private void DeleteOverlaps(List<OneDimentionalIntervalTree<T>> currentTrees, T[] start, T[] end, int index)
        {
            //base case
            if (index == start.Length)
                return;

            var allOverlaps = new List<OneDimentionalIntervalTree<T>>();

            foreach (var tree in currentTrees)
            {
                var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[index], end[index], defaultValue));

                foreach (var overlap in overlaps) allOverlaps.Add(overlap.NextDimensionIntervals);
            }

            DeleteOverlaps(allOverlaps, start, end, ++index);

            index--;

            foreach (var tree in allOverlaps)
                if (tree.Count > 0)
                    tree.Delete(new OneDimentionalInterval<T>(start[index], end[index], defaultValue));
        }

        public bool DoOverlap(T[] start, T[] end)
        {
            ValidateDimensions(start, end);

            var allOverlaps = GetOverlaps(tree, start, end, 0);

            return allOverlaps.Count > 0;
        }

        public List<Tuple<T[], T[]>> GetOverlaps(T[] start, T[] end)
        {
            return GetOverlaps(tree, start, end, 0);
        }

        private List<Tuple<T[], T[]>> GetOverlaps(OneDimentionalIntervalTree<T> currentTree,
            T[] start, T[] end, int dimension)
        {
            var nodes = currentTree.GetOverlaps(new OneDimentionalInterval<T>(start[dimension], end[dimension],
                defaultValue));

            if (dimension + 1 == dimensions)
            {
                var result = new List<Tuple<T[], T[]>>();

                foreach (var node in nodes)
                {
                    var fStart = new T[dimensions];
                    var fEnd = new T[dimensions];

                    fStart[dimension] = node.Start;
                    fEnd[dimension] = node.End[node.MatchingEndIndex];

                    var thisDimResult = new Tuple<T[], T[]>(fStart, fEnd);

                    result.Add(thisDimResult);
                }

                return result;
            }
            else
            {
                var result = new List<Tuple<T[], T[]>>();

                foreach (var node in nodes)
                {
                    var nextDimResult = GetOverlaps(node.NextDimensionIntervals, start, end, dimension + 1);

                    foreach (var nextResult in nextDimResult)
                    {
                        nextResult.Item1[dimension] = node.Start;
                        nextResult.Item2[dimension] = node.End[node.MatchingEndIndex];

                        result.Add(nextResult);
                    }
                }

                return result;
            }
        }

        private void ValidateDimensions(T[] start, T[] end)
        {
            if (start == null) throw new ArgumentNullException(nameof(start));

            if (end == null) throw new ArgumentNullException(nameof(end));

            if (start.Length != dimensions || start.Length != end.Length)
                throw new ArgumentOutOfRangeException($"Expecting {dimensions} points in start and end values for this interval.");

            if (start.Where((t, i) => t.Equals(defaultValue.Value)
                                      || end[i].Equals(defaultValue.Value)).Any())
                throw new ArgumentOutOfRangeException("Points cannot contain Minimum Value or Null values");
        }
    }
}