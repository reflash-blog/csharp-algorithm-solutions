using System;
using System.Linq;

public class QuickSort {

 private Random rand = new Random();
    
 public int[] Sort(int[] a, int l, int r)
    {
        if (l >= r)
          return a;

        Swap(a, l, rand.Next(l, r)); // Random swap to make quicksort stable
        int x = a[l]; // pivot element

        int i = l, j = l, k = r; // 3-way partitioning
        
        while (i <= k){
          if (a[i] < x)
            Swap(a, j++, i++);
          else if (a[i] > x)
            Swap(a, i, k--);
          else i++;
        }
        

        Sort(a, l, j - 1);
        Sort(a, k + 1, r);

        return a;
    }

    public int[] Sort(int[] a)
    {
        return Sort(a, 0, a.Length - 1);
    }

    private void Swap(int[] a, int i, int j){
      int temp = a[i];
      a[i] = a[j];
      a[j] = temp;
    }   
}

public static class BinarySearch {

  public static int Search(int[] a, int key, bool direction)  
  { 
    int l = 0;
    int r = a.Length - 1; 
    int m = 0;

    while (l <= r)  
    {  
      m = l + (r - l) / 2;  
      Console.WriteLine("l {0} r {1} m {2}", l, r, m);
      
      if (key == a[m])
      {
        if (direction)
          if ((m+1) <= r && key != a[m+1]) 
            return m + 1;
          else if (m == r)
            return m + 1;
          else
            l = m + 1; 
        else
          if ((m-1) >= l && key != a[m-1]) 
            return m;
          else if (m == l)
            return m;
          else
            r = m - 1; 
      }
      else if (key < a[m])  
        r = m - 1;   
      else  
        l = m + 1;
    }  

    return key >= a[m] ? m + 1: m;  
  }
}

public class MainClass
{
    /*
      2 3
      0 5
      7 10
      1 6 11

      5 4
      2 12
      4 14
      6 16
      8 10
      18 20
      9 1 17 19
    */
    public static void Main()
    {
        int[] line = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        int n = line[0];
        int m = line[1];

        //Console.WriteLine("n {0} m {1}", n, m);

        int[] lefts = new int[n]; 
        int[] rights = new int[n];

        for (int i = 0; i < n; i++){
          line = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

          lefts[i] = line[0];
          rights[i] = line[1];
        }

        var sorter = new QuickSort();
        sorter.Sort(lefts);
        sorter.Sort(rights);

        Console.WriteLine(String.Join(" ", lefts));
        Console.WriteLine(String.Join(" ", rights));

        line = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        Console.WriteLine(String.Join(" ", line));

        for (int i = 0; i < m; i++){
          int el = line[i];
          int p1 = BinarySearch.Search(lefts, el, true);
          Console.WriteLine("p1");
          int p2 = BinarySearch.Search(rights, el, false);
          Console.WriteLine("el {0} p1 {1} p2 {2}", el, p1, p2);
          line[i] = p1 - p2;
        }

        Console.WriteLine(String.Join(" ", line));
    }
} 