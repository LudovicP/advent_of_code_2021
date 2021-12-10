using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdventOfCode.Day1
{
    public class Day9 : IDay
    {
        public string GetPart1Result()
        {
            List<Point> points = new();
            int lineIndex = 0;
            foreach (var line in File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt"))
            {
                int columnIndex = 0;
                points.AddRange(line.Select(x => new Point(lineIndex, columnIndex++, int.Parse(x.ToString()))));
                lineIndex++;
            }

            var lowPoints = points.Where(x => IsLower(x, points)).ToList();

            return (lowPoints.Sum(x => x.Heigh) + lowPoints.Count()).ToString();
        }

        private bool IsLower(Point point, List<Point> points)
        {
            return !points.Any(x => x.Heigh <= point.Heigh &&
                                    ((x.X == point.X && (x.Y == point.Y - 1 || x.Y == point.Y + 1)) ||
                                     (x.Y == point.Y && (x.X == point.X - 1 || x.X == point.X + 1))));
        }
        
        private List<Point> GetHigherAndNotInBassin(Point point, List<Point> points)
        {
            return points.Where(x => x.Heigh >= point.Heigh && !x.IsInBassin && !x.IsNotDeepEnough).ToList();
        }

        private List<Point> NearPoint(Point point, List<Point> points)
        {
            return points.Where(x => ((x.X == point.X && (x.Y == point.Y - 1 || x.Y == point.Y + 1)) ||
                               (x.Y == point.Y && (x.X == point.X - 1 || x.X == point.X + 1)))).ToList();
        }

        public string GetPart2Result()
        {
            List<Point> points = new();
            int lineIndex = 0;
            foreach (var line in File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt"))
            {
                int columnIndex = 0;
                points.AddRange(line.Select(x => new Point(lineIndex, columnIndex++, int.Parse(x.ToString()))));
                lineIndex++;
            }
            
            var bassins = GetBassins(points);

            int result = 1;

            foreach (var size in bassins.OrderByDescending(x => x.Count)
                .Take(3)
                .Select(b => b.Count))
            {
                result *= size;
            }
            return result.ToString();
        }

        private List<List<Point>> GetBassins(List<Point> points)
        {
            var lowerPoints = points.Where(x => IsLower(x, points)).ToList();
            List<List<Point>> bassins = new();
            foreach (var lowerPoint in lowerPoints)
            {
                List<Point> newPointList = new List<Point>(points.Count);

                points.ForEach((item)=>
                {
                    newPointList.Add(new Point(item));
                });
                bassins.Add(getPointsOfBassin(lowerPoint, new (newPointList)));
            }

            return bassins;
        }

        private List<Point> getPointsOfBassin(Point point, List<Point> points)
        {
            points.Single(x => x.X == point.X && x.Y == point.Y).IsInBassin = true;
            var currentBassinPoints = GetHigherAndNotInBassin(point, NearPoint(point, points));

            foreach (var currentBassinPoint in currentBassinPoints)
            {
                getPointsOfBassin(currentBassinPoint, points);
            }
            
            return points.Where(x => x.IsInBassin).ToList();
        }

        private class Point
        {
            public Point(Point point)
            {
                X = point.X;
                Y = point.Y;
                Heigh = point.Heigh;
                IsInBassin = point.IsInBassin;
                IsNotDeepEnough = point.IsNotDeepEnough;
            }
            public Point(int x, int y, int height)
            {
                X = x;
                Y = y;
                Heigh = height;
                IsNotDeepEnough = height == 9;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Heigh { get; set; }
            public bool IsInBassin { get; set; }
            public bool IsNotDeepEnough { get; set; }
        }
    }
}