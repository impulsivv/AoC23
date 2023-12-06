using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day1
{
    class Program
    {
 
        static void Main(string[] args)
        {
            
            string input = File.ReadAllText(@"./input.txt").Replace("\r", "");
            string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            //part1
            var we = lines
                .Select(x => x.ToCharArray().Where(Char.IsDigit))
                .Select(nums => int.Parse(nums.First().ToString() + nums.Last().ToString()) );
            //Console.WriteLine(we.Sum());

            //part2
            var replacements = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 } };
            var we2 = lines
                .Select(line => new Regex(@"one|two|three|four|five|six|seven|eight|nine|\d")
                        .Matches(line)
                        .Select(x => int.TryParse(x.ToString(), out int num) ? num : replacements[x.ToString()] ))
                .Select(nums => int.Parse(nums.First().ToString() + nums.Last().ToString()) );

            var count1 = 0;
            foreach (var item in new Regex(@"one|two|three|four|five|six|seven|eight|nine|\d").Matches("2rzvpfpgzxk3863eightoneighttbb"))
            {
                Console.WriteLine(count1 + ":" +item);
            }

            Console.WriteLine(we2.Sum());
        }
    }
}
