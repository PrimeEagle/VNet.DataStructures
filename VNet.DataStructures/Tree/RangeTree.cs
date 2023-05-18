using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class RangeTree<T> : IEnumerable<T[]> where T : IComparable
    {
        private readonly int dimensions;
        private readonly HashSet<T[]> items = new(new ArrayComparer<T>());

        private readonly OneDimentionalRangeTree<T> tree = new();

        public RangeTree(int dimensions)
        {
            if (dimensions <= 0) throw new ArgumentOutOfRangeException("Dimension should be greater than 0.");

            this.dimensions = dimensions;
        }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public void Insert(T[] value)
        {
            ValidateDimensions(value);

            if (items.Contains(value)) throw new Exception("value exists.");
            var currentTree = tree;

            for (var i = 0; i < dimensions; i++) currentTree = currentTree.Insert(value[i]).Tree;

            items.Add(value);
            Count++;
        }

        public void Delete(T[] value)
        {
            ValidateDimensions(value);

            if (!items.Contains(value)) throw new Exception("Item not found.");

            var found = false;
            DeleteRecursive(tree, value, 0, ref found);
            items.Remove(value);
            Count--;
        }

        private void DeleteRecursive(OneDimentionalRangeTree<T> tree, T[] value,
            int currentDimension, ref bool found)
        {
            var node = tree.Find(value[currentDimension]);

            if (node != null)
            {
                if (currentDimension + 1 == dimensions)
                    found = true;
                else
                    DeleteRecursive(node.Tree, value, currentDimension + 1, ref found);
            }

            if (node != null && found && (currentDimension + 1 == dimensions
                                          || node.Tree.Count == 0 && currentDimension + 1 < dimensions))
                tree.Delete(value[currentDimension]);
        }

        public List<T[]> RangeSearch(T[] start, T[] end)
        {
            ValidateDimensions(start);
            ValidateDimensions(end);

            return RangeSearch(tree, start, end, 0);
        }

        private List<T[]> RangeSearch(
            OneDimentionalRangeTree<T> currentTree,
            T[] start, T[] end, int dimension)
        {
            var nodes = currentTree.RangeSearch(start[dimension], end[dimension]);

            if (dimension + 1 == dimensions)
            {
                var result = new List<T[]>();

                foreach (var value in nodes.SelectMany(x => x.Values))
                {
                    var thisDimResult = new T[dimensions];
                    thisDimResult[dimension] = value;
                    result.Add(thisDimResult);
                }

                return result;
            }
            else
            {
                var result = new List<T[]>();

                foreach (var node in nodes)
                {
                    var nextDimResult = RangeSearch(node.Tree, start, end, dimension + 1);

                    foreach (var value in node.Values)
                        foreach (var nextResult in nextDimResult)
                        {
                            nextResult[dimension] = value;
                            result.Add(nextResult);
                        }
                }

                return result;
            }
        }

        private void ValidateDimensions(T[] start)
        {
            if (start == null) throw new ArgumentNullException(nameof(start));

            if (start.Length != dimensions) throw new Exception($"Expecting {dimensions} points.");
        }
    }
}