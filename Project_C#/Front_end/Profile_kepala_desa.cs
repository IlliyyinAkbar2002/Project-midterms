using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    internal class Profile_kepala_desa
    {
        public static void Profile()
        {
            Console.WriteLine("Welcome to the Kepala Desa Profile Page!");
            Console.WriteLine("Name: Satoshi Nakamoto");
            Console.WriteLine("Birth: 25/08/1989");
            Console.WriteLine("Address: Jl. Raya Sukabumi No. 123, Jonggol");
            Console.WriteLine("1. Back");
            string choice = Console.ReadLine();

            if (string.IsNullOrEmpty(choice)) // Check for null or empty input
            {
                Console.WriteLine("No input provided. Please try again.");
                return;
            }

            switch (choice)
            {
                case "1":
                    // Call the function to go back to the previous menu
                    Console.WriteLine("Going back to the previous menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
