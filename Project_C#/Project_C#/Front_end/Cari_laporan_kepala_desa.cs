using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    internal class Cari_laporan_kepala_desa
    {
        public static void Search()
        {
            Console.WriteLine("Welcome to the Search Page!");
            Console.WriteLine("1. Search by Name");
            Console.WriteLine("2. Search by Date");
            Console.WriteLine("3. Search by Category");
            Console.WriteLine("4. Back");
            string choice = Console.ReadLine();

            if (string.IsNullOrEmpty(choice)) // Check for null or empty input
            {
                Console.WriteLine("No input provided. Please try again.");
                return;
            }

            switch (choice)
            {
                case "1":
                    // Call the function to search by name
                    break;
                case "2":
                    // Call the function to search by date
                    break;
                case "3":
                    // Call the function to search by category
                    break;
                case "4":
                    // Call the function to go back to the previous menu
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
