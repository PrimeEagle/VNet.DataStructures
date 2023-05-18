namespace VNet.DataStructures.Tree
{
    public interface IDistanceCalculator<T> where T : IComparable
    {
        int Compare(T[] a, T[] b, T[] point);
        int Compare(T a, T b, T[] start, T[] end);
    }
}