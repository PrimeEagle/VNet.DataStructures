namespace VNet.DataStructures.Heap
{
    public class MinHeap
    {
        public MinHeap(int[] input, int length)
        {

            this.Length = length;
            this.Array = input;
            BuildMinHeap();
        }

        public int[] Array { get; private set; }

        public int Length { get; private set; }

        private void BuildMinHeap()
        {
            for (var i = this.Length / 2; i > 0; i--)
            {
                MinHeapify(i);
            }

            return;
        }

        public void MinHeapify(int index)
        {
            while (true)
            {
                var left = 2 * index;
                var right = 2 * index + 1;

                var min = index;
                if (left <= this.Length && this.Array[left - 1] < this.Array[index - 1])
                {
                    min = left;
                }

                if (right <= this.Length && this.Array[right - 1] < this.Array[min - 1])
                {
                    min = right;
                }

                if (min != index)
                {
                    (this.Array[min - 1], this.Array[index - 1]) = (this.Array[index - 1], this.Array[min - 1]);
                    index = min;
                    continue;
                }

                return;
                break;
            }
        }

        public int RemoveMinimum()
        {
            var minimum = this.Array[0];

            this.Array[0] = this.Array[this.Length - 1];
            this.Length--;
            MinHeapify(1);
            return minimum;
        }
    }
}
