using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MathHelpers
{
    public class CSharpAssignmentHelper
    {
        public static SortedList<string, int> GetSortedDataFromFile(string filePath, Encoding encoding,
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

        public static IEnumerable<KeyValuePair<string, int>> QuickSortByNumber(IEnumerable<KeyValuePair<string, int>> sequence)
        {
            var words = sequence as KeyValuePair<string, int>[] ?? sequence.ToArray();
            if (words.Count() <= 1)
                return words;

            var pivot = words.First();
            var less = words.Skip(1).Where(x => x.Value <= pivot.Value);
            var greater = words.Skip(1).Where(x => x.Value > pivot.Value);

            return QuickSortByNumber(greater).Union(new[] { pivot }).Union(QuickSortByNumber(less));
        }
    }
}
