using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day3Part1
    {
        public static uint Run()
        {
            var report = File.ReadAllLines(@"Inputs\Day3.txt");
            var common = string.Empty;

            for (int i = 0; i < report[0].Length; i++)
            {
                common += report.Select(x => x[i]).GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First();
            }

            var gRate = Convert.ToUInt32(common, 2);
            var eRate = gRate ^ 0b111111111111;

            return gRate * eRate;
        }
    }

    public static class Day3Part2
    {
        public static uint Run()
        {
            var o2report = File.ReadAllLines(@"Inputs\Day3.txt");
            var co2report = (string[])o2report.Clone();
            char o2 = '\0', co2 = '\0';
            uint o2rating = 0, co2rating = 0;

            for (int i = 0; i < o2report[0].Length; i++)
            {
                if (o2rating > 0 && co2rating > 0)
                    break;

                if (o2rating == 0)
                {
                    var o2value = o2report.Select(x => x[i]).GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => (C: x.Key, Count: x.Count())).ToList();

                    if (o2value[0].Count == o2value[1].Count)
                    {
                        o2 = '1';
                    }
                    else
                    {
                        o2 = o2value[0].C;
                    }

                    o2report = o2report.Where(x => x[i] == o2).ToArray();

                    if (o2report.Count() == 1)
                        o2rating = Convert.ToUInt32(o2report[0], 2);
                }

                if (co2rating == 0)
                {
                    var co2value = co2report.Select(x => x[i]).GroupBy(x => x).OrderBy(x => x.Count()).Select(x => (C: x.Key, Count: x.Count())).ToList();

                    if (co2value[0].Count == co2value[1].Count)
                    {
                        co2 = '0';
                    }
                    else
                    {
                        co2 = co2value[0].C;
                    }

                    co2report = co2report.Where(x => x[i] == co2).ToArray();

                    if (co2report.Count() == 1)
                        co2rating = Convert.ToUInt32(co2report[0], 2);
                }
            }

            return o2rating * co2rating;
        }
    }
}