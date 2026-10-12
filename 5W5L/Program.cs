using System;
using System.IO;
using System.Reflection.Metadata;

public class Program
{
    static void Main()
    {

        string[] alp = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };


        int word_length = 5;

        int word_length2 = word_length * 2;
        int word_length4 = word_length2 * 2;
        int word_length5 = word_length4 * 2;

        int stepgap = 1000;

        int number_of_sets = 5;

        string wordlist = WordMachine();

        Console.WriteLine("There's" + wordlist + " words in total");
        Console.ReadLine();
    }

    static string WordMachine()
    {
        string words;
        string allWords = "";

        try
        {
            StreamReader sr = new StreamReader("C:\\Users\\temp\\source\\repos\\LootingBery\\H2-FiveWordFiveLetters\\5W5L\\new_beta.txt");
            words = sr.ReadLine();
            while (words != null)
            {
                Console.WriteLine(words);
                allWords += words + Environment.NewLine;
                words = sr.ReadLine();
            }
            sr.Close();
            Console.ReadLine();

        }
        catch (Exception e)
        {
            Console.WriteLine("Big ass error: " + e);
        }

        return allWords;
    }

}
