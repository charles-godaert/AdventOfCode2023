using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace D3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Users\\Charles\\Desktop\\Projects\\AdventOfCode2023\\D3\\input.txt";
            // Match all numbers
            string pattern = @"\d+";
            // Detect all *
            string patternForStar = @"\*";

            List<string> lines = new List<string>();

            int total = 0;
            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                #region Part 2
                foreach (string line in lines)
                {
                    List<int> adjacentNumbers = new List<int>();
                    foreach (Match match in Regex.Matches(line, patternForStar))
                    {
                        adjacentNumbers.Clear();

                        int starPosition = match.Index;

                        // Check if numbers are adjacents to the star on the same line
                        foreach (Match matchNumber in Regex.Matches(line, pattern))
                        {
                            int number = int.Parse(matchNumber.Value);
                            int startNumberPositionInLine = matchNumber.Index;
                            int endNumberPositionInLine = matchNumber.Index + matchNumber.Length - 1;

                            if(startNumberPositionInLine == starPosition + 1 || endNumberPositionInLine == starPosition - 1)
                            {
                                adjacentNumbers.Add(number);
                                continue;
                            }
                        }

                        // Check if numbers are adjacents to the star on the previous line
                        int previousLineNumber = lines.IndexOf(line) - 1;
                        string previousLine = (previousLineNumber >= 0 ? lines[previousLineNumber] : String.Empty);
                        if (previousLine != String.Empty)
                        {
                            // Get all numbers in the line
                            foreach (Match matchNumber in Regex.Matches(previousLine, pattern))
                            {
                                int number = int.Parse(matchNumber.Value);
                                int startNumberPositionInLine = matchNumber.Index;
                                int endNumberPositionInLine = matchNumber.Index + matchNumber.Length - 1;

                                if ((startNumberPositionInLine <= starPosition + 1  && startNumberPositionInLine >= starPosition - 1) || 
                                    (endNumberPositionInLine   <= starPosition + 1  && endNumberPositionInLine   >= starPosition - 1))
                                    
                                {
                                    adjacentNumbers.Add(number);
                                    continue;
                                }
                            }
                        }

                        // Check if numbers are adjacents to the star on the next line
                        int nextLineNumber = lines.IndexOf(line) + 1;
                        string nextLine = (nextLineNumber < lines.Count ? lines[nextLineNumber] : String.Empty);
                        if (nextLine != String.Empty)
                        {
                            // Get all numbers in the line
                            foreach (Match matchNumber in Regex.Matches(nextLine, pattern))
                            {
                                int number = int.Parse(matchNumber.Value);
                                int startNumberPositionInLine = matchNumber.Index;
                                int endNumberPositionInLine = matchNumber.Index + matchNumber.Length - 1;

                                if ((startNumberPositionInLine <= starPosition + 1 && startNumberPositionInLine >= starPosition - 1) ||
                                    (endNumberPositionInLine <= starPosition + 1 && endNumberPositionInLine >= starPosition - 1))

                                {
                                    adjacentNumbers.Add(number);
                                    continue;
                                }

                               
                            }
                        }

                        if(adjacentNumbers.Count == 2)
                        {
                            total += adjacentNumbers[0] * adjacentNumbers[1];
                        }
                    }
                }
                #endregion Part 2

                #region Part 1
                /*
                foreach(string line in lines)
                {
                    // Print number of lines
                    //Console.WriteLine($"Line {lines.IndexOf(line)} : {line}");

                    // Foreach number in the line
                    foreach (Match match in Regex.Matches(line, pattern))
                    {
                        int number = int.Parse(match.Value);
                        int startPositionInLine = match.Index;
                        int endPositionInLine = match.Index + match.Length - 1;


                        ////// Check if a special char (not a dot) is adjacent to the number
                        
                        // Current line
                        if (startPositionInLine > 0 && line[startPositionInLine - 1] != '.' ||
                            endPositionInLine < line.Length - 1 && line[endPositionInLine + 1] != '.')
                        {
                            total += number;
                            //Console.WriteLine($"Number : {number} - Start position : {startPositionInLine} - End position : {endPositionInLine}");
                            continue;
                        }

                        // Previous line
                        int previousLineNumber = lines.IndexOf(line) - 1;
                        string previousLine = (previousLineNumber >= 0 ? lines[previousLineNumber] : String.Empty);
                        if (previousLine != String.Empty)
                        {
                            int previousLineSubstringStartIndex = startPositionInLine == 0 ? 0 : startPositionInLine - 1;
                            int previousLineSubstringEndIndex = endPositionInLine == previousLine.Length - 1 ? endPositionInLine : endPositionInLine + 1;
                            string previousLineSubstring = previousLine.Substring(previousLineSubstringStartIndex, previousLineSubstringEndIndex - previousLineSubstringStartIndex + 1);

                            // Check if contains a special char (all char except dot or number)
                            if (Regex.IsMatch(previousLineSubstring, @"[^\.0-9]"))
                            {
                                total += number;
                                //Console.WriteLine($"Number : {number} - Start position : {startPositionInLine} - End position : {endPositionInLine}");
                                continue;
                            }
                        }

                        // Next line
                        int nextLineNumber = lines.IndexOf(line) + 1;
                        string nextLine = (nextLineNumber < lines.Count ? lines[nextLineNumber] : String.Empty);

                        if (nextLine != String.Empty)
                        {
                            int nextLineSubstringStartIndex = startPositionInLine == 0 ? 0 : startPositionInLine - 1;
                            int nextLineSubstringEndIndex = endPositionInLine == nextLine.Length - 1 ? endPositionInLine : endPositionInLine + 1;
                            string nextLineSubstring = nextLine.Substring(nextLineSubstringStartIndex, nextLineSubstringEndIndex - nextLineSubstringStartIndex + 1);

                            // Check if contains a special char (all char except dot or number)
                            if (Regex.IsMatch(nextLineSubstring, @"[^\.0-9]"))
                            {
                                total += number;
                                //Console.WriteLine($"Number : {number} - Start position : {startPositionInLine} - End position : {endPositionInLine}");
                                continue;
                            }
                        }

                    }              
                }
                */
                #endregion Part 1
                Console.WriteLine("Total : " + total);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading text file");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
