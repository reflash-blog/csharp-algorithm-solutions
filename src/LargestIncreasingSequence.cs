using System;
using System.Linq;

public static class MethodExt {
  private static Random randNum = new Random();

  public static void Populate<T>(this T[] arr, T value ) {
    for ( int i = 0; i < arr.Length;i++ ) {
      arr[i] = value;
    }
  }

  public static int[] RandomInts(int min, int max, int len){
    return Enumerable
        .Repeat(0, len)
        .Select(i => randNum.Next(min, max))
        .ToArray();
  }
}

public class LargestIncreasingSequenceSolution {
  
  public int IndexOfMax(int[] a) 
  {
      int n = a.Length;
      int[] D = new int[n];

      for (int i = 0; i < n; i++){
        D[i] = 1;
        for (int j = 0; j < i; j++){
          if (a[i] % a[j] == 0  && (D[j]+1) > D[i])
            D[i] = D[j] + 1;
        }
      }

      //Console.WriteLine(String.Join(" ", D));

      return D.Max();
  }

  // Binary search (note boundaries  
  // in the caller) A[] is ceilIndex  
  // in the caller 
  private int CeilIndex(int[] T, int[] A, int l, 
                        int r, int key) 
  { 
      while (r - l > 1) 
      { 
          int m = l + (r - l)/2; 
          
          if (A[T[m]] == key && 
              m+1 < r && 
              A[T[m+1]] != key)
            return m+1;

          if (A[T[m]] > key) 
              r = m; 
          else
              l = m; 
      } 

      return r; 
  } 

  public int[] SequenceMaxFast(int[] A, int size) 
  {
      int[] T = new int[size]; 
      int[] R = new int[size];
      R.Populate(-1);

      int len;

      T[0] = 0; 
      len = 1; 
      for (int i = 1; i < size; i++) {
          if (A[i] < A[T[0]]) 
              T[0] = i; 
          else if (A[i] >= A[T[len-1]]) {
              R[i] = T[len-1];
              T[len++] = i; 
          }
          else {
              int ind = CeilIndex(T, A, -1, len-1, A[i]);
              T[ind] = i; 
              R[i] = T[ind-1];
          }
          // Console.WriteLine("T/R " + A[i]);
          // Console.WriteLine(String.Join(" ", T));
          // Console.WriteLine(String.Join(" ", R));
          // Console.WriteLine(len);
          // Console.WriteLine();
      }

      int[] res = new int[len];
      int p = T[len-1];
      //res[len-1] = A[p];
      
      res[len-1] = p;
      for (int i = len-1; i > 0; i--){
        p = R[p];
        //res[i-1] = A[p];
        res[i-1] = p;
      }

      return res; 
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

        //array = MethodExt.RandomInts(1, 15, 10);
        //n = array.Length;
        //Array.Reverse(array);

        //Console.WriteLine(String.Join(" ", array));
        Array.Reverse(array);
        //Console.WriteLine();
        
        var solution = new LargestIncreasingSequenceSolution();
        int[] res = solution.SequenceMaxFast(array, n);
        
        Array.Reverse(res);
        res = res.Select(x=> n - x).ToArray();
        Console.WriteLine(res.Length);
        Console.WriteLine(String.Join(" ", res));
    }
}