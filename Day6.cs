using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day6
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata6.txt");
      Console.WriteLine("Answer data:");
      Run("data/data6.txt");
    }

    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      int answer = 0;
      int answer2 = 0;
      var set = new HashSet<char>();
      List<char> all_yes = null;
      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          answer += set.Count;
          answer2 += all_yes.Count;
          set = new HashSet<char>();
          all_yes = null;
          continue;
        }
        else
        {
          for (int i = 0; i < line.Length; i++)
          {
            set.Add(line[i]);
          }
          all_yes = all_yes == null ? line.ToList() : all_yes.Intersect(line).ToList();
        }
      }
      answer += set.Count;
      if (all_yes != null)
      {
        answer2 += all_yes.Count;

      }
      Console.WriteLine($"Answer = {answer}");
      Console.WriteLine($"Answer2 = {answer2}");

    }
  }
}