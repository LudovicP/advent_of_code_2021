using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day1: IDay
    {
        public string GetPart1Result()
        {
            int counter = 0;
            IEnumerable<int> measures = File.ReadLines("./Day1/Inputs/Input1.txt").Select(x => int.Parse(x));
            int lastMeasure = measures.First();
            foreach (var measure in measures.Skip(1))
            {
                if (measure > lastMeasure)
                {
                    counter++;
                }

                lastMeasure = measure;
            }
            return counter.ToString();
        }

        public string GetPart2Result()
        {
            int counter = 0;
            IEnumerable<int> measures = File.ReadLines("./Day1/Inputs/Input1.txt").Select(x => int.Parse(x));
            Queue<int> lastMeasures = new Queue<int>();
            foreach (var measure in measures)
            {
                if (lastMeasures.Count == 3)
                {
                    var lastMeasureSum = lastMeasures.Sum(x => x);
                    lastMeasures.Dequeue();
                    var actualMeasure = lastMeasures.Sum(x => x) + measure;
                    if (actualMeasure > lastMeasureSum)
                    {
                        counter++;
                    }
                }
                lastMeasures.Enqueue(measure);
            }
            return counter.ToString();
        }
    }
}