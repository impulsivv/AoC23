using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace day1
{
    class Program
    {
        static List<long> SetSeeds(List<long> seed, string map)
        {
            Console.WriteLine("doin" + map);
            List<long> newSeed = new();
            for (int i = 0; i < seed.Count; i++)
                {   
                    bool found = false;
                    string[] numToNum = map.Split(":").Last().Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in numToNum)
                    {
                        long[] instruction = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).AsParallel().Select(x => long.Parse(x)).ToArray();
                        //instruction: Dstart | Sstart | range
                            //mapTo.Add(instruction[1] + i, instruction[0] + i);
                        if (!found && seed[i] < instruction[1] + instruction[2] && seed[i] >= instruction[1] )
                        {
                            found = true;
                            newSeed.Add(instruction[0] + Math.Abs(seed[i] - instruction[1]));
                        }

                    }


                } 
            return newSeed;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] maps = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            //part1
            long[] seeds = maps.First().Split(": ").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).AsParallel().Select(x => long.Parse(x)).ToArray();
            
    	    List<long> newSeeds = new();
            for (long i = seeds[0]; i < seeds[0] + seeds[1]; i++)
                newSeeds.Add(i);
            for (long i = seeds[2]; i < seeds[2] + seeds[3]; i++)
                newSeeds.Add(i);
            /* 
            for (long i = 1; i < seeds.Length; i += 2)
                newSeeds.Add(i);
            //Console.WriteLine(newSeeds.Contains(seeds[1]));
           
            foreach (var map in maps.Skip(1))
            {
                for (int i = 0; i < seeds.Length; i++)
                {   
                    bool found = false;
                    string[] numToNum = map.Split(":").Last().Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in numToNum)
                    {
                        long[] instruction = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
                        //instruction: Dstart | Sstart | range
                            //mapTo.Add(instruction[1] + i, instruction[0] + i);
                        if (seeds[i] < instruction[1] + instruction[2] && seeds[i] >= instruction[1] && !found)
                        {
                            found = true;
                            seeds[i] = instruction[0] + Math.Abs(seeds[i] - instruction[1]);
                        }

                    }


                } 
                //Dictionary<long, long> mapTo = new();
                
 
            }
            Console.WriteLine(seeds.Min());
*/
            //part2
            //Console.WriteLine(maps.Aggregate(newSeeds, (List<long> acc, string map) => SetSeeds(acc, map)).Min());
            
            foreach (var map in maps.Skip(1))
            {
                Console.WriteLine("doin" + map);
                long[][] numToNum = map
                                    .Split(":")
                                    .Last()
                                    .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => x
                                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                .Select(x => long.Parse(x))
                                                .ToArray()
                                    ).ToArray();
                for (int i = 0; i < newSeeds.Count; i++)
                {   
                    bool found = false;
                    foreach (long[] instruction in numToNum)
                    {
                        //long[] instruction = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
                        //instruction: Dstart | Sstart | range
                            //mapTo.Add(instruction[1] + i, instruction[0] + i);
                        if (!found && newSeeds[i] < instruction[1] + instruction[2] && newSeeds[i] >= instruction[1]  )
                        {
                            found = true;
                            newSeeds[i] = instruction[0] + Math.Abs(newSeeds[i] - instruction[1]);
                        }

                    }


                } 
                //Dictionary<long, long> mapTo = new();
                
 
            }
            Console.WriteLine(newSeeds.Min());
        }
    }
}
