using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public record LowPointCoord (int X, int Y);
    public class Basins
    {
        public List<LowPointCoord> Coords { get; init; }
        public int Size { get; set; }
    }
    public static class Day9Part1
    {
        public static int Run()
        {
            var heightMap = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day9.txt"), GetMap);
            var sumRiskLevel = 0;

            for (int x = 0; x < heightMap.Length; x++)
            {
                for (int y = 0; y < heightMap[x].Length; y++)
                {
                    if ((x - 1 < 0 || heightMap[x - 1][y] > heightMap[x][y]) &&
                       (y - 1 < 0 || heightMap[x][y - 1] > heightMap[x][y]) &&
                       (x + 1 >= heightMap.Length || heightMap[x + 1][y] > heightMap[x][y]) &&
                       (y + 1 >= heightMap[x].Length || heightMap[x][y + 1] > heightMap[x][y]))
                    {
                        sumRiskLevel += 1 + heightMap[x][y];
                    }
                }
            }

            return sumRiskLevel;
        }

        public static int[] GetMap(string input) => input.Select(i => int.Parse(i.ToString())).ToArray();
    }

    public static class Day9Part2
    {
        public static int Run()
        {
            var heightMap = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day9.txt"), GetMap);
            var basins = new List<Basins>();
            var isBigger = true;
            var total = 0;

            for (int x = 0; x < heightMap.Length; x++)
            {
                for (int y = 0; y < heightMap[x].Length; y++)
                {
                    if ((x - 1 < 0 || heightMap[x - 1][y] > heightMap[x][y]) &&
                       (y - 1 < 0 || heightMap[x][y - 1] > heightMap[x][y]) &&
                       (x + 1 >= heightMap.Length || heightMap[x + 1][y] > heightMap[x][y]) &&
                       (y + 1 >= heightMap[x].Length || heightMap[x][y + 1] > heightMap[x][y]))
                    {
                        basins.Add(new Basins { Coords = new List<LowPointCoord> { new LowPointCoord(x, y) }, Size = 0 });
                    }
                }
            }

            while(isBigger)
            {
                isBigger = false;
                foreach(var basin in basins.Where(b => b.Coords.Any()))
                {
                    basin.Size += basin.Coords.Count;
                    foreach (var coord in basin.Coords)
                    {

                    }
                }
            }

            return total;
        }

        public static int[] GetMap(string input) => input.Select(i => int.Parse(i.ToString())).ToArray();
    }
}
