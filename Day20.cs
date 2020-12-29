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
      // Console.WriteLine("Test data:");
      // Run("data/testdata20.txt");

      Console.WriteLine("Answer data:");
      Run("data/data20.txt");
    }

    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);

      List<(int, List<string>)> parsed_tiles = new();
      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line)) { continue; }
        if (line.StartsWith("Tile"))
        {
          int key = int.Parse(line.Substring(5, 4));
          parsed_tiles.Add((key, new List<string>()));
        }
        else
        {
          parsed_tiles.Last().Item2.Add(line);
        }
      }
      Dictionary<int, string[]> tiles = parsed_tiles.ToDictionary(x => x.Item1, x => x.Item2.ToArray());
      var tile_ids = tiles.Keys.ToArray();
      var tile_count = new Dictionary<int, HashSet<int>>();
      foreach (var id in tile_ids)
      {
        tile_count.Add(id, new HashSet<int>());
      }

      for (int i = 0; i < tiles.Count; i++)
      {
        for (int j = i + 1; j < tiles.Count; j++)
        {
          int count = 0;
          var left = tile_ids[i];
          var right = tile_ids[j];
          var all_combs_left = Combinations(tiles[left]);
          var all_combs_right = Combinations(tiles[right]);
          foreach (var l in all_combs_left)
          {
            foreach (var r in all_combs_right)
            {
              if (l[0] == r[0])
              {
                count++;
              }
            }
          }
          if (count != 0)
          {
            tile_count[left].Add(right);

            tile_count[right].Add(left);

          }

        }
      }

      // foreach (var pair in tile_count)
      // {
      //   Console.WriteLine($"key = {pair.Key} with sides {pair.Value.Count}");
      // }

      var resA = tile_count.Where(x => x.Value.Count == 2).Aggregate(1L, (prev, next) => prev * next.Key);
      Console.WriteLine(resA);
      int n = (int)Math.Sqrt(tiles.Count);
      var placement = new int[n][];
      for (int i = 0; i < n; i++)
      {
        placement[i] = new int[n];
      }
      int corner = tile_count.First(x => x.Value.Count == 2).Key;
      placement[0][0] = corner;
      placement[0][1] = tile_count[corner].First();
      placement[1][0] = tile_count[corner].Last();
      var tilesToPlace = tile_ids.Except(new int[] { corner, placement[0][1], placement[1][0] }).ToHashSet();

      for (int row = 0; row < n; row++)
      {
        for (int col = 0; col < n; col++)
        {
          if (placement[row][col] == 0)
          {
            int[] dx = new int[] { 1, -1, 0, 0 };
            int[] dy = new int[] { 0, 0, 1, -1 };
            List<int> neighbours = new();
            int neighbour_count = 0;
            for (int i = 0; i < 4; i++)
            {
              int new_row = row + dy[i];
              int new_col = col + dx[i];
              if (new_row >= 0 && new_row < n && new_col >= 0 && new_col < n)
              {
                neighbour_count++;
                if (placement[new_row][new_col] != 0)
                {
                  neighbours.Add(placement[new_row][new_col]);
                }
              }
            }
            foreach (var t in tilesToPlace)
            {
              if (neighbours.All(x => tile_count[t].Contains(x) && neighbour_count == tile_count[t].Count))
              {
                placement[row][col] = t;
                break;
              }
            }
            tilesToPlace.Remove(placement[row][col]);

          }
        }
      }

      foreach (var row in placement)
      {
        foreach (var c in row)
        {
          Console.Write(c + ", ");
        }
        Console.WriteLine();
      }
      Console.WriteLine(placement.Length);
      var found = TryPlace(new Dictionary<int, string[]>(), tiles[placement[0][0]], placement, 0, 0, tiles);
      Console.WriteLine(found);
      Console.WriteLine(Solution.Count);

      //remove borders

      foreach (var id in tile_ids)
      {
        var tile = Solution[id];
        var newTile = new string[tile.Length - 2];
        for (int i = 1; i < tile.Length - 1; i++)
        {
          newTile[i - 1] = tile[i][1..^1];
        }
        Solution[id] = newTile;
      }

      //make one tile
      int subsize = Solution[placement[0][0]].Length;
      int size = subsize * n;
      string[] combinedTile = new string[size];

      for (int row = 0; row < n; row++)
      {
        for (int subrow = 0; subrow < subsize; subrow++)
        {
          var sb = new StringBuilder();
          for (int col = 0; col < n; col++)
          {
            sb.Append(Solution[placement[row][col]][subrow]);
          }
          int i = row * subsize + subrow;
          combinedTile[i] = sb.ToString();
        }
      }
      var dragon = File.ReadAllLines("data/dragon.txt");
      var offsets = new List<Tuple<int, int>>();
      for (int i = 0; i < dragon[0].Length; i++)
      {
        for (int j = 0; j < dragon.Length; j++)
        {
          if (dragon[j][i] == '#')
          {
            offsets.Add(Tuple.Create(i, j));
          }
        }
      }

      foreach (var c in Combinations(combinedTile))
      {
        int dragon_count = 0;
        for (int i = 0; i < size; i++)
        {
          for (int j = 0; j < size; j++)
          {
            if (offsets.All(x => i + x.Item1 < c.Length && j + x.Item2 < c.Length && c[j + x.Item2][i + x.Item1] == '#'))
            {
              dragon_count++;
            }
          }
        }
        Console.WriteLine(dragon_count);
        if (dragon_count > 0)
        {
          int total = c.SelectMany(x => x).Where(x => x == '#').Count();
          int dragon_tile_count = dragon.SelectMany(x => x).Where(x => x == '#').Count();
          Console.WriteLine(total - dragon_count * dragon_tile_count);
        }

      }


      // foreach (var row in placement)
      // {
      //   for (int innerrow = 0; innerrow < tiles[placement[0][0]].Length; innerrow++)
      //   {
      //     foreach (var c in row)
      //     {
      //       Console.Write(Solution[c][innerrow] + " ");
      //     }
      //     Console.WriteLine();
      //   }

      //   Console.WriteLine();
      // }


    }

    private static Dictionary<int, string[]> Solution = null;

    private static bool TryPlace(Dictionary<int, string[]> placed, string[] tile, int[][] placement, int row, int col, Dictionary<int, string[]> tiles)
    {
      if (Solution != null) { return true; }
      int tile_id = placement[row][col];
      if (placed.ContainsKey(tile_id))
      {
        throw new Exception();
      }
      // Console.WriteLine($"{row}, {col}");
      foreach (var c in Combinations(tile))
      {
        if (row == 11 && col == 0)
        {
          Console.WriteLine("HERE");
        }
        if (!matches(c, placed, row, col, placement))
        {
          continue;
        }
        var new_placed = placed.ToDictionary(x => x.Key, x => x.Value);
        new_placed[placement[row][col]] = c;
        int new_row = row;
        int new_col = col + 1;
        if (new_col < placement.Length && !placed.ContainsKey(placement[new_row][new_col]))
        {
          var found = TryPlace(new_placed, tiles[placement[new_row][new_col]], placement, new_row, new_col, tiles);
          if (!found)
          {
            continue;
          }
        }
        new_row = row;
        new_col = col - 1;
        if (new_col >= 0 && !placed.ContainsKey(placement[new_row][new_col]))
        {
          var found = TryPlace(new_placed, tiles[placement[new_row][new_col]], placement, new_row, new_col, tiles);
          if (!found)
          {
            continue;
          }
        }
        new_row = row + 1;
        new_col = col;
        if (new_row < placement.Length && !placed.ContainsKey(placement[new_row][new_col]))
        {
          var found = TryPlace(new_placed, tiles[placement[new_row][new_col]], placement, new_row, new_col, tiles);
          if (!found)
          {
            continue;
          }
        }


        if (new_placed.Count == tiles.Count)
        {
          Solution = new_placed;
        }

        return true;

      }
      return false;
    }

    private static bool matches(string[] c, Dictionary<int, string[]> placed, int row, int col, int[][] placement)
    {
      if (col - 1 >= 0 && placed.ContainsKey(placement[row][col - 1]))
      {
        var west_tile = placed[placement[row][col - 1]];
        for (int i = 0; i < west_tile.Length; i++)
        {
          if (west_tile[i].Last() != c[i][0])
          {
            return false;
          }
        }
      }
      //East
      if (col + 1 < placement.Length && placed.ContainsKey(placement[row][col + 1]))
      {
        var east_tile = placed[placement[row][col + 1]];
        for (int i = 0; i < east_tile.Length; i++)
        {
          if (east_tile[i][0] != c[i][east_tile.Length - 1])
          {
            return false;
          }
        }
      }
      //south
      if (row + 1 < placement.Length && placed.ContainsKey(placement[row + 1][col]))
      {
        var south_tile = placed[placement[row - 1][col]];
        for (int i = 0; i < south_tile.Length; i++)
        {
          if (south_tile[0][i] != c[south_tile.Length - 1][i])
          {
            return false;
          }
        }
      }
      //north
      if (row - 1 >= 0 && placed.ContainsKey(placement[row - 1][col]))
      {
        var north_tile = placed[placement[row - 1][col]];
        for (int i = 0; i < north_tile.Length; i++)
        {
          if (north_tile[^1][i] != c[0][i])
          {
            return false;
          }
        }
      }
      return true;


    }

    private static string[][] Combinations(string[] tile)
    {
      var combinations = new string[8][];
      combinations[0] = tile.ToArray();
      combinations[1] = Rotate(combinations[0]);
      combinations[2] = Rotate(combinations[1]);
      combinations[3] = Rotate(combinations[2]);
      combinations[4] = Flip(combinations[0]);
      combinations[5] = Flip(combinations[1]);
      combinations[6] = Flip(combinations[2]);
      combinations[7] = Flip(combinations[3]);
      return combinations;
    }

    private static string[] Flip(string[] tile)
    {
      return tile.Reverse().ToArray();

    }

    private static string[] Rotate(string[] tile)
    {
      char[][] newTile = new char[tile.Length][];
      for (int i = 0; i < newTile.Length; i++)
      {
        newTile[i] = new char[newTile.Length];
      }
      for (int x = 0; x < tile.Length; x++)
      {
        for (int y = 0; y < tile.Length; y++)
        {
          newTile[y][x] = tile[tile.Length - 1 - x][y];
        }
      }
      return newTile.Select(x => new string(x)).ToArray();
    }
  }
}
