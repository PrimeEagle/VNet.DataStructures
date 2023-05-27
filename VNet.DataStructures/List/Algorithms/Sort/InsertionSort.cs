namespace VNet.DataStructures.List.Algorithms.Sort;

//Insertion sort is a sorting algorithm that builds the final sorted array one item at a time.
//It works the way we sort playing cards in our hands. It is much less efficient on large lists than more advanced algorithms such as Quick sort, Heap sort, or Merge sort.
public class InsertionSort : ISortAlgorithm
{
    public void Sort(ref int[] data)
    {
        for (var i = 1; i < data.Length; ++i)
        {
            var j = i;

            while (j > 0)
                if (data[j - 1] > data[j])
                {
                    data[j - 1] ^= data[j];
                    data[j] ^= data[j - 1];
                    data[j - 1] ^= data[j];

                    --j;
                }
                else
                {
                    break;
                }
        }
    }
}