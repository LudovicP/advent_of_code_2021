using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day2: IDay
    {
        public string GetPart1Result()
        {
            (int horizontal, int depth) position = (0,0);
            IEnumerable<(string direction, int distance)> moves = File.ReadLines($"./Day2/Inputs/Input1.txt").Select(x => (x.Split(' ')[0], int.Parse(x.Split(' ')[1])));
            
            foreach (var move in moves)
            {
                switch (move.direction)
                {   
                    case "forward":
                        position.horizontal += move.distance;
                        break;
                    case "down":
                        position.depth += move.distance;
                        break;
                    case "up":
                        position.depth -= move.distance;
                        break;
                }
            }

            return (position.depth * position.horizontal).ToString();
        }

        public string GetPart2Result()
        {
            (int horizontal, int depth) position = (0,0);
            IEnumerable<(string direction, int distance)> moves = File.ReadLines($"./Day2/Inputs/Input1.txt").Select(x => (x.Split(' ')[0], int.Parse(x.Split(' ')[1])));

            int aim = 0;
            foreach (var move in moves)
            {
                switch (move.direction)
                {   
                    case "forward":
                        position.horizontal += move.distance;
                        position.depth += move.distance * aim;
                        break;
                    case "down":
                        aim += move.distance;
                        break;
                    case "up":
                        aim -= move.distance;
                        break;
                }
            }

            return (position.depth * position.horizontal).ToString();
        }
    }
}