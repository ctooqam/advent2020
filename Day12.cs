using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day12
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata12.txt");
      Console.WriteLine("Answer data:");
      Run("data/data12.txt");

      Console.WriteLine("Test data:");
      RunB("data/testdata12.txt");
      Console.WriteLine("Answer data:");
      RunB("data/data12.txt");
    }
    static void RunB(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      int y = 0;
      int x = 0;
      int way_x = 10;
      int way_y = 1;
      foreach (var line in lines)
      {
        int num = int.Parse(line.Substring(1, line.Length - 1));
        switch (line[0])
        {
          case 'N':
            {
              way_y += num;
              break;
            }
          case 'W':
            {
              way_x -= num;
              break;
            }
          case 'S':
            {
              way_y -= num;
              break;
            }
          case 'E':
            {
              way_x += num;
              break;
            }
          case 'L':
            {
              if (num == 180)
              {
                way_x = -way_x;
                way_y = -way_y;
              }
              else if (num == 90)
              {
                int temp = way_x;
                way_x = -way_y;
                way_y = temp;

              }
              else if (num == 270)
              {
                int temp = way_x;
                way_x = way_y;
                way_y = -temp;


              }
              else
              {
                Console.WriteLine(num);
                throw new InvalidOperationException();
              }
              break;
            }
          case 'R':
            {
              if (num == 180)
              {
                way_x = -way_x;
                way_y = -way_y;
              }
              else if (num == 90)
              {
                int temp = way_x;
                way_x = way_y;
                way_y = -temp;
              }
              else if (num == 270)
              {
                int temp = way_x;
                way_x = -way_y;
                way_y = temp;

              }
              else { throw new InvalidOperationException(); }
              break;
            }
          case 'F':
            {
              x += way_x * num;
              y += way_y * num;

              break;
            }

          default:
            {
              throw new InvalidOperationException();
            }
        }
      }
      Console.WriteLine($"{x}, {y} gives {Math.Abs(x) + Math.Abs(y)}");

    }

    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      int current_dir = 0;
      int y = 0;
      int x = 0;
      foreach (var line in lines)
      {
        int num = int.Parse(line.Substring(1, line.Length - 1));
        switch (line[0])
        {
          case 'N':
            {
              y += num;
              break;
            }
          case 'W':
            {
              x -= num;
              break;
            }
          case 'S':
            {
              y -= num;
              break;
            }
          case 'E':
            {
              x += num;
              break;
            }
          case 'L':
            {
              current_dir = (current_dir - num) % 360;
              break;
            }
          case 'R':
            {
              current_dir = (current_dir + num) % 360; ;
              break;
            }
          case 'F':
            {
              if (current_dir == 0)
              {
                x += num;
              }
              else if (current_dir == 90 || current_dir == -270)
              {
                y -= num;
              }
              else if (current_dir == -90 || current_dir == 270)
              {
                y += num;
              }
              else
              {
                x -= num;
              }


              break;
            }

          default:
            {
              throw new InvalidOperationException();
            }
        }

      }
      Console.WriteLine($"{x}, {y} gives {Math.Abs(x) + Math.Abs(y)}");

    }
  }
}