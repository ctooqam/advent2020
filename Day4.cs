using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day4
  {
    public static HashSet<string> validColors = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
    public static Dictionary<string, Func<string, bool>> Validators = new Dictionary<string, Func<string, bool>> {
      {"byr",  x=> x.Length == 4 && int.Parse(x) >= 1920 && int.Parse(x) <= 2002},
      {"iyr",  x=> x.Length == 4 && int.Parse(x) >= 2010 && int.Parse(x) <= 2020 },
      {"eyr",  x=> x.Length == 4 && int.Parse(x) >= 2020 && int.Parse(x) <= 2030 },
      {"hgt", ValidHeight},
      {"hcl", ValidHaircolor},
      {"ecl", x=> validColors.Contains(x)},
      {"pid", x=> x.Length == 9 && x.All(char.IsDigit)}
    };



    private static bool ValidHaircolor(string s)
    {
      if (s.Length != 7)
      {
        return false;
      }
      if (s[0] != '#')
      {
        return false;
      }
      s = s.Substring(1, s.Length - 1);
      if (s.All(x => char.IsDigit(x) || (x >= 'a' && x <= 'f')))
      {
        return true;
      }
      return false;

    }

    private static bool ValidHeight(string s)
    {
      if (s.Contains("in"))
      {
        s = s.Substring(0, s.Length - 2);
        int h = int.Parse(s);
        if (h >= 59 && h <= 76)
        {
          return true;
        }
        return false;
      }
      else if (s.Contains("cm"))
      {
        s = s.Substring(0, s.Length - 2);
        int h = int.Parse(s);
        if (h >= 150 && h <= 193)
        {
          return true;
        }
        return false;

      }
      else
      {
        return false;
      }
    }

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata4.txt");
      Console.WriteLine("Answer data:");
      Run("data/data4.txt");

      Console.WriteLine("invalid data:");
      Run("data/invalid4.txt");

      Console.WriteLine("valid data:");
      Run("data/valid4.txt");
    }
    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      var passports = Parse(lines);
      var mandatory = new HashSet<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
      int valid = 0;
      foreach (var passport in passports)
      {
        if (mandatory.All(x => passport.ContainsKey(x)))
        {
          valid++;
        }
      }
      Console.WriteLine($"valid passports: {valid}");
      valid = 0;
      foreach (var passport in passports)
      {
        if (Validators.All(v => passport.ContainsKey(v.Key) && v.Value(passport[v.Key])))
        {
          valid++;
        }
      }
      Console.WriteLine($"B valid {valid}");
    }
    static List<Dictionary<string, string>> Parse(string[] lines)
    {
      int i = 0;
      var passports = new List<Dictionary<string, string>>();
      passports.Add(new Dictionary<string, string>());

      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          i++;
          passports.Add(new Dictionary<string, string>());
          continue;
        };
        var pairs = line.Split(" ");
        foreach (var pair in pairs)
        {
          var splitted = pair.Split(":");
          passports[i].Add(splitted[0], splitted[1]);
        }
      }
      return passports;
    }

  }
}