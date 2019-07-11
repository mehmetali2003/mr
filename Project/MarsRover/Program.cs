using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("Input.txt"))
            {
                StringBuilder result = new StringBuilder();
                string line = sr.ReadLine();
                string[] boundaryCoords = line.Split(' ');
                int boundaryX = Convert.ToInt32(boundaryCoords[0]);
                int boundaryY = Convert.ToInt32(boundaryCoords[1]);
                List<Position> others = new List<Position>();

                line = sr.ReadLine();
                while (line != null)
                {
                    string[] positionInfo = line.Split(' ');
                    int currentX = Convert.ToInt32(positionInfo[0]);
                    int currentY = Convert.ToInt32(positionInfo[1]);
                    char currentOrientation = positionInfo[2][0];

                    Position currentPos = new Position(currentX, currentY, currentOrientation);

                    line = sr.ReadLine();
                    if (line != null)
                    {
                        foreach (var instruction in line)
                        {
                            switch (instruction)
                            {
                                case 'L':
                                case 'R':
                                    SetNewOrientation(currentPos, instruction);
                                    break;
                                case 'M':
                                    SetNewPosition(currentPos, boundaryX, boundaryY, others);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    others.Add(currentPos);
                    result.AppendLine(currentPos.GetInfo());
                    line = sr.ReadLine();
                }
                File.WriteAllText("Output.txt", result.ToString());
                Console.WriteLine(@"
Output has been written to this path [bin\Debug\netcoreapp2.1\Output.txt] by using values in [..\bin\Debug\netcoreapp2.1\Input.txt].
You can check values in these files against values in [..\..\..\Problem Specs.txt] file.");
                Console.ReadLine();
            }
        }

        static string orients = "ESWN";
        private static void SetNewOrientation(Position current, char instruction)
        {
            int currentIndex = orients.IndexOf(current.Orientation);
            char newOrientation;
            if (instruction == 'L')
            {
                if (currentIndex == 0)
                    newOrientation = orients.Last();
                else
                    newOrientation = orients[currentIndex - 1];
            }
            else
            {
                if (currentIndex == orients.Length - 1)
                    newOrientation = orients.First();
                else
                    newOrientation = orients[currentIndex + 1];
            }

            current.Orientation = newOrientation;
        }

        private static void SetNewPosition(Position current, int boundaryX, int boundaryY, List<Position> others)
        {
            Position next = new Position(current.Xcoordinate, current.Ycoordinate, current.Orientation);
            switch (current.Orientation)
            {
                case 'E':
                    next.Xcoordinate++;
                    break;
                case 'S':
                    next.Ycoordinate--;
                    break;
                case 'W':
                    next.Xcoordinate--;
                    break;
                case 'N':
                    next.Ycoordinate++;
                    break;
                default:
                    break;
            }

            if (CheckAvailability(next, boundaryX, boundaryY, others))
                current.SetValue(next);
        }

        private static bool CheckAvailability(Position candidate, int boundaryX, int boundaryY, List<Position> others)
        {
            if (candidate.Xcoordinate > boundaryX || candidate.Xcoordinate < 0)
                return false;

            if (candidate.Ycoordinate > boundaryY || candidate.Ycoordinate < 0)
                return false;

            if (others.Any(p => p.Xcoordinate == candidate.Xcoordinate && p.Ycoordinate == candidate.Ycoordinate))
                return false;

            return true;
        }
    }
}