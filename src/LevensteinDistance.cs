using System;
using System.Linq;

public class LevensteinDistanceSolution {

  private static void Print2DArray<T>(T[,] matrix)
  {
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
          for (int j = 0; j < matrix.GetLength(1); j++)
          {
              Console.Write(matrix[i,j] + "\t");
          }
          Console.WriteLine();
      }
  }

  public static int LevensteinDistance(string a, string b){
    int n = a.Length + 1, m = b.Length + 1;
    int[,] D = new int[n,m];

    for (int i = 0; i < n; i++)
      D[i,0] = i;
    for (int j = 0; j < m; j++)
      D[0,j] = j;

    for (int i = 1; i < n; i++)
      for (int j = 1; j < m; j++)
        D[i,j] = 
        Math.Min(D[i-1,j] + 1, 
        Math.Min(D[i,j-1]+1, 
                 D[i-1,j-1] + (a[i-1] != b[j-1] ? 1 : 0)
        ));
    //Console.WriteLine(a);
    //Console.WriteLine(b);
    //Print2DArray(D);
    
    return D[n-1,m-1];
  }

}

public class MainClass
{
    public static void Main()
    {
        string a = Console.ReadLine();
        string b = Console.ReadLine();

        Console.WriteLine(LevensteinDistanceSolution.LevensteinDistance(a,b));
    }
}