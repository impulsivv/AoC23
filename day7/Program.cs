using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace day1
{
    
    
    class Program
    {
        
        static long CheckHand( (string hand, long bet) line)
        {
            Dictionary<char, int> uniques = new();
            foreach (var c in line.hand)
                if (uniques.ContainsKey(c)) uniques[c] += 1;
                else uniques[c] = 1;

            switch (uniques.Count)
            {
                case 1: return 0; // Five of a kind
                case 2: { // Four of a kind OR Full house
                    foreach ((char k, int v) in uniques)
                        if(v == 4)
                            return 1;
                    return 2;
                } 
                case 3: { //Three of a kind OR Two Pair
                    foreach ((char k, int v) in uniques)
                        if(v == 3)
                            return 3;
                    return 4;
                }
                case 4: return 5; // One Pair
                case 5: return 6; // High Card
                default: return 10000000;
            }
        }

        static long charToLong(char c)
        {
                    if (c == 'T') return 10;
                    else if (c == 'J') return 11;
                    else if (c == 'Q') return 12;
                    else if (c == 'K') return 13;
                    else if (c == 'A') return 14;
                    else return (long)c - '0';
        }

        static (long[], long)[] SortArray( (string hand, long bet)[] array)
        {
            (long[] hand, long bet)[] sortedArray = new (long[], long )[array.Length];
            int j = 0;

            var rank1 = array.Select(x => (x.hand.Select(x =>charToLong(x)).ToArray(), x.bet))
                            .GroupBy(n => n.Item1[0])
                            .OrderBy(x => x.Key);
            foreach (var x1 in rank1)
                foreach (var x2 in x1.GroupBy(n => n.Item1[1]).OrderBy(x => x.Key))
                    foreach (var x3 in x2.GroupBy(n => n.Item1[2]).OrderBy(x => x.Key))
                        foreach (var x4 in x3.GroupBy(n => n.Item1[3]).OrderBy(x => x.Key))
                            foreach (var x5 in x4.GroupBy(n => n.Item1[4]).OrderBy(x => x.Key))
                                if (x5.First().Item1.GetType().IsArray)
                                    sortedArray[j++] = (x5.First().Item1, x5.First().bet);

            
            //sortedArray = sortedArray.OrderBy(c => c.hand[0]).ThenBy(c => c.hand[1]).ThenBy(c => c.hand[2]).ThenBy(c => c.hand[3]).ThenBy(c => c.hand[4]).ToArray();
            return sortedArray;
        }
        static long CheckHand2( (string hand, long bet) line)
        {
            Dictionary<char, int> uniques = new();
            foreach (var c in line.hand)
                if (uniques.ContainsKey(c)) uniques[c] += 1;
                else uniques[c] = 1;

            if (uniques.ContainsKey('J'))
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
                case 2: { // Four of a kind OR Full house
                    foreach ((char k, int v) in uniques)
                        if(v == 4)
                            return 1;
                    return 2;
                } 
                case 3: { //Three of a kind OR Two Pair
                    foreach ((char k, int v) in uniques)
                        if(v == 3)
                            return 3;
                    return 4;
                }
                case 4: return 5; // One Pair
                case 5: return 6; // High Card
                default: return 10000000;
            }
        }

        static long charToLong2(char c)
        {
                    if (c == 'T') return 10;
                    else if (c == 'J') return -1;
                    else if (c == 'Q') return 11;
                    else if (c == 'K') return 12;
                    else if (c == 'A') return 13;
                    else return (long)c - '0';
        }

        static (long[], long)[] SortArray2( (string hand, long bet)[] array)
        {
            (long[] hand, long bet)[] sortedArray = new (long[], long )[array.Length];
            int j = 0;

            var rank1 = array.Select(x => (x.hand.Select(x =>charToLong2(x)).ToArray(), x.bet))
                            .GroupBy(n => n.Item1[0])
                            .OrderBy(x => x.Key);
            foreach (var x1 in rank1)
                foreach (var x2 in x1.GroupBy(n => n.Item1[1]).OrderBy(x => x.Key))
                    foreach (var x3 in x2.GroupBy(n => n.Item1[2]).OrderBy(x => x.Key))
                        foreach (var x4 in x3.GroupBy(n => n.Item1[3]).OrderBy(x => x.Key))
                            foreach (var x5 in x4.GroupBy(n => n.Item1[4]).OrderBy(x => x.Key))
                                if (x5.First().Item1.GetType().IsArray)
                                    sortedArray[j++] = (x5.First().Item1, x5.First().bet);

            
            //sortedArray = sortedArray.OrderBy(c => c.hand[0]).ThenBy(c => c.hand[1]).ThenBy(c => c.hand[2]).ThenBy(c => c.hand[3]).ThenBy(c => c.hand[4]).ToArray();
            return sortedArray;
        }

        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            (string hand, long bet)[] lines = input
                                                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                                                .Select(line => (line.Split(" ").First(), long.Parse(line.Split(" ").Last())) ).ToArray();
            //part1
            Dictionary<long, (string hand, long bet)[]> typeOfHand = new();
            foreach(var line in lines) 
                if(typeOfHand.ContainsKey(CheckHand(line)))
                    typeOfHand[CheckHand(line)] = typeOfHand[CheckHand(line)].Append( (line) ).ToArray();
                else
                    typeOfHand[CheckHand(line)] = new (string,long)[] {line};
            
            long rank = 1;
            long result = typeOfHand.Keys
                        .OrderDescending() // 6 Bad Hand --> 0 Best Hand
                        .Select(k => SortArray(typeOfHand[k]) // decide who got better hand
                                       // .Reverse()
                                        .Select(x => x.Item2 * rank++).Sum()
                        )
                        .Sum();
            Console.WriteLine(result);


            //part2
            Dictionary<long, (string hand, long bet)[]> typeOfHand2 = new();
            foreach(var line in lines)
            {
                long handEval = CheckHand2(line);
                if(typeOfHand2.ContainsKey(handEval))
                    typeOfHand2[handEval] = typeOfHand2[handEval].Append( (line) ).ToArray();
                else
                    typeOfHand2[handEval] = new (string,long)[] {line};
            }
            rank = 1;
            long result2 = typeOfHand2.Keys
                        .OrderDescending() // 6 Bad Hand --> 0 Best Hand
                        .Select(k => SortArray2(typeOfHand2[k]) // decide who got better hand
                                       // .Reverse()
                                        .Select(x => x.Item2 * rank++).Sum()
                        )
                        .Sum();
            Console.WriteLine(result2);
            //string t = lines[0].Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Aggregate("",(string acc, string next) => acc + next);
            //string d = lines[1].Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Aggregate("",(string acc, string next) => acc + next);
            //Console.WriteLine(CheckRace( (long.Parse(t), long.Parse(d)) ));
        }
    }
}
