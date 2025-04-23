using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    internal class kepala_desa
    {
        public static void berandaKepalaDesa()
        {
            Console.WriteLine("1. Profile");
            Console.WriteLine("2. Cek Laporan");
            Console.WriteLine("3. Cari Post");
            Console.WriteLine("4. Logout");
            Console.Write("Please select an option (1-4): ");
            string choice = Console.ReadLine();

            if (string.IsNullOrEmpty(choice)) // Check for null or empty input
            {
                Console.WriteLine("No input provided. Please try again.");
                return;
            }

            switch (choice)
            {
                case "1":
                    // Call the function to view profile
                    break;
                case "2":
                    // Call the function to check reports
                    break;
                case "3":
                    // Call the function to search posts
                    break;
                case "4":
                    Login.login();
                    Console.WriteLine("Logging out...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
