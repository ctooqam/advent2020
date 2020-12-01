using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Test data:");
      Run("testdata1.txt");
      Console.WriteLine("Answer data:");
      Run("data1.txt");
    }

    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      var values = lines.Select(int.Parse).ToHashSet();
      int resultA = RunA(values);
      int resultB = RunB(values);
      Console.WriteLine($"Result for A: {resultA}");
      Console.WriteLine($"Result for B: {resultB}");
    }

    private static int RunA(HashSet<int> values)
    {
      foreach (var value in values)
      {
        int target = 2020 - value;
        if (values.Contains(target))
        {
          return target * value;
        }

      }
      return 0;
    }
    private static int RunB(HashSet<int> values)
    {
      int[] numbers = values.ToArray();
      for (int i = 0; i < numbers.Length; i++)
      {
        for (int j = i + 1; j < numbers.Length; j++)
        {
          int target = 2020 - numbers[i] - numbers[j];
          if (values.Contains(target))
          {
            return target * numbers[i] * numbers[j];
          }
        }
      }

      return 0;
    }


  }
}
