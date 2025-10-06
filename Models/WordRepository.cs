using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanMVC.Model
{
    public static class WordRepository
    {
        private static string FilePath = "Models/words.csv";

        public static List<string> LoadWords()
        {
            if (!File.Exists(FilePath))
                File.WriteAllText(FilePath, "IMPACT,DEVELOPER,HANGMAN"); // default words

            return File.ReadAllLines(FilePath)
                       .SelectMany(line => line.Split(','))
                       .Where(word => !string.IsNullOrWhiteSpace(word))
                       .Select(word => word.Trim().ToUpper())
                       .ToList();
        }

        public static void AddWord(string newWord)
        {
            var words = LoadWords();
            if (!words.Contains(newWord.ToUpper()))
            {
                File.AppendAllText(FilePath, $",{newWord.ToUpper()}");
            }
        }

        public static string GetRandomWord()
        {
            var words = LoadWords();

            if (words == null || words.Count == 0)
                throw new InvalidOperationException("No words available in the CSV file.");

            Random rnd = new Random();
            int index = rnd.Next(words.Count);
            return words[index];
        }

    }
}
