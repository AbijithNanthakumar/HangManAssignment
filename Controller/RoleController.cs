using System;
using HangmanMVC.View;
using HangmanMVC.Model;

namespace HangmanMVC.Controller
{
    public class RoleController
    {
        private readonly ConsoleView view;

        public RoleController()
        {
            view = new ConsoleView();
        }

        public void Start()
        {
            string role = view.AskRole();

            if (role == "a")
            {
                HandleAdmin();
            }
            else if (role == "u")
            {
                HandleUser();
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting...");
            }
        }

        private void HandleAdmin()
        {
            Console.Clear();
            Console.WriteLine("--- Admin Mode ---");
            string newWord = view.PromptForNewWord();

            if (!string.IsNullOrWhiteSpace(newWord))
            {
                WordRepository.AddWord(newWord);
                Console.WriteLine($"✅ Word '{newWord}' added successfully!");
            }
        }

        private void HandleUser()
        {
            try
            {
                string selectedWord = WordRepository.GetRandomWord();
                var controller = new GameController(selectedWord);
                controller.Run();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("❌ No words found. Ask admin to add some first.");
            }

        }
    }
}
