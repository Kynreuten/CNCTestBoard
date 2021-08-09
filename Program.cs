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
                FeedRateInitial = 15.0,
                FeedRateIncrement = 0.5,
                StepDown = 0.025, //0.6mm
                TestDistance = 3,
                MaterialThickness = 0.5,
                MaxIterations = 30,
                RapidRate = 30,
                SpindleSpeed = 20000,
                PauseTimeIncrement = 0,
                PauseTimeInitial = 0,
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
