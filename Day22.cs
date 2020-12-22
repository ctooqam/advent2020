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

      int winner = Play(cards[0], cards[1]);


      long ans = 0;
      var winningDeck = cards[0].Count != 0 ? cards[0] : cards[1];
      while (winningDeck.Count != 0)
      {
        ans += winningDeck.Dequeue() * total_nbr_of_cards;
        total_nbr_of_cards--;
      }
      Console.WriteLine(ans);

    }

    private static int Play(Queue<int> p1, Queue<int> p2)
    {
      var visited = new HashSet<Tuple<string, string>>();
      while (p1.Count != 0 && p2.Count != 0)
      {
        var key1 = p1.Aggregate("", (p, n) => p + "," + n.ToString());
        var key2 = p2.Aggregate("", (p, n) => p + "," + n.ToString());
        // Console.WriteLine(key1 + " " + key2);
        if (visited.Contains(Tuple.Create(key1, key2)) || visited.Contains(Tuple.Create(key2, key1))) //check reverse?
        {
          // Console.WriteLine("Player one wins");
          return 1;
        }
        visited.Add(Tuple.Create(key1, key2));
        int card1 = p1.Dequeue();
        int card2 = p2.Dequeue();
        if (card1 == card2) { throw new Exception(); }
        if (card1 <= p1.Count && card2 <= p2.Count)
        {
          var p1new = new Queue<int>();
          var p2new = new Queue<int>();
          foreach (var c in p1.Take(card1))
          {
            p1new.Enqueue(c);
          }
          foreach (var c in p2.Take(card2))
          {
            p2new.Enqueue(c);
          }
          int winner = Play(p1new, p2new);
          if (winner == 1)
          {
            p1.Enqueue(card1);
            p1.Enqueue(card2);
          }
          else
          {
            p2.Enqueue(card2);
            p2.Enqueue(card1);
          }
        }
        else
        {

          if (card1 > card2)
          {
            p1.Enqueue(card1);
            p1.Enqueue(card2);
          }
          else
          {
            p2.Enqueue(card2);
            p2.Enqueue(card1);
          }
        }
      }
      return p1.Count != 0 ? 1 : 2;
    }
  }
}
