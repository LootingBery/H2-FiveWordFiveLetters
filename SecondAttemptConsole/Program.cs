using System.Diagnostics;
using System.Threading;
using SolutionFinderLibary;

namespace SecondAttemptConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            var solutionFinder = new SolutionFinder(5, 5);
            Thread t = new Thread(Threads);

            stopWatch.Start();
            int wordsLoaded = solutionFinder.LoadWords("C:\\Users\\temp\\source\\repos\\LootingBery\\H2-FiveWordFiveLetters\\5W5L\\words_alpha.txt"); // Erstat med rigtig stig 
            Console.WriteLine("{0} words found", wordsLoaded);
            string[] solutions = solutionFinder.FindSolutions();
            stopWatch.Stop();
            foreach (var word in solutions)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("\nFound {0} solutions", solutions.Length);
            Console.WriteLine("It took: " + stopWatch + " seconds.");
        }

        static void Threads()
        {

        }
    }
}
