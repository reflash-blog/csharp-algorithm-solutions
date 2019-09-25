using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExt {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T element in source)
        {
            action(element);
        }
    }
}

public class NodeInfo
{
    public TreeNode Node;
    public string Text;
    public int StartPos;
    public int Size { get { return Text.Length; } }
    public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
    public NodeInfo Parent, Left, Right;
}

public static class TreeNodePrinter 
{
  public static void Print(this TreeNode root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
    {
        if (root == null) return;
        int rootTop = Console.CursorTop + topMargin;
        var last = new List<NodeInfo>();
        var next = root;
        for (int level = 0; next != null; level++)
        {
            var item = new NodeInfo { Node = next, Text = next.item };
            if (level < last.Count)
            {
                item.StartPos = last[level].EndPos + spacing;
                last[level] = item;
            }
            else
            {
                item.StartPos = leftMargin;
                last.Add(item);
            }
            if (level > 0)
            {
                item.Parent = last[level - 1];
                if (next == item.Parent.Node.left)
                {
                    item.Parent.Left = item;
                    item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                }
                else
                {
                    item.Parent.Right = item;
                    item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                }
            }
            next = next.left ?? next.right;
            for (; next == null; item = item.Parent)
            {
                int top = rootTop + 2 * level;
                Console.WriteLine("Printing: " + item.Text);
                Print(item.Text, top, item.StartPos);
                if (item.Left != null)
                {
                    Print("/", top + 1, item.Left.EndPos);
                    Print("_", top, item.Left.EndPos + 1, item.StartPos);
                }
                if (item.Right != null)
                {
                    Print("_", top, item.EndPos, item.Right.StartPos - 1);
                    Print("\\", top + 1, item.Right.StartPos - 1);
                }
                if (--level < 0) break;
                if (item == item.Parent.Left)
                {
                    item.Parent.StartPos = item.EndPos + 1;
                    next = item.Parent.Node.right;
                }
                else
                {
                    if (item.Parent.Left == null)
                        item.Parent.EndPos = item.StartPos - 1;
                    else
                        item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                }
            }
        }
        Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
    }

    private static void Print(string s, int top, int left, int right = -1)
    {
        Console.SetCursorPosition(left, top);
        if (right < 0) right = left + s.Length;
        while (Console.CursorLeft < right) Console.Write(s);
    }
}
public class TreeNode 
{
    public string item;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(string item) 
    {
      this.item = item;
      this.left = null;
      this.right = null;
    }
    
    public TreeNode(string item, TreeNode left, TreeNode right)
    {
      this.item = item;
      this.left = left;
      this.right = right;
    }
    
}

public class HuffmanCodeResult {
    
    public HuffmanCodeResult(Dictionary<char, string> tree, string encoded)
    {
        this.Tree = tree;
        this.Encoded = encoded;
    }
    
    public Dictionary<char, string> Tree { get; set; }
    public string Encoded { get; set; }
}

public class HuffmanCode{
    private List<Tuple<int, TreeNode>> GetFrequencies(string s)
    {
        var priorities = new List<Tuple<int, TreeNode>>();
        var characterCount= new Dictionary<char,int>();
        foreach(var c in s)
        {
            if(characterCount.ContainsKey(c))
                characterCount[c]++;
            else
                characterCount[c] = 1;
        }  
        
        priorities.AddRange(characterCount
                                .Select(e => new Tuple<int, TreeNode>(e.Value, new TreeNode(""+e.Key)))
                               );
        
        return priorities.OrderBy(p => p.Item1).ToList();
    }
    
    private void PrintPriorityQueue(List<Tuple<int, TreeNode>> pq)
    {
        Console.WriteLine("Printing priority queue: ");
        foreach(var entry in pq)
            Console.WriteLine("key: " + entry.Item1 + 
                              " value: " + entry.Item2.item);    
    }
    
    
    // Function returns key-value pairs letter-encoding sequence + encoded string
    public HuffmanCodeResult Encode(string s)
    {
        List<Tuple<int, TreeNode>> pq = GetFrequencies(s);
        PrintPriorityQueue(pq);
        int n = pq.Count;
        
        for (int i = 0; i < n - 1; i++){
          Tuple<int, TreeNode> f1 = pq[0];
          pq.RemoveAt(0);
          Tuple<int, TreeNode> f2 = pq[0];
          pq.RemoveAt(0);
          pq.Add(new Tuple<int, TreeNode>(
                     f1.Item1 + f2.Item1, 
                     new TreeNode(f1.Item2.item+f2.Item2.item, f1.Item2, f2.Item2)
                 )
          );
          pq = pq.OrderBy(p => p.Item1).ToList();
        }
        TreeNode tree = pq[0].Item2;
        tree.Print();
        
        return new HuffmanCodeResult(
            new Dictionary<char, string>(){{'a', "0"}, {'b', "10"}, {'c', "110"}, {'d', "111"}}, 
            "01001100100111");
    }
}

public class MainClass
{
    public static void Main()
    {
        string input = Console.ReadLine();
        Console.WriteLine(input);
        var encoder = new HuffmanCode();
        var res = encoder.Encode(input);
        Console.WriteLine(""+ res.Tree.Count + " " + res.Encoded.Length);
        res.Tree
            .Select(p => string.Format("{0}: {1}", ""+p.Key, p.Value))
            .ForEach(Console.WriteLine);
        Console.WriteLine(res.Encoded);
    }
}