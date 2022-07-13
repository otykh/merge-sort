using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public static class Algo
{
    public static bool IsPowerOfTwo(int x)
    {
        return (x != 0) && (x & (x - 1)) == 0;
    }
    public static int[] GenerateRandomIntArray(int arraySize)
    {
        int[] outputArray = new int[arraySize];
        Random rnd = new Random();
        for(int i = 0; i < arraySize; i++)
        {
            outputArray[i] = rnd.Next(0, 1000000);
        }
        return outputArray;
    }
    public static float[] GenerateRandomFloatArray(int arraySize)
    {
        float[] outputArray = new float[arraySize];
        Random rnd = new Random();
        for (int i = 0; i < arraySize; i++)
        {
            outputArray[i] = rnd.Next(0, 10);
        }
        return outputArray;
    }
    public static void MergeSort<T>(ref T[] arr) where T : IComparable<T>
    {
        //@TODO we here assume that the array is actually can be devided by two.
        //@TODO do checks if it can work like if array > 0, if array can be equally devided into two parts etc.
        MergeSort<T>(ref arr, 0, arr.Length - 1);
    }
    public static void MergeSort<T>(ref T[] arr, int startIndex, int endIndex) where T : IComparable<T>
    {
        if(endIndex == startIndex) { return; }

        if(endIndex - startIndex > 1) //More then two elements to be sorted
        {
            int middleIndex;
            if (IsPowerOfTwo(endIndex - startIndex + 1))
            {
                middleIndex = (int)Math.Round(((startIndex + endIndex) / 2.0f), MidpointRounding.ToZero);
            }
            else
            {
                middleIndex = (int)Math.Round(((startIndex + endIndex) / 2.0f), MidpointRounding.AwayFromZero);
            }
            MergeSort<T>(ref arr, startIndex, middleIndex);
            MergeSort<T>(ref arr, middleIndex + 1, endIndex);
            Merge<T>(ref arr, startIndex, middleIndex, endIndex);
        }
        else
        {
            SortTwo<T>(ref arr, startIndex, endIndex);
        }
    }
    private static void SortTwo<T>(ref T[] arr, int first, int second) where T : IComparable<T>
    {
        if(arr[first].CompareTo(arr[second]) >= 0)
        {
            SwapTwo(ref arr, first, second);
        }
    }
    private static void SwapTwo<T>(ref T[] arr, int first, int second) where T : IComparable<T>
    {
        T temp = arr[first];
        arr[first] = arr[second];
        arr[second] = temp;
    }
    private static void Merge<T>(ref T[] arr, int startIndex, int middleIndex, int endIndex) where T : IComparable<T>
    {
        int leftLenght = middleIndex - startIndex + 1;
        int rightLenght = endIndex - middleIndex;

        T[] leftArr = new T[leftLenght];
        T[] rightArr = new T[rightLenght];

        for(int left_i = 0; left_i < leftLenght; left_i++)
        {
            leftArr[left_i] = arr[startIndex + left_i];
        }

        for(int right_i = 0; right_i < rightLenght; right_i++)
        {
            rightArr[right_i] = arr[middleIndex + right_i + 1];
        }

        int i = 0;
        int j = 0;

        for(int p = startIndex; p <= endIndex; p++)
        {
            if (i >= leftArr.Length)
            {
                arr[p] = rightArr[j];
                j++;
            }
            else if (j >= rightArr.Length)
            {
                arr[p] = leftArr[i];
                i++;
            }
            else
            {
                if (leftArr[i].CompareTo(rightArr[j]) <= 0)
                {
                    arr[p] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[p] = rightArr[j];
                    j++;
                }
            }
        }
    }

    public static void MergeTest()
    {
        Console.WriteLine("Generating arrays: ");
        int[] smallArray = GenerateRandomIntArray(8);
        int[] smallArrayNotEven = GenerateRandomIntArray(13);

        float[] smallArrayFloat = GenerateRandomFloatArray(40);

        int[] bigArray = GenerateRandomIntArray(1000);
        int[] hugeArray = GenerateRandomIntArray(1000000);
        Console.WriteLine("Done");

        Console.WriteLine("Sorting generated arrays");

        int[] smallArraySorted = new int[smallArray.Length];
        Array.Copy(smallArray, smallArraySorted, smallArray.Length);
        int[] smallArrayNotEvenSorted = new int[smallArrayNotEven.Length];
        Array.Copy(smallArrayNotEven, smallArrayNotEvenSorted, smallArrayNotEven.Length);
        float[] smallArrayFloatSorted = new float[smallArrayFloat.Length];
        Array.Copy(smallArrayFloat, smallArrayFloatSorted, smallArrayFloat.Length);
        int[] bigArraySorted = new int[bigArray.Length];
        Array.Copy(bigArray, bigArraySorted, bigArray.Length);
        int[] hugeArraySorted = new int[hugeArray.Length];
        Array.Copy(hugeArray, hugeArraySorted, hugeArray.Length);

        Array.Sort(smallArraySorted);
        Array.Sort(smallArrayNotEvenSorted);
        Array.Sort(smallArrayFloatSorted);
        Array.Sort(bigArraySorted);
        Array.Sort(hugeArraySorted);
        Console.WriteLine("Copying and sorting is done!");

        Console.WriteLine("Doing merge sort");
        MergeTest<int>(smallArray, smallArraySorted);
        MergeTest<int>(smallArrayNotEven, smallArrayNotEvenSorted);
        MergeTest<float>(smallArrayFloat, smallArrayFloatSorted);
        MergeTest<int>(bigArray, bigArraySorted);
        MergeTest<int>(hugeArray, hugeArraySorted);
        Console.WriteLine("Done");
    }
    private static void MergeTest<T>(T[] array, T[] sortedArray) where T : IComparable<T>
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        MergeSort<T>(ref array);
        stopwatch.Stop();

        // stopwatch.ElapsedMilliseconds;

        for(int i = 0; i < array.Length; i++)
        {
            if(array[i].CompareTo(sortedArray[i]) != 0)
            {
                Console.WriteLine("Test failed!");
                return;
            }
        }

        Console.WriteLine("Test passed! " + stopwatch.ElapsedMilliseconds);
    }
}

