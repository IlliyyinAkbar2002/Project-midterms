using System.Data;
using Testing_connect_MySql;
using Project_C_.Back_end;
using Project_C_.Front_end;

namespace Project_C_
{
    internal class Program
    {
        private static UserManager userManager = new UserManager();
        private static State currentState = State.Home;  // Automata: State dimulai dari Home
        private static User currentUser;

        static void Main(string[] args)
        {
            while (true)
            {
                switch (currentState)  // Automata: State Transitions
                {
                    case State.Login:
                        // Automata: Login adalah salah satu state yang akan berpindah ke MainMenu jika sukses
                        currentUser = LoginHandler.Login(userManager, out currentState, out currentUser);
                        break;
                    case State.Register:
                        // Automata: Register adalah state yang berpindah ke Login setelah sukses
                        RegisterHandler.Register(userManager, out currentState);
                        break;
                    case State.MainMenu:
                        Menu.ShowMainMenu(currentUser);
                        HandleMainMenuSelection();
                        break;
                    case State.Logout:
                        Console.WriteLine("Logged out.");
                        return;
                    case State.Home:
                        // **Login/Registration Menu**: Pilihan awal untuk login atau register
                        currentState = LandingPage.ShowLoginOrRegisterMenu();
                        break;
                }
            }
        }

        static void HandleMainMenuSelection()
        {
            Console.WriteLine("Choose an option:");
            string choice = Console.ReadLine();

            // Handle user input for menu options
            if (choice == "1")  // option 1 is "??"
            {
                // Call the function to create a post
                Console.WriteLine("Creating Post...");
            }
            else if (choice == "2")  // option 2 is "??"
            {
                // Call the function to view posts
                Console.WriteLine("Viewing Posts...");
            }
            else if (choice == "3")  // "Log Out"
            {
                currentState = State.Logout;  // Transition to logout state
            }
            else
            {
                Console.WriteLine("Invalid choice! Please try again.");
            }
        }
    }
}
