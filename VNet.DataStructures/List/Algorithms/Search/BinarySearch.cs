namespace VNet.DataStructures.List.Algorithms.Search;

// Binary search (a.k.a Half-Interval Search) algorithms finds the position of a target value within an array.
public class BinarySearch
{
    public int Search(int[] data, int value)
    {
        int low = 0, high = data.Length - 1;

        while (low <= high)
        {
            var midpoint = low + (high - low) / 2;

            if (value == data[midpoint])
                return midpoint;
            if (value < data[midpoint])
                high = midpoint - 1;
            else
                low = midpoint + 1;
        }

        return -1;
    }
}