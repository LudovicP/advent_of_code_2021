using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day8 : IDay
    {
        public string GetPart1Result()
        {
            List<Display> displays = File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt")
                .Select(x =>new Display(x.Split('|')))
                .ToList();

            int count = 0;
            foreach (var display in displays)
            {
                count += display.Count(1) + display.Count(4) + display.Count(7) + display.Count(8);
            }

          return count.ToString();
        }

        public string GetPart2Result()
        {
            List<Display> displays = File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt")
                .Select(x =>new Display(x.Split('|')))
                .ToList();
            
            int sum = 0;
            foreach (var display in displays)
            {
                sum += display.GetCode();
            }

            return sum.ToString();
        }

        public class Display
        {
            public Display(IEnumerable<string> signals)
            {
                Patterns = signals.ElementAt(0).Trim().Split(' ').Select(x => new string(x.OrderBy(v => v).ToArray()));
                Output = signals.ElementAt(1).Trim().Split(' ').Select(x => new string(x.OrderBy(v => v).ToArray()));
            }
            
            public int Count(int number)
            {
                switch (number)
                {
                    case 1 :
                        return Number1.Count();
                    case 4 :
                        return Number4.Count();
                    case 7 :
                        return Number7.Count();
                    case 8 :
                        return Number8.Count();
                }

                return 0;
            }

            public string One => Patterns.Single(x => x.Length == 2);
            public string Two => Patterns.Single(x => x.Length == 5 && Contains(x, Diff(Eight, Nine)));
            public string Three => Patterns.Single(x => x.Length == 5 && Contains(x, Seven));
            public string Four => Patterns.Single(x => x.Length == 4);
            public string Five => Patterns.Single(x => x.Length == 5 && !x.Equals(Two) && !x.Equals(Three));
            public string Six => Patterns.Single(x => x.Length == 6 && Contains(x, Five) && !x.Equals(Nine));
            public string Seven => Patterns.Single(x => x.Length == 3);
            public string Eight => Patterns.Single(x => x.Length == 7);
            public string Nine => Patterns.Single(x => x.Length == 6 && Contains(x, Four));
            public string Zero => Patterns.Single(x => x.Length == 6 && !x.Equals(Nine) && !x.Equals(Six));
            
            private bool Contains(string value, string containedString)
            {
                foreach (var character in containedString)
                {
                    if (!value.Contains(character))
                    {
                        return false;
                    }
                }

                return true;
            }

            private string Diff(string value1, string value2)
            {
                if (value1.Count() > value2.Count())
                {
                    return new string(value1.Except(value2).ToArray());
                }
                else
                {
                    return new string(value2.Except(value1).ToArray());
                }
            }

            public IEnumerable<string> Patterns { get; }
            public IEnumerable<string> Output { get; }

            public IEnumerable<string> Number1 => Output.Where(x => x.Length == 2).ToList();

            public IEnumerable<string> Number4 => Output.Where(x => x.Length == 4).ToList();

            public IEnumerable<string> Number7 => Output.Where(x => x.Length == 3).ToList();
            
            public IEnumerable<string> Number8 => Output.Where(x => x.Length == 7).ToList();

            public int GetCode()
            {
                List<char> result = new();

                foreach (var value in Output)
                {
                    if(Equals(value, One)) result.Add('1');
                    if(Equals(value, Two)) result.Add('2');
                    if(Equals(value, Three)) result.Add('3');
                    if(Equals(value, Four)) result.Add('4');
                    if(Equals(value, Five)) result.Add('5');
                    if(Equals(value, Six)) result.Add('6');
                    if(Equals(value, Seven)) result.Add('7');
                    if(Equals(value, Eight)) result.Add('8');
                    if(Equals(value, Nine)) result.Add('9');
                    if(Equals(value, Zero)) result.Add('0');
                }

                return int.Parse(new string(result.ToArray()));
            }
        }
    }
}