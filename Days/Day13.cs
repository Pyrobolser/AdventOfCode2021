using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public record InkDot (int X, int Y);

    public class Day13Part1
    {
        public static int Run()
        {
            var input = File.ReadAllLines(@"Inputs\Day13.txt").Where(x => !string.IsNullOrEmpty(x)).ToArray();
            List<string> instructions = new();
            HashSet<InkDot> dots = new();


            foreach(var line in input)
            {
                if(line.StartsWith('f'))
                {
                    instructions.Add(line.Split(' ')[2]);
                }
                else
                {
                    var coords = line.Split(',');
                    dots.Add(new InkDot(int.Parse(coords[0]), int.Parse(coords[1])));
                }
            }

            var fold = instructions[0].Split('=');
            var value = int.Parse(fold[1]);

            if(fold[0] == "x")
            {
                var moving = dots.Where(d => d.X > value).ToHashSet();
                foreach (var dot in moving)
                {
                    dots.Remove(dot);
                    var moved = dot with { X = value - (dot.X - value) };
                    dots.Add(moved);
                }
            }
            else
            {
                var moving = dots.Where(d => d.Y > value).ToHashSet();
                foreach(var dot in moving)
                {
                    dots.Remove(dot);
                    var moved = dot with { Y = value - (dot.Y - value) };
                    dots.Add(moved);
                }
            }

            return dots.Count;
        }
    }

    public class Day13Part2
    {
        public static void Run()
        {
            var input = File.ReadAllLines(@"Inputs\Day13.txt").Where(x => !string.IsNullOrEmpty(x)).ToArray();
            List<string> instructions = new();
            HashSet<InkDot> dots = new();


            foreach (var line in input)
            {
                if (line.StartsWith('f'))
                {
                    instructions.Add(line.Split(' ')[2]);
                }
                else
                {
                    var coords = line.Split(',');
                    dots.Add(new InkDot(int.Parse(coords[0]), int.Parse(coords[1])));
                }
            }

            foreach(var instruction in instructions.Select(i => i.Split('=')))
            {
                var value = int.Parse(instruction[1]);
                if (instruction[0] == "x")
                {
                    var moving = dots.Where(d => d.X > value).ToHashSet();
                    foreach (var dot in moving)
                    {
                        dots.Remove(dot);
                        var moved = dot with { X = value - (dot.X - value) };
                        dots.Add(moved);
                    }
                }
                else
                {
                    var moving = dots.Where(d => d.Y > value).ToHashSet();
                    foreach (var dot in moving)
                    {
                        dots.Remove(dot);
                        var moved = dot with { Y = value - (dot.Y - value) };
                        dots.Add(moved);
                    }
                }
            }

            for(int y = 0; y <= dots.Max(d => d.Y); y++)
            {
                for (int x = 0; x <= dots.Max(d => d.X); x++)
                {
                    Console.Write(dots.Any(d => d.X == x && d.Y == y) ? "#" : ".");
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
