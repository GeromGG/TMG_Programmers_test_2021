using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TMG_Programmers_test_2021__3
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }

        public Dictionary<string, string> Compare(string[] rus, string[] eng)
        {
            Dictionary<string, string> matchingLines = new Dictionary<string, string>();

            for (int i = 0; i < rus.Length; i++)
            {
                Regex rgx = new Regex(@"\p{IsBasicLatin}+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var stringRus = rgx.Replace(rus[i], String.Empty);
                

                for (int c = 0; c < eng.Length; c++)
                {
                    var mstr1 = eng[c].Split("|", StringSplitOptions.RemoveEmptyEntries);
                    Regex rgx1 = new Regex(@"\W+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    var stringEng = rgx1.Replace(mstr1[0], String.Empty);
                    var stringEngComment = rgx1.Replace(mstr1[1], String.Empty);

                    if (IndexPetrenko(stringRus) == IndexPetrenko(stringEng) + IndexPetrenko(stringEngComment))
                    {
                        matchingLines.Add(rus[i], eng[c]);
                    }
                }
            }

            return matchingLines;
        }

        private double IndexPetrenko(string str)
        {
            double index = 0;
            var series = 0.5;
            for (int b = 0; b < str.Length; b++)
            {
                index = index + series;
                series = series + 1;
            }
            return index = index * str.Length;
        }
    }
}
