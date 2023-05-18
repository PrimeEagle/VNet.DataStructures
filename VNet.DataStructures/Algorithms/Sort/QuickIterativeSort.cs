namespace VNet.DataStructures.Algorithms.Sort
{
    // Quick sort is an efficient sorting algorithm, serving as a systematic method for placing the elements of an array in order.
    public class QuickIterativeSort : ISortAlgorithm
    {
        public void Sort(ref int[] data)
        {
            int startIndex = 0;
            int endIndex = data.Length - 1;
            int top = -1;
            int[] stack = new int[data.Length];

            stack[++top] = startIndex;
            stack[++top] = endIndex;

            while (top >= 0)
            {
                endIndex = stack[top--];
                startIndex = stack[top--];

                int p = Partition(ref data, startIndex, endIndex);

                if (p - 1 > startIndex)
                {
                    stack[++top] = startIndex;
                    stack[++top] = p - 1;
                }

                if (p + 1 < endIndex)
                {
                    stack[++top] = p + 1;
                    stack[++top] = endIndex;
                }
            }
        }

        private int Partition(ref int[] data, int left, int right)
        {
            int x = data[right];
            int i = (left - 1);

            for (int j = left; j <= right - 1; ++j)
            {
                if (data[j] <= x)
                {
                    ++i;
                    Swap(ref data[i], ref data[j]);
                }
            }

            Swap(ref data[i + 1], ref data[right]);

            return (i + 1);
        }

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}