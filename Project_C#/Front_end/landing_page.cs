using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_.Back_end;  // Importing the Back_end namespace for UserManager and State

namespace Project_C_.Front_end
{
    public static class LandingPage
    {
        // **Login/Registration Menu**: Pilihan awal untuk login atau register
        public static State ShowLoginOrRegisterMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to LaporDesa System!");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option (1, 2, or 3): ");
            string choice = Console.ReadLine();

            // **Precondition**: Pastikan input adalah angka 1, 2, atau 3
            if (choice == "1")
            {
                return State.Login;  // Automata: Pindah ke Login
            }
            else if (choice == "2")
            {
                return State.Register;  // Automata: Pindah ke Register
            }
            else if (choice == "3")
            {
                Environment.Exit(0);  // Keluar dari program
            }
            else
            {
                Console.WriteLine("Invalid choice! Please try again.");
            }

            return State.Home;  // Automata: Tetap di Home jika input invalid
        }
    }
}
