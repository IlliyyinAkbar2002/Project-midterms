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
            Console.WriteLine("Welcome to the Admin Page!");
            Console.WriteLine("1. View User Data");
            Console.WriteLine("2. Create User Data");
            Console.WriteLine("3. View Complaints");
            Console.WriteLine("4. Search Complaints");
            Console.WriteLine("q untuk Logout");
            Console.Write("Please select an option (1-4): ");
        }

        private static void ShowLurahMenu()
        {
            Console.WriteLine("1. Profile");
            Console.WriteLine("2. Cek Laporan");
            Console.WriteLine("3. Cari Post");
            Console.WriteLine("q untuk Logout");
            Console.Write("Please select an option (1-4): ");
        }

        private static void ShowMasyarakatMenu()
        {
            Console.WriteLine("Welcome to the Masyarakat Page!");
            Console.WriteLine("1. Profile");
            Console.WriteLine("2. Create Complaint");
            Console.WriteLine("3. View Complaints");
            Console.WriteLine("4. Search Complaints");
            Console.WriteLine("q untuk Logout");
            Console.Write("Please select an option (1-4): ");
        }
    }
}
