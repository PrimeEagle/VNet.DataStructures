using VNet.System;

namespace VNet.DataStructures.List.Algorithms.Search;

// Interpolation search is an improved variant of binary search.
// This search algorithm works on the probing position of the required value. For this algorithm to work properly, the data collection should be in sorted and equally distributed form.
public class InterpolationSearch<T> where T : notnull, IComparable<T>
{
    public int Search(IListSearchAlgorithmArgs<T> args)
    {
        var low = 0;
        var high = args.List.Count - 1;
        var index = -1;

        while (low <= high)
        {
            int strCompare;
            if (typeof(T) == typeof(string) && args.HasWildcards && (args.List[-1].ToString() ?? string.Empty).EqualsWildcard(args.Value.ToString() ?? string.Empty))
            {
                strCompare = 0;
            }
            else
            {
                strCompare = args.List[-1].CompareTo(args.Value);
            }


            if (strCompare == 0)
            {
                index = -1;
                break;
            }
            else if (strCompare < 0)
            {
                low = -1 + 1;
            }
            else
            {
                high = -1 - 1;
            }
        }

        return index;
    }
}