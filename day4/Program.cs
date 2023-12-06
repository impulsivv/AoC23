using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static int contains(int leftlower, int leftupper, int rightlower, int rightupper)
        {
            if (leftlower <= rightlower && leftupper >= rightupper) return 1;
            else if (rightlower <= leftlower && rightupper >= leftupper) return 1;
            else return 0;
        }

        static int overlap(int leftlower, int leftupper, int rightlower, int rightupper)
        {
            IEnumerable<int> left = Enumerable.Range(leftlower, leftupper - leftlower + 1);
            IEnumerable<int> right = Enumerable.Range(rightlower, rightupper - rightlower + 1);
            IEnumerable<int> inter = left.Intersect(right);

            return inter.Any() ? 1 : 0;

        }
        static (int, int, int ,int) getBounds(string pair)
        {
            string[] elves = pair.Split(",");
            string[] left = elves[0].Split("-");
            string[] right = elves[1].Split("-");
            int leftlower = Int32.Parse(left[0]);
            int leftupper = Int32.Parse(left[1]);
            int rightlower = Int32.Parse(right[0]);
            int rightupper = Int32.Parse(right[1]);
            return (leftlower, leftupper, rightlower, rightupper);
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            //part1
            double gameResult = 0;
            foreach (var card in lines)
            {
                int[] cardPick = card
                                .Split(": ")
                                .Last()
                                .Split(" | ")
                                .First()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => int.Parse(x))
                                .ToArray();

                int[] cardwin = card
                                .Split(": ")
                                .Last()
                                .Split(" | ")
                                .Last()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => int.Parse(x))
                                .ToArray();

                gameResult += (int)Math.Pow(2, cardwin.Intersect(cardPick).Count() -1);
            } 
            Console.WriteLine(gameResult);

            //part2
            Dictionary<int, int> copies = new();
            for (int i = 0; i < lines.Count(); i++)
                copies.Add(i, 1);
            
            for (int i = 0; i < lines.Count(); i++)
            {
                int[] cardPick = lines[i]
                                .Split(": ")
                                .Last()
                                .Split(" | ")
                                .First()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => int.Parse(x))
                                .ToArray();

                int[] cardwin = lines[i]
                                .Split(": ")
                                .Last()
                                .Split(" | ")
                                .Last()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => int.Parse(x))
                                .ToArray();
                //Console.WriteLine(pick.First());
                var foo = cardwin.Intersect(cardPick);
                for (int times = 1; times < foo.Count() + 1; times++)
                        copies[i + times] += copies[i];
            } 
            Console.WriteLine(copies.Values.Sum());

        }
    }
}
