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

        public static void SetState(State newState)
        {
            currentState = newState;
        }

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
                        Menu.ShowMainMenu(currentUser, userManager);
                        break;
                    case State.Posts:
                        // Removed Posts state and handler
                        break;
                    case State.Logout:
                        Console.WriteLine("Logged out.");
                        currentState = State.Home;  // Automata: Kembali ke Home setelah logout
                        break;
                    case State.Home:
                        // **Login/Registration Menu**: Pilihan awal untuk login atau register
                        currentState = LandingPage.ShowLoginOrRegisterMenu();
                        break;
                }
            }
        }
    }
}
