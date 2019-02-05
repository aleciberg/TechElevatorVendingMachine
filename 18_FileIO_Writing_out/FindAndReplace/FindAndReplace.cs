using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindAndReplace
{
    public static class FindAndReplace
    {
        public static void FindAndReplaceWord()
        {
            Console.WriteLine("Enter Search Word: ");
            string searchWord = Console.ReadLine();

            Console.WriteLine("Enter Replace Word: ");
            string replaceWord = Console.ReadLine();

            Console.WriteLine("Enter a Source File Path ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Enter a Destination File Path");
            string destinationFilePath = Console.ReadLine();

            bool fileExists = File.Exists(filePath);
            bool destinationFilePathExists = File.Exists(destinationFilePath);
            int wordCounter = 0;

            while (!fileExists)
            {
                Console.WriteLine("Source file path does not exist. Please try with a valid entry.");
                filePath = Console.ReadLine();
                fileExists = File.Exists(filePath);
            }

            if (!destinationFilePathExists)
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    using (StreamWriter sw = new StreamWriter(destinationFilePath, true))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line.Contains(searchWord))
                            {
                                wordCounter++;
                            }
                            sw.WriteLine(line);
                            line = line.Replace(searchWord, replaceWord);
                        }
                        Console.WriteLine("Number of occurences of the search phrase that was found and replaced: " + wordCounter);
                    }
                }
            }
            else
            {
                Console.WriteLine("Destination file already exists. Press any key to exit.");
                Console.ReadLine();
            }
        }
    }
}
