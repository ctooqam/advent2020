using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent2020

{
  class Day13
  {

    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata13.txt");
      Console.WriteLine("Answer data:");
      Run("data/data13.txt");
    }
    static void Run(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      int time = int.Parse(lines[0]);
      int[] bustimes = lines[1].Split(',').Where(x => x != "x").Select(int.Parse).ToArray();
      int min = int.MaxValue;
      int bus_id = 0;
      foreach (var bus in bustimes)
      {
        int minutes = bus - (time % bus);
        if (minutes < min)
        {
          min = minutes;
          bus_id = bus;
        }
      }
      Console.WriteLine($"min: {min} and bus_id {bus_id} gives {bus_id * min}");
    }

  }
}