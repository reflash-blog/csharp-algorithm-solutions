using System;
using System.Linq;

public class MergeSortSolution {
 public int Count { get; set; }
    
 public int[] MergeSort(int[] a, int l, int r)
    {
        if (l < r)
        {
            int m = (l + r) / 2;
            Console.WriteLine("Splitting to ({0}, {1}) : ({2}, {3}) ranges", 
              l, m, m+1, r);
            return Merge(MergeSort(a, l, m), MergeSort(a, m + 1, r), l, m, r);
        }
        
        if (l == r) return new int[] {a[l]};
        else return new int[] {};
    }

    public int[] MergeSort(int[] a)
    {
        this.Count = 0;
        return MergeSort(a, 0, a.Length - 1);
    }

    private int[] Merge(int[] a, int[] b, int l, int m, int r)
    {
        Console.WriteLine("Merging the ({0}, {1}) : ({2}, {3}) range",
          l, m, m+1, r);
        Console.WriteLine("Arrays");
        Console.WriteLine(String.Join(" ", a));
        Console.WriteLine(String.Join(" ", b));

        int len = (r - l) + 1;
        Console.WriteLine("Len is " + len);
        int[] merged = new int[len];
        int j = 0, k = 0; 
        int j_len = m - l + 1, k_len = r - m;
        
        for (int i = 0; i < len; i++) {
            Console.WriteLine("k: {0}, j: {1}", k, j);
            if (k >= k_len || j < j_len && a[j] <= b[k])
              merged[i] = a[j++];
            else {
              merged[i] = b[k++];
              if(j < j_len){
                Console.WriteLine("Inversion k {0} j {1}", k, j);
                Console.WriteLine("j_len {0} k_len {1}", j_len, k_len);
              
                this.Count += j_len - j;
              }

            }
        }
        
        return merged;
    }   
}

public class MainClass
{
    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        int[] array = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        Console.WriteLine(String.Join(" ", array));
        
        var solution = new MergeSortSolution();
        int[] sorted = solution.MergeSort(array);
        
        Console.WriteLine(String.Join(" ", sorted));
        Console.WriteLine(solution.Count);
    }
}