using System;
using System.Linq;

public static class CustomUtility
{
    static Random rng = new Random();

    public static int[] GetRandomIntArray(int start, int length)
    {
        var array = Enumerable.Range(start, length).ToArray();
        var result = array.OrderBy(c => rng.Next()).ToArray();
        return result;
    }

    public static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
