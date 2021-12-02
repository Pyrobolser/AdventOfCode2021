using System;
using System.IO;

namespace AdventOfCode2021.Days
{
    public static class Day2Part1
    {
        public static int Run()
        {
            var commands = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day2.txt"), ToTuple);
            int hPos = 0, depth = 0;

            foreach(var command in commands)
            {
                switch (command.Action)
                {
                    case "forward":
                        hPos += command.Value;
                        break;
                    case "down":
                        depth += command.Value;
                        break;
                    case "up":
                        depth -= command.Value;
                        break;
                }
            }

            return hPos * depth;
        }

        public static (string Action, int Value) ToTuple(string command) => command.Split(' ') switch { var chunks => (chunks[0], int.Parse(chunks[1])) };
    }

    public static class Day2Part2
    {
        public static int Run()
        {
            var commands = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day2.txt"), ToTuple);
            int hPos = 0, depth = 0, aim = 0;

            foreach (var command in commands)
            {
                switch (command.Action)
                {
                    case "forward":
                        hPos += command.Value;
                        depth += aim * command.Value;
                        break;
                    case "down":
                        aim += command.Value;
                        break;
                    case "up":
                        aim -= command.Value;
                        break;
                }
            }

            return hPos * depth;
        }

        public static (string Action, int Value) ToTuple(string command) => command.Split(' ') switch { var chunks => (chunks[0], int.Parse(chunks[1])) };
    }
}
