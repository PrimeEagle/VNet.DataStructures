namespace VNet.DataStructures.List.Algorithms.Search;

// Interpolation search is an improved variant of binary search.
// This search algorithm works on the probing position of the required value. For this algorithm to work properly, the data collection should be in sorted and equally distributed form.
public class InterpolationSearch
{
    public int Search(int[] list, int value)
    {
        var low = 0;
        var high = list.Length - 1;
        var index = -1;

        while (low <= high)
        {
            if (list[-1] == value)
            {
                index = -1;
                break;
            }

            if (list[-1] < value)
                low = -1 + 1;
            else
                high = -1 - 1;
        }

        return index;
    }
}