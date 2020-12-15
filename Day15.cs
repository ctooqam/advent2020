using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day15
  {

    public static void Run()
    {

      Console.WriteLine("Test data:");
      Run(new[] { 0, 3, 6 }, 2020);
      Console.WriteLine("Answer data:");
      Run(new[] { 13, 16, 0, 12, 15, 1 }, 2020);

      Console.WriteLine("Test data:");
      Run(new[] { 0, 3, 6 }, 30000000);
      Console.WriteLine("Answer data:");
      Run(new[] { 13, 16, 0, 12, 15, 1 }, 30000000);
    }

    static void Run(int[] startingNumbers, int turns)
    {
      var visited = new Dictionary<int, int>(turns);
      for (int i = 0; i < startingNumbers.Length - 1; i++)
      {
        visited.Add(startingNumbers[i], i);
      }
      int last = startingNumbers.Last();
      for (int i = startingNumbers.Length; i < turns; i++)
      {
        if (visited.ContainsKey(last))
        {
          int prev = visited[last];
          visited[last] = i - 1;
          last = i - 1 - prev;
        }
        else
        {
          visited[last] = i - 1;
          last = 0;
        }
      }

      Console.WriteLine(last);

    }
  }
}
