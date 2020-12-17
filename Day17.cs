using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day17
  {

    public record Coordinate(int X, int Y, int Z);
    public record Coordinate4D(int X, int Y, int Z, int W);
    public static void Run()
    {

      // Console.WriteLine("Test data:");
      // Run("data/testdata17.txt");
      // Console.WriteLine("Answer data:");
      // Run("data/data17.txt");
      Console.WriteLine("Test data:");
      RunB("data/testdata17.txt");
      Console.WriteLine("Answer data:");
      RunB("data/data17.txt");
    }


    static void RunB(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      var activeCubes = new HashSet<Coordinate4D>();
      for (int y = 0; y < lines.Length; y++)
      {
        for (int x = 0; x < lines[y].Length; x++)
        {
          if (lines[y][x] == '#')
          {
            activeCubes.Add(new Coordinate4D(x, y, 0, 0));
          }
        }
      }
      var next = new HashSet<Coordinate4D>();
      int cycles = 6;
      var visited = new HashSet<Coordinate4D>();
      for (int i = 1; i <= cycles; i++)
      {
        foreach (var c in activeCubes)
        {
          for (int dx = -1; dx < 2; dx++)
          {
            for (int dy = -1; dy < 2; dy++)
            {
              for (int dz = -1; dz < 2; dz++)
              {
                for (int dw = -1; dw < 2; dw++)
                {
                  Coordinate4D current = new Coordinate4D(c.X + dx, c.Y + dy, c.Z + dz, c.W + dw);
                  if (visited.Contains(current))
                  {
                    continue;
                  }
                  visited.Add(current);
                  int activeNeightbours = CalculateActiveNeighboursB(current, activeCubes);
                  bool currentlyActive = activeCubes.Contains(current);
                  if (activeNeightbours == 3 && !currentlyActive)
                  {
                    next.Add(current);
                  }
                  else if ((activeNeightbours == 2 || activeNeightbours == 3) && currentlyActive)
                  {
                    next.Add(current);
                  }
                }
              }
            }
          }
        }
        activeCubes = next.ToHashSet();
        next = new HashSet<Coordinate4D>();
        visited.Clear();
      }
      Console.WriteLine($"count = {activeCubes.Count}");
    }

    private static int CalculateActiveNeighboursB(Coordinate4D coordinate, HashSet<Coordinate4D> activeCubes)
    {
      int count = 0;
      for (int dx = -1; dx < 2; dx++)
      {
        for (int dy = -1; dy < 2; dy++)
        {
          for (int dz = -1; dz < 2; dz++)
          {
            for (int dw = -1; dw < 2; dw++)
            {
              if (dx == 0 && dy == 0 && dz == 0 && dw == 0)
              {
                continue;
              }
              if (activeCubes.Contains(new Coordinate4D(coordinate.X + dx, coordinate.Y + dy, coordinate.Z + dz, coordinate.W + dw)))
              {
                count++;
              }
            }

          }
        }
      }
      return count;
    }

    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      var activeCubes = new HashSet<Coordinate>();
      for (int y = 0; y < lines.Length; y++)
      {
        for (int x = 0; x < lines[y].Length; x++)
        {
          if (lines[y][x] == '#')
          {
            activeCubes.Add(new Coordinate(x, y, 0));
          }
        }
      }
      var next = new HashSet<Coordinate>();
      int cycles = 6;
      var visited = new HashSet<Coordinate>();
      for (int i = 1; i <= cycles; i++)
      {
        foreach (var c in activeCubes)
        {
          for (int dx = -1; dx < 2; dx++)
          {
            for (int dy = -1; dy < 2; dy++)
            {
              for (int dz = -1; dz < 2; dz++)
              {
                Coordinate current = new Coordinate(c.X + dx, c.Y + dy, c.Z + dz);
                if (visited.Contains(current))
                {
                  continue;
                }
                visited.Add(current);
                int activeNeightbours = CalculateActiveNeighbours(current, activeCubes);
                bool currentlyActive = activeCubes.Contains(current);
                if (activeNeightbours == 3 && !currentlyActive)
                {
                  next.Add(current);
                }
                else if ((activeNeightbours == 2 || activeNeightbours == 3) && currentlyActive)
                {
                  next.Add(current);
                }

              }
            }
          }

        }
        activeCubes = next.ToHashSet();
        next = new HashSet<Coordinate>();
        visited.Clear();
      }
      Console.WriteLine($"count = {activeCubes.Count}");
    }

    private static int CalculateActiveNeighbours(Coordinate coordinate, HashSet<Coordinate> activeCubes)
    {
      int count = 0;
      for (int dx = -1; dx < 2; dx++)
      {
        for (int dy = -1; dy < 2; dy++)
        {
          for (int dz = -1; dz < 2; dz++)
          {
            if (dx == 0 && dy == 0 && dz == 0)
            {
              continue;
            }
            if (activeCubes.Contains(new Coordinate(coordinate.X + dx, coordinate.Y + dy, coordinate.Z + dz)))
            {
              count++;
            }
          }
        }
      }
      return count;
    }
  }
}
