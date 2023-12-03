using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace D2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Users\\Charles\\Desktop\\Projects\\AdventOfCode2023\\D2\\input.txt";
            Dictionary<int, List<Dictionary<string, int>>> gamesData = new Dictionary<int, List<Dictionary<string, int>>>();

            const int maxNbOfRed = 12;
            const int maxNbOfGreen = 13;
            const int maxNbOfBlue = 14;

            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    // Lines examples : 
                    // Game 1: 10 green, 9 blue, 1 red; 1 red, 7 green; 11 green, 6 blue; 8 blue, 12 green
                    // Game 2: 11 red, 7 green, 3 blue; 1 blue, 8 green, 5 red; 2 red, 12 green, 1 blue; 10 green, 5 blue, 7 red
                    // Game 3: 2 red, 7 green, 1 blue; 1 blue, 8 red; 7 green, 19 red, 5 blue; 1 blue, 10 green, 18 red; 10 red, 6 blue, 4 green

                    GetDataStrutureFromInput(gamesData, sr);

                    //int totalEx1 = CheckIfGameIsPossible(gamesData, maxNbOfRed, maxNbOfGreen, maxNbOfBlue);
                    //Console.WriteLine("----- Total : " + totalEx1);

                    // Minimal number of cubes of each color for a game to be possible
                    // At the end, do the product the minimal number of cubes of each color for a game to be possible
                    int totalEx2 = 0;

    

                    foreach (KeyValuePair<int, List<Dictionary<string, int>>> game in gamesData)
                    {
                        int minNbOfRed = 0;
                        int minNbOfGreen = 0;
                        int minNbOfBlue = 0;

                        List<Dictionary<string, int>> rounds = game.Value;

                        // Iterate through each round
                        foreach (Dictionary<string, int> round in rounds)
                        {
                            // Iterate through each color of the round
                            foreach (KeyValuePair<string, int> color in round)
                            {
                                switch (color.Key)
                                {
                                    case "red":
                                        minNbOfRed = minNbOfRed > color.Value ? minNbOfRed : color.Value ;
                                        break;
                                    case "green":
                                        minNbOfGreen = minNbOfGreen > color.Value ? minNbOfGreen : color.Value;
                                        break;
                                    case "blue":
                                        minNbOfBlue = minNbOfBlue > color.Value ? minNbOfBlue : color.Value;
                                        break;
                                }
                            }
                        }

                        Console.WriteLine($"Min nb of red : {minNbOfRed} - Min nb of green : {minNbOfGreen} - Min nb of blue : {minNbOfBlue}");
                        int totalOfGame = minNbOfRed * minNbOfGreen * minNbOfBlue;
                        Console.WriteLine($"Total of game {game.Key} : {totalOfGame}");
                        totalEx2 += totalOfGame;
                        Console.WriteLine($"Total : {totalEx2}");
                        Console.WriteLine("--------------------");
                    }
                    Console.WriteLine("----- Total : " + totalEx2);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading text file");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static int CheckIfGameIsPossible(Dictionary<int, List<Dictionary<string, int>>> gamesData, int maxNbOfRed, int maxNbOfGreen, int maxNbOfBlue)
        {
            int total = 0;

            // Now we have the data, test if the games are possibles from the statement
            foreach (KeyValuePair<int, List<Dictionary<string, int>>> game in gamesData)
            {
                int gameId = game.Key;
                List<Dictionary<string, int>> rounds = game.Value;

                // Iterate through each round
                bool isPossible = true;
                foreach (Dictionary<string, int> round in rounds)
                {
                    int nbOfRed = 0;
                    int nbOfGreen = 0;
                    int nbOfBlue = 0;

                    // Iterate through each color of the round
                    foreach (KeyValuePair<string, int> color in round)
                    {
                        switch (color.Key)
                        {
                            case "red":
                                nbOfRed = color.Value;
                                break;
                            case "green":
                                nbOfGreen = color.Value;
                                break;
                            case "blue":
                                nbOfBlue = color.Value;
                                break;
                        }
                    }

                    if (nbOfRed > maxNbOfRed || nbOfGreen > maxNbOfGreen || nbOfBlue > maxNbOfBlue)
                    {
                        isPossible = false;
                        break;
                    }
                }

                if (isPossible)
                {
                    total += gameId;
                    Console.WriteLine("Game " + gameId + " is possible");
                }
                else
                {
                    Console.WriteLine("Game " + gameId + " is not possible");
                }
            }

            return total;
        }

        private static void GetDataStrutureFromInput(Dictionary<int, List<Dictionary<string, int>>> gamesData, StreamReader sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                int gameId = int.Parse(line.Substring(5, line.IndexOf(':') - 5));
                string roundsData = line.Substring(line.IndexOf(':') + 2);
                string[] rounds = roundsData.Split(';');

                List<Dictionary<string, int>> roundsList = new List<Dictionary<string, int>>();

                foreach (string round in rounds)
                {
                    Dictionary<string, int> roundData = new Dictionary<string, int>();
                    string[] parts = round.Trim().Split(',');

                    foreach (string part in parts)
                    {
                        string[] elements = part.Trim().Split(' ');
                        int number = int.Parse(elements[0]);
                        string color = elements[1];

                        roundData[color] = number;
                    }

                    roundsList.Add(roundData);
                }

                gamesData[gameId] = roundsList;
            }
        }
    }
}
