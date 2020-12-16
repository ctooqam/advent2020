using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day16
  {

    public static void Run()
    {

      // Console.WriteLine("Test data:");
      // Run("data/testdata16b.txt");
      Console.WriteLine("Answer data:");
      Run("data/data16.txt");
    }

    public static int[] result = Array.Empty<int>();

    record Ranges(int low, int high)
    {
      public bool Within(int value)
      {
        return value >= low && value <= high;
      }
    }

    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      var rules = new List<(string token, Ranges first, Ranges second)>();
      int i = 0;
      while (!string.IsNullOrWhiteSpace(lines[i]))
      {
        var splitted = lines[i].Split(":");
        var token = splitted[0];
        var ranges = splitted[1].Split("or");
        var r1 = ranges[0].Split("-");
        var r2 = ranges[1].Split("-");
        rules.Add((token, new Ranges(int.Parse(r1[0]), int.Parse(r1[1])), new Ranges(int.Parse(r2[0]), int.Parse(r2[1]))));
        i++;
      }
      i += 2;
      int[] myTicketValues = lines[i].Split(',').Select(int.Parse).ToArray();

      i += 3;
      List<int[]> nearbyTickets = new List<int[]>();
      for (; i < lines.Length; i++)
      {
        nearbyTickets.Add(lines[i].Split(',').Select(int.Parse).ToArray());
      }

      var allRanges = rules.SelectMany(x => new Ranges[] { x.first, x.second }).ToArray();

      var value = nearbyTickets.SelectMany(x => x).Where(x => allRanges.All(r => !r.Within(x))).Sum();
      Console.WriteLine(value);

      nearbyTickets = nearbyTickets.Where(x => x.All(y => allRanges.Any(r => r.Within(y)))).ToList();
      nearbyTickets.Add(myTicketValues);
      var found = Valid(nearbyTickets, rules, 0, Array.Empty<int>(), new Dictionary<string, bool>());
      Console.WriteLine(found);
      foreach (var r in result)
      {
        Console.Write($"{r},");
      }
      long res = 1;
      for (int j = 0; j < result.Length; j++)
      {
        if (rules[result[j]].token.StartsWith("departure"))
        {
          res *= myTicketValues[j];
        }
      }
      Console.WriteLine(res);

     
    }


    // static List<List<int>> Dfs(List<int[]> tickets, List<(string token, Ranges first, Ranges second)> rules, int pos, int[] path)
    // {
    //   var allValidCombinations = new List<List<int>>();
    //   for (int i = 0; i < rules.Count; i++)
    //   {

    //     if (path.Contains(i))
    //     {
    //       continue;
    //     }
    //     var (_, first, second) = rules[i];
    //     if (tickets.All(t => first.Within(t[pos]) || second.Within(t[pos])))
    //     {
    //       if (pos == rules.Count - 1)
    //       {
    //         //Found
    //         foreach (var p in path)
    //         {
    //           Console.Write($"{p}");
    //         }
    //         Console.Write(i);
    //       }
    //       else
    //       {
    //         var newSet = visistedRules.ToHashSet();
    //         newSet.Add(i);
    //         var validCombinations = Dfs(tickets, rules, pos + 1, newSet);
    //         foreach (var comb in validCombinations)
    //         {
    //           var a = comb.ToList();
    //           a.Insert(0, i);
    //           allValidCombinations.Add(a);
    //         }
    //       }
    //     }
    //   }
    //   return allValidCombinations;
    // }
    static bool Valid(List<int[]> tickets, List<(string token, Ranges first, Ranges second)> rules, int pos, int[] path, Dictionary<string, bool> visited)
    {
      int[] candidates = Enumerable.Range(0, rules.Count).Where(x => !path.Contains(x)).ToArray();
      var key = candidates.Aggregate("", (t, p) => t + "," + p.ToString());
      if (visited.ContainsKey(key))
      {
        return visited[key];
      }
      foreach (var i in candidates)
      {
        var newpath = path.Concat(new[] { i }).ToArray();


        var (_, first, second) = rules[i];
        if (tickets.All(t => first.Within(t[pos]) || second.Within(t[pos])))
        {
          if (pos == rules.Count - 1)
          {
            //Found
            foreach (var p in path)
            {
              Console.Write($"{p},");
            }
            Console.Write(i);
            result = newpath;
            return true;
          }
          else
          {
            var valid = Valid(tickets, rules, pos + 1, newpath, visited);
            if (valid) { return true; }
          }
        }
      }
      visited[key] = false;
      return false;
    }
  }
}
