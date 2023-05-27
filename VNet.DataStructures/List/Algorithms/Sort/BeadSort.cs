namespace VNet.DataStructures.List.Algorithms.Sort;

//Bead sort, also known as gravity sort, is a natural sorting algorithm, developed by Joshua J. Arulanandham, Cristian S. Calude and Michael J. Dinneen in 2002.
//Both digital and analog hardware implementations of bead sort can achieve a sorting time of O(n); however, the implementation of this algorithm tends to be
//significantly slower in software and can only be used to sort lists of positive integers. Also, it would seem that even in the best case, the algorithm requires O(n2) space.
public class BeadSort : ISortAlgorithm
{
    public void Sort(ref int[] data)
    {
        int i, j, max;

        for (i = 1, max = data[0]; i < data.Length; ++i)
            if (data[i] > max)
                max = data[i];

        var beads = new byte[max * data.Length];

        for (i = 0; i < data.Length; ++i)
        for (j = 0; j < data[i]; ++j)
            beads[i * max + j] = 1;

        for (j = 0; j < max; ++j)
        {
            int sum;
            for (sum = i = 0; i < data.Length; ++i)
            {
                sum += beads[i * max + j];
                beads[i * max + j] = 0;
            }

            for (i = data.Length - sum; i < data.Length; ++i)
                beads[i * max + j] = 1;
        }

        for (i = 0; i < data.Length; ++i)
        {
            for (j = 0; j < max && Convert.ToBoolean(beads[i * max + j]); ++j) ;
            data[i] = j;
        }
    }
}