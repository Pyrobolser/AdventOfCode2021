using System;
using System.IO;

namespace AdventOfCode2021.Days
{
    public static class Day1Part1
    {
        public static int Run()
        {
            int[] measures = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day1.txt"), int.Parse);
            var total = 0;

            for (int i = 0; i < measures.Length - 1; i++)
            {
                if (measures[i] < measures[i + 1])
                    total++;
            }

            return total;
        }
    }

    public static class Day1Part2
    {
        public static int Run()
        {
            int[] measures = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day1.txt"), int.Parse);
            var total = 0;

            for (int i = 0; i < measures.Length - 3; i++)
            {
                if ((measures[i] + measures[i + 1] + measures[i + 2]) < (measures[i + 1] + measures[i + 2] + measures[i + 3]))
                    total++;
            }

            return total;
        }
    }
}
