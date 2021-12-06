using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day1
{
    public class Day5 : IDay
    {
        public string GetPart1Result()
        {
            IEnumerable<Vent> vents = File.ReadLines($"./{GetType().Name}/Inputs/Input1.txt")
                .Select(x =>
                {
                    var coordinates = x.Split("->");

                    var start = coordinates[0].Trim();
                    var end = coordinates[1].Trim();

                    Point first = new(int.Parse(start.Split(',')[0]), int.Parse(start.Split(',')[1]));
                    Point last = new(int.Parse(end.Split(',')[0]), int.Parse(end.Split(',')[1]));

                    return new Vent(first, last);
                });
            

            return vents
                .SelectMany(x => x.Points)
                .GroupBy(x => new { x.X, x.Y })
                .Select(x => x.Count() >= 2 ? 1 : 0)
                .Sum()
                .ToString();
        }

        public string GetPart2Result()
        {
            IEnumerable<Vent> vents = File.ReadLines($"./{GetType().Name}/Inputs/Input1.txt")
                .Select(x =>
                {
                    var coordinates = x.Split("->");

                    var start = coordinates[0].Trim();
                    var end = coordinates[1].Trim();

                    Point first = new(int.Parse(start.Split(',')[0]), int.Parse(start.Split(',')[1]));
                    Point last = new(int.Parse(end.Split(',')[0]), int.Parse(end.Split(',')[1]));

                    return new Vent(first, last, true);
                });

            // foreach (var point in vents
            //     .SelectMany(x => x.Points)
            //     .GroupBy(x => new { x.X, x.Y }))
            // {
            //     Console.WriteLine($"{point.Key.X},{point.Key.Y} : {point.Count()}");
            // }

            return vents
                .SelectMany(x => x.Points)
                .GroupBy(x => new { x.X, x.Y })
                .Select(x => x.Count() >= 2 ? 1 : 0)
                .Sum()
                .ToString();
        }
    }

    public class Vent
    {
        public Vent()
        {
        }

        public Vent(Point start, Point end, bool includeDiagonales = false)
        {
            if (start.Y == end.Y)
            {
                foreach (var x in Enumerable.Range(Math.Min(start.X, end.X), Math.Max(start.X, end.X) + 1 - Math.Min(start.X, end.X)))
                {
                    Points.Add(new(x, start.Y));
                }
            }

            if (start.X == end.X)
            {
                foreach (var y in Enumerable.Range(Math.Min(start.Y, end.Y), Math.Max(start.Y, end.Y) + 1 - Math.Min(start.Y, end.Y)))
                {
                    Points.Add(new(start.X, y));
                }
            }

            if (includeDiagonales && start.X != end.X && start.Y != end.Y)
            {
                int index = 0;
                var ys = Enumerable.Range(Math.Min(start.Y, end.Y), Math.Max(start.Y, end.Y) + 1 - Math.Min(start.Y, end.Y));
                var xs = Enumerable.Range(Math.Min(start.X, end.X), Math.Max(start.X, end.X) + 1 - Math.Min(start.X, end.X));
                for (int i = 0; i < xs.Count(); i++)
                {
                    int x = xs.ElementAt(start.X > end.X ? xs.Count() - 1 - index : index);
                    int y = ys.ElementAt(start.Y > end.Y ? ys.Count() - 1 - index : index);
                    Points.Add(new(x, y));
                    index++;
                }
            }
        }

        public List<Point> Points { get; } = new();
    }
}