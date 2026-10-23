namespace SolutionFinderLibary
{
    public class SolutionFinder
    {
        int _wordLength;
        int _numberOfWords;

        string[] _words;
        int[] _masks;
        private Dictionary<int, string> bitMaskDictionary = new Dictionary<int, string>();

        public SolutionFinder(int wordLength, int numberOfWords)
        {
            if (wordLength * numberOfWords > 26)
                throw new ArgumentOutOfRangeException("Kræver mere end 26 bogstaver");
            _numberOfWords = numberOfWords;
            _wordLength = wordLength;
        }

        public int LoadWords(string file)
        {
            _words = File.ReadAllLines(file)
                .Where(word => word.Length == _wordLength && word.Distinct().Count() == _wordLength).ToArray();

            foreach (var word in _words)
            {
                bitMaskDictionary.TryAdd(WordToMask(word), word);
            }
            Console.WriteLine("_masks: {0}", bitMaskDictionary.Count());
            _masks = bitMaskDictionary.Keys.Order().ToArray();
            Console.WriteLine("mask: {0:b26} -> word: {1}", _masks[12], bitMaskDictionary[_masks[12]]);
            Console.WriteLine("mask: {0:b26} -> word: {1}", _masks[13], bitMaskDictionary[_masks[13]]);
            return _words.Length;
        }

        private int WordToMask(string word)
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
            int count = 0;
            int processingCount = 0;
            List<string> solutions = new List<string>();
            //RecursiveFind("", _words.Length - 1, solutions);
            BitMaskFinder(0, _words.Length - 1, solutions, new int[0]);

            return solutions.ToArray();
        }

        /*
        private void RecursiveFind(string usedWords, int startIndex, List<string> solutions)
        {
            if (usedWords.Length == _numberOfWords * _wordLength)
            {
                for (var a = 1; a < _numberOfWords; a++)
                {
                    usedWords = usedWords.Insert(a * _wordLength + (a - 1), " ");
                }
                solutions.Add(usedWords);
                return;
            }

            for (var i = startIndex; i >= 0; i--)
            {
                string joinedWords = string.Join("", usedWords, _words[i]);
                if (joinedWords.Distinct().Count() != joinedWords.Length)
                    continue;
                RecursiveFind(joinedWords, i - 1, solutions);
            }
        }
        */

        // Cook some shit here, plz. future ben, we're all counting on you.
        // future ben here aka present ben, done did the cook. stole willy code, which is mads code. soooo ty ty mads
        private void BitMaskFinder(int usedMask, int startIndex, List<string> solutions, int[] words)
        {
            if (words.Count() == _numberOfWords)
            {
                //TODO: add solutions

                Console.WriteLine("{0:b26}", usedMask);
                foreach (var mask in words)
                {
                    Console.WriteLine(bitMaskDictionary[mask]);
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
