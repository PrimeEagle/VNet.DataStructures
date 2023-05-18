namespace VNet.DataStructures.Algorithms.Sort
{
    //Bubble sort (a.k.a Sinking Sort and Comparison Sort) is a sorting algorithm that works by repeatedly swapping and adjacent elements if they are in wrong order.
    public class BubbleSort : ISortAlgorithm
    {
        public void Sort(ref int[] data)
        {
            for (int i = 1; i < data.Length; ++i)
            {
                for (int j = 0; j < data.Length - i; ++j)
                {
                    if (data[j] > data[j + 1])
                    {
                        data[j] ^= data[j + 1];
                        data[j + 1] ^= data[j];
                        data[j] ^= data[j + 1];
                    }
                }
            }
        }
    }
}