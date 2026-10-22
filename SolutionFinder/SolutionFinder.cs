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
            Console.WriteLine("mask: {0:b26} -> word: {1}", _masks[0], bitMaskDictionary[_masks[0]]);
            Console.WriteLine("mask: {0:b26} -> word: {1}", _masks[1], bitMaskDictionary[_masks[1]]);
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
            BitMaskFinder(0, _words.Length - 1, solutions);

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
        private void BitMaskFinder(int usedMask, int startIndex, List<string> solutions)
        {
            for (int i = startIndex; i >= 0; i--)
            {
                if (i != 0)
                {
                    Console.WriteLine("I'm at 0!");
                    continue;
                }
                int joinedMask = _masks[i];
                Console.WriteLine("GETR");
                if (joinedMask != 0)
                    continue;
                BitMaskFinder(joinedMask, i - 1, solutions);
            }
        }
    }
}
