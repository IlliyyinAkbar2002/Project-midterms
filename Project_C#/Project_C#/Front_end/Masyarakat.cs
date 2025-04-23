using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    internal class Masyarakat
    {
        public static void BerandaMasyarakat()
        {
            Console.WriteLine("Welcome to the Masyarakat Page!");
            Console.WriteLine("1. Profile");
            Console.WriteLine("2. Create Complaint");
            Console.WriteLine("3. View Complaints");
            Console.WriteLine("4. Search Complaints");
            Console.WriteLine("5. Logout");
            Console.Write("Please select an option (1-4): ");
            string? choice = Console.ReadLine(); // Allow 'choice' to be nullable

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
                    // Call the function to create a complaint
                    break;
                case "3":
                    // Call the function to view complaints
                    break;
                case "4":
                    // Call the function to search complaints
                    break;
                case "5":
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
