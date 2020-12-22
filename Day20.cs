using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent2020

{
  class Day20
  {
    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata20.txt");

      // Console.WriteLine("Answer data:");
      // Run("data/data20.txt");
    }

    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);

      List<(int, List<string>)> tiles = new();
      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line)) { continue; }
        if (line.StartsWith("Tile"))
        {
          int key = int.Parse(line.Substring(5, 4));
          tiles.Add((key, new List<string>()));
        }
        else
        {
          tiles.Last().Item2.Add(line);
        }
      }
      List<(int, int, int, int, bool)> matchingSides = new();
      // Match(tiles[0].Item2, tiles.Last().Item2, 1, 3, false);
      for (int i = 0; i < tiles.Count; i++)
      {
        for (int j = i + 1; j < tiles.Count; j++)
        {
          for (int side = 0; side < 4; side++)
          {
            for (int otherSide = 0; otherSide < 4; otherSide++)
            {
              bool matches = Match(tiles[i].Item2, tiles[j].Item2, side, otherSide, true);
              if (matches)
              {
                matchingSides.Add((tiles[i].Item1, tiles[j].Item1, side, otherSide, true));
              }
              matches = Match(tiles[i].Item2, tiles[j].Item2, side, otherSide, false);
              if (matches)
              {
                matchingSides.Add((tiles[i].Item1, tiles[j].Item1, side, otherSide, false));
              }
            }
          }
        }
      }
      long res = 1;
      foreach (var tile in tiles)
      {
        int key = tile.Item1;
        int count = matchingSides.Where(x => x.Item1 == key || x.Item2 == key).Count();
        if (count == 2) { res *= key; }

        // Console.WriteLine($"tile= {key}, count= {count}");
      }
      Console.WriteLine(res);
      foreach (var k in tiles)
      {
        var key = k.Item1;
        Console.WriteLine(key);
        foreach (var m in matchingSides.Where(x => x.Item1 == key || x.Item2 == key))
        {
          Console.WriteLine($"{m.Item1} matched {m.Item2} on side {m.Item3} vs {m.Item4}, flipped= {m.Item5}");
        }
        Console.WriteLine();
      }


    }

    private static bool Match(List<string> tile, List<string> other, int side, int otherSide, bool flipped)
    {
      string s = GetSide(tile, side);
      string o = GetSide(other, otherSide);
      if (flipped)
      {
        return s == o;
      }
      return s == new string(o.Reverse().ToArray());
    }

    private static string GetSide(List<string> tile, int side)
    { //North
      if (side == 0)
      {
        return tile[0];
      }
      //East
      else if (side == 1)
      {
        var sb = new StringBuilder();
        for (int i = 0; i < tile.Count; i++)
        {
          sb.Append(tile[i].Last());
        }
        return sb.ToString();

      }//South
      else if (side == 2)
      {
        return new string(tile.Last().Reverse().ToArray());

      }//West
      else if (side == 3)
      {
        var sb = new StringBuilder();
        for (int i = 0; i < tile.Count; i++)
        {
          sb.Append(tile[i][0]);
        }
        return new string(sb.ToString().Reverse().ToArray());
      }
      else { throw new Exception(); }
    }
  }
}
