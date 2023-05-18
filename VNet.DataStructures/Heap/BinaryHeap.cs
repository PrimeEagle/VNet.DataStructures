using System.Xml.Linq;

namespace VNet.DataStructures.Heap
{
    public class BinaryHeap
    {
        #region Instance variables

        private readonly SimpleHeapNode[] data;
        private readonly double[] priorities;

        #endregion

        public BinaryHeap(int capacity)
        {
            data = new SimpleHeapNode[capacity];
            priorities = new double[capacity];
            Count = 0;
        }

        public void Add(SimpleHeapNode item, double priority)
        {
            if (Count == data.Length)
                throw new Exception("Heap capacity exceeded");

            int position = Count++;
            data[position] = item;
            item.QueuePosition = position;
            priorities[position] = priority;

            MoveUp(position);

        }

        public SimpleHeapNode ExtractMin()
        {
            var minNode = data[0];
            Swap(0, Count - 1);
            Count--;
            MoveDown(0);
            return minNode;
        }

        public void DecreasePriority(SimpleHeapNode n, double priority)
        {
            var position = n.QueuePosition;
            while ((position > 0) && (priorities[Parent(position)] > priority))
            {
                var originalParentPos = Parent(position);
                Swap(originalParentPos, position);
                position = originalParentPos;
            }
            priorities[position] = priority;
        }

        private void MoveUp(int position)
        {
            while ((position > 0) && (priorities[Parent(position)] > priorities[position]))
            {
                var originalParentPos = Parent(position);
                Swap(position, originalParentPos);
                position = originalParentPos;
            }
        }

        private void MoveDown(int position)
        {
            while (true)
            {
                var leftChild = LeftChild(position);
                var rightChild = RightChild(position);
                var largest = 0;
                if ((leftChild < Count) && (priorities[leftChild] < priorities[position]))
                {
                    largest = leftChild;
                }
                else
                {
                    largest = position;
                }

                if ((rightChild < Count) && (priorities[rightChild] < priorities[largest]))
                {
                    largest = rightChild;
                }

                if (largest != position)
                {
                    Swap(position, largest);
                    position = largest;
                    continue;
                }

                break;
            }
        }

        public int Count { get; private set; }

        #region Utilities

        private void Swap(int position1, int position2)
        {
            (data[position2], data[position1]) = (data[position1], data[position2]);
            data[position1].QueuePosition = position1;
            data[position2].QueuePosition = position2;

            (priorities[position1], priorities[position2]) = (priorities[position2], priorities[position1]);
        }

        private static int Parent(int position)
        {
            return (position - 1) / 2;
        }

        private static int LeftChild(int position)
        {
            return 2 * position + 1;
        }

        private static int RightChild(int position)
        {
            return 2 * position + 2;
        }

        public void TestHeapValidity()
        {
            for (var i = 1; i < Count; i++)
                if (priorities[Parent(i)] > priorities[i])
                    throw new Exception("Heap violates the Heap Property at position " + i);
        }
        #endregion
    }
}