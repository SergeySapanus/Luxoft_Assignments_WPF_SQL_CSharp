using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _1_CSharp_Assignment
{
    class Program
    {
        private const int COUNT_UNIQUE_WORDS_THRESHOLD = 10000000;
        private const int LENGTH_WORD_THRESHOLD = 50;

        private const string DATA_FILE_PATH = "data.txt";
        private const string RESULTS_FILE_PATH = "results.txt";

        static void Main(string[] args)
        {
            try
            {
                var encoding = GetEncoding(DATA_FILE_PATH);
                var sortedByWord = GetSortedDataFromFile(DATA_FILE_PATH, encoding, COUNT_UNIQUE_WORDS_THRESHOLD, LENGTH_WORD_THRESHOLD);
#if DEBUG
                for (var i = 0; i < sortedByWord.Count; i++)
                {
                    Console.WriteLine($"{sortedByWord.Keys[i]}:{sortedByWord.Values[i]}");
                }

                Console.WriteLine(new string('-', 10));
#endif
                var sortedByNumber = QuickSortByNumber(sortedByWord);
#if DEBUG
                foreach (var pair in sortedByNumber)
                {
                    Console.WriteLine($"{pair.Key}:{pair.Value}");
                }
#endif
                using var file = new StreamWriter(RESULTS_FILE_PATH, false, encoding);
                foreach (var pair in sortedByNumber)
                    file.WriteLine($"{pair.Key} {pair.Value}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static SortedList<string, int> GetSortedDataFromFile(string filePath, Encoding encoding,
            int lengthWordThreshold, int countUniqueWordsThreshold)
        {
            var stringBuilder = new StringBuilder(lengthWordThreshold);
            var sortedList = new SortedList<string, int>(countUniqueWordsThreshold);

            using (var streamReader = new StreamReader(filePath, encoding))
            {
                int intSymbol;

                while ((intSymbol = streamReader.Read()) >= 0 || stringBuilder.Length > 0)
                {
                    var charSymbol = (char)intSymbol;

                    if (intSymbol == -1 || char.IsSeparator(charSymbol) || char.IsControl(charSymbol))
                    {
                        if (stringBuilder.Length == 0)
                            continue;

                        var key = stringBuilder.ToString();

                        if (sortedList.TryGetValue(key, out var value))
                        {
                            sortedList[key] = ++value;
                        }
                        else
                        {
                            sortedList.Add(key, 1);
                        }

                        stringBuilder.Clear();
                    }
                    else
                    {
                        if (char.IsUpper(charSymbol))
                            charSymbol = char.ToLower(charSymbol);

                        stringBuilder.Append(charSymbol);
                    }
                }
            }

            return sortedList;
        }

        private static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76)
                return Encoding.UTF7;

            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
                return Encoding.UTF8;

            if (bom[0] == 0xff && bom[1] == 0xfe)
                return Encoding.Unicode;

            if (bom[0] == 0xfe && bom[1] == 0xff)
                return Encoding.BigEndianUnicode;

            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
                return Encoding.UTF32;

            return Encoding.ASCII;
        }

        private static IEnumerable<KeyValuePair<string, int>> QuickSortByNumber(IEnumerable<KeyValuePair<string, int>> sequence)
        {
            var words = sequence /*as KeyValuePair<string, int>[] ?? sequence.ToArray()*/;
            if (words.Count() <= 1)
                return words;

            var pivot = words.First();
            var less = words.Skip(1).Where(x => x.Value <= pivot.Value);
            var greater = words.Skip(1).Where(x => x.Value > pivot.Value);

            return QuickSortByNumber(greater).Union(new[] { pivot }).Union(QuickSortByNumber(less));
        }
    }
}
