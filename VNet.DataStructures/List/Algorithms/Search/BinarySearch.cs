using VNet.System;

namespace VNet.DataStructures.List.Algorithms.Search;

// Binary search (a.k.a Half-Interval Search) algorithms finds the position of a target value within an array.
public class BinarySearch<T> where T : notnull, IComparable<T>
{
    public int Search(IListSearchAlgorithmArgs<T> args)
    {
        int low = 0, high = args.List.Count - 1;

        while (low <= high)
        {
            var midpoint = low + (high - low) / 2;

            int strCompare;
            if (typeof(T) == typeof(string) && args.HasWildcards && (args.Value.ToString() ?? string.Empty).EqualsWildcard(args.List[midpoint].ToString() ?? string.Empty))
            {
                strCompare = 0;
            }
            else
            {
                strCompare = args.Value.CompareTo(args.List[midpoint]);
            }

            switch (strCompare)
            {
                case 0:
                    return midpoint;
                case < 0:
                    high = midpoint - 1;
                    break;
                default:
                    low = midpoint + 1;
                    break;
            }
        }

        return -1;
    }
}