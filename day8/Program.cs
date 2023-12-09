using System;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace day8
{
    class Program
    {
        static long LCM(long[] numbers)
        {
            return numbers.Aggregate((a,b) => Math.Abs(a * b) / GCD(a, b));
        }
        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        static long CountSteps(string start, string finish, Dictionary<string, (string left, string right)> nodeDict, char[] instr)
        {
            int i = 0;
            int result = 0;
            string currentKey = start;
            while (! currentKey.EndsWith(finish))
            {
                if(i >= instr.Length) i = 0;
                if(instr[i] == 'L') 
                    currentKey = nodeDict[currentKey].left;
                else
                    currentKey = nodeDict[currentKey].right;
                i++;
                result++;
            }

            return result;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            char[] instr = lines.First().ToArray();
            string[] nodes = lines.Last().Split("\n", StringSplitOptions.RemoveEmptyEntries).ToArray();
            Dictionary<string, (string left, string right)> nodeDict = new();
            foreach (var node in nodes)
            {
                nodeDict.Add(node.Split(" = ").First(), (node.Split(" = ").Last().Split(", ").First().Substring(1), node.Split(" = ").Last().Split(", ").Last().Substring(0,3)));
            }
            //part1
            long result = CountSteps("AAA", "ZZZ", nodeDict, instr);
            Console.WriteLine(result);

            //part2

            string[] startingKeys = nodeDict.Keys.Where(x => x.EndsWith("A")).ToArray();
            long[] results = startingKeys.Select(x => CountSteps(x, "Z", nodeDict, instr)).ToArray();
            Console.WriteLine(LCM(results));
            
        }
    }
}
