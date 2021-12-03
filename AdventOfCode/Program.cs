using System;
using AdventOfCode.Day1;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day3();
            Console.WriteLine(day.GetPart2Result());
        }
    }
}