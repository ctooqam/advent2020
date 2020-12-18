using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day18
  {
    public static void Run()
    {
      // string test1 = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
      // long r1 = Calculate(test1);
      // Console.WriteLine(r1);

      Console.WriteLine("Answer data:");
      Run("data/data18.txt");
    }


    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      long sum = 0;
      foreach (var line in lines)
      {
        sum += Calculate(line);
      }
      Console.WriteLine(sum);
    }

    static long Calculate(string line)
    {
      // Console.WriteLine(line);
      if (line.Contains("("))
      {
        int first = line.IndexOf("(");
        int last = first + 1;
        int count = 1;
        while (count != 0)
        {
          if (line[last] == ')')
          {
            count--;
          }
          if (line[last] == '(')
          {
            count++;
          }
          last++;
        }
        last--;
        string substring = line.Substring(first + 1, last - first - 1);
        long num = Calculate(substring);
        return Calculate(line.Replace("(" + substring + ")", num.ToString()));
      }
      else if (line.Contains('+'))
      {
        var tokens = line.Split(" ");
        if (tokens.Length == 3)
        {
          return long.Parse(tokens[0]) + long.Parse(tokens[2]);
        }
        int first = Array.IndexOf(tokens, "+");


        string substring = tokens[first - 1] + " " + tokens[first] + " " + tokens[first + 1];
        long num = Calculate(substring);
        int prev = first - 1;
        int next = first + 2;
        var newline = string.Join(" ", tokens[..prev].Concat(new string[] { num.ToString() }).Concat(tokens[next..]));
        return Calculate(newline);
      }
      else
      {
        var tokens = line.Split(" ");
        if (tokens.Length % 2 == 0) { throw new Exception(); }
        long num = long.Parse(tokens[0]);
        for (int i = 1; i < tokens.Length; i += 2)
        {
          num *= long.Parse(tokens[i + 1]);
        }
        return num;
      }
    }
  }
}
