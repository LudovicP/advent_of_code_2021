using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day1 = new Day1.Day1();
            Console.WriteLine(day1.GetPart2Result());
        }
    }
}