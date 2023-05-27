namespace VNet.DataStructures.List.Algorithms.Sort;

//Heap sort is a comparison based sorting algorithm. It is similar to selection sort where we first find the maximum element and place the maximum element
//at the end. We repeat the same process for remaining element.
public class HeapSort : ISortAlgorithm
{
    public void Sort(ref int[] data)
    {
        var heapSize = data.Length;

        for (var p = (heapSize - 1) / 2; p >= 0; --p)
            MaxHeapify(ref data, heapSize, p);

        for (var i = data.Length - 1; i > 0; --i)
        {
            (data[i], data[0]) = (data[0], data[i]);

            --heapSize;
            MaxHeapify(ref data, heapSize, 0);
        }
    }

    private void MaxHeapify(ref int[] data, int heapSize, int index)
    {
        var left = (index + 1) * 2 - 1;
        var right = (index + 1) * 2;
        var largest = 0;

        if (left < heapSize && data[left] > data[index])
            largest = left;
        else
            largest = index;

        if (right < heapSize && data[right] > data[largest])
            largest = right;

        if (largest != index)
        {
            (data[index], data[largest]) = (data[largest], data[index]);

            MaxHeapify(ref data, heapSize, largest);
        }
    }
}