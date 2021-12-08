using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day8Part1
    {
        public static int Run()
        {
            var digits = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day8.txt"), GetDigits);

            return digits.Sum();
        }

        public static int GetDigits(string line)
        {
            var digits = line.Split(" | ")[1].Split(' ').Where(s => s.Count() == 2 || s.Count() == 3 || s.Count() == 4 || s.Count() == 7).Count();

            return digits;
        }
    }

    public static class Day8Part2
    {
        public static int Run()
        {
            var inputs = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day8.txt"), GetInputs);
            var total = 0;

            //foreach(var input in inputs)
            //{
            //    if(input.Digits.Where(s => s.Count() == 2 || s.Count() == 3 || s.Count() == 4 || s.Count() == 7).Count() == 4)
            //    {
            //        total += GetValue(input.Digits);
            //    }
            //}

            return total;
        }

        public static (string[] Signals, string[] Digits) GetInputs(string line)
        {
            var values = line.Split(" | ").Select(s => s.Split(' ').Select(e => string.Concat(e.OrderBy(e => e))).ToArray()).ToArray();

            return (Signals: values[0], Digits: values[1]);
        }
    }
}
