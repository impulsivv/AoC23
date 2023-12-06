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
        static void GetSurroundedCoords((int srow, int scol) sChar, ref HashSet<(int row, int col)> coords)
        {
            for (int row = -1; row < 2; row++)
                for (int col = -1; col < 2; col++)
                    coords.Add( (sChar.srow + row, sChar.scol + col) );
        }

        static bool CheckIfNeeded(Match m, HashSet<(int row, int col)> coords, int row)
        {
            for (int col = 0; col < m.Length; col++)
                if (coords.Contains( (row, m.Index + col) ))
                    return true;

            return false;
        }

        static int CheckIfTwoAround(MatchCollection m1, MatchCollection m2, MatchCollection m3, (int row, int col) coord)
        {
            HashSet<(int row, int col)> coords = new();
            GetSurroundedCoords(coord, ref coords);
            List<int> results = new();

            foreach (Match m in m1)
            {
                for (int col = 0; col < m.Length; col++)
                    if (coords.Contains( (coord.row - 1, m.Index + col) ))
                        results.Append(int.Parse(m.Value));
            }
            foreach (Match m in m2)
            {
                for (int col = 0; col < m.Length; col++)
                    if (coords.Contains( (coord.row, m.Index + col) ))
                        results.Append(int.Parse(m.Value));
            }
            foreach (Match m in m3)
            {
                for (int col = 0; col < m.Length; col++)
                    if (coords.Contains( (coord.row + 1, m.Index + col) ))
                        results.Append(int.Parse(m.Value));
            }

            if (results.Count == 2)
                return results.Aggregate(1, (int accu, int next) => accu * next);
            else return 0;

        }
        //x - 48
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split("\n");

            char[][] matrix = lines.Select(x => x.ToArray()).ToArray();
            char[] sChars = new char[]{'/','*','%','+','-','#','&','=','$','@'};
            //part1
            int p1Result = 0;
            HashSet<(int row, int col)> coords = new();
            
            for (int row = 0; row < matrix.Length; row++)
                for (int col = 0; col < matrix[row].Length; col++)
                    if (sChars.Contains(matrix[row][col])) 
                        GetSurroundedCoords((row, col), ref coords);

            Regex regexNumbers = new Regex(@"\d+");
            for (int row = 0; row < lines.Length; row++)
                foreach (Match m in regexNumbers.Matches(lines[row]))
                    p1Result += CheckIfNeeded(m, coords, row) ? int.Parse(m.Value) : 0;

            Console.WriteLine(p1Result);



            
            //part2
            int p2Result = 0;
            HashSet<(int row, int index, int len)> coordsNums = new();

            Regex regexStar = new Regex(@"\*");
            for (int row = 0; row < lines.Length; row++)
            {   
                //var nums = regexNumbers.Matches(lines[row]);
                //foreach (Match m in nums) coordsNums.Add( (row, m.Index, m.Length) );
                    //p2Result += CheckIfNeeded(m, coords, row) ? int.Parse(m.Value) : 0;

                var stars = regexStar.Matches(lines[row]);
                foreach (Match m in stars)
                {
                    (int row, int col) starCoord = (row, m.Index);
                    string row1 = lines[row-1];
                    string row2 = lines[row];
                    string row3 = lines[row+1];
                    var nums1 = regexNumbers.Matches(row1);
                    var nums2 = regexNumbers.Matches(row2);
                    var nums3 = regexNumbers.Matches(row3);

                    p2Result += CheckIfTwoAround(nums1,nums2,nums3,starCoord); 

                }
            }
            Console.WriteLine(p2Result);
    
        }
    }
}
