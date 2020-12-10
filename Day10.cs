using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day10
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata10.txt");
      Console.WriteLine("Test data b:");
      Run("data/testdata10b.txt");
      Console.WriteLine("Answer data:");
      Run("data/data10.txt");

      Console.WriteLine("Test data:");
      RunB("data/testdata10.txt");
      Console.WriteLine("Test data b:");
      RunB("data/testdata10b.txt");
      Console.WriteLine("Answer data:");
      RunB("data/data10.txt");
    }


    static void Run(string filepath)
    {
      var numbers = File.ReadAllLines(filepath).Select(int.Parse).OrderBy(x => x).ToArray();
      int ones = 0;
      int threes = 1;
      for (int i = 0; i < numbers.Length; i++)
      {
        int prev_nbr;
        if (i == 0)
        {
          prev_nbr = 0;
        }
        else
        {
          prev_nbr = numbers[i - 1];
        }
        int diff = numbers[i] - prev_nbr;
        if (diff == 1)
        {
          ones++;
        }
        if (diff == 3)
        {
          threes++;
        }
      }
      Console.WriteLine($"ones= {ones}, threes= {threes} and res: {ones * threes}");

    }

    static void RunB(string filepath)
    {
      var numbers = File.ReadAllLines(filepath).Select(long.Parse).Concat(new long[] { 0 }).OrderBy(x => x).ToArray();
      long[] dp = new long[numbers.Length];
      dp[0] = 1;
      for (int i = 1; i < numbers.Length; i++)
      {
        if (numbers[i] - numbers[i - 1] <= 3)
        {
          dp[i] += dp[i - 1];
        }
        if (i > 1 && numbers[i] - numbers[i - 2] <= 3)
        {
          dp[i] += dp[i - 2];
        }
        if (i > 2 && numbers[i] - numbers[i - 3] <= 3)
        {
          dp[i] += dp[i - 3];
        }
      }
      Console.WriteLine(dp.Last());

    }



  }
}