using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day11 : IDay
    {
        public string GetPart1Result()
        {
            int lineIndex = 0;
            int[,] octopuses = new int[10, 10];
            foreach (var line in File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt"))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    octopuses[lineIndex, i] = int.Parse(line[i].ToString());
                }

                lineIndex++;
            }

            int steps = 100;
            int countFlashes = 0;

            for (int i = 0; i < steps; i++)
            {
                octopuses = IncreeaseEnergy(octopuses);
                octopuses = FlashOctopus(octopuses);
                octopuses = ReInitEnergy(octopuses);
                countFlashes += GetFlashCount(octopuses);
                // Console.WriteLine($"step {i}");
                // Display(octopuses);
            }

            return countFlashes.ToString();
        }

        private void Display(int[,] octopuses)
        {
            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Console.Write(octopuses[line, column]);
                }
                Console.Write('\n');
            }

        }

        private int[,] ReInitEnergy(int[,] octopuses)
        {
            int[,] result = new int[10, 10];
            Array.Copy(octopuses, 0, result, 0, octopuses.Length);
            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if (result[line, column] < 0)
                    {
                        result[line, column] = 0;
                    }
                }
            }

            return result;
        }

        private int[,] IncreeaseEnergy(int[,] octopuses)
        {
            int[,] result = new int[10, 10];
            Array.Copy(octopuses, 0, result, 0, octopuses.Length);
            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 10; column++)
                {
                    result[line, column] = octopuses[line, column] + 1;
                }
            }

            return result;
        }

        private int[,] FlashOctopus(int[,] octopuses)
        {
            int[,] result = new int[10, 10];
            Array.Copy(octopuses, 0, result, 0, octopuses.Length);
            bool flash = false;
            do
            {
                flash = false;
                for (int line = 0; line < 10; line++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        if (result[line, column] > 9)
                        {
                            result = FlashNearOctopuses(result, line, column);
                            result[line, column] = Int32.MinValue;
                            flash = true;
                        }
                    }
                }
            } while (flash);

            return result;
        }

        private int[,] FlashNearOctopuses(int[,] octopuses, int line, int column)
        {
            int[,] result = new int[10, 10];
            Array.Copy(octopuses, 0, result, 0, octopuses.Length);

            var rangeLine = Enumerable.Range(line - 1, 3);
            var rangeColumn = Enumerable.Range(column - 1, 3);

            for (int l = 0; l < 10; l++)
            {
                for (int c = 0; c < 10; c++)
                {
                    if (rangeLine.Contains(l) && rangeColumn.Contains(c))
                    {
                        result[l, c]++;
                    }
                }
            }

            return result;
        }

        private int GetFlashCount(int[,] octopuses)
        {
            int count = 0;
            foreach (var octopus in octopuses)
            {
                count += octopus == 0 ? 1 : 0;
            }

            return count;
        }

        public string GetPart2Result()
        {
            int lineIndex = 0;
            int[,] octopuses = new int[10, 10];
            foreach (var line in File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt"))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    octopuses[lineIndex, i] = int.Parse(line[i].ToString());
                }

                lineIndex++;
            }

            int step = 0;
            int allFlashStep = 0;

            do
            {
                octopuses = IncreeaseEnergy(octopuses);
                octopuses = FlashOctopus(octopuses);
                octopuses = ReInitEnergy(octopuses);
                step++;
                allFlashStep = GetFlashCount(octopuses) == 100 ? step : allFlashStep;

            } while (allFlashStep == 0);

            return allFlashStep.ToString();
        }
    }
}