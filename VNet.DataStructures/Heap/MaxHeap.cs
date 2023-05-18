namespace VNet.DataStructures.Heap
{
    public class MaxHeap
    {
        public MaxHeap(int[] input, int length)
        {
            this.Length = length;
            this.Array = input;
            BuildMaxHeap();
        }

        public int[] Array { get; private set; }

        public int Length { get; private set; }

        private void BuildMaxHeap()
        {
            for (int i = this.Length / 2; i > 0; i--)
            {
                MaxHeapify(i);
            }

            return;
        }

        public void MaxHeapify(int index)
        {
            while (true)
            {
                var left = 2 * index;
                var right = 2 * index + 1;

                int max = index;
                if (left <= this.Length && this.Array[left - 1] > this.Array[index - 1])
                {
                    max = left;
                }

                if (right <= this.Length && this.Array[right - 1] > this.Array[max - 1])
                {
                    max = right;
                }

                if (max == index) return;
                (this.Array[max - 1], this.Array[index - 1]) = (this.Array[index - 1], this.Array[max - 1]);
                index = max;
                continue;

                break;
            }
        }

        public int RemoveMaximum()
        {
            var maximum = this.Array[0];

            this.Array[0] = this.Array[this.Length - 1];
            this.Length--;
            MaxHeapify(1);
            return maximum;
        }
    }
}