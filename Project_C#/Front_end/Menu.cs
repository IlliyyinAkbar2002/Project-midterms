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
            Console.WriteLine($"Welcome, {currentUser.Nama}! Your role is {currentUser.Role}.");
            if (currentUser.Role == Role.Admin)
            {
                ShowAdminMenu(currentUser, userManager);
            }
            else if (currentUser.Role == Role.Lurah)
            {
                ShowLurahMenu(currentUser, userManager);
            }
            else
            {
                ShowMasyarakatMenu(currentUser);
            }
        }

        private static void ShowAdminMenu(User currentUser, UserManager userManager)
        {
            Console.WriteLine("=== Admin Menu ===");
            Console.WriteLine("1. View Profile");
            Console.WriteLine("2. Create Post");
            Console.WriteLine("3. Search User");
            Console.WriteLine("4. Create New User");
            Console.WriteLine("5. Lihat semua Complaints");
            Console.WriteLine("6. Hapus Complaint");
            Console.WriteLine("q. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            var postsManager = new Posts();
            var crudHandler = new CrudHandler(new UserManager(), postsManager, currentUser.Username);
            var crudAdmin = new CrudAdmin();

            var menuActions = new Dictionary<string, Action>
    {
        { "1", () => crudHandler.ViewProfile() },
        { "2", () => crudHandler.CreatePost() },
        { "3", () => crudAdmin.SearchUser(userManager) },
        { "4", () => crudAdmin.CreateUser(userManager) },
        { "5", () => crudAdmin.ViewApprovedPosts() },
        { "6", () => crudHandler.DeletePost() },
        { "q", () => Program.SetState(State.Logout) }
    };

            if (menuActions.ContainsKey(choice))
            {
                menuActions[choice].Invoke();
            }
            else
            {
                Console.WriteLine("Invalid option.");
                Program.SetState(State.MainMenu);
            }
        }


        private static void ShowLurahMenu(User currentUser, UserManager userManager)
        {

            Console.WriteLine("=== Lurah Menu ===");
            Console.WriteLine("1. View Profile");
            Console.WriteLine("2. Approve/Reject Complaints");
            Console.WriteLine("3. Rubah status komplain");
            Console.WriteLine("4. Hapus Complaint");
            Console.WriteLine("q. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            var postsManager = new Posts();
            var crudHandler = new CrudHandler(new UserManager(), postsManager, currentUser.Username);
            var lurahHandler = new CrudAdmin();

            var menuActions = new Dictionary<string, Action>
    {
        { "1", () => crudHandler.ViewProfile() },
        { "2", () => lurahHandler.ReviewPendingPosts() },
        { "3", () => lurahHandler.ReviewApprovedPosts() },
        { "4", () => crudHandler.DeletePost() }, 
        { "q", () => Program.SetState(State.Logout) }
    };

            if (menuActions.ContainsKey(choice))
            {
                menuActions[choice].Invoke();

                // Only skip main menu if logging out
                if (choice != "q")
                {
                    Program.SetState(State.MainMenu);
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
                Program.SetState(State.MainMenu);
            }
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

            var menuActions = new Dictionary<string, Action>
    {
        { "1", () => crudHandler.ViewProfile() },
        { "2", () => crudHandler.CreatePost() },
        { "3", () => crudHandler.ViewApprovedPosts() },
        { "4", () => crudHandler.DeleteOwnPost() },
        { "q", () => Program.SetState(State.Logout) }
    };

            if (menuActions.ContainsKey(choice))
            {
                menuActions[choice].Invoke();
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }

            if (choice != "q")
                Program.SetState(State.MainMenu);
        }

    }
}