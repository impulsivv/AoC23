using System;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace day1
{
    class Program
    {
        static int GetTouchingNumbers(int row, string[] lines, int starCol, int maxTouch=0)
        {   
            Regex regexNumbers = new Regex(@"\d+");
            List<int> results = new();
            foreach (Match m in regexNumbers.Matches(lines[row-1]))
                if(Enumerable.Range(m.Index , m.Length).Intersect(Enumerable.Range(starCol -1, 3)).Count() != 0)
                    results.Add(int.Parse(m.Value));
            foreach (Match m in regexNumbers.Matches(lines[row]))
                if(Enumerable.Range(m.Index , m.Length).Intersect(Enumerable.Range(starCol -1, 3)).Count() != 0)
                    results.Add(int.Parse(m.Value));
            foreach (Match m in regexNumbers.Matches(lines[row+1]))
                if(Enumerable.Range(m.Index , m.Length).Intersect(Enumerable.Range(starCol -1, 3)).Count() != 0)
                    results.Add(int.Parse(m.Value));
            
            if (maxTouch > 0 && maxTouch == results.Count)
                return results.Aggregate(1, (int accu, int next) => accu * next);
            else if (maxTouch == 0)
                return results.Aggregate(0, (int accu, int next) => accu + next);
            else 
                return 0;
        }
        //x - 48
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split("\n");
            Regex regexStar = new Regex(@"\*");
            Regex regexSChars = new Regex(@"\*|/|%|\+|-|#|&|=|\$|@");
            //part1
            int p1Result = 0;
            //no special chars on first and last row anyways :)
            for (int row = 1; row < lines.Length - 1; row++)
                foreach (Match m in regexSChars.Matches(lines[row]))
                    p1Result += GetTouchingNumbers(row, lines, m.Index); 
            Console.WriteLine(p1Result);
            //part2
            int p2Result = 0;
            for (int row = 1; row < lines.Length - 1; row++)
                foreach (Match m in regexStar.Matches(lines[row]))
                    p2Result += GetTouchingNumbers(row, lines, m.Index, 2); 
            Console.WriteLine(p2Result);
    
        }
    }
}
