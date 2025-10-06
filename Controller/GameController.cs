using HangmanMVC.Model;
using HangmanMVC.View;
using System;

namespace HangmanMVC.Controller
{
    public class GameController
    {
        private HangmanGame game;
        private ConsoleView view;

        public GameController(string wordguess)
        {
            game = new HangmanGame(wordguess);
            view = new ConsoleView();
            game.GameEnded += OnGameEnded;
        }

        public void Run()
        {
            while (game.GameStatus == GameStatus.Ongoing)
            {
                Console.Clear();
                view.DisplayGame(game); // ? use instance to call method
                char guess = view.GetUserInput();
                game.Guess(guess);
            }
        }

        private void OnGameEnded()
        {
            Console.Clear();

            view.DisplayGame(game); // ? use instance here too

            if (game.GameStatus == GameStatus.PlayerWon)
            {
                view.DisplayWinAnimation(); // ? Show win screen
            }
            else
            {
                Console.WriteLine($"\n? You lost! The word was: {game.WordToGuess}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
