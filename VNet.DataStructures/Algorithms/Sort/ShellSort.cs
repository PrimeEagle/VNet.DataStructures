namespace VNet.DataStructures.Algorithms.Sort
{
    // Shell sort (a.k.a Shell's Method and Diminishing Increment Sort) is an in-place comparison sort algorithm.
    // The method starts by sorting pairs of elements far apart from each other, then progressively reducing the gap between elements to be compared.
    // Starting with far apart elements can move some out-of-place elements into position faster than a simple nearest neighbor exchange.
    public class ShellSort : ISortAlgorithm
    {
        public void Sort(ref int[] data)
        {
            int hSort = 1;

            while (hSort < data.Length / 3)
                hSort = (3 * hSort) + 1;

            while (hSort >= 1)
            {
                for (int i = hSort; i < data.Length; i++)
                {
                    for (int a = i; a >= hSort && (data[a] < data[a - hSort]); a -= hSort)
                    {
                        data[a] ^= data[a - hSort];
                        data[a - hSort] ^= data[a];
                        data[a] ^= data[a - hSort];
                    }
                }

                hSort /= 3;
            }
        }
    }
}