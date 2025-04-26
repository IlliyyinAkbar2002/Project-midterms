using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    internal class regis
    {
        public static void register()
        {
            Console.WriteLine("Welcome to the registration page!");
            Console.Write("Please enter your name: ");
            //string name = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            //string email = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            //string password = Console.ReadLine();
            // Here you would typically save the user data to a database or file
            Console.WriteLine($"Thank you for registering!");
            Console.WriteLine("1. submit");
            string choice = Console.ReadLine();

            if (string.IsNullOrEmpty(choice)) // Check for null or empty input
            {
                Console.WriteLine("No input provided. Please try again.");
                return;
            }

            switch (choice)
            {
                case "1":
                    // Call the function to submit the registration
                    Console.WriteLine("Registration submitted successfully!");
                    Login.login();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
