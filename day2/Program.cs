using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace day2
{
    class Program
    {
        
        static int DecideRound(string round)
        {
            Dictionary<string, int> stuff = new Dictionary<string, int>{ {"blue", 14}, {"red", 12}, {"green", 13} };
            foreach (var picks in round.Split(", "))
            {
                stuff[picks.Split(" ").Last()] -= int.Parse(picks.Split(" ").First());
            }
            if (stuff.Any(x => x.Value < 0))
                return 0;
            else
                return 1;
        }

        static (int,int,int) DecideRound2(string round)
        {
            Dictionary<string, int> stuff = new Dictionary<string, int>{ {"blue", 1}, {"red", 1}, {"green", 1} };
            foreach (var picks in round.Split(", "))
            {
                stuff[picks.Split(" ").Last()] = int.Parse(picks.Split(" ").First()) > stuff[picks.Split(" ").Last()] ?
                    int.Parse(picks.Split(" ").First()):
                    stuff[picks.Split(" ").Last()];
            }
            return (stuff["blue"],stuff["red"],stuff["green"]);
        }

        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int result = 0;
            //part1
            foreach (var line in lines)
            {
                int gameId = int.Parse(line.Split(": ").First().Split(" ").Last());
                int we = line.Split(": ").Last().Split("; ").Select(x => DecideRound(x)).Aggregate(1, (int accu, int next) => accu * next);

                result += gameId * we;
            }
            Console.WriteLine(result);

            //part2
            result = 0;
            foreach (var line in lines)
            {
                int gameId = int.Parse(line.Split(": ").First().Split(" ").Last());
                (int,int,int) we = line.Split(": ").Last().Split("; ")
                                .Select(x => DecideRound2(x))
                                .Aggregate((0,0,0), ((int,int,int) accu, (int,int,int) next) 
                                    => (Math.Max(next.Item1,accu.Item1),Math.Max(next.Item2,accu.Item2),Math.Max(next.Item3,accu.Item3))
                                );

                result += we.Item1 * we.Item2 * we.Item3;
            }
            Console.WriteLine(result);

        }
    }
}
