using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day6Part1
    {
        public static int Run()
        {
            var fishes = File.ReadAllText(@"Inputs\Day6.txt").Split(',').Select(n => int.Parse(n)).GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());

            for (int i = 0; i < 9; i++)
                fishes.TryAdd(i, 0);

            for(int i = 0; i < 80; i++)
            {
                var temp = fishes[0];
                fishes[0] = fishes[1];
                fishes[1] = fishes[2];
                fishes[2] = fishes[3];
                fishes[3] = fishes[4];
                fishes[4] = fishes[5];
                fishes[5] = fishes[6];
                fishes[6] = fishes[7] + temp;
                fishes[7] = fishes[8];
                fishes[8] = temp;
            }

            return fishes.Sum(f => f.Value);
        }
    }

    public static class Day6Part2
    {
        public static long Run()
        {
            var fishes = File.ReadAllText(@"Inputs\Day6.txt").Split(',').Select(n => int.Parse(n)).GroupBy(n => n).ToDictionary(g => g.Key, g => g.LongCount());

            for (int i = 0; i < 9; i++)
                fishes.TryAdd(i, 0);

            for (int i = 0; i < 256; i++)
            {
                var temp = fishes[0];
                fishes[0] = fishes[1];
                fishes[1] = fishes[2];
                fishes[2] = fishes[3];
                fishes[3] = fishes[4];
                fishes[4] = fishes[5];
                fishes[5] = fishes[6];
                fishes[6] = fishes[7] + temp;
                fishes[7] = fishes[8];
                fishes[8] = temp;
            }

            return fishes.Sum(f => f.Value);
        }
    }
}
