namespace VNet.DataStructures.Algorithms.Sort
{
    // Selection sort is a sorting algorithm. It works by selecting the smallest element of the array and placing it at the head of the array.
    // Then the process is repeated for the remainder of the array, the next largest element is selected and put into the next slot, and so on down the line.
    public class SelectionSort : ISortAlgorithm
    {
        public void Sort(ref int[] data)
        {
            int min;

            for (int i = 0; i < data.Length - 1; ++i)
            {
                min = i;

                for (int j = i + 1; j < data.Length; ++j)
                {
                    if (data[j] < data[min])
                    {
                        min = j;
                    }
                }

                int temp = data[min];
                data[min] = data[i];
                data[i] = temp;
            }
        }
    }
}