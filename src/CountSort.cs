using System;
using System.Linq;

public class CountSortSolution {
  int MAX_M = 10000;

  public int[] Sort(int[] a) 
  {
      int n = a.Length;
      int[] B = new int[MAX_M];
      
      for (int i = 0; i < n; i++)
        B[a[i]-1]++;
      
      for (int i = 1; i < MAX_M; i++)
        B[i] += B[i-1];
      //Console.WriteLine(String.Join(" ", B));
      int[] a_n = new int[n];

      for (int i = n-1; i >= 0 ; i--){
        a_n[B[a[i]-1]-1] = a[i];
        B[a[i]-1]--;
      }

      return a_n;
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

        //Console.WriteLine(String.Join(" ", array));
        
        var solution = new CountSortSolution();
        int[] sorted = solution.Sort(array);
        
        Console.WriteLine(String.Join(" ", sorted));
    }
}