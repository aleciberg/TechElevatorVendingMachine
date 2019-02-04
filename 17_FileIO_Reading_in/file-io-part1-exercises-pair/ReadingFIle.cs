using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace file_io_part1_exercises_pair
{
    public static class ReadingFIle
    {
        public static void ReadFile()
        {
            //string directory = "C:\Users\aleci\team0-c-sharp-week4-pair-exercises\17_FileIO_Reading_in\file-io-part1-exercises-pair\bin\Debug\netcoreapp2.1\";
            // string file = "alices_adventures_in_wonderland.txt";
            Console.WriteLine("Enter File Path: ");
            string directory = Console.ReadLine();
            //string fullPath = Path.Combine(directory, file);

            List<string> allWords = new List<string>();
            List<string> allSentences = new List<string>();
            int counter = 0;
            try
            {
                using (StreamReader streamReader = new StreamReader(directory))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        string[] inputArray = line.Split(' ');

                        allWords.AddRange(inputArray);
                    }

                }

                using (StreamReader streamReader = new StreamReader(directory))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();
                        if (line.Contains('.') || line.Contains('?') || line.Contains('!'))
                        {
                            counter++;
                        }

                    }
                    // ". ", "! ", "? ", "?\" ", "!\" ", ".) ", ".\" "
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading the file");
                Console.WriteLine(e.Message);
            }

            int wordCount = allWords.Count();
            Console.WriteLine("The word count is " + wordCount);
            int sentenceCount = allSentences.Count();
            Console.WriteLine("The sentence count is " + counter);
        }
    }
}
