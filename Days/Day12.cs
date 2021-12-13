using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Path
    {
        public string Node { get; set; }
        public List<string> Done { get; set; }
        public string SmallCave { get; set; }
    }

    public class Day12Part1
    { 

        public static int Run()
        {
            var lines = File.ReadAllLines(@"Inputs\Day12.txt").Select(l => l.Split('-')).ToArray();
            var total = 0;


            List<Path> paths = new() { new Path { Node = "start", Done = new() { "start" } } };
            List<Path> followUp = new();
            
            while(paths.Count > 0)
            {
                foreach (var path in paths)
                {
                    var next = lines.Where(p => p.Any(e => e == path.Node) &&
                                          (char.IsUpper(p.First(e => e != path.Node)[0]) || !path.Done.Contains(p.First(e => e != path.Node)))
                                          ).ToList();
                    foreach(var node in next)
                    {
                        var to = node.First(e => e != path.Node);
                        if(to != "end")
                        {
                            followUp.Add(new Path { Node = to, Done = path.Done.Append(to).ToList() });
                        }
                        else
                        {
                            total++;
                        }
                    }
                }
                paths.Clear();
                paths.AddRange(followUp);
                followUp.Clear();
            }

            return total;
        }
    }

    public class Day12Part2
    {

        public static int Run()
        {
            var lines = File.ReadAllLines(@"Inputs\Day12.txt").Select(l => l.Split('-')).ToArray();
            var total = 0;


            List<Path> paths = new() { new Path { Node = "start", Done = new() { "start" }, SmallCave = string.Empty } };
            List<Path> followUp = new();

            while (paths.Count > 0)
            {
                foreach (var path in paths)
                {
                    var next = lines.Where(p => p.Any(e => e == path.Node)
                                          //&& (char.IsUpper(p.First(e => e != path.Node)[0]) || !path.Done.Contains(p.First(e => e != path.Node)))
                                          ).ToList();

                    foreach (var node in next)
                    {
                        var to = node.First(e => e != path.Node);
                        if (to == "start")
                            continue;

                        if (to != "end")
                        {
                            if (to == path.SmallCave)
                                continue;

                            if (!string.IsNullOrEmpty(path.SmallCave) && char.IsLower(to[0]) && path.Done.Any(p => p == to))
                                continue;

                            if (string.IsNullOrEmpty(path.SmallCave) && char.IsLower(to[0]) && path.Done.Count(p => p == to) == 1)
                                path.SmallCave = to;


                            followUp.Add(new Path { Node = to, Done = path.Done.Append(to).ToList(), SmallCave = path.SmallCave });
                        }
                        else
                        {
                            path.Done.Add("end");
                            System.Console.WriteLine(string.Join(',', path.Done));
                            total++;
                        }
                    }
                }
                paths.Clear();
                paths.AddRange(followUp);
                followUp.Clear();
            }

            return total;
        }
    }
}
