using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day1
{
    public class Day4 : IDay
    {
        public string GetPart1Result()
        {
            IEnumerable<string> lines = File.ReadLines($"./{GetType().Name}/Inputs/Input1.txt");
            int[] calledNumbers = lines.First().Split(',').Select(x => int.Parse(x)).ToArray();
            List<Board> boards = new List<Board>();
            int lineNumber = 0;
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrEmpty(line))
                {
                    boards.Add(new());
                    lineNumber = 0;
                    continue;
                }

                var numbers = Regex.Matches(line, "[0-9]*")
                    .Where(x => !string.IsNullOrEmpty(x.ToString()))
                    .Select(x => (int.Parse(x.ToString()), false));
                for (int i = 0; i < 5; i++)
                {
                    boards.Last().Grid[lineNumber, i] = numbers.ElementAt(i);
                }
                
                lineNumber++;
            }

            foreach (var calledNumber in calledNumbers)
            {
                bool won = false;
                foreach (var board in boards)
                {
                    won = board.CallNumber(calledNumber);
                    
                    if (won)
                    {
                        return (board.UnCalledNumberSum() * calledNumber).ToString();
                    }
                }

            }

            return String.Empty;
        }

        public string GetPart2Result()
        {
            IEnumerable<string> lines = File.ReadLines($"./{GetType().Name}/Inputs/Input1.txt");
            int[] calledNumbers = lines.First().Split(',').Select(x => int.Parse(x)).ToArray();
            List<Board> boards = new List<Board>();
            int lineNumber = 0;
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrEmpty(line))
                {
                    boards.Add(new());
                    lineNumber = 0;
                    continue;
                }

                var numbers = Regex.Matches(line, "[0-9]*")
                    .Where(x => !string.IsNullOrEmpty(x.ToString()))
                    .Select(x => (int.Parse(x.ToString()), false));
                for (int i = 0; i < 5; i++)
                {
                    boards.Last().Grid[lineNumber, i] = numbers.ElementAt(i);
                }
                
                lineNumber++;
            }

            (Board board, int calledNumber) lastWon = default;
            foreach (var calledNumber in calledNumbers)
            {
                List<Board> toRemove = new();
                for (int i = 0; i < boards.Count(); i++)
                {
                    if (boards[i].CallNumber(calledNumber))
                    {
                        toRemove.Add(boards[i]);
                        lastWon = (boards[i], calledNumber);
                    }
                }

                foreach (var board in toRemove)
                {
                    boards.Remove(board);
                }
            }

            return (lastWon.board.UnCalledNumberSum() * lastWon.calledNumber).ToString();
        }

    }

    public class Board
    {
        public int UnCalledNumberSum()
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!Grid[i, j].called)
                    {
                        sum += Grid[i, j].number;
                    }
                }
            }
            return sum;
        }
        
        public bool CallNumber(int number)
        {
            bool won = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Grid[i,j].number == number)
                    {
                        Grid[i, j].called = true;
                        if (IsColumnWon(j) || IsRowWon(i))
                        {
                            won = true;
                        }
                    }
                }
            }

            return won;
        }

        private bool IsColumnWon(int columnNumber)
        {
            bool won = true;
            for (int i = 0; i < 5; i++)
            {
                if (Grid[i, columnNumber].called == false)
                {
                    won = false;
                    break;
                }
            }
            return won;
        }
        
        private bool IsRowWon(int rowNumber)
        {
            bool won = true;
            for (int i = 0; i < 5; i++)
            {
                if (Grid[rowNumber, i].called == false)
                {
                    won = false;
                    break;
                }
            }
            return won;
        }

        public (int number, bool called)[,] Grid { get; set; } = new (int number, bool called)[5,5];
    }
}