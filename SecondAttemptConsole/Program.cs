using SolutionFinderLibary;

namespace SecondAttemptConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var solutionFinder = new SolutionFinder(5, 5);

            int wordsLoaded = solutionFinder.LoadWords("C:\\Users\\temp\\source\\repos\\LootingBery\\H2-FiveWordFiveLetters\\5W5L\\new_beta.txt");
            Console.WriteLine("{0} words found", wordsLoaded);
            string[] solutions = solutionFinder.FindSolutions();
            Console.WriteLine("Found {0} solutions", solutions.Length);

            foreach (var word in solutions)
            {
                Console.WriteLine(word);
            }
        }
    }
}
