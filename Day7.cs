using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day7
  {

    public static void Run()
    {
      // Console.WriteLine("Test data:");
      // Run("data/testdata7.txt");
      Console.WriteLine("Test data:");
      RunB("data/testdata7.txt");
      Console.WriteLine("Answer data:");
      RunB("data/data7.txt");
      // Console.WriteLine("Answer data:");
      // Run("data/data7.txt");
    }
    //light red bags contain 1 bright white bag, 2 muted yellow bags.
    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      var bags = new Dictionary<string, List<string>>(lines.Length);
      foreach (var line in lines)
      {
        var splitted = line.Split("contain");
        string bag_name = splitted[0].Split("bags")[0].Trim();
        var bags_can_contain = new List<string>();
        if (splitted[1] != " no other bags.")
        {
          var bags_text = splitted[1].Trim().Split(", ");
          foreach (var bag_text in bags_text)
          {
            var bag_split = bag_text.Split(" ");
            var can_bag_name = bag_split[1] + " " + bag_split[2];
            bags_can_contain.Add(can_bag_name.Trim());
          }
        }
        bags.Add(bag_name, bags_can_contain);
      }

      var visited = new Dictionary<string, bool>();

      int count = 0;
      foreach (var b in bags)
      {
        if (dfs(bags, visited, b.Key))
        {
          count++;
        }
      }
      Console.WriteLine(count);


    }

    //light red bags contain 1 bright white bag, 2 muted yellow bags.
    static void RunB(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      var bags = new Dictionary<string, List<Tuple<int, string>>>(lines.Length);
      foreach (var line in lines)
      {
        var splitted = line.Split("contain");
        string bag_name = splitted[0].Split("bags")[0].Trim();
        var bags_can_contain = new List<Tuple<int, string>>();
        if (splitted[1] != " no other bags.")
        {
          var bags_text = splitted[1].Trim().Split(", ");
          foreach (var bag_text in bags_text)
          {
            var bag_split = bag_text.Split(" ");
            var can_bag_name = bag_split[1] + " " + bag_split[2];
            var quantity = int.Parse(bag_split[0]);
            bags_can_contain.Add(Tuple.Create(quantity, can_bag_name.Trim()));
          }
        }
        bags.Add(bag_name, bags_can_contain);
      }

      var visited = new Dictionary<string, int>();

      int count = dfs(bags, visited, "shiny gold");

      Console.WriteLine(count - 1); //disregards itself
    }


    static int dfs(Dictionary<string, List<Tuple<int, string>>> bags, Dictionary<string, int> visited, string currentNode)
    {
      if (visited.ContainsKey(currentNode))
      {
        return visited[currentNode];
      }
      int count = 1;
      foreach (var child in bags[currentNode])
      {
        if (visited.ContainsKey(child.Item2))
        {
          count += child.Item1 * visited[child.Item2];
          continue;
        }
        count += child.Item1 * dfs(bags, visited, child.Item2);

      }
      // Console.WriteLine($"{currentNode} counts {count}");
      visited.Add(currentNode, count);
      return count;

    }

    static bool dfs(Dictionary<string, List<string>> bags, Dictionary<string, bool> visited, string currentNode)
    {
      // var start_bag = "shiny gold";
      if (currentNode == "shiny gold")
      {
        return false;
      }
      if (visited.ContainsKey(currentNode))
      {
        return visited[currentNode];
      }
      foreach (var child in bags[currentNode])
      {
        if (child == "shiny gold")
        {
          visited.Add(currentNode, true);
          return true;
        }
        var found = dfs(bags, visited, child);
        if (found)
        {
          visited.Add(currentNode, true);
          return true;
        }


      }
      visited.Add(currentNode, false);
      return false;

    }
  }
}