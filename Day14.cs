using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day14
  {

    public static void Run()
    {
      // Console.WriteLine("Test data:");
      // Run("data/testdata14.txt");
      // Console.WriteLine("Answer data:");
      // Run("data/data14.txt");

      Console.WriteLine("Test data:");
      RunB("data/testdata14b.txt");
      Console.WriteLine("Answer data:");
      RunB("data/data14.txt");
    }

    static void Run(string filepath)
    {
      string mask = "";
      long or_mask = 0;
      long zero_mask = 0;

      var nums = new Dictionary<int, long>();
      var lines = File.ReadAllLines(filepath);
      foreach (var line in lines)
      {
        if (line.StartsWith("mask"))
        {
          mask = line.Substring(line.Length - 36, 36);
          or_mask = Convert.ToInt64(mask.Replace('X', '0'), 2);
          zero_mask = Convert.ToInt64(mask.Replace('1', 'X').Replace('0', '1').Replace('X', '0'), 2);

        }
        else
        {
          var splitted = line.Split(" = ");
          long value = long.Parse(splitted[^1]);
          int idx = int.Parse(splitted[0][4..^1]);
          value = value |= or_mask;
          value = value & ~(zero_mask);
          nums[idx] = value;
        }
      }
      Console.WriteLine(nums.Sum(x => x.Value));


    }
    static void RunB(string filepath)
    {
      string mask = "";

      var nums = new Dictionary<long, long>();
      var lines = File.ReadAllLines(filepath);
      foreach (var line in lines)
      {
        if (line.StartsWith("mask"))
        {
          mask = line.Substring(line.Length - 36, 36);


        }
        else
        {
          var splitted = line.Split(" = ");
          long value = long.Parse(splitted[^1]);
          long idx = int.Parse(splitted[0][4..^1]);
          List<int> x_idx = new List<int>();
          for (int i = 0; i < mask.Length; i++)
          {
            if (mask[i] == 'X')
            {
              x_idx.Add(i);
            }
          }
          Console.WriteLine(x_idx.Count);

          List<int[]> permutations = new List<int[]>();
          permutations.Add(new[] { 0 });
          permutations.Add(new[] { 1 });
          for (int i = 1; i < x_idx.Count; i++)
          {
            int l = permutations.Count;
            List<int[]> old = permutations;
            permutations = new List<int[]>();
            for (int j = 0; j < l; j++)
            {
              permutations.Add(old[j].Concat(new[] { 0 }).ToArray());
              permutations.Add(old[j].Concat(new[] { 1 }).ToArray());
            }
          }

          long main = idx | Convert.ToInt64(mask.Replace('X', '0'), 2);
          long[] res = new long[permutations.Count];
          string main_binary = Convert.ToString(main, 2);
          main_binary = main_binary.PadLeft(36).Replace(' ', '0');
          for (int i = 0; i < res.Length; i++)
          {
            var p = permutations[i];
            char[] res_binary = main_binary.ToCharArray();
            for (int j = 0; j < p.Length; j++)
            {
              res_binary[x_idx[j]] = p[j] == 0 ? '0' : '1';
            }
            res[i] = Convert.ToInt64(new string(res_binary), 2);
          }

          foreach (var r in res)
          {
            nums[r] = value;
          }
        }

      }
      Console.WriteLine(nums.Sum(x => x.Value));
    }
  }
}
