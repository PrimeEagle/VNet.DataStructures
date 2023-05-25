namespace VNet.DataStructures.Algorithms.Search;

// Binary search (a.k.a Half-Interval Search) algorithms finds the position of a target value within an array.
public class Binary
{
    public int Search(int[] data, int value)
    {
        int low = 0, high = data.Length - 1, midpoint = 0;

        while (low <= high)
        {
            midpoint = low + (high - low) / 2;

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