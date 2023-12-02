using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Users\\Charles\\Desktop\\Projects\\AdventOfCode2023\\AdventOfCode2023\\input.txt";

            Dictionary<string, int> figuresInText = new Dictionary<string, int>()
            {
                {"one", 1},
                {"two", 2},
                {"three", 3},
                {"four", 4},
                {"five", 5},
                {"six", 6},
                {"seven", 7},
                {"eight", 8},
                {"nine", 9}
            };

            int total = 0;
            string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";

            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> foundFigures = new List<string>();

                        for (int i = 0; i < line.Length; i++)
                        {
                            Match match = Regex.Match(line.Substring(i), pattern);
                            if (match.Success)
                            {
                                foundFigures.Add(match.Value);
                            }
                        }

                        if (foundFigures.Count > 0)
                        {
                            string firstFigure = foundFigures[0];
                            firstFigure = (figuresInText.ContainsKey(firstFigure) ? figuresInText[firstFigure].ToString() : firstFigure);

                            string lastFigure = foundFigures[foundFigures.Count - 1];
                            lastFigure = (figuresInText.ContainsKey(lastFigure) ? figuresInText[lastFigure].ToString() : lastFigure);
                            lastFigure = (lastFigure == String.Empty ? firstFigure : lastFigure);

                            int numberFromFirstAndLastFigure = int.Parse(firstFigure + lastFigure);
                            total += numberFromFirstAndLastFigure;
                            Console.WriteLine($"{line} -> {numberFromFirstAndLastFigure} - total : {total}");
                        }
                    }
                }
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
