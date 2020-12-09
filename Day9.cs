using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day9
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      int res = Run("data/testdata9.txt", 5);
      Console.WriteLine(res);
      RunB("data/testdata9.txt", res);
      Console.WriteLine("Answer data:");
      int res2 = Run("data/data9.txt", 25);
      RunB("data/data9.txt", res2);
      Console.WriteLine(res2);


    }

    static int Run(string filepath, int preamble_size)
    {
      var numbers = File.ReadAllLines(filepath);
      var set = new HashSet<int>(preamble_size + 1);
      for (int i = 0; i < preamble_size; i++)
      {
        set.Add(int.Parse(numbers[i]));
      }
      for (int i = preamble_size; i < numbers.Length; i++)
      {
        int current = int.Parse(numbers[i]);
        if (i != preamble_size) { set.Remove(int.Parse(numbers[i - preamble_size - 1])); }
        if (ExistSum(set, current))
        {
          set.Add(current);
          continue;
        }
        else
        {
          return current;
        }


      }
      Console.WriteLine(set.Count);
      throw new InvalidOperationException();

    }

    private static bool ExistSum(HashSet<int> set, int v)
    {
      foreach (var num in set)
      {
        int target = v - num;
        if (target == num)
        {
          continue;
        }
        if (set.Contains(target))
        {
          return true;
        }
      }
      return false;
    }

    static void RunB(string filepath, int target)
    {
      var numbers = File.ReadAllLines(filepath).Select(long.Parse).ToArray();
      int low = 0;
      int high = 1;
      long sum = numbers[low] + numbers[high];

      while (sum != target)
      {
        if (sum < target)
        {
          high++;
          sum += numbers[high];
        }
        else
        {
          sum -= numbers[low];
          low++;
        }
      }
      long min = numbers[low..high].Min();
      long max = numbers[low..high].Max();
      Console.WriteLine($"min = {min}, max= {max} and sum= {min + max}");



    }
  }
}