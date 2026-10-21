namespace SolutionFinderLibary
{
    public class SolutionFinder
    {
        int _wordLength;
        int _numberOfWords;

        string[] _words;

        public SolutionFinder(int wordLength, int numberOfWords)
        {
            if (wordLength * numberOfWords > 26)
                throw new ArgumentOutOfRangeException("Kræver mere end 26 bogstaver");
            _numberOfWords = numberOfWords;
            _wordLength = wordLength;
        }

        public int LoadWords(string file)
        {
            _words = File.ReadAllLines(file);
            return _words.Length;
        }

        
        public string[] FindSolutions()
        {
            int count = 0;
            int processingCount = 0;
            List<string> solutions = new List<string>();
            for (var a = _words.Length - 1 ; a >= 0; a--)
            {
                for (var b = a - 1; b >= 0; b--)
                {
                    for (var c = b - 1; c >= 0; c--)
                    {
                        for (var d = c - 1; d >= 0; d--)
                        {
                            for (var e = d - 1; e >= 0; e--)
                            {
                                var abcde = _words[a] + _words[b] + _words[c] + _words[d] + _words[e];
                                if (abcde.Distinct().Count() != 5 * _wordLength)
                                    continue;

                                count++;
                                processingCount++;
                                solutions.Add(abcde);
                                if (processingCount == 5)
                                {
                                    Console.WriteLine("Processing...");
                                    processingCount = 0;
                                }

                            }
                        }
                    }
                }
            }        
            

            return solutions.ToArray();

        }
    }
}
