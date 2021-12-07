using System;
using AdventOfCode.Day1;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day7();
            Console.WriteLine(day.GetPart1Result());
            Console.WriteLine(day.GetPart2Result());
        }
    }
}