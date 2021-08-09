using System;
using System.IO;

using GoldenLlama.Cnc.Test;

namespace CNCTestBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating G-Code test!");

            var tester = new BackAndForthRateTester
            {
                FeedRateInitial = 4.87,
                // FeedRateIncrement = ?,
                TestDistance = 2.5,
            };

            string operations = tester.GenerateTest();

            string targetFilePath = Path.Combine(Environment.CurrentDirectory,
                                                    "Generated Tests",
                                                    "BackAndForthRateTester.nc");
            var fileInfo = new FileInfo(targetFilePath);      
            if (!fileInfo.Directory.Exists) {
                fileInfo.Directory.Create();
            }
            using (var output = new StreamWriter(fileInfo.OpenWrite()))
            {
                output.WriteLine(operations);
            }
            Console.WriteLine(operations);
        }
    }
}
