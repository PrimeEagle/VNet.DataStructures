namespace VNet.DataStructures.List.Algorithms.Sort;

// Quick sort is an efficient sorting algorithm, serving as a systematic method for placing the elements of an array in order.
public class QuickRecursiveSort : ISortAlgorithm
{
    public void Sort(ref int[] data, int left, int right)
    {
        if (left >= right) return;
        var q = Partition(ref data, left, right);
        Sort(ref data, left, q - 1);
        Sort(ref data, q + 1, right);
    }

    private int Partition(ref int[] data, int left, int right)
    {
        var pivot = data[right];
        var i = left;

        for (var j = left; j < right; ++j)
            if (data[j] <= pivot)
            {
                (data[j], data[i]) = (data[i], data[j]);
                i++;
            }

        data[right] = data[i];
        data[i] = pivot;

        return i;
    }
}