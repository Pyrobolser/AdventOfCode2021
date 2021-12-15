using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day10Part1
    {
        public static int Run()
        {
            var lines = File.ReadAllLines(@"Inputs\Day10.txt");
            var open = new char[] { '(', '[', '{', '<' };
            var total = 0;

            foreach (var line in lines)
            {
                Stack<char> symbols = new();
                foreach (var symbol in line)
                {
                    if (open.Any(c => c == symbol))
                    {
                        symbols.Push(symbol);
                    }
                    else
                    {
                        var match = symbols.Pop();
                        if((match == '(' && symbol != ')') ||
                           (match == '[' && symbol != ']') ||
                           (match == '{' && symbol != '}') ||
                           (match == '<' && symbol != '>'))
                        {
                            total += GetSymbolScore(symbol);
                            break;
                        }
                    }
                }
            }

            return total;
        }

        public static int GetSymbolScore(char symbol) => symbol switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
             _  => 0
        };
    }

    public static class Day10Part2
    {
        public static long Run()
        {
            var lines = File.ReadAllLines(@"Inputs\Day10.txt");
            var open = new char[] { '(', '[', '{', '<' };
            List<long> scores = new();

            foreach (var line in lines)
            {
                Stack<char> symbols = new();
                var score = 0L;
                foreach (var symbol in line)
                {
                    if (open.Any(c => c == symbol))
                    {
                        symbols.Push(symbol);
                    }
                    else
                    {
                        var match = symbols.Pop();
                        if ((match == '(' && symbol != ')') ||
                           (match == '[' && symbol != ']') ||
                           (match == '{' && symbol != '}') ||
                           (match == '<' && symbol != '>'))
                        {
                            symbols.Clear();
                            break;
                        }
                    }
                }

                foreach(var symbol in symbols)
                {
                    score *= 5;
                    score += GetSymbolScore(symbol);
                }

                if(symbols.Count > 0)
                    scores.Add(score);
            }

            return scores.OrderBy(i => i).ToList()[scores.Count / 2];
        }

        public static int GetSymbolScore(char symbol) => symbol switch
        {
            '(' => 1,
            '[' => 2,
            '{' => 3,
            '<' => 4,
            _ => 0
        };
    }
}
