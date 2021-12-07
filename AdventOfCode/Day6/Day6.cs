using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day1
{
    public class Day6 : IDay
    {
        public string GetPart1Result()
        {
            List<int> fishes = File.ReadAllText($"./{GetType().Name}/Inputs/Example.txt").Split(',').Select(x => int.Parse(x)).ToList();

            for (int i = 0; i < 80; i++)
            {
                int fishesToAdd = 0;
                for (int j = 0; j < fishes.Count(); j++)
                {
                    if (fishes[j] == 0)
                    {
                        fishesToAdd++;
                        fishes[j] = 6;
                    }
                    else
                    {
                        fishes[j]--;
                    }
                }

                for (int j = 0; j < fishesToAdd; j++)
                {
                    fishes.Add(8);
                }
            }

            return fishes.Count().ToString();
        }

        public string GetPart2Result()
        {
            List<int> fishes = File.ReadAllText($"./{GetType().Name}/Inputs/Input1.txt").Split(',').Select(x => int.Parse(x)).ToList();

            int days = 256;
            var groupFishes = new Dictionary<long, long>();
            for (int i = 0; i <= 8; i++)
            {
                groupFishes.Add(i, fishes.Count(x => x == i));
            }

            for (int i = 0; i < days; i++)
            {
                var fishesCopy = new Dictionary<long, long>(groupFishes);
                for (int j = 8; j >= 0; j--)
                {
                    if (j == 0)
                    {
                        fishesCopy[8] = groupFishes[j];
                        fishesCopy[6] += groupFishes[j];
                    }
                    else
                    {
                        fishesCopy[j - 1] = groupFishes[j];
                    }
                }

                groupFishes = fishesCopy;
            }
            return groupFishes.Sum(x => x.Value).ToString();
        }
    }
}