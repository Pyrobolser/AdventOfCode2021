using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public record LowPointCoord (int X, int Y);
    public class Basins
    {
        public HashSet<LowPointCoord> NextCoords { get; init; }
        public HashSet<LowPointCoord> BasinCoords { get; init; }
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

            for (int x = 0; x < heightMap.Length; x++)
            {
                for (int y = 0; y < heightMap[x].Length; y++)
                {
                    if ((x - 1 < 0 || heightMap[x - 1][y] > heightMap[x][y]) &&
                       (y - 1 < 0 || heightMap[x][y - 1] > heightMap[x][y]) &&
                       (x + 1 >= heightMap.Length || heightMap[x + 1][y] > heightMap[x][y]) &&
                       (y + 1 >= heightMap[x].Length || heightMap[x][y + 1] > heightMap[x][y]))
                    {
                        basins.Add(new Basins { NextCoords = new HashSet<LowPointCoord> { new LowPointCoord(x, y) }, BasinCoords = new HashSet<LowPointCoord> { new LowPointCoord(x, y) } });
                    }
                }
            }

            while(isBigger)
            {
                isBigger = false;
                foreach(var basin in basins.Where(b => b.NextCoords.Any()))
                {
                    var nextLayer = new HashSet<LowPointCoord>();
                    foreach (var coord in basin.NextCoords)
                    {
                        if (heightMap[coord.X][coord.Y] == 8)
                            continue;

                        if(coord.X - 1 >= 0 && 9 > heightMap[coord.X - 1][coord.Y] && heightMap[coord.X - 1][coord.Y] > heightMap[coord.X][coord.Y])
                        {
                            nextLayer.Add(new LowPointCoord(coord.X - 1, coord.Y));
                            isBigger = true;
                        }

                        if(coord.Y - 1 >= 0 && 9 > heightMap[coord.X][coord.Y - 1] && heightMap[coord.X][coord.Y - 1] > heightMap[coord.X][coord.Y])
                        {
                            nextLayer.Add(new LowPointCoord(coord.X, coord.Y - 1));
                            isBigger = true;
                        }

                        if(coord.X + 1 < heightMap.Length && 9 > heightMap[coord.X + 1][coord.Y] && heightMap[coord.X + 1][coord.Y] > heightMap[coord.X][coord.Y])
                        {
                            nextLayer.Add(new LowPointCoord(coord.X + 1, coord.Y));
                            isBigger = true;
                        }

                        if (coord.Y + 1 < heightMap[coord.X].Length && 9 > heightMap[coord.X][coord.Y + 1] && heightMap[coord.X][coord.Y + 1] > heightMap[coord.X][coord.Y])
                        {
                            nextLayer.Add(new LowPointCoord(coord.X, coord.Y + 1));
                            isBigger = true;
                        }
                    }

                    basin.BasinCoords.UnionWith(basin.NextCoords);
                    basin.NextCoords.Clear();
                    basin.NextCoords.UnionWith(nextLayer);
                }
            }

            basins = basins.OrderByDescending(b => b.BasinCoords.Count).ToList();

            return basins[0].BasinCoords.Count * basins[1].BasinCoords.Count * basins[2].BasinCoords.Count;
        }

        public static int[] GetMap(string input) => input.Select(i => int.Parse(i.ToString())).ToArray();
    }
}
