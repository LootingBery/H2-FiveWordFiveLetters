namespace SolutionFinderLibary
{
    public class SolutionFinder
    {
        int _wordLength;
        int _numberOfWords;

        string[] _words;
        int[] _masks;
        public Dictionary<int, string> _bitMaskDictionary = new Dictionary<int, string>();

       
        public SolutionFinder(int wordLength, int numberOfWords) // Puller fra "Program.cs" hvor wordLength og numberOfWords' værdiere bliver sat.
        {
            if (wordLength * numberOfWords > 26)
                throw new ArgumentOutOfRangeException("Kræver mere end 26 bogstaver");
            _numberOfWords = numberOfWords;
            _wordLength = wordLength;
        }

        public int LoadWords(string file) // Puller fra "Program.cs" og putter hvert gyldigt ord ind i ordbogen.
        {
            _words = File.ReadAllLines(file)
                .Where(word => word.Length == _wordLength && word.Distinct().Count() == _wordLength).ToArray();

            foreach (var word in _words)
            {
                _bitMaskDictionary.TryAdd(WordToMask(word), word);
            }
            Console.WriteLine("{0} masks found", _bitMaskDictionary.Count());
            _masks = _bitMaskDictionary.Keys.Order().ToArray();
            //Console.WriteLine("mask: {0:b26} -> word: {1}", _masks[12], _bitMaskDictionary[_masks[12]]);
            //Console.WriteLine("mask: {0:b26} -> word: {1}", _masks[13], _bitMaskDictionary[_masks[13]]);
            return _words.Length;
        }

        private int WordToMask(string word) // Forvandler hvert ord om til binær for at optimere programmet. 
        {
            int mask = 0;
            foreach (char ch in word)
            {
                int bit = 1 << (ch - 'a');
                mask |= bit;
            }
            return mask;
        }

        public string[] FindSolutions()
        {
            List<string> solutions = new List<string>();
            BitMaskFinder(0, _masks.Length - 1, solutions, new int[0]);

            return solutions.ToArray();
        }

        private void BitMaskFinder(int usedMask, int startIndex, List<string> solutions, int[] words) // Tilføj "foreach" loops til at øge hastigheden? ~3 minutter pr. words_alpha.txt
        {
            if (words.Count() == _numberOfWords)
            {
                Console.WriteLine("Solution no.: {0:d3} | Bitmask: {1:b26}", solutions.Count(), usedMask);
                foreach (var mask in words)
                {
                    var solutionWords = words.Select(mask => _bitMaskDictionary[mask]);
                    solutions.Add(string.Join(" ", solutionWords));
                    return;
                }
                return;
            }

            for (var i = startIndex; i >= 0; i--)
            {
                if ((usedMask & _masks[i]) == 0)
                {
                    var newWords = words.ToList();
                    newWords.Add(_masks[i]);
                    BitMaskFinder(usedMask | _masks[i], i - 1, solutions, newWords.ToArray());
                }
            }
        }
    }
}
