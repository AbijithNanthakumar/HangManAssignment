using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanMVC.Model
{
    public delegate void GameStatusDelegate();


    public class HangmanGame
    {
        public string WordToGuess { get; private set; }
        public List<char> CorrectGuesses { get; private set; }
        public List<char> WrongGuesses { get; private set; }
        public GameStatus GameStatus { get; private set; }

        public event GameStatusDelegate GameEnded;

        private const int MaxWrongAttempts = 3; 


        public HangmanGame(string wordToGuess)
        {
            WordToGuess = wordToGuess.ToUpper();
            CorrectGuesses = new List<char>();
            WrongGuesses = new List<char>();
            GameStatus = GameStatus.Ongoing;
        }

        public void Guess(char character)
        {
            character = char.ToUpper(character);

            // ?? Ignore duplicate guesses
            if (CorrectGuesses.Contains(character) || WrongGuesses.Contains(character))
                return;

            if (WordToGuess.Contains(character))
            {
                // ? Correct guess
                CorrectGuesses.Add(character);

                // ?? Reset wrong attempts when correct letter is guessed
                WrongGuesses.Clear();

                // ?? Check if all letters have been guessed
                if (WordToGuess.All(letter => CorrectGuesses.Contains(letter)))
                {
                    GameStatus = GameStatus.PlayerWon;
                    GameEnded?.Invoke();
                }
            }
            else
            {
                // ? Wrong guess
                WrongGuesses.Add(character);

                // ?? Check for game over
                if (WrongGuesses.Count >= MaxWrongAttempts)
                {
                    GameStatus = GameStatus.PlayerLost;
                    GameEnded?.Invoke();
                }
            }
        }



        private void CheckGameStatus()
        {
            if (WrongGuesses.Count >= 3)
            {
                GameStatus = GameStatus.PlayerLost;
                GameEnded?.Invoke();
                return;
            }

            if (WordToGuess.All(letter => CorrectGuesses.Contains(letter)))
            {
                GameStatus = GameStatus.PlayerWon;
                GameEnded?.Invoke();
            }
        }
    }
}
