using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class Day10 : IDay
    {
        private readonly Dictionary<char, int> _points = new()
        {
            {')' , 3},
            {']' , 57},
            {'}' , 1197},
            {'>' , 25137}
        };
        private readonly Dictionary<char, int> _pointPart2 = new()
        {
            {')' , 1},
            {']' , 2},
            {'}' , 3},
            {'>' , 4}
        };

        public string GetPart1Result()
        {
            long score = 0;
            Stack<char> stack = new Stack<char>();
            foreach (var line in File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt"))
            {
                foreach (var item in line.ToCharArray())
                {
                    if (item == '(' || item == '{' || item == '[' || item == '<')
                    {
                        stack.Push(item);
                    }
                    else if(item == ')' || item == '}' || item == ']' || item == '>')
                    {
                        var expected = stack.Pop();
                        if (expected != GetComplementBracket(item))
                        {
                            score += _points[item];
                            break;
                        }
                    }
                }
            }

            return score.ToString();
        }
        
        private static char GetComplementBracket(char item)
        {
            switch (item)
            {
                case ')':
                    return '(';
                case '}':
                    return '{';
                case ']':
                    return '[';
                case '>':
                    return '<';
                case '(':
                    return ')';
                case '{':
                    return '}';
                case '[':
                    return ']';
                case '<':
                    return '>';
                default:
                    return ' ';
            }
        }

        public string GetPart2Result()
        {
            List<long> scores = new();
            foreach (var line in File.ReadAllLines($"./{GetType().Name}/Inputs/Input1.txt"))
            {
                Stack<char> stack = new Stack<char>();
                bool incorrect = false;
                foreach (var item in line.ToCharArray())
                {
                    if (item == '(' || item == '{' || item == '[' || item == '<')
                    {
                        stack.Push(item);
                    }
                    else if(item == ')' || item == '}' || item == ']' || item == '>')
                    {
                        var expected = stack.Pop();
                        if (expected != GetComplementBracket(item))
                        {
                            incorrect = true;
                            break;
                        }
                    }
                }

                if (!incorrect)
                {
                    scores.Add(GetScore(stack));
                }
            }

            return scores.OrderBy(x => x).ElementAt((scores.Count / 2)+1).ToString();
        }

        private long GetScore(Stack<char> stack)
        {
            long score = 0;
            while (stack.TryPop(out char value))
            {
                score *= 5;
                score += _pointPart2[GetComplementBracket(value)];
            }

            return score;
        }
    }
}