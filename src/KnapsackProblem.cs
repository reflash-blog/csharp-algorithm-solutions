using System;
using System.Linq;

public class KnapsackWeightSolution {

  public int MaxWeight(int W, int[] itemWeights, int n){
    int[,] D = new int[W+1, n+1];

    for (int w = 0; w <= W; w++)
      D[w,0] = 0;
    for (int i = 0; i <= n; i++)
      D[0,i] = 0;

    for (int i = 1; i <= n; i++){
      int wi = itemWeights[i-1];
      for (int w = 1; w <= W; w++){
        D[w,i] = D[w, i-1];
        if (wi <= w){
          D[w,i] = Math.Max(D[w,i], D[w-wi, i-1]+wi);
        }
      }
    }

    return D[W,n];
  }

}

public class MainClass
{
    public static void Main()
    {
      int[] array = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();
      int W = array[0], n = array[1];

      int[] weights = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();
      var s = new KnapsackWeightSolution();
      Console.WriteLine(s.MaxWeight(W,weights, n));
    }
}