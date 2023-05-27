namespace VNet.DataStructures.List.Algorithms.Sort;

//Merge sort is a Divide and Conquer algorithm. It divides input array in two halves, calls itself for the two halves and then merges the two sorted halves.
public class MergeIterativeSort : ISortAlgorithm
{
    public void Sort(ref int[] data)
    {
        int currentSize;
        int leftStart;

        for (currentSize = 1; currentSize <= data.Length - 1; currentSize = 2 * currentSize)
        for (leftStart = 0; leftStart < data.Length - 1; leftStart += 2 * currentSize)
        {
            var mid = leftStart + currentSize - 1;
            var rightEnd = Math.Min(leftStart + 2 * currentSize - 1, data.Length - 1);

            Merge(ref data, leftStart, mid, rightEnd);
        }
    }

    private void Merge(ref int[] data, int left, int mid, int right)
    {
        int i, j;
        var n1 = mid - left + 1;
        var n2 = right - mid;
        var L = new int[n1];
        var R = new int[n2];

        for (i = 0; i < n1; i++)
            L[i] = data[left + i];

        for (j = 0; j < n2; j++)
            R[j] = data[mid + 1 + j];

        i = 0;
        j = 0;
        var k = left;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                data[k] = L[i];
                i++;
            }
            else
            {
                data[k] = R[j];
                j++;
            }

            k++;
        }

        while (i < n1)
        {
            data[k] = L[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            data[k] = R[j];
            j++;
            k++;
        }
    }
}