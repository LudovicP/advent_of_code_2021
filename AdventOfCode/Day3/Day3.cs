using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day3 : IDay
    {
        public string GetPart1Result()
        {
            IEnumerable<string> reports = File.ReadLines($"./{GetType().Name}/Inputs/Input1.txt");
            int[] counter = new int[reports.First().Length];
            foreach (var report in reports)
            {
                for (int i = 0; i < report.Length; i++)
                {
                    counter[i] += int.Parse(report[i].ToString());
                }
            }

            int gammaRate = GetIntFromBytes(new string(counter.Select(x => x > reports.Count() / 2 ? '1' : '0').ToArray()));
            int epsilonRate = GetIntFromBytes(new string(counter.Select(x => x < reports.Count() / 2 ? '1' : '0').ToArray()));
            return (gammaRate * epsilonRate).ToString();
        }

        private int GetIntFromBytes(string bytes)
        {
            int value = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == '1')
                {
                    value += (int)Math.Pow(2, bytes.Length - i - 1);
                }
            }

            return value;
        }

        public string GetPart2Result()
        {
            IEnumerable<string> reports = File.ReadLines($"./{GetType().Name}/Inputs/Input1.txt");

            int oxygenGeneratorRating = GetIntFromBytes(GetOxygenGeneratorRating(0, reports));
            int co2ScrubberRating = GetIntFromBytes(GetCo2ScrubberRating(0, reports));
            return (oxygenGeneratorRating * co2ScrubberRating).ToString();
        }

        private string GetOxygenGeneratorRating(int index, IEnumerable<string> reports)
        {
            if (reports.Count() == 1)
            {
                return reports.First();
            }

            string pattern = new string(reports.First().AsSpan()[..index]);
            int counter = 0;
            foreach (var report in reports)
            {
                counter += int.Parse(report[index].ToString());
            }

            index++;
            pattern += (counter * 2 >= reports.Count() ? '1' : '0');
            return GetOxygenGeneratorRating(index, reports.Where(x => x.StartsWith(pattern)));
        }

        private string GetCo2ScrubberRating(int index, IEnumerable<string> reports)
        {
            if (reports.Count() == 1)
            {
                return reports.First();
            }

            string pattern = new string(reports.First().AsSpan()[..index]);
            int counter = 0;
            foreach (var report in reports)
            {
                counter += int.Parse(report[index].ToString());
            }

            index++;
            pattern += (counter * 2 >= reports.Count() ? '0' : '1');
            return GetCo2ScrubberRating(index, reports.Where(x => x.StartsWith(pattern)));
        }
    }
}