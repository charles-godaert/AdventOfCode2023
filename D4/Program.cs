using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Users\\Charles\\Desktop\\Projects\\AdventOfCode2023\\D4\\input.txt";

            List<string> lines = new List<string>();

            Dictionary<int, int> numberOfIterationsOfThisCard = new Dictionary<int, int>();

            int total = 0;
            try
            {
                lines = GetLines(inputPath);

                foreach(string line in lines)
                {

                    int cardNumber = int.Parse(line.Substring(line.IndexOf(' ') + 1, line.IndexOf(' ') - 1));

                    int iterationForThisCard = (numberOfIterationsOfThisCard.ContainsKey(cardNumber) ? (numberOfIterationsOfThisCard[cardNumber] + 1) : 1);

                    for(int i = 0; i < iterationForThisCard; i++)
                    {
                        string lineWithoutCardNumber = line.Substring(line.IndexOf(':') + 1);
                        string lineWithoutSecondPart = lineWithoutCardNumber.Substring(0, lineWithoutCardNumber.IndexOf('|'));
                        string lineWithoutFirstPart = lineWithoutCardNumber.Substring(lineWithoutCardNumber.IndexOf('|') + 1);

                        List<int> firstPartNumbers = lineWithoutSecondPart.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).Select(int.Parse).ToList();
                        List<int> secondPartNumbers = lineWithoutFirstPart.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).Select(int.Parse).ToList();

                        // Check the number of number that is in the first part that is in the second part
                        int numberOfNumberInFirstPartThatIsInSecondPart = firstPartNumbers.Intersect(secondPartNumbers).Count();
                        int numberToAdd = ((int)Math.Pow(2, numberOfNumberInFirstPartThatIsInSecondPart)) / 2;

                        for (int j = 1; j < numberOfNumberInFirstPartThatIsInSecondPart + 1; j++)
                        {
                            numberOfIterationsOfThisCard[cardNumber + j] = (numberOfIterationsOfThisCard.ContainsKey(cardNumber + j) ? numberOfIterationsOfThisCard[cardNumber + j] + 1 : 1);
                        }

                        //total += numberToAdd;
                        total += 1;
                        Console.WriteLine($"Card {cardNumber}, total after adding {total}");
                    }       
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading text file");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Total: " + total);
            Console.ReadLine();
        }

        private static List<string> GetLines(string inputPath)
        {
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(inputPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
