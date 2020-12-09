using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day8
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata8.txt");
      Console.WriteLine("Answer data:");
      Run("data/data8.txt");

      int res1 = RunB("data/testdata8.txt");
      Console.WriteLine($"Test data: {res1}");

      int res2 = RunB("data/data8.txt");
      Console.WriteLine($"Answer data: {res2}");
    }

    static void Run(string filepath)
    {
      var instructions = File.ReadAllLines(filepath);
      int idx = 0;
      int accumulator = 0;
      var visited = new HashSet<int>();
      while (!visited.Contains(idx))
      {
        visited.Add(idx);

        switch (instructions[idx].Substring(0, 3))
        {
          case "nop":
            {
              idx++;
              break;
            }
          case "acc":
            {
              int n = instructions[idx].Length;
              int val = int.Parse(instructions[idx].Substring(4, n - 4));
              idx++;
              accumulator += val;
              break;

            }
          case "jmp":
            {
              int n = instructions[idx].Length;
              int val = int.Parse(instructions[idx].Substring(4, n - 4));
              idx += val;
              break;
            }

          default:
            {
              throw new InvalidOperationException();
            }
        }
      }
      Console.WriteLine($"Answer = {accumulator}");

    }

    static int RunB(string filepath)
    {
      var instructions = File.ReadAllLines(filepath);

      for (int i = 0; i < instructions.Length; i++)
      {
        string current_instruction = instructions[i].Substring(0, 3);
        switch (current_instruction)
        {
          case "acc":
            {

              break;
            }
          case "nop":
            {
              string current = instructions[i];
              string updated = current.Replace("nop", "jmp");
              instructions[i] = updated;
              if (CanTerminate(instructions, out int accumulator))
              {
                return accumulator;
              }
              instructions[i] = current;
              break;
            }
          case "jmp":
            {
              string current = instructions[i];
              string updated = current.Replace("jmp", "nop");
              instructions[i] = updated;
              if (CanTerminate(instructions, out int accumulator))
              {
                return accumulator;
              }
              instructions[i] = current;
              break;
            }

          default:
            {
              throw new InvalidOperationException();
            }
        }
      }
      throw new InvalidOperationException();
    }

    private static bool CanTerminate(string[] instructions, out int accumulator)
    {
      int idx = 0;
      accumulator = 0;
      var visited = new HashSet<int>();
      while (!visited.Contains(idx))
      {
        int n = instructions[idx].Length;
        visited.Add(idx);

        switch (instructions[idx].Substring(0, 3))
        {
          case "nop":
            {
              idx++;
              break;
            }
          case "acc":
            {
              int val = int.Parse(instructions[idx].Substring(4, n - 4));
              idx++;
              accumulator += val;
              break;

            }
          case "jmp":
            {
              int val = int.Parse(instructions[idx].Substring(4, n - 4));
              idx += val;
              break;
            }

          default:
            {
              throw new InvalidOperationException();
            }
        }
        if (idx == instructions.Length)
        {
          return true;
        }
      }
      return false;
    }
  }
}