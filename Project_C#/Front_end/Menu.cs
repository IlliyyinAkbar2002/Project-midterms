using Project_C_.Back_end;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    public static class Menu
    {
        public static void ShowMainMenu(User currentUser)
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user is logged in.");
                return;
            }

            // **Design by Contract**: Postcondition - Menampilkan menu berdasarkan role pengguna
            Console.WriteLine($"Welcome, {currentUser.Username}! Your role is {currentUser.Role}.");
            if (currentUser.Role == Role.Admin)
            {
                ShowAdminMenu();
            }
            else if (currentUser.Role == Role.Lurah)
            {
                ShowLurahMenu();
            }
            else
            {
                ShowMasyarakatMenu();
            }


        }

        private static void ShowAdminMenu()
        {
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. View Posts");
            Console.WriteLine("3. Log Out");
        }

        private static void ShowLurahMenu()
        {
            Console.WriteLine("1. Acc Post");
            Console.WriteLine("2. View Posts");
            Console.WriteLine("3. Log Out");
        }

        private static void ShowMasyarakatMenu()
        {
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. View Posts");
            Console.WriteLine("3. Log Out");
        }
    }
}
