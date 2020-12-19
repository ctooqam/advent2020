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
      Console.WriteLine("Answer data:");
      Run("data/data19.txt");
      // Console.WriteLine("Answer data B:");
      // RunContains("data/data19b.txt");
    }

    private static void RunContains(string filePath)
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
      foreach (var rule in rules)
      {
        bool is_static = IsStatic(rule.Key, rules);
        if (is_static)
        {
          Console.WriteLine($"key= {rule.Key} and line =  {rule.Value}");
        }
      }

      string derived = Derived(42, rules);
      Console.WriteLine(derived);

    }

    private static string Derived(int key, Dictionary<int, string> rules)
    {
      if (rules[key].Contains("|"))
      {
        var splitted = rules[key].Split("|");
        var r1 = splitted[0].Trim().Split(" ").Select(int.Parse).ToArray();
        var r2 = splitted[1].Trim().Split(" ").Select(int.Parse).ToArray();
        return DeriveSub(rules, r1) + " | " + DeriveSub(rules, r2);
      }
      else if (rules[key].Contains('"'))
      {
        return rules[key];
      }
      else
      {
        var rulesKeys = rules[key].Split(" ").Select(int.Parse).ToArray();
        return DeriveSub(rules, rulesKeys);
      }

    }

    private static string DeriveSub(Dictionary<int, string> rules, int[] rulesKeys)
    {
      StringBuilder sb = new();
      foreach (var r in rulesKeys)
      {
        sb.Append(Derived(r, rules));
      }
      return sb.ToString();
    }

    private static bool IsStatic(int key, Dictionary<int, string> rules)
    {
      if (rules[key].Contains("|"))
      {
        return false;
      }
      else if (rules[key].Contains('"'))
      {
        return true;
      }
      else
      {
        var rulesKeys = rules[key].Split(" ").Select(int.Parse).ToArray();
        foreach (var r in rulesKeys)
        {
          if (!IsStatic(r, rules))
          {
            return false;
          }
        }
        return true;
      }
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
      var depths = new Dictionary<int, int>();
      Depth(0, depths, rules);
      int count = 0;
      foreach (var msg in messages)
      {
        if (Valid(msg, depths, rules, 0))
        {
          count++;
        }
      }
      Console.WriteLine(count);
      Console.WriteLine(depths[0]);

    }

    private static bool Valid(string msg, Dictionary<int, int> depths, Dictionary<int, string> rules, int ruleKey)
    {
      if (string.IsNullOrEmpty(msg)) { return false; }
      if (msg.Length < depths[ruleKey])
      {
        return false;
      }
      if (rules[ruleKey].Contains("|"))
      {
        var splitted = rules[ruleKey].Split("|");
        foreach (var r in splitted)
        {
          var rulesKeys = r.Trim().Split(" ").Select(int.Parse).ToArray();
          bool valid = ValidRules(msg, rulesKeys, rules, depths);
          if (valid)
          {
            return true;
          }
        }
        return false;
      }
      else if (rules[ruleKey].Contains('"'))
      {
        return rules[ruleKey][1] == msg[0] && msg.Length == 1;
      }
      else
      {
        var rulesKeys = rules[ruleKey].Split(" ").Select(int.Parse).ToArray();
        bool valid = ValidRules(msg, rulesKeys, rules, depths);
        return valid;


      }
    }

    private static bool ValidRules(string msg, int[] ruleKeys, Dictionary<int, string> rules, Dictionary<int, int> depths)
    {
      int idx = 0;
      foreach (var r in ruleKeys)
      {
        int depth = depths[r];
        var substring = msg[idx..(idx + depth)];
        idx += depth;
        if (!Valid(substring, depths, rules, r))
        {
          return false;
        }
      }
      return true;
    }

    private static int Depth(int ruleKey, Dictionary<int, int> depths, Dictionary<int, string> rules)
    {
      if (depths.ContainsKey(ruleKey))
      {
        return depths[ruleKey];
      }
      if (rules[ruleKey].Contains("|"))
      {
        var rule = rules[ruleKey];
        var allRuleKeys = rule.Split("|");
        int[] sums = new int[allRuleKeys.Length];
        for (int i = 0; i < allRuleKeys.Length; i++)
        {
          var allKeys = allRuleKeys[i].Trim().Split(" ");
          sums[i] = 0;
          foreach (var a in allKeys)
          {
            sums[i] += Depth(int.Parse(a), depths, rules);
          }
        }
        if (sums.Distinct().Count() != 1)
        {
          throw new Exception();
        }
        depths.Add(ruleKey, sums[0]);
        return sums[0];
      }
      else if (rules[ruleKey].Contains('"'))
      {
        depths.Add(ruleKey, 1);
        return 1;
      }
      else
      {
        var rule = rules[ruleKey];
        var allRuleKeys = rule.Split(" ");
        int sum = 0;
        foreach (var a in allRuleKeys)
        {
          sum += Depth(int.Parse(a), depths, rules);
        }
        depths.Add(ruleKey, sum);
        return sum;
      }
    }
  }
}
