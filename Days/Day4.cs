using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public record BingoTile
    {
        public int Number { get; init; }
        public bool IsMarked { get; set; }
    }

    public static class Day4Part1
    {
        public static int Run()
        {
            var lines = File.ReadAllLines(@"Inputs\Day4.txt")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => Array.ConvertAll(x.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse)).ToArray();
            var numbers = lines[0];
            var boards = new List<BingoTile[,]>();
            var score = 0;

            for (int i = 1; i < lines.Length; i += 5)
            {
                var board = new BingoTile[5, 5];
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        board[x, y] = new BingoTile { Number = lines[i + y][x], IsMarked = false };
                    }
                }
                boards.Add(board);
            }

            foreach(var number in numbers)
            {
                foreach (var board in boards)
                {
                    for (int x = 0; x < board.GetLength(0); x++)
                    {
                        for (int y = 0; y < board.GetLength(1); y++)
                        {
                            if (board[x, y].Number == number)
                                board[x, y].IsMarked = true;
                        }
                    }

                    if (IsWinner(board))
                    {
                        score = GetScore(board, number);
                        break;
                    }
                }

                if (score > 0)
                    break;
            }

            return score;

            static bool IsWinner(BingoTile[,] board)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    if (board[x, 0].IsMarked && board[x, 1].IsMarked && board[x, 2].IsMarked && board[x, 3].IsMarked && board[x, 4].IsMarked)
                        return true;
                }

                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[0, y].IsMarked && board[1, y].IsMarked && board[2, y].IsMarked && board[3, y].IsMarked && board[4, y].IsMarked)
                        return true;
                }

                return false;
            }

            static int GetScore(BingoTile[,] board, int number)
            {
                var unmarked = 0;

                for (int x = 0; x < board.GetLength(0); x++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if(!board[x,y].IsMarked)
                            unmarked += board[x, y].Number;
                    }
                }

                return number * unmarked;
            }
        }
    }

    public static class Day4Part2
    {
        public static int Run()
        {
            var lines = File.ReadAllLines(@"Inputs\Day4.txt")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => Array.ConvertAll(x.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse)).ToArray();
            var numbers = lines[0];
            var boards = new List<BingoTile[,]>();
            var winners = new HashSet<int>();
            int score = 0; 

            for (int i = 1; i < lines.Length; i += 5)
            {
                var board = new BingoTile[5, 5];
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        board[x, y] = new BingoTile { Number = lines[i + y][x], IsMarked = false };
                    }
                }
                boards.Add(board);
            }

            foreach (var number in numbers)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    BingoTile[,] board = boards[i];
                    for (int x = 0; x < board.GetLength(0); x++)
                    {
                        for (int y = 0; y < board.GetLength(1); y++)
                        {
                            if (board[x, y].Number == number)
                                board[x, y].IsMarked = true;
                        }
                    }

                    if (IsWinner(board))
                    {
                        winners.Add(i);
                        if (winners.Count == boards.Count)
                        {
                            score = GetScore(board, number);
                            break;
                        }
                    }
                }

                if (score > 0)
                    break;
            }

            return score;

            static bool IsWinner(BingoTile[,] board)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    if (board[x, 0].IsMarked && board[x, 1].IsMarked && board[x, 2].IsMarked && board[x, 3].IsMarked && board[x, 4].IsMarked)
                        return true;
                }

                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[0, y].IsMarked && board[1, y].IsMarked && board[2, y].IsMarked && board[3, y].IsMarked && board[4, y].IsMarked)
                        return true;
                }

                return false;
            }

            static int GetScore(BingoTile[,] board, int number)
            {
                var unmarked = 0;

                for (int x = 0; x < board.GetLength(0); x++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if (!board[x, y].IsMarked)
                            unmarked += board[x, y].Number;
                    }
                }

                return number * unmarked;
            }
        }
    }
}
