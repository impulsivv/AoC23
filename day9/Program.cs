using System;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.X509Certificates;

namespace day8
{
    class Program
    {
        static List<long> NumsDiff(List<long> nums)
        {
            List<long> result = new();
            for (int i = 0; i < nums.Count-1; i++)
                result.Add(nums[i + 1] - nums[i]);
            return result;
        }
        static List<List<long>> GetNextNum(List<long> nums)
        {
            List<List<long>> listOFNums = new(){nums};
            List<long> current = listOFNums.Last();
            while(!current.All(x => x == 0))
            {
                current = listOFNums.Last();
                listOFNums.Add(NumsDiff(current));         
            }
            return listOFNums;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            //part1
            var result = lines.Select(item => item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList())
                         .Select(nums => GetNextNum(nums)
                                            .Select(x=>x.Last())
                                            .Sum()
                         ).Sum();
            Console.WriteLine(result);

            //part2
            var result2 = lines.Select(item => item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList())
                          .Select(nums => GetNextNum(nums)
                                            .Select(x => x.First())
                                            .Reverse()
                                            .Aggregate(0, (long acc, long next) => next - acc)
                          ).Sum();
            Console.WriteLine(result2);

            IEnumerable<int> range = Enumerable.Range(1, 20);
            IEnumerable<int> banned = Enumerable.Range(15, 4);

            foreach (var item in range.Except(banned))
            {
                Console.WriteLine(item);
            };
        }
    }
}
