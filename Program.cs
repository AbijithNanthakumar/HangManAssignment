using HangmanMVC.Controller;

namespace HangmanMVC
{
    class Program
    {
        static void Main()
        {
            var roleController = new RoleController();
            roleController.Start();
        }
    }
}
