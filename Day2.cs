using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day2
  {
    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata2.txt");
      Console.WriteLine("Answer data:");
      Run("data/data2.txt");
    }
    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      Console.WriteLine("A:");
      Run(lines, Is_Valid_A);
      Console.WriteLine("B:");
      Run(lines, Is_Valid_B);

    }

    private static bool Is_Valid_B(int first_pos, int second_pos, char letter, string password)
    {
      return (password[first_pos - 1] == letter) ^ (password[second_pos - 1] == letter);
    }

    private static bool Is_Valid_A(int left, int right, char letter, string password)
    {
      int letter_count = 0;
      foreach (var character in password)
      {
        if (character == letter)
        {
          letter_count++;
        }
      }
      if (letter_count >= left && letter_count <= right)
      {
        return true;
      }
      return false;
    }

    static void Run(string[] lines, Func<int, int, char, string, bool> is_valid)
    {
      int valid_count = 0;

      foreach (var line in lines)
      {
        var splitted = line.Split('-');
        int left = int.Parse(splitted[0]);
        splitted = splitted[1].Split(' ');
        int right = int.Parse(splitted[0]);
        char letter = splitted[1][0];
        string password = splitted[2];
        if (is_valid(left, right, letter, password))
        {
          valid_count++;
        }
      }
      Console.WriteLine(valid_count);
    }
  }
}