using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day8Part1
    {
        public static int Run()
        {
            var digits = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day8.txt"), GetDigits);

            return digits.Sum();
        }

        public static int GetDigits(string line)
        {
            var digits = line.Split(" | ")[1].Split(' ').Where(s => s.Count() == 2 || s.Count() == 3 || s.Count() == 4 || s.Count() == 7).Count();

            return digits;
        }
    }

    public static class Day8Part2
    {
        public static int Run()
        {
            var inputs = Array.ConvertAll(File.ReadAllLines(@"Inputs\Day8.txt"), GetInputs);
            var total = 0;

            ///  var display = 7-segments display
            ///  3333
            /// 4    0
            /// 4    0
            ///  5555
            /// 6    1
            /// 6    1
            ///  2222

            foreach (var (Signals, Digits) in inputs)
            {
                var display = new string[7];
                if (Digits.Where(s => s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7).Count() == 4)
                {
                    total += GetValue(Digits, display);
                }
                else
                {
                    display[0] = Signals.First(s => s.Length == 2);
                    display[1] = Signals.First(s => s.Length == 2);
                    display[3] = Signals.First(s => s.Length == 3).Except(Signals.First(s => s.Length == 2)).First().ToString();
                    display[4] = string.Concat(Signals.First(s => s.Length == 4).Except(Signals.First(s => s.Length == 2)));
                    display[5] = string.Concat(Signals.First(s => s.Length == 4).Except(Signals.First(s => s.Length == 2)));
                    var three = Signals.First(s => s.Length == 5 && s.Intersect(display[0]).Count() == 2);
                    var five = Signals.First(s => s.Length == 5 && s.Intersect(display[4]).Count() == 2);
                    display[1] = five.Intersect(display[1]).First().ToString();
                    display[0] = display[0].Except(display[1]).First().ToString();
                    display[5] = three.Intersect(display[5]).First().ToString();
                    display[4] = display[4].Except(display[5]).First().ToString();
                    display[2] = three.Except(string.Concat(display[3], display[0], display[5], display[1])).First().ToString();
                    display[6] = Signals.First(s => s.Length == 7).Except(string.Concat(display[3], display[0], display[5], display[1], display[2], display[4])).First().ToString();

                    total += GetValue(Digits, display);
                }
            }

            return total;
        }

        public static int GetValue(string[] digits, string[] display)
        {
            var value = string.Empty;
            foreach(var digit in digits)
            {
                switch(digit.Length)
                {
                    case 2:
                        value += "1";
                        break;
                    case 3:
                        value += "7";
                        break;
                    case 4:
                        value += "4";
                        break;
                    case 5:
                        if (digit.Intersect(string.Concat(display[3], display[0], display[5], display[1], display[2])).Count() == 5)
                        {
                            value += "3";
                        }
                        else if (digit.Intersect(string.Concat(display[3], display[0], display[5], display[6], display[2])).Count() == 5)
                        {
                            value += "2";
                        }
                        else
                        {
                            value += "5";
                        }
                        break;
                    case 6:
                        if (digit.Intersect(string.Concat(display[3], display[0], display[5], display[4], display[1], display[2])).Count() == 6)
                        {
                            value += "9";
                        }
                        else if (digit.Intersect(string.Concat(display[3], display[4], display[5], display[6], display[1], display[2])).Count() == 6)
                        {
                            value += "6";
                        }
                        else
                        {
                            value += "0";
                        }
                        break;
                    case 7:
                        value += "8";
                        break;
                }
            }

            return int.Parse(value);
        }

        public static (string[] Signals, string[] Digits) GetInputs(string line)
        {
            var values = line.Split(" | ").Select(s => s.Split(' ').Select(e => string.Concat(e.OrderBy(e => e))).ToArray()).ToArray();

            return (Signals: values[0], Digits: values[1]);
        }
    }
}
