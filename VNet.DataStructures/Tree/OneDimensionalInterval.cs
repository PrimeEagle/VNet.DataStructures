namespace VNet.DataStructures.Tree
{
    internal class OneDimentionalInterval<T> : IComparable where T : IComparable
    {
        public OneDimentionalInterval(T start, T end, Lazy<T> defaultValue)
        {
            Start = start;
            End = new List<T> { end };
            NextDimensionIntervals = new OneDimentionalIntervalTree<T>(defaultValue);
        }

        public T Start { get; set; }
        public List<T> End { get; set; }
        internal T MaxEnd { get; set; }
        internal OneDimentionalIntervalTree<T> NextDimensionIntervals { get; set; }
        internal int MatchingEndIndex { get; set; }

        public int CompareTo(object obj)
        {
            return Start.CompareTo(((OneDimentionalInterval<T>)obj).Start);
        }
    }

    internal class IntervalComparer<T> : IEqualityComparer<Tuple<T[], T[]>> where T : IComparable
    {
        public bool Equals(Tuple<T[], T[]> x, Tuple<T[], T[]> y)
        {
            if (x == y) return true;

            for (var i = 0; i < x.Item1.Length; i++)
            {
                if (!x.Item1[i].Equals(y.Item1[i])) return false;

                if (!x.Item2[i].Equals(y.Item2[i])) return false;
            }

            return true;
        }

        public int GetHashCode(Tuple<T[], T[]> x)
        {
            unchecked
            {
                if (x == null) return 0;
                var hash = 17;
                foreach (var element in x.Item1) hash = hash * 31 + element.GetHashCode();
                foreach (var element in x.Item2) hash = hash * 31 + element.GetHashCode();
                return hash;
            }
        }
    }
}