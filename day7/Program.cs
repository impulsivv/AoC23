using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace day1
{
    
    
    class Program
    {
        static (long[], long)[] SortArray( (string hand, long bet)[] array, bool joker=false)
        {
            (long[] hand, long bet)[] sortedArray = new (long[], long )[array.Length];
            int j = 0;
            /*
            //group and sort ascending by every digit -> janky ass Tree -> preorder traversal -> if Leaf not empty -> add to sorted array
            var rank1 = array.Select(x => (x.hand.Select(x =>CharToLong(x, joker)).ToArray(), x.bet)).GroupBy(n => n.Item1[0]).OrderBy(x => x.Key);
            foreach (var x1 in rank1)
                foreach (var x2 in x1.GroupBy(n => n.Item1[1]).OrderBy(x => x.Key))
                    foreach (var x3 in x2.GroupBy(n => n.Item1[2]).OrderBy(x => x.Key))
                        foreach (var x4 in x3.GroupBy(n => n.Item1[3]).OrderBy(x => x.Key))
                            foreach (var x5 in x4.GroupBy(n => n.Item1[4]).OrderBy(x => x.Key))
                                if (x5.First().Item1.GetType().IsArray)
                                    sortedArray[j++] = (x5.First().Item1, x5.First().bet);*/
            //better solution 
            sortedArray = array.Select(x=> (x.hand.Select(y=>CharToLong(y, joker)).ToArray(), x.bet))
                                            .OrderBy(c => c.Item1[0])
                                            .ThenBy(c1 => c1.Item1[1])
                                            .ThenBy(c2 => c2.Item1[2])
                                            .ThenBy(c3 => c3.Item1[3])
                                            .ThenBy(c4 => c4.Item1[4])
                                            .ToArray();
            return sortedArray;
        }

        static long CharToLong(char c, bool joker=false)
        {
                    if (c == 'T') return 10;
                    else if (c == 'J') return joker ? -1: 11;
                    else if (c == 'Q') return joker ? 11: 12;
                    else if (c == 'K') return joker ? 12: 13;
                    else if (c == 'A') return joker ? 13: 14;
                    else return (long)c - '0';
        }

        static long CheckHand( (string hand, long bet) line, bool joker=false)
        {
            Dictionary<char, int> uniques = new();
            foreach (var c in line.hand)
                if (uniques.ContainsKey(c)) uniques[c] += 1;
                else uniques[c] = 1;

            if (joker && uniques.ContainsKey('J'))
            {
                char k = uniques.Aggregate((l, r) => l.Value > r.Value ? l : r).Key; // key for max value
                int val = uniques['J']; // J value
                if (val == 5)
                    return 0;
                if (val == 4)
                    return 0;
                uniques.Remove('J');
                char k2 = uniques.FirstOrDefault(x => x.Value == uniques.Values.Max()).Key; // again
                uniques[k2] += val;
                
            }
            switch (uniques.Count)
            {
                case 1: return 0; // Five of a kind
                case 2: // Four of a kind OR Full house
                    foreach ((char k, int v) in uniques)
                        if(v == 4)
                            return 1;
                    return 2;
                 
                case 3:  //Three of a kind OR Two Pair
                    foreach ((char k, int v) in uniques)
                        if(v == 3)
                            return 3;
                    return 4;
                
                case 4: return 5; // One Pair
                case 5: return 6; // High Card
                default: return 10000000;
            }
        }

        static Dictionary<long, (string hand, long bet)[]>  GetTypesofHands( (string hand, long bet)[] input, bool joker=false)
        {
            Dictionary<long, (string hand, long bet)[]> typeOfHand = new();
            foreach(var line in input) 
            {
                long handEval = CheckHand(line, joker);
                if(typeOfHand.ContainsKey(handEval))
                    typeOfHand[handEval] = typeOfHand[handEval].Append( (line) ).ToArray();
                else
                    typeOfHand[handEval] = new (string,long)[] {line};
            }
            return typeOfHand;
        }


        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            (string hand, long bet)[] lines = input
                                                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                                                .Select(line => (line.Split(" ").First(), long.Parse(line.Split(" ").Last())) ).ToArray();
            //[part1, part2] 
            foreach (var joker in new bool[]{false, true})
            {
                long rank = 1;
                var toh = GetTypesofHands(lines, joker);
                long result = toh.Keys
                            .OrderDescending() // 6 Bad Hand --> 0 Best Hand
                            .Select(k => SortArray(toh[k], joker) // decide who got better hand
                                            .Select(x => x.Item2 * rank++).Sum()
                            )
                            .Sum();
                Console.WriteLine(result);  
            }
        }
    }
}
