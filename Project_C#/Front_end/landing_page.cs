using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_.Back_end;  

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

            var menuActions = new Dictionary<string, Func<State>>
            {
                { "1", () => State.Login },
                { "2", () => State.Register },
                { "3", () => { Environment.Exit(0); return State.Home; } }
            };

            if (menuActions.ContainsKey(choice))
            {
                return menuActions[choice]();
            }
            else
            {
                Console.WriteLine("Invalid choice! Please try again.");
                return State.Home;
            }
        }
    }
}
