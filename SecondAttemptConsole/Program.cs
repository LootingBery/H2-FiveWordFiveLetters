using System.Diagnostics;
using SolutionFinderLibary;

namespace SecondAttemptConsole
{
    internal class Program
    {

        private static int _wordsLoaded = 0;
        private static int _progress = 0;

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            var solutionFinder = new SolutionFinder(5, 5);
            solutionFinder.FoundWords += FoundSolutionWriter;
            solutionFinder.ProgressBar += ProgressBar;
            stopWatch.Start();
            _wordsLoaded = solutionFinder.LoadWords("C:\\Users\\temp\\source\\repos\\LootingBery\\H2-FiveWordFiveLetters\\5W5L\\new_beta.txt"); // Erstat med rigtig stig 
            Console.WriteLine("{0} words found", _wordsLoaded);
            string[] solutions = solutionFinder.FindSolutions();
            stopWatch.Stop();

            Console.WriteLine("\nFound {0} solutions", solutions.Length);
            Console.WriteLine("It took: " + stopWatch + " seconds.");
        }

        private static void ProgressBar(object? sender, int e)
        {
            _progress = e;
            Console.Write("\r[{0:d3} of {1}]...", e, _wordsLoaded);
        }

        private static void FoundSolutionWriter(object? sender, string e)
        {
            Console.WriteLine("\r[{0:d3} of {1}] : {2}", _progress, _wordsLoaded, e);
        }
    }
}
