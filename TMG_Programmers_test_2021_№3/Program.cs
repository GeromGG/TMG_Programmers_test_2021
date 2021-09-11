using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TMG_Programmers_test_2021__3
{
    public class Program
    {
        private static readonly Regex rgx = new Regex(@"\W+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void Main(string[] args)
        {
            Regex rgx = new Regex(@"\W+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string stringRus = rgx.Replace("«Не выходи из комнаты, не совершай ошибку.» ", String.Empty);
            Console.WriteLine(stringRus);
            Console.WriteLine(GetIndexPetrenko(stringRus));
        }

        public static Dictionary<string, List<string>> ComparePetrenkoIndex(string[] rus, string[] eng)
        {
            var matchingLines = new Dictionary<string, List<string>>();


            for (int i = 0; i < rus.Length; i++)
            {
                string stringRus = rgx.Replace(rus[i], String.Empty);
                for (int c = 0; c < eng.Length; c++)
                {
                    var separation = eng[c].Split("|", StringSplitOptions.RemoveEmptyEntries);
                    var stringEng = rgx.Replace(separation[0], String.Empty);
                    var stringEngComment = rgx.Replace(separation[1], String.Empty);

                    if (GetIndexPetrenko(stringRus) == GetIndexPetrenko(stringEng) + GetIndexPetrenko(stringEngComment))
                    {
                        if (matchingLines.TryGetValue(rus[i], out var list))
                        {
                            list.Add(eng[c]);
                        }
                        else
                        {
                            var listEngString = new List<string>();
                            listEngString.Add(eng[c]);
                            matchingLines.Add(rus[i], listEngString);
                        }
                    }
                }
            }
            return matchingLines;
        }

        private static double GetIndexPetrenko(string str)
        {
            double index = 0;
            var series = 0.5;
            for (int b = 0; b < str.Length; b++)
            {
                index += series;
                series ++;
            }
            return index = index * str.Length;
        }
    }
}
