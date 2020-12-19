using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent2020

{
  class Day19
  {
    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata19.txt");
      Visited.Clear();
      VisitedList.Clear();

      Console.WriteLine("Answer data:");
      Run("data/data19.txt");
      Visited.Clear();
      VisitedList.Clear();
      Console.WriteLine("Answer data B:");
      Run("data/data19b.txt");
    }

    // 0: 4 1 5
    // 1: 2 3 | 3 2
    // 2: 4 4 | 5 5
    // 3: 4 5 | 5 4
    // 4: "a"
    // 5: "b"

    // ababbb
    // bababa
    // abbbab
    // aaabbb
    // aaaabbb
    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      var rules = new Dictionary<int, string>();
      var messages = new List<string>();
      bool parse_rules = true;
      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          parse_rules = false;
          continue;
        }
        if (parse_rules)
        {
          var tokens = line.Split(":");
          var key = int.Parse(tokens[0]);
          string value = tokens[1].Trim();
          rules[key] = value;
        }
        else
        {
          messages.Add(line);
        }
      }
      int count = 0;
      foreach (var msg in messages)
      {
        if (Valid(msg, rules, 0))
        {
          count++;
        }
      }
      // Console.WriteLine(Valid(messages[0], rules, 0));
      Console.WriteLine(count);

    }

    private static Dictionary<string, bool> Visited = new();
    private static Dictionary<string, bool> VisitedList = new();

    private static bool Valid(string msg, Dictionary<int, string> rules, int ruleKey)
    {
      if (string.IsNullOrWhiteSpace(msg)) { return false; }
      string key = msg + ruleKey;
      if (Visited.ContainsKey(key))
      {
        return Visited[key];
      }
      if (rules[ruleKey].Contains("|"))
      {
        var splitted = rules[ruleKey].Split("|");
        foreach (var r in splitted)
        {
          var rulesKeys = r.Trim().Split(" ").Select(int.Parse).ToArray();
          bool valid = ValidRules(msg, rulesKeys, rules);
          if (valid)
          {
            Visited[key] = true;
            return true;
          }
        }
        Visited[key] = false;
        return false;
      }
      else if (rules[ruleKey].Contains('"'))
      {
        return rules[ruleKey][1] == msg[0] && msg.Length == 1;
      }
      else
      {
        var rulesKeys = rules[ruleKey].Split(" ").Select(int.Parse).ToArray();
        bool valid = ValidRules(msg, rulesKeys, rules);
        Visited[key] = valid;
        return valid;

      }
    }

    private static bool ValidRules(string msg, int[] ruleKeys, Dictionary<int, string> rules)
    {
      if (string.IsNullOrWhiteSpace(msg) && ruleKeys.Length == 0)
      {
        return true;
      }
      if (ruleKeys.Length == 0)
      {
        return false;
      }
      if (string.IsNullOrWhiteSpace(msg))
      {
        return false;
      }
      if (ruleKeys.Length == 1)
      {
        return Valid(msg, rules, ruleKeys[0]);
      }
      string key = msg + ruleKeys.Aggregate("", (prev, rule) => prev + "," + rule);
      if (VisitedList.ContainsKey(key))
      {
        return VisitedList[key];
      }
      for (int i = 1; i < msg.Length; i++)
      {
        if (Valid(msg[..i], rules, ruleKeys[0]) && ValidRules(msg[i..], ruleKeys[1..], rules))
        {
          VisitedList[key] = true;
          return true;
        }
      }
      VisitedList[key] = false;
      return false;
    }
  }
}
