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
        public static void ShowMainMenu(User currentUser, UserManager userManager)
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
                ShowAdminMenu(currentUser, userManager);
            }
            else if (currentUser.Role == Role.Lurah)
            {
                ShowLurahMenu(currentUser);
            }
            else
            {
                ShowMasyarakatMenu(currentUser);
            }
        }

        private static void ShowAdminMenu(User currentUser, UserManager userManager)
        {
            Console.WriteLine("=== Admin Menu ===");
            Console.WriteLine("1. Search User");
            Console.WriteLine("2. Create New User");
            Console.WriteLine("3. View All Complaints");
            Console.WriteLine("4. Search Complaints");
            Console.WriteLine("q. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            var crudAdmin = new CrudAdmin();

            switch (choice)
            {
                case "1":
                    crudAdmin.SearchUser(userManager);

                    break;
                case "2":
                    crudAdmin.CreateUser(userManager);

                    break;
                case "3":
                    crudAdmin.ViewApprovedPosts();

                    break;
                case "4":
                    crudAdmin.ViewComplaints();
                    
                    break;
                case "q":
                    Program.SetState(State.Logout);
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Program.SetState(State.MainMenu);
                    break;
            }
        }

        private static void ShowLurahMenu(User currentUser)
        {
            //Console.WriteLine("=== Lurah Menu ===");
            //Console.WriteLine("1. View Profile");
            //Console.WriteLine("2. Approve/Reject Complaints");
            //Console.WriteLine("3. Search Posts");
            //Console.WriteLine("q. Logout");
            //Console.Write("Choose an option: ");
            //string choice = Console.ReadLine();

            //var lurahHandler = new LurahHandler(currentUser);

            //switch (choice)
            //{
            //    case "1":
            //        lurahHandler.ViewProfile();
            //        break;
            //    case "2":
            //        lurahHandler.ApproveRejectPosts();
            //        break;
            //    case "3":
            //        lurahHandler.SearchPosts();
            //        break;
            //    case "q":
            //        Program.SetState(State.Logout);
            //        return;
            //    default:
            //        Console.WriteLine("Invalid option.");
            //        break;
            //}

            //Program.SetState(State.MainMenu);
        }

        private static void ShowMasyarakatMenu(User currentUser)
        {
            Console.WriteLine("=== Masyarakat Menu ===");
            Console.WriteLine("1. View Profile");
            Console.WriteLine("2. Create Complaint");
            Console.WriteLine("3. View Complaints");
            Console.WriteLine("4. Delete Post");
            Console.WriteLine("q. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            var postsManager = new Posts();
            var crudHandler = new CrudHandler(new UserManager(), postsManager, currentUser.Username);

            switch (choice)
            {
                case "1":
                    crudHandler.ViewProfile();
                    break;
                case "2":
                    crudHandler.CreatePost();
                    break;
                case "3":
                    crudHandler.ViewPost();
                    break;
                case "4":
                    crudHandler.DeletePost();
                    break;
                case "q":
                    Program.SetState(State.Logout);
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Program.SetState(State.MainMenu);
        }
    }
}