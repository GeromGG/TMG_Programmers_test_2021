using System;
using System.Text.RegularExpressions;

namespace TMG_Programmers_test_2021.Model
{
    public class StringResult
    {

        private static readonly char[] _separators = new char[] { ' ', '.', ',', '—' };
        //patch with Russian and European vowels / патч с русскими и европейскими гласными
        private static readonly string _pattern = @"[ӧауоыиэяюёеaeiіouäöüÄÖÜAEIІOUАЕЁИОУЫЭЮЯӦ]+";
        private static readonly Regex rgx = new Regex(_pattern);

        public StringResult(string id, string text)
        {
            if (id is not null && text is not null)
            {
                Id = id;
                Text = text;
                string[] words = Text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
                WordCount = words.Length;
                NumberOfVowels = rgx.Matches(text).Count;
            }
        }

        public string Id { get; }
        public string Text { get; }
        public int WordCount { get; }
        public int NumberOfVowels { get; }
    }
}