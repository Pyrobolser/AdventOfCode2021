using AdventOfCode2021.Days;
using static System.Console;

namespace AdventOfCode2021
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("+ * = * + = * = +| Advent of Code 2021 |+ = * = + * = * +");
            //WriteLine("                 =>+-+>   Day 1   <+-+<=                 ");
            //WriteLine("           >-=>>+-+>   Sonar Sweep   <+-+<<=-<           ");
            //WriteLine($"+> Part 1: {Day1Part1.Run()} measurements are larger than the previous measurement");
            //WriteLine($"+> Part 2: {Day1Part2.Run()} sums are larger than the previous sum");
            //WriteLine("                 =>+-+>   Day 2   <+-+<=                 ");
            //WriteLine("              >-=>>+-+>   Dive!   <+-+<<=-<              ");
            //WriteLine($"+> Part 1: Final Horizontal Position * Final Depth = {Day2Part1.Run()}");
            //WriteLine($"+> Part 2: Final Horizontal Position * Final Depth = {Day2Part2.Run()}");
            WriteLine("                 =>+-+>   Day 3   <+-+<=                 ");
            WriteLine("        >-=>>+-+>   Binary Diagnostic   <+-+<<=-<        ");
            WriteLine($"+> Part 1: The power consumption of the submarine is {Day3Part1.Run()}");
            WriteLine($"+> Part 2: The life support rating of the submarine is {Day3Part2.Run()}");
        }
    }
}
