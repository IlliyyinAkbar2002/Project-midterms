using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Front_end
{
    internal class Login
    {
        public static void login()
        {
            Console.WriteLine("Welcome to the login page!");
            Console.WriteLine("Please enter your email: ");
            //string email = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            //string password = Console.ReadLine();
            // Here you would typically check the user credentials against a database or file
            //Console.WriteLine($"Thank you for logging in!");
            Console.WriteLine("1. Page Admin");
            Console.WriteLine("2. Page Masyarakat");
            Console.WriteLine("3. Page Kepala Desa");
            string choice = Console.ReadLine();

            if (string.IsNullOrEmpty(choice)) // Check for null or empty input
            {
                Console.WriteLine("No input provided. Please try again.");
                return;
            }

            switch (choice)
            {
                case "1":
                    Admin.BerandaAdmin();
                    break;
                case "2":
                    Masyarakat.BerandaMasyarakat();
                    break;
                case "3":
                    kepala_desa.berandaKepalaDesa();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
