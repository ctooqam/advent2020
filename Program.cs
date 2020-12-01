using System;
using System.IO;
using System.Linq;

namespace advent2020
{
  class Program
  {
    static void Main(string[] args)
    {
      RunA("testdata1.txt");
      RunB("testdata1.txt");
      RunA("data1.txt");
      RunB("data1.txt");
    }

    static void RunA(string filepath) {
      var lines = File.ReadAllLines(filepath);
      int[] values = lines.Select(int.Parse).ToArray();
      for (int i = 0; i < values.Length; i++)
      {
          for (int j = i; j < values.Length; j++)
          {
            if(values[i]+ values[j] == 2020)  {
              Console.WriteLine(values[i] * values[j]);
              return;
            }
          }
      }      
    }
    static void RunB(string filepath) {
      var lines = File.ReadAllLines(filepath);
      int[] values = lines.Select(int.Parse).ToArray();
      for (int i = 0; i < values.Length; i++)
      {
          for (int j = i; j < values.Length; j++)
          {
            for (int k = j; k < values.Length; k++)
            {
              if(values[i]+ values[j] + values[k] == 2020)  {
              Console.WriteLine(values[i] * values[j] *values[k]);
              return;
            }
                
            }
            
          }
      }      
    }
  }    
}
