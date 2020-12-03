using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day3
  {
    private const char Tree = '#';
    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata3.txt");
      Console.WriteLine("Answer data:");
      Run("data/data3.txt");
    }
    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      int number_of_trees = Run(lines, 3, 1);
      Console.WriteLine($"Number of trees = {number_of_trees}");

      int product = 1;
      var pairs = new[] { new { x = 1, y = 1 },
                          new { x = 3, y = 1 },
                          new {x= 5, y= 1},
                          new {x= 7, y=1},
                          new {x=1 , y=2} };
      foreach (var pair in pairs)
      {
        product *= Run(lines, pair.x, pair.y);
      }
      Console.WriteLine($"B = {product}");


    }

    static int Run(string[] lines, int step_size_x, int step_size_y)
    {
      int width = lines[0].Length;
      int height = lines.Length;
      int number_of_trees = 0;
      for (int i = 0; i < height / step_size_y; i++)
      {
        if (lines[i * step_size_y][(i * step_size_x) % width] == Tree)
        {
          number_of_trees++;
        }
      }
      return number_of_trees;

    }


  }
}