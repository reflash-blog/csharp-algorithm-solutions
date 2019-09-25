using System;
using System.Linq;

public class LadderClimbSolution {

  public int MaxScore(int[] steps, int n){
    if (n == 1) return steps[0];

    int[] D = new int[n];
    D[0] = steps[0];
    D[1] = Math.Max(0, D[0]) + steps[1];
    
    for (int i = 2; i < n; i++){
      D[i] = Math.Max(D[i-1], D[i-2]) + steps[i];
    }

    return D[n-1];
  }

}

public class CalculatorSolution {

  public int[] MinSolution(int n){
    int[] D = new int[n];
    D[0] = 0;
    
    for (int i = 1; i < n; i++){
      int p = D[i - 1];
      int q = (i+1) % 2 == 0 ? D[(i+1) / 2 - 1] : (n+1); // n+1 = inf
      int r = (i+1) % 3 == 0 ? D[(i+1) / 3 - 1] : (n+1);
      //Console.WriteLine("n {0} p {1} q {2} r {3}", i+1, p, q, r);
      D[i] = Math.Min(p, Math.Min(q, r)) + 1;
    }

    //Console.WriteLine("============");
    int len = D[n-1];
    int[] s = new int[len+1];
    s[len] = n;
    int next = n;
    //Console.WriteLine(String.Join(" ", D));
    //Console.WriteLine(D[32076]);
    for (int i = len-1; i >= 0; i--){
      len--;
      // Console.WriteLine("Loop =====");
      // Console.WriteLine(next);
      // Console.WriteLine(len);

      // Console.WriteLine(D[next / 3 - 1]);
      // Console.WriteLine(D[next / 2 - 1]);
      // Console.WriteLine(D[next-2]);
      
      if (next % 3 == 0 && D[next / 3 - 1] == len)
        next = next / 3;
      else if (next % 2 == 0 && D[next / 2 - 1] == len)
        next = next / 2;
      else if (next > 0 && D[next - 2] == len)
        next--;
      else
        throw new Exception("Something's wrong!");
      s[i] = next;
    }

    return s;
  }

}

public class MainClass
{
    public static void Main()
    {
      // int n = Convert.ToInt32(Console.ReadLine());
      // int[] array = Console
      //       .ReadLine()
      //       .Split()
      //       .Select(int.Parse)
      //       .ToArray();

      // var s = new LadderClimbSolution();
      // Console.WriteLine(s.MaxScore(array, n));

      int n = Convert.ToInt32(Console.ReadLine());

      var s = new CalculatorSolution();
      var res = s.MinSolution(n);
      Console.WriteLine(res.Length - 1);
      Console.WriteLine(String.Join(" ", res));
    }
}