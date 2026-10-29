using System.ComponentModel;

namespace SolutionFinderLibary
{
    public class SolutionFinder
    {
        public event Action solutionNotification;
        public event EventHandler? solutionFound;

        int _wordLength;
        int _numberOfWords;

        string[] _words;
        int[] _masks;
        public Dictionary<int, string> _bitMaskDictionary = new Dictionary<int, string>();
        public event EventHandler<string> FoundWords;
        public event EventHandler<int> ProgressBar;
        public event EventHandler<int> SolutionsFound;
        private int _maxValue;
        public int FirstWords { get => _maxValue; private set; }


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
            BitMaskFinder(0, _masks.Length - 1, solutions, new List<int>());
            OnSolutionsFound(solutions.Count());

            return solutions.ToArray();
        }

        private void BitMaskFinder(int usedMask, int startIndex, List<string> solutions, List<int> words) // Tilføj "foreach" loops til at øge hastigheden? ~3 minutter pr. words_alpha.txt
        {
            _maxValue = _masks.Length;
            if (words.Count() == _numberOfWords)
            {
                var solutionWords = words.Select(mask => _bitMaskDictionary[mask]);
                var solution = string.Join(" ", solutionWords);
                solutions.Add(solution);
                OnFoundSolution(solution);
                return;
            }

            for (var i = startIndex; i >= 0; i--)
            {
                if ((usedMask & _masks[i]) == 0)
                {
                    List<int> newWords = new List<int>(words);
                    newWords.Add(_masks[i]);
                    BitMaskFinder(usedMask | _masks[i], i - 1, solutions, newWords);
                }
                if (usedMask == 0)
                {
                    OnProgressBar(FirstWords - i);
                }
            }
        }

        protected virtual void OnSolutionsFound(int solutionsFound)
        {
            EventHandler<int> handler = SolutionsFound;
            if (handler != null)
                handler(this, solutionsFound);
        }

        protected virtual void OnFoundSolution(string eventSolution)
        {
            EventHandler<string> handler = FoundWords;
            if (handler != null)
                handler(this, eventSolution);
        }

        protected virtual void OnProgressBar(int progress)
        {
            EventHandler<int> handler = ProgressBar;
            if (handler != null)
                handler(this, progress);
        }
    }
}
