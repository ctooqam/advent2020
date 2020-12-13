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
      // Console.WriteLine("Test data:");
      // Run("data/testdata13.txt");
      // Console.WriteLine("Answer data:");
      // Run("data/data13.txt");

      Console.WriteLine("Test data:");
      RunB("data/testdata13.txt");
      Console.WriteLine("Test data b:");
      RunB("data/testdata13b.txt");
      Console.WriteLine("Answer data:");
      RunB("data/data13.txt");
    }

    static void RunB(string filepath)
    {
      var lines = File.ReadAllLines(filepath);
      var splitted = lines[1].Split(',');
      List<Tuple<int, int>> bustimes = new List<Tuple<int, int>>();
      for (int i = 0; i < splitted.Length; i++)
      {
        if (splitted[i] != "x")
        {
          bustimes.Add(Tuple.Create(int.Parse(splitted[i]), i));
        }
      }

      long stepsize = bustimes[0].Item1;
      long candidate = 0;

      for (int i = 1; i < bustimes.Count; i++)
      {
        while ((candidate + bustimes[i].Item2) % bustimes[i].Item1 != 0)
        {
          candidate += stepsize;
        }
        stepsize *= bustimes[i].Item1;
      }
      Console.WriteLine(candidate);

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