using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static long CheckRace( (long time, long dist) next)
        {
            long acc = 0;
            for (int i = 0; i < next.time; i++)
                if (i * (next.time - i) > next.dist)
                    acc++;
            return acc;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            long[] times = lines[0].Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
            long[] dists = lines[1].Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
            //part1
            (long time, long dist)[] races = times.Zip(dists, (l,n) => (l,n)).ToArray();
            long result = races.Select(x => CheckRace(x)).Aggregate(1, (long acc, long next) => acc * next);
            Console.WriteLine(result);
            //part2
            string t = lines[0].Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Aggregate("",(string acc, string next) => acc + next);
            string d = lines[1].Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Aggregate("",(string acc, string next) => acc + next);
            Console.WriteLine(CheckRace( (long.Parse(t), long.Parse(d)) ));
        }
    }
}
