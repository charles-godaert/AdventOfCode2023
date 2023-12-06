using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Users\\Charles\\Desktop\\Projects\\AdventOfCode2023\\D5\\input.txt";

            List<string> lines = new List<string>();
            List<long> seeds = new List<long>();

            List<(long, long, long)> seedToSoil = new List<(long, long, long)>();
            List<(long, long, long)> soilToFertilizer = new List<(long, long, long)>();
            List<(long, long, long)> fertilizerToWater = new List<(long, long, long)>();
            List<(long, long, long)> waterToLight = new List<(long, long, long)>();
            List<(long, long, long)> lightToTemperature = new List<(long, long, long)>();
            List<(long, long, long)> temperatureToHumidity = new List<(long, long, long)>();
            List<(long, long, long)> humidityToLocation = new List<(long, long, long)>();


            long total = 0;
            try
            {
                lines = GetLines(inputPath);

                foreach (string line in lines)
                {
                    /* The following comment correspond to an extract of the the lines, parse it in correspondind variables :
                     seeds: 104847962 3583832 1212568077 114894281 3890048781 333451605 1520059863 217361990 310308287 12785610 3492562455 292968049 1901414562 516150861 2474299950 152867148 3394639029 59690410 862612782 176128197

                    seed-to-soil map:
                    2023441036 2044296880 396074363
                    2419515399 3839972576 454994720

                    1233431450 958743033 18292543

                    soil-to-fertilizer map:
                    1479837493 1486696129 480988794
                    3637384566 3730606485 267472485


                    fertilizer-to-water map:
                    4274676882 2765984054 20290414
                    3642266392 2324011621 382224743
                    3159410287 4157769177 137198119
                    3437898965 2786274468 204367427

                    water-to-light map:
                    139728365 0 27290780
                    4161521920 2345099742 65970280
                    3549264451 2411070022 15588060
                    846553766 4012820620 62155872

                    light-to-temperature map:
                    3741602262 2758947303 142653736
                    628739598 2901601039 50811783
                    1842260329 1084521599 145122645

                    904704230 3170653120 79578289

                    temperature-to-humidity map:
                    671484955 1144907174 532089323
                    1414132335 1960778188 125717021
                    2631474761 2586973888 1058655511
                    1991131055 744338927 221864400


                    humidity-to-location map:
                    547577859 2546258172 54451455
                    2564186976 3913248498 28610653
                    2460249359 129990669 103937617
                    257798579 3257354132 21143365
                    511274864 3365252234 24536388
                    412475023 3389788622 98799841
                    2843712442 3615348771 251219053
                    0 2984989380 24111266
                                        */
                    if (line.Contains("seeds:"))
                    {
                        string[] split = line.Split(' ');
                        foreach (string s in split)
                        {
                            if (s != "seeds:")
                            {
                                seeds.Add(long.Parse(s));
                            }
                        }
                    }

                    ParseAndMapValues(lines, seedToSoil, "seed-to-soil map:");
                    ParseAndMapValues(lines, soilToFertilizer, "soil-to-fertilizer map:");
                    ParseAndMapValues(lines, fertilizerToWater, "fertilizer-to-water map:");
                    ParseAndMapValues(lines, waterToLight, "water-to-light map:");
                    ParseAndMapValues(lines, lightToTemperature, "light-to-temperature map:");
                    ParseAndMapValues(lines, temperatureToHumidity, "temperature-to-humidity map:");
                    ParseAndMapValues(lines, humidityToLocation, "humidity-to-location map:");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Total: " + total);
            Console.ReadLine();
        }

        private static void ParseAndMapValues(List<string> lines, List<(long, long, long)> list, string mapKey)
        {
            bool startMapping = false;

            foreach (string line in lines)
            {
                if (line.Contains(mapKey))
                {
                    startMapping = true;
                    continue;
                }

                if (startMapping)
                {
                    if (string.IsNullOrWhiteSpace(line)) break;

                    string[] split = line.Split(' ');
                    if (split.Length == 3)
                    {
                        list.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
                    }
                }
            }
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
