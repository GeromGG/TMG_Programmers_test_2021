using System;
using System.Text.RegularExpressions;

namespace TMG_Programmers_test_2021.Model
{
    public class SummaryTable
    {
        public SummaryTable()
        {

        }

        public SummaryTable(string id, string text)
        {
            Id = id;
            Text = text;

            char[] separators = new char[] { ' ', '.', ',', '—' };

            string[] words = Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            WordCount = words.Length;

            string pattern = @"[ӧауоыиэяюёеaeiіouäöüÄÖÜAEIІOUАЕЁИОУЫЭЮЯӦ]+";
            Regex rgx = new Regex(pattern);
            NumberOfVowels = rgx.Matches(text).Count;
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public int WordCount { get; set; }
        public int NumberOfVowels { get; set; }
    }
}