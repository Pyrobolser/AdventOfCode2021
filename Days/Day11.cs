using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public record Octopus(int X, int Y, int Energy, bool HasFlashed)
    {
        public int Energy { get; set; } = Energy;
        public bool HasFlashed { get; set; } = HasFlashed;
    }

    public static class OctopusListExtension
    {
        public static void IncrementEnergy(this List<Octopus> levels) => levels.ForEach(o => o.Energy += 1);
        public static void ResetFlash(this List<Octopus> levels) => levels.ForEach(o => o.HasFlashed = false);
    }

    public static class Day11Part1
    {
        public static int Run()
        {
            var levels = File.ReadAllLines(@"Inputs\Day11.txt").Select((l, y) => l.Select((c, x) => new Octopus(x, y, int.Parse(c.ToString()), false))).SelectMany(o => o).ToList(); ;
            var flashes = 0;

            for (int i = 0; i < 100; i++)
            {
                levels.IncrementEnergy();

                var hasFlashed = true;
                while (hasFlashed)
                {
                    hasFlashed = false;
                    foreach(var octopus in levels.Where(o => o.Energy >= 10 && !o.HasFlashed))
                    {
                        hasFlashed = true;
                        octopus.HasFlashed = true;
                        octopus.Energy = 0;
                        flashes++;
                        foreach(var neighbor in levels.Where(o => !o.HasFlashed &&
                                                                 ((o.X == octopus.X - 1 && o.Y == octopus.Y - 1) ||
                                                                  (o.X == octopus.X     && o.Y == octopus.Y - 1) ||
                                                                  (o.X == octopus.X + 1 && o.Y == octopus.Y - 1) ||
                                                                  (o.X == octopus.X - 1 && o.Y == octopus.Y    ) ||
                                                                  (o.X == octopus.X + 1 && o.Y == octopus.Y    ) ||
                                                                  (o.X == octopus.X - 1 && o.Y == octopus.Y + 1) ||
                                                                  (o.X == octopus.X     && o.Y == octopus.Y + 1) ||
                                                                  (o.X == octopus.X + 1 && o.Y == octopus.Y + 1))))
                        {
                            neighbor.Energy += 1;
                        }
                    }
                }

                levels.ResetFlash();
            }

            return flashes;
        }
    }

    public static class Day11Part2
    {
        public static int Run()
        {
            var levels = File.ReadAllLines(@"Inputs\Day11.txt").Select((l, y) => l.Select((c, x) => new Octopus(x, y, int.Parse(c.ToString()), false))).SelectMany(o => o).ToList(); ;
            var isSynced = false;
            var step = 0;

            while (!isSynced)
            {
                step++;
                levels.IncrementEnergy();

                var hasFlashed = true;
                while (hasFlashed)
                {
                    hasFlashed = false;
                    foreach (var octopus in levels.Where(o => o.Energy >= 10 && !o.HasFlashed))
                    {
                        hasFlashed = true;
                        octopus.HasFlashed = true;
                        octopus.Energy = 0;
                        foreach (var neighbor in levels.Where(o => !o.HasFlashed &&
                                                                  ((o.X == octopus.X - 1 && o.Y == octopus.Y - 1) ||
                                                                   (o.X == octopus.X && o.Y == octopus.Y - 1) ||
                                                                   (o.X == octopus.X + 1 && o.Y == octopus.Y - 1) ||
                                                                   (o.X == octopus.X - 1 && o.Y == octopus.Y) ||
                                                                   (o.X == octopus.X + 1 && o.Y == octopus.Y) ||
                                                                   (o.X == octopus.X - 1 && o.Y == octopus.Y + 1) ||
                                                                   (o.X == octopus.X && o.Y == octopus.Y + 1) ||
                                                                   (o.X == octopus.X + 1 && o.Y == octopus.Y + 1))))
                        {
                            neighbor.Energy += 1;
                        }
                    }
                }

                if (levels.Count(o => o.HasFlashed) == levels.Count)
                    isSynced = true;

                levels.ResetFlash();
            }

            return step;
        }
    }
}
