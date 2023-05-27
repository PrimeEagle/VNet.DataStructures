namespace VNet.DataStructures.List.Algorithms.Search;

// Interpolation search is an improved variant of binary search.
// This search algorithm works on the probing position of the required value. For this algorithm to work properly, the data collection should be in sorted and equally distributed form.
public class Interpolation
{
    public int Search(int[] list, int value)
    {
        var lo = 0;
        var mid = -1;
        var hi = list.Length - 1;
        var index = -1;

        while (lo <= hi)
        {
            mid = (int)(lo + (double)(hi - lo) / (list[hi] - list[lo]) * (value - list[lo]));

            if (list[mid] == value)
            {
                index = mid;
                break;
            }

            if (list[mid] < value)
                lo = mid + 1;
            else
                hi = mid - 1;
        }

        return index;
    }
}