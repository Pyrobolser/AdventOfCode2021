using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class RiskTile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Risk { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int TotalCost { get; set; }
        public RiskTile Parent { get; set; }

        public RiskTile(int x, int y, int risk)
        {
            X = x;
            Y = y;
            Risk = risk;
        }
    }

    public class Day15Part1
    {
        public static List<RiskTile> GetAdjacentTiles(RiskTile current, RiskTile[,] cavern)
        {
            List<RiskTile> adjacentTiles = new();

            if (current.X - 1 >= 0)
                adjacentTiles.Add(cavern[current.X - 1, current.Y]);
            if (current.X + 1 < cavern.GetLength(0))
                adjacentTiles.Add(cavern[current.X + 1, current.Y]);
            if (current.Y - 1 >= 0)
                adjacentTiles.Add(cavern[current.X, current.Y - 1]);
            if (current.Y + 1 < cavern.GetLength(1))
                adjacentTiles.Add(cavern[current.X, current.Y + 1]);

            return adjacentTiles;
        }

        public static int Run()
        {
            var input = File.ReadAllLines(@"Inputs\Day15.txt");
            var totalRisk = 0;
            RiskTile[,] cavern = new RiskTile[input[0].Length, input.Length];

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    cavern[x, y] = new RiskTile(x, y, int.Parse(input[y][x].ToString()));
                }
            }

            var start = cavern[0, 0];
            start.Cost = 0;
            start.Distance = start.Risk;
            start.TotalCost = start.Cost + start.Distance;
            var end = cavern[input[0].Length - 1, input.Length - 1];
            List<RiskTile> activeTiles = new();
            activeTiles.Add(start);
            List<RiskTile> visitedTiles = new();

            while (activeTiles.Any())
            {
                RiskTile current = activeTiles.OrderBy(n => n.TotalCost).First();
                
                activeTiles.Remove(current);
                visitedTiles.Add(current);

                if (current == end)
                {
                    break;
                }

                var adjacentTiles = GetAdjacentTiles(current, cavern);
                foreach (var tile in adjacentTiles)
                {
                    if (visitedTiles.Contains(tile))
                        continue;

                    if (!activeTiles.Contains(tile))
                    {
                        tile.Parent = current;
                        tile.Cost = current.Cost + tile.Risk;
                        tile.Distance = current.Distance + tile.Risk;
                        tile.TotalCost = tile.Cost + tile.TotalCost;

                        activeTiles.Add(tile);
                    }
                    else if ((current.Cost + tile.Risk + current.Parent.Distance + tile.Risk) < tile.TotalCost)
                    {
                        tile.Parent = current;
                        tile.Cost = current.Cost + tile.Risk;
                        tile.Distance = current.Distance + tile.Risk;
                        tile.TotalCost = tile.Cost + tile.TotalCost;
                    }
                }
            }

            var step = end;
            while(step.Parent != null)
            {
                totalRisk += step.Risk;
                step = step.Parent;
            }

            return totalRisk;
        }
    }

    public static class Day15Part2
    {
        public static List<RiskTile> GetAdjacentTiles(RiskTile current, RiskTile[,] cavern)
        {
            List<RiskTile> adjacentTiles = new();

            if (current.X - 1 >= 0)
                adjacentTiles.Add(cavern[current.X - 1, current.Y]);
            if (current.X + 1 < cavern.GetLength(0))
                adjacentTiles.Add(cavern[current.X + 1, current.Y]);
            if (current.Y - 1 >= 0)
                adjacentTiles.Add(cavern[current.X, current.Y - 1]);
            if (current.Y + 1 < cavern.GetLength(1))
                adjacentTiles.Add(cavern[current.X, current.Y + 1]);

            return adjacentTiles;
        }

        public static int Run()
        {
            var input = File.ReadAllLines(@"Inputs\Day15.txt");
            var totalRisk = 0;
            RiskTile[,] model = new RiskTile[input[0].Length, input.Length];
            RiskTile[,] cavern = new RiskTile[input[0].Length * 5, input.Length * 5];
            int[,] additionalRisk = new int[5, 5]
            {
                { 0,1,2,3,4 },
                { 1,2,3,4,5 },
                { 2,3,4,5,6 },
                { 3,4,5,6,7 },
                { 4,5,6,7,8 },
            };

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    model[x, y] = new RiskTile(x, y, int.Parse(input[y][x].ToString()));
                }
            }

            
            for (int y = 0; y < cavern.GetLength(1); y++)
            {
                for (int x = 0; x < cavern.GetLength(0); x++)
                {

                    var risk = (model[x % model.GetLength(0), y % model.GetLength(1)].Risk + additionalRisk[x / model.GetLength(0), y / model.GetLength(1)]);
                    if(risk > 9)
                    {
                        risk -= 9;
                    }
                    cavern[x, y] = new RiskTile(x, y, risk);
                }
            }

            var start = cavern[0, 0];
            start.Cost = 0;
            start.Distance = start.Risk;
            start.TotalCost = start.Cost + start.Distance;
            var end = cavern[cavern.GetLength(0) - 1, cavern.GetLength(1) - 1];
            List<RiskTile> activeTiles = new();
            activeTiles.Add(start);
            List<RiskTile> visitedTiles = new();

            while (activeTiles.Any())
            {
                RiskTile current = activeTiles.OrderBy(n => n.TotalCost).First();

                activeTiles.Remove(current);
                visitedTiles.Add(current);

                if (current == end)
                {
                    break;
                }

                var adjacentTiles = GetAdjacentTiles(current, cavern);
                foreach (var tile in adjacentTiles)
                {
                    if (visitedTiles.Contains(tile))
                        continue;

                    if (!activeTiles.Contains(tile))
                    {
                        tile.Parent = current;
                        tile.Cost = current.Cost + tile.Risk;
                        tile.Distance = current.Distance + tile.Risk;
                        tile.TotalCost = tile.Cost + tile.TotalCost;

                        activeTiles.Add(tile);
                    }
                    else if ((current.Cost + tile.Risk + current.Parent.Distance + tile.Risk) < tile.TotalCost)
                    {
                        tile.Parent = current;
                        tile.Cost = current.Cost + tile.Risk;
                        tile.Distance = current.Distance + tile.Risk;
                        tile.TotalCost = tile.Cost + tile.TotalCost;
                    }
                }
            }

            var step = end;
            while (step.Parent != null)
            {
                totalRisk += step.Risk;
                step = step.Parent;
            }

            return totalRisk;
        }
    }
}
