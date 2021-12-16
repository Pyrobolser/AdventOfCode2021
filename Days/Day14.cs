using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Days
{
    public class Day14Part1
    {
        public static int Run()
        {
            var input = File.ReadAllLines(@"Inputs\Day14.txt");
            var polymer = input[0];
            Dictionary<string, char> rules = new();

            for(int i = 2; i < input.Length; i++)
            {
                var rule = input[i].Split(" -> ");
                rules.Add(rule[0], rule[1][0]);
            }

            var sb = new StringBuilder();
            for (int n = 0; n < 10; n++)
            {
                for (int i = 0; i < polymer.Length - 1; i++)
                {
                    var pair = polymer.Substring(i, 2);
                    rules.TryGetValue(pair, out char result);
                    sb.Append(pair[0]);
                    sb.Append(result);
                }
                sb.Append(polymer[^1]);
                polymer = sb.ToString();
                sb.Clear();
            }

            var groups = polymer.GroupBy(x => x).OrderByDescending(g => g.Count()).ToList();

            return groups.First().Count() - groups.Last().Count();
        }
    }

    public class Day14Part2
    {
        public static long Run()
        {
            var input = File.ReadAllLines(@"Inputs\Day14.txt");
            Dictionary<string, char> rules = new();
            Dictionary<string, long> polymer = new();
            Dictionary<string, long> nextPolymer = new();
            Dictionary<char, long> count = new();

            for (int i = 2; i < input.Length; i++)
            {
                var rule = input[i].Split(" -> ");
                var value = 0;
                rules.Add(rule[0], rule[1][0]);

                polymer.Add(rule[0], value);
                nextPolymer.Add(rule[0], value);

                count.TryAdd(rule[0][0], 0);
                count.TryAdd(rule[0][1], 0);
            }

            for (int i = 0; i < input[0].Length - 1; i++)
            {
                polymer[string.Concat(input[0][i], input[0][i + 1])] += 1;
                nextPolymer[string.Concat(input[0][i], input[0][i + 1])] += 1;
            }

            for (int n = 0; n < 40; n++)
            {
                foreach (var pair in polymer.Where(kvp => kvp.Value > 0))
                {
                    nextPolymer[string.Concat(pair.Key[0], rules[pair.Key])] += pair.Value;
                    nextPolymer[string.Concat(rules[pair.Key], pair.Key[1])] += pair.Value;
                    nextPolymer[pair.Key] -= pair.Value;
                }

                polymer = new Dictionary<string, long>(nextPolymer);
            }

            foreach (var pair in polymer)
            {
                count[pair.Key[0]] += pair.Value;
                count[pair.Key[1]] += pair.Value;
            }

            foreach(var value in count)
            {
                count[value.Key] = value.Value / 2;
            }

            count[input[0][0]] += 1;
            count[input[0][^1]] += 1;

            return count.Values.Max() - count.Values.Min();
        }
    }
}
