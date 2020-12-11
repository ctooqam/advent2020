using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day11
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata11.txt");
      Console.WriteLine("Answer data:");
      Run("data/data11.txt");
      Console.WriteLine("Test data:");
      RunB("data/testdata11.txt");
          Console.WriteLine("Answer data:");
      RunB("data/data11.txt");
  

    }


    static void RunB(string filepath)
    {
      var lines = File.ReadAllLines(filepath).Select(x => x.ToCharArray()).ToArray();

      var changes = new List<Tuple<int, int, char>>();
      bool first = true;
      while (changes.Count != 0 || first)
      {
        first = false;
        changes.Clear();
        for (int i = 0; i < lines.Length; i++)
        {
          for (int j = 0; j < lines[0].Length; j++)
          {
            if (lines[i][j] == '.')
            {
              continue;
            }
            int adj_seats = 0;
            for (int dx = -1; dx < 2; dx++)
            {
              for (int dy = -1; dy < 2; dy++)
              {
                if (dx == 0 && dy == 0)
                {
                  continue;
                }
                int x = j + dx;
                int y = i + dy;
                while (x >= 0 && x < lines[0].Length && y >= 0 && y < lines.Length)
                {
                  if (lines[y][x] == '#')
                  {
                    adj_seats++;
                    break;
                  }
                  else if (lines[y][x] == 'L')
                  {
                    break;
                  }
                  x = x + dx;
                  y = y + dy;
                }
              }
            }
            if (lines[i][j] == 'L' && adj_seats == 0)
            {
              changes.Add(Tuple.Create(j, i, '#'));
            }
            else if (lines[i][j] == '#' && adj_seats >= 5)
            {
              changes.Add(Tuple.Create(j, i, 'L'));
            }

          }

        }
        foreach (var change in changes)
        {
          lines[change.Item2][change.Item1] = change.Item3;
        }
        // foreach (var line in lines)
        // {
        //   Console.WriteLine( string.Join(' ',line) );
        // }
        // Console.WriteLine();
      }
      int res = 0;
      foreach (var line in lines)
      {
        foreach (var c in line)
        {
          if (c == '#')
          {
            res++;
          }
        }
      }
      Console.WriteLine(res);
    }

    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath).Select(x => x.ToCharArray()).ToArray();

      var changes = new List<Tuple<int, int, char>>();
      bool first = true;
      while (changes.Count != 0 || first)
      {
        first = false;
        changes.Clear();
        for (int i = 0; i < lines.Length; i++)
        {
          for (int j = 0; j < lines[0].Length; j++)
          {
            if (lines[i][j] == '.')
            {
              continue;
            }
            int adj_seats = 0;
            for (int dx = -1; dx < 2; dx++)
            {
              for (int dy = -1; dy < 2; dy++)
              {
                if (dx == 0 && dy == 0)
                {
                  continue;
                }
                int x = j + dx;
                int y = i + dy;
                if (x >= 0 && x < lines[0].Length && y >= 0 && y < lines.Length)
                {
                  if (lines[y][x] == '#')
                  {
                    adj_seats++;
                  }
                }
              }
            }
            if (lines[i][j] == 'L' && adj_seats == 0)
            {
              changes.Add(Tuple.Create(j, i, '#'));
            }
            else if (lines[i][j] == '#' && adj_seats >= 4)
            {
              changes.Add(Tuple.Create(j, i, 'L'));
            }

          }

        }
        foreach (var change in changes)
        {
          lines[change.Item2][change.Item1] = change.Item3;
        }
        // foreach (var line in lines)
        // {
        //   Console.WriteLine( string.Join(' ',line) );
        // }
        // Console.WriteLine();
      }
      int res = 0;
      foreach (var line in lines)
      {
        foreach (var c in line)
        {
          if (c == '#')
          {
            res++;
          }
        }
      }
      Console.WriteLine(res);
    }

  }
}