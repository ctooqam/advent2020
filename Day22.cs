using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent2020

{
  class Day22
  {
    public static void Run()
    {
      Console.WriteLine("Test data:");
      Run("data/testdata22.txt");

      Console.WriteLine("Answer data:");
      Run("data/data22.txt");
    }

    static void Run(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      var cards = new List<Queue<int>>();
      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line)) { continue; }
        if (line.StartsWith("Player"))
        {
          cards.Add(new Queue<int>());
        }
        else
        {
          cards.Last().Enqueue(int.Parse(line));
        }
      }
      int total_nbr_of_cards = cards.Sum(x => x.Count);
      while (!cards.Any(x => x.Count == 0))
      {
        int card1 = cards[0].Dequeue();
        int card2 = cards[1].Dequeue();
        if (card1 == card2) { throw new Exception(); }
        if (card1 > card2)
        {
          cards[0].Enqueue(card1);
          cards[0].Enqueue(card2);
        }
        else
        {
          cards[1].Enqueue(card2);
          cards[1].Enqueue(card1);
        }
      }


      long ans = 0;
      var winner = cards[0].Count != 0 ? cards[0] : cards[1];
      while (winner.Count != 0)
      {
        ans += winner.Dequeue() * total_nbr_of_cards;
        total_nbr_of_cards--;
      }
      Console.WriteLine(ans);

    }
  }
}
