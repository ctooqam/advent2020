using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent2020

{
  class Day21
  {
    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata21.txt");

      Console.WriteLine("Answer data:");
      Run("data/data21.txt");
    }

    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      var recipes = new List<(HashSet<string>, HashSet<string>)>();
      foreach (var line in lines)
      {
        var splitted = line.Split(" (contains ");
        var ingredients = splitted[0].Split(" ").ToHashSet();
        var allergens = splitted[1].Replace(")", "").Split(", ").ToHashSet();
        recipes.Add((ingredients, allergens));
      }
      // mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
      // trh fvjkl sbzzf mxmxvkd (contains dairy)
      // sqjhc fvjkl (contains soy)
      // sqjhc mxmxvkd sbzzf (contains fish)
      var candidates = recipes.SelectMany(x => x.Item2).Distinct().ToDictionary(x => x, x => new List<string>());
      foreach (var allergen in candidates.Keys.ToList())
      {
        foreach (var r in recipes)
        {
          if (r.Item2.Contains(allergen))
          {
            if (candidates[allergen].Count == 0)
            {
              candidates[allergen] = r.Item1.ToList();
            }
            else
            {
              candidates[allergen] = r.Item1.Intersect(candidates[allergen]).ToList();
            }
          }
        }
      }

      while (candidates.Values.Any(x => x.Count != 1))
      {
        foreach (var k in candidates.Keys.ToList())
        {
          if (candidates[k].Count == 1)
          {
            foreach (var o in candidates)
            {
              if (o.Key == k) { continue; }
              o.Value.Remove(candidates[k].Single());
            }
          }
        }
      }
      // foreach (var c in candidates)
      // {
      //   Console.WriteLine($"{c.Key} matches {c.Value.Single()}");
      // }

      int count = 0;
      var matches = candidates.Values.SelectMany(x => x).ToHashSet();
      foreach (var r in recipes)
      {
        foreach (var ingredient in r.Item1)
        {
          if (!matches.Contains(ingredient))
          {
            count++;
          }
        }
      }
      Console.WriteLine(count);

      var sb = new StringBuilder();
      var order = candidates.Keys.OrderBy(x => x);
      foreach (var o in order)
      {
        sb.Append(candidates[o].Single() + ",");
      }
      Console.WriteLine(sb.ToString());

    }
  }
}
