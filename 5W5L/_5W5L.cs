using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System;
using System.Formats.Asn1;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static _5W5L.Report;

namespace _5W5L
{

    public class Report(ulong How_long, IProgress<how_long> progress)
    {
        public struct how_long
        {
            public ulong How_far = 0;
            public ulong total;

            public how_long(ulong How_far, ulong total)
            {
                this.How_far = How_far;
                this.total = total;
            }
        }
        static object LockProgress = new object();

        ulong How_far = 0;
        bool Rerun = true;

        public void report(ulong count)
        {
            lock (LockProgress)
            {
                How_far += count;
            }
        }
        public async void update()
        {
            Thread.Sleep(50);

            how_long how_longer = new how_long(How_far, How_long);
            progress.Report(how_longer);

            if (Rerun)
            {
                update();
            }
        }

        public void Dispose()
        {
            Rerun = false;
        }
    }
    public class Main
    {
        static List<uint[]> wait_word_arr = new List<uint[]>(4096);
        static List<uint[]> bit_mask_check = new List<uint[]>();
        static object locker_list = new object();
        static Dictionary<uint, List<string>> Bit_Mask = new Dictionary<uint, List<string>>();
        static List<string> ValidWords = new List<string>();

        public static void bits(string FileName, List<string> Text, IProgress<Report.how_long> progress)
        {
            // Threads
            int t = 14;

            string[] FileWord = File.ReadAllLines(FileName);

            foreach (string Word in FileWord)
            {
                if (Word.Length == 5)
                {
                    bool IsValid = true;
                    for (int q = 0; q < Word.Length - 1; q++)
                    {
                        if (Word[q] == Word[q + 1])
                        {
                            IsValid = false;
                            break;
                        }
                    }
                    if (IsValid)
                    {
                        uint WaitMask = 0;

                        // Køre igennem for hver character
                        foreach (char ch in Word.ToCharArray())
                        {
                            int bit = ch - 'a';

                            WaitMask |= 1u << bit;
                        }

                        if (Bit_Mask.TryGetValue(WaitMask, out List<string> WaitGoodWords))
                        {
                            WaitGoodWords.Add(Word);
                        }
                        else
                        {
                            List<string> WaitWords = new List<string>();
                            WaitWords.Add(Word);
                            Bit_Mask.Add(WaitMask, WaitWords);
                        }
                    }
                }
            }

            Report report = new Report((ulong)Math.Pow(Bit_Mask.Count, 5), progress);

            Task.Run(() =>
            {
                report.update();
            });


            Parallel.For(1, t + 1, (id) => WordsCounter(id, t, Bit_Mask));
            List<uint> All_Bit_masks = new List<uint>();
            int count = 0;
            int wordCount = 0;
            foreach (var BitArray in wait_word_arr)
            {
                for (int w = 0; w < bit_mask_check.Count; w++)
                {
                    wordCount = 0;
                    foreach (var ExWordCheck in bit_mask_check[w])
                    {
                        foreach (var wordCheck in BitArray)
                        {
                            if (ExWordCheck == wordCheck)
                            {
                                wordCount++;
                                continue;
                            }
                        }
                    }
                    if (wordCount == 5)
                    {
                        break;
                    }
                }
                if (wordCount == 5)
                {
                    wordCount = 0;
                    continue;
                }
                bit_mask_check.Add(BitArray);

                foreach (var Word0 in Bit_Mask.GetValueOrDefault(BitArray[0]))
                {
                    foreach (var Word1 in Bit_Mask.GetValueOrDefault(BitArray[1]))
                    {
                        foreach (var Word2 in Bit_Mask.GetValueOrDefault(BitArray[2]))
                        {
                            foreach (var Word3 in Bit_Mask.GetValueOrDefault(BitArray[3]))
                            {
                                foreach (var Word4 in Bit_Mask.GetValueOrDefault(BitArray[4]))
                                {
                                    All_Bit_masks.Add(BitArray[0]);
                                    string FinalWord = Word0 + " " + Word1 + " " + Word3 + " " + Word4;
                                    Text.Add(FinalWord);
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void WordsCounter(int id, int t, Dictionary<uint, List<string>> Bit_Mask)
        {
            List<uint> Keys = new List<uint>(Bit_Mask.Keys);
            List<uint[]> WaitWord = new List<uint[]>(4096);

            for (int x = id * (Keys.Count() / t) - (Keys.Count() / t); x < (Keys.Count() / t) + (id * (Keys.Count() / t) - (Keys.Count() / t)); x++)
            {
                for (int y = 0; y < Keys.Count(); y++)
                {
                    if ((Keys[x] & Keys[y]) != 0)
                    {
                        continue;
                    }
                    uint Key1 = (Keys[x] | Keys[y]);

                    for (int z = 0; z < Keys.Count(); z++)
                    {
                        if ((Key1 & Keys[z]) != 0)
                        {
                            continue;
                        }
                        uint Key2 = (Key1 | Keys[z]);
                        for (int i = 0; i < Keys.Count(); i++)
                        {
                            if ((Key2 & Keys[i]) != 0)
                            {
                                continue;
                            }
                            uint Key3 = (Key2 | Keys[i]);
                            for (int j = 0; j < Keys.Count(); j++)
                            {
                                if ((Key3 & Keys[j]) != 0)
                                {
                                    continue;
                                }
                                uint Key4 = (Key3 | Keys[j]);

                                uint[] WaitWords = { Keys[x], Keys[y], Keys[z], Keys[i], Keys[j] };

                                WaitWord.Add(WaitWords);

                                if (WaitWord.Count() == 4096)
                                {
                                    lock (locker_list)
                                    {
                                        wait_word_arr.AddRange(WaitWord);
                                    }
                                    WaitWord.Clear();
                                }
                            }
                        }
                    }
                }
            }

            lock (locker_list)
            {
                wait_word_arr.AddRange(WaitWord);
            }
            WaitWord.Clear();
        }
    }

}
