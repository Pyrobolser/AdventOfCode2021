using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day7Part1
    {
        public static int Run()
        {
            var positions = Array.ConvertAll(File.ReadAllText(@"Inputs\Day7.txt").Split(','), int.Parse);
            int median = 0, total = 0;

            if (positions.Length % 2 == 1)
                median = Median(positions, positions.Length / 2);
            else
                median = (int)(0.5 * (Median(positions, positions.Length / 2 - 1) + Median(positions, positions.Length / 2)));

            foreach (var position in positions)
            {
                total += Math.Abs(position - median);
            }

            return total;
        }

        public static int Median(int[] positions, int index)
        {
            var pivot = positions[new Random().Next(0, positions.Length - 1)];

            var left = positions.Where(p => p < pivot).ToArray();
            var right = positions.Where(p => p > pivot).ToArray();
            var pivots = positions.Where(p => p == pivot).ToArray();

            if (index < left.Length)
            {
                return Median(left, index);
            }
            else if (index < left.Length + pivots.Length)
            {
                return pivots[0];
            }
            else
            {
                return Median(right, index - left.Length - pivots.Length);
            }
        }
    }

    public static class Day7Part2
    {
        public static int Run()
        {
            var positions = Array.ConvertAll(File.ReadAllText(@"Inputs\Day7.txt").Split(','), int.Parse);
            double mean = positions.Average();
            int lowMean = (int)Math.Floor(mean), highMean = (int)Math.Ceiling(mean);
            int lowMeanTotal = 0, highMeanTotal = 0;

            foreach (var position in positions)
            {
                var steps = Math.Abs(position - lowMean);

                lowMeanTotal += (steps * (steps + 1)) / 2;
            }

            foreach (var position in positions)
            {
                var steps = Math.Abs(position - highMean);

                highMeanTotal += (steps * (steps + 1)) / 2;
            }

            return lowMeanTotal <= highMeanTotal ? lowMeanTotal : highMeanTotal;
        }
    }
}
