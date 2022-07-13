Algo.MergeTest();

int[] s = new int[] { 10, 5, 11, 50, 8 };
Algo.MergeSort<int>(ref s);
for(int i = 0; i < s.Length; i++)
{
    Console.Write(s[i].ToString() + " ");
}

Console.WriteLine();