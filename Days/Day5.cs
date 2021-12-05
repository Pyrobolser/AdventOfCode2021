using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public record VentCoord(int X, int Y);

    public static class Day5Part1
    {
        public static int Run()
        {
            var lines = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day5.txt"), GetCoordinates);
            var vents = new HashSet<VentCoord>();
            var overlaps = new HashSet<VentCoord>();

            foreach(var line in lines.Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y))
            {
                if (line.Start.X == line.End.X)
                {
                    var min = line.Start.Y > line.End.Y ? line.End.Y : line.Start.Y;
                    var max = min == line.End.Y ? line.Start.Y : line.End.Y;

                    for (int i = min; i <= max; i++)
                    {
                        var vent = new VentCoord(line.Start.X, i);
                        if (vents.TryGetValue(vent, out _))
                        {
                            overlaps.Add(vent);
                        }
                        else
                        {
                            vents.Add(vent);
                        }
                    }
                }
                else if (line.Start.Y == line.End.Y)
                {
                    var min = line.Start.X > line.End.X ? line.End.X : line.Start.X;
                    var max = min == line.End.X ? line.Start.X : line.End.X;

                    for (int i = min; i <= max; i++)
                    {
                        var vent = new VentCoord(i, line.Start.Y);
                        if (vents.TryGetValue(vent, out _))
                        {
                            overlaps.Add(vent);
                        }
                        else
                        {
                            vents.Add(vent);
                        }
                    }
                }
            }

            return overlaps.Count;
        }

        private static (VentCoord Start, VentCoord End) GetCoordinates(string input)
        {
            var coords = input.Split(" -> ").Select(c => Array.ConvertAll(c.Split(','), int.Parse)).ToArray();

            return (new VentCoord(coords[0][0], coords[0][1]), new VentCoord(coords[1][0], coords[1][1]));
        }
    }

    public static class Day5Part2
    {
        public static int Run()
        {
            var lines = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day5.txt"), GetCoordinates);
            var vents = new HashSet<VentCoord>();
            var overlaps = new HashSet<VentCoord>();

            foreach (var line in lines)
            {
                if (line.Start.X == line.End.X)
                {
                    var min = line.Start.Y > line.End.Y ? line.End.Y : line.Start.Y;
                    var max = min == line.End.Y ? line.Start.Y : line.End.Y;

                    for (int i = min; i <= max; i++)
                    {
                        var vent = new VentCoord(line.Start.X, i);
                        if (vents.TryGetValue(vent, out _))
                        {
                            overlaps.Add(vent);
                        }
                        else
                        {
                            vents.Add(vent);
                        }
                    }
                }
                else if (line.Start.Y == line.End.Y)
                {
                    var min = line.Start.X > line.End.X ? line.End.X : line.Start.X;
                    var max = min == line.End.X ? line.Start.X : line.End.X;

                    for (int i = min; i <= max; i++)
                    {
                        var vent = new VentCoord(i, line.Start.Y);
                        if (vents.TryGetValue(vent, out _))
                        {
                            overlaps.Add(vent);
                        }
                        else
                        {
                            vents.Add(vent);
                        }
                    }
                }
                else
                {
                    if(line.Start.X < line.End.X && line.Start.Y < line.End.Y)
                    {
                        var xMin = line.Start.X;
                        var yMin = line.Start.Y;
                        var xMax = line.End.X;
                        var yMax = line.End.Y;

                        for (int i = 0; i <= (xMax - xMin); i++)
                        {
                            var vent = new VentCoord(xMin + i, yMin + i);
                            if (vents.TryGetValue(vent, out _))
                            {
                                overlaps.Add(vent);
                            }
                            else
                            {
                                vents.Add(vent);
                            }
                        }

                    }
                    else if (line.Start.X > line.End.X && line.Start.Y > line.End.Y)
                    {
                        var xMin = line.End.X;
                        var yMin = line.End.Y;
                        var xMax = line.Start.X;
                        var yMax = line.Start.Y;

                        for (int i = 0; i <= (xMax - xMin); i++)
                        {
                            var vent = new VentCoord(xMax - i, yMax - i);
                            if (vents.TryGetValue(vent, out _))
                            {
                                overlaps.Add(vent);
                            }
                            else
                            {
                                vents.Add(vent);
                            }
                        }
                    }
                    else if (line.Start.X < line.End.X && line.Start.Y > line.End.Y)
                    {
                        var xMin = line.Start.X;
                        var yMin = line.End.Y;
                        var xMax = line.End.X;
                        var yMax = line.Start.Y;

                        for (int i = 0; i <= (xMax - xMin); i++)
                        {
                            var vent = new VentCoord(xMin + i, yMax - i);
                            if (vents.TryGetValue(vent, out _))
                            {
                                overlaps.Add(vent);
                            }
                            else
                            {
                                vents.Add(vent);
                            }
                        }
                    }
                    else
                    {
                        var xMin = line.End.X;
                        var yMin = line.Start.Y;
                        var xMax = line.Start.X;
                        var yMax = line.End.Y;

                        for (int i = 0; i <= (xMax - xMin); i++)
                        {
                            var vent = new VentCoord(xMax - i, yMin + i);
                            if (vents.TryGetValue(vent, out _))
                            {
                                overlaps.Add(vent);
                            }
                            else
                            {
                                vents.Add(vent);
                            }
                        }
                    }
                }
            }

            return overlaps.Count;
        }

        private static (VentCoord Start, VentCoord End) GetCoordinates(string input)
        {
            var coords = input.Split(" -> ").Select(c => Array.ConvertAll(c.Split(','), int.Parse)).ToArray();

            return (new VentCoord(coords[0][0], coords[0][1]), new VentCoord(coords[1][0], coords[1][1]));
        }
    }
}
