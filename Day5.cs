using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day5
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata5.txt");
      Console.WriteLine("Answer data:");
      Run("data/data5.txt");
    }

    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      var seat_ids = new HashSet<int>();
      int current_max = int.MinValue;
      int current_min = int.MaxValue;
      foreach (var line in lines)
      {
        string binary = line.Replace('F', '0').Replace('B', '1').Replace('R', '1').Replace('L', '0');
        int row = Convert.ToInt32(binary.Substring(0, 7), 2);
        int col = Convert.ToInt32(binary.Substring(7, 3), 2);
        int seat_id = row * 8 + col;
        seat_ids.Add(seat_id);
        current_max = Math.Max(current_max, seat_id);
        current_min = Math.Min(current_min, seat_id);
      }
      Console.WriteLine($"Max seat id: {current_max}");
      Console.WriteLine($"Min seat id: {current_min}");

      for (int i = current_min + 1; i < current_max; i++)
      {
        if (!seat_ids.Contains(i))
        {
          Console.WriteLine($"Your id: {i}");
          break;
        }

      }





    }
  }
}