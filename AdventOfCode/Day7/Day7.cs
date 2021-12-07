using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day1
{
    public class Day7 : IDay
    {
        public string GetPart1Result()
        {
            List<int> crabSubMarines = File.ReadAllText($"./{GetType().Name}/Inputs/Input1.txt").Split(',').Select(x => int.Parse(x)).ToList();
            
            int numberCount = crabSubMarines.Count();
            int halfIndex = crabSubMarines.Count()/2;
            var sortedNumbers = crabSubMarines.OrderBy(n=>n);
            double median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                           sortedNumbers.ElementAt((halfIndex - 1)))/ 2);
            } else {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            

          return crabSubMarines.Select(x => Math.Abs(x - median)).Sum().ToString();
        }

        public string GetPart2Result()
        {
            List<int> crabSubMarines = File.ReadAllText($"./{GetType().Name}/Inputs/Input1.txt").Split(',').Select(x => int.Parse(x)).ToList();
            
            double min = double.MaxValue;
            
            for (int i = crabSubMarines.Min(); i <= crabSubMarines.Max(); i++)
            {
                var fuel = crabSubMarines.Select(x => GetFuel(Math.Abs(x - i))).Sum();
                if (fuel < min)
                {
                    min = fuel;
                }
            }

            return min.ToString();
        }

        private double GetFuel(double distance)
        {
            double fuel = 0;

            for (int i = 0; i < distance; i++)
            {
                fuel += i+1;
            }
            
            return fuel;
        }
    }
}