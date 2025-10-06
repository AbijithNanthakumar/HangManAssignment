using System;
using System.Collections.Generic;
using System.Linq;
using HangmanMVC.Model;

namespace HangmanMVC.View
{
    public class ConsoleView
    {
        private static readonly string[] HangmanStages = new string[]
{
    @"
     
     
     
     
     
",
    @"
 |
 |
 |
 |
_|_
",
    @"
 +---+
 |   
 |
 |
_|_
",
    @"
 +---+
 |   O
 |  /|\\
 |  / \\
_|_
"
};
        public  void DisplayWinAnimation()
        {
            Console.WriteLine(@"
  O O O O O O O O O O 
  O  YOU WON!      O
  O O O O O O O O O O O 

        O
       \|/
        |
       / \

  CONGRATULATIONS CHAMP!
");
        }


        public  void DisplayGame(HangmanGame game)
        {
            Console.Clear();

            // Show hangman stage
            DisplayHangman(game.WrongGuesses.Count);

            // Show wrong guesses
            Console.WriteLine($"Wrong guesses ({game.WrongGuesses.Count}/3): {string.Join(", ", game.WrongGuesses)}");

            // Show guessed word
            Console.WriteLine(DisplayWord(game.WordToGuess, game.CorrectGuesses));
        }


        public char GetUserInput()
        {
            Console.Write("Enter a character: ");
            return Console.ReadKey().KeyChar;
        }

        public void DisplayEndMessage(HangmanGame game)
        {
            Console.Clear();
            DisplayGame(game);
            if (game.GameStatus == GameStatus.PlayerWon)
            {
                Console.WriteLine("🎉 Congratulations! You won!");
            }
            else
            {
                Console.WriteLine($"💀 Sorry, you lost. The word was: {game.WordToGuess}");
            }
        }

        private static string DisplayWord(string word, List<char> correctGuesses)
        {
            return string.Join(" ", word.Select(c => correctGuesses.Contains(c) ? c : '_'));
        }

        public string AskRole()
        {
            Console.WriteLine("Are you a User or Admin? (u/a): ");
            string input = Console.ReadLine()?.Trim().ToLower();
            return input;
        }

        public string PromptForNewWord()
        {
            Console.Write("Enter a new word to add: ");
            return Console.ReadLine();
        }

        static void DisplayHangman(int wrongAttempts)
        {
            Console.WriteLine(HangmanStages[wrongAttempts]);
        }


    }
}
