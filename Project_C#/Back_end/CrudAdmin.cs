using System;
using System.Linq;
using System.Collections.Generic;
using Project_C_.Back_end;
using Google.Protobuf.Collections;

public class CrudAdmin
{
	private Posts postsManager;

	public CrudAdmin()
	{
		postsManager = new Posts();
	}

    public void SearchUser(UserManager userManager)
    {
        Console.WriteLine("=== Admin: Search User ===");
        Console.WriteLine("1. View All Users");
        Console.WriteLine("2. Search User by Username");
        Console.Write("Choose an option (1-2): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ViewAllUsers(userManager);
                break;

            case "2":
                SearchUserByUsername(userManager);
                break;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private void ViewAllUsers(UserManager userManager)
    {
        var users = userManager.GetAllUsers(); // Get all users from userManager
        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
            return;
        }

        Console.WriteLine("=== All Users ===");
        foreach (var user in users)
        {
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"Name: {user.Nama}");
            Console.WriteLine($"Role: {user.Role}");
            Console.WriteLine($"NIK: {user.NIK}, RT: {user.RT}, RW: {user.RW}");
            Console.WriteLine(new string('-', 30));
        }
    }

    private void SearchUserByUsername(UserManager userManager)
    {
        Console.Write("Enter the username to search: ");
        string username = Console.ReadLine();

        var user = userManager.GetUserByUsername(username); // Find user by username
        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
        else
        {
            Console.WriteLine("=== User Found ===");
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"Name: {user.Nama}");
            Console.WriteLine($"Role: {user.Role}");
            Console.WriteLine($"NIK: {user.NIK}, RT: {user.RT}, RW: {user.RW}");

            Console.WriteLine("\nEdit Options:");
            Console.WriteLine("1. Change Role");
            Console.WriteLine("2. Edit Personal Data");
            Console.WriteLine("x. Cancel");
            Console.Write("Choose option: ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("Select new role:");
                Console.WriteLine("1. Masyarakat");
                Console.WriteLine("2. Lurah");
                Console.WriteLine("3. Admin");
                Console.Write("Choice: ");
                string roleInput = Console.ReadLine();

                user.Role = roleInput switch
                {
                    "1" => Role.Masyarakat,
                    "2" => Role.Lurah,
                    "3" => Role.Admin,
                    _ => user.Role
                };

                Console.WriteLine("Role updated.");
                userManager.SaveChanges();
            }
            else if (input == "2")
            {
                // Ensure the username is unique before updating
                string newUsername;
                do
                {
                    Console.Write("Enter new username: ");
                    newUsername = Console.ReadLine();

                    if (userManager.GetUserByUsername(newUsername) != null)
                    {
                        Console.WriteLine("Username is already taken. Please choose a different one.");
                    }

                } while (userManager.GetUserByUsername(newUsername) != null);

                user.Username = newUsername;

                // Ensure NIK is unique before updating
                string newNIK;
                do
                {
                    Console.Write("Enter new NIK: ");
                    newNIK = Console.ReadLine();

                    if (userManager.GetUserByNIK(newNIK) != null)
                    {
                        Console.WriteLine("NIK is already associated with another user. Please choose a different one.");
                    }

                } while (userManager.GetUserByNIK(newNIK) != null);

                user.NIK = newNIK;

                Console.Write("Enter new name: ");
                user.Nama = Console.ReadLine();

                Console.Write("Enter new RT: ");
                user.RT = Console.ReadLine();

                Console.Write("Enter new RW: ");
                user.RW = Console.ReadLine();

                Console.WriteLine("User data updated.");
                userManager.SaveChanges();
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
        }
    }

    public void CreateUser(UserManager userManager)
    {
        Console.WriteLine("=== Admin: Create New User ===");

        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        Console.Write("Enter full name: ");
        string nama = Console.ReadLine();

        Console.Write("Enter NIK: ");
        string nik = Console.ReadLine();

        Console.Write("Enter RT: ");
        string rt = Console.ReadLine();

        Console.Write("Enter RW: ");
        string rw = Console.ReadLine();

        Console.WriteLine("Select Role:");
        Console.WriteLine("1. Masyarakat");
        Console.WriteLine("2. Lurah");
        Console.WriteLine("3. Admin");
        Console.Write("Choose role (1-3): ");
        string roleInput = Console.ReadLine();

        Role role = roleInput switch
        {
            "1" => Role.Masyarakat,
            "2" => Role.Lurah,
            "3" => Role.Admin,
            _ => Role.Masyarakat // Default if invalid input
        };

        try
        {
            userManager.Register(username, password, role, nama, nik, rt, rw);
            Console.WriteLine($"User '{username}' successfully created with role '{role}'.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ViewComplaints()
	{
		List<Post> posts = postsManager.GetAllPosts();
		if (posts.Count == 0)
		{
			Console.WriteLine("No complaints/posts found.");
			return;
		}

		Console.WriteLine("Complaints from Masyarakat:");
		foreach (var post in posts)
		{
			Console.WriteLine($"Title: {post.Title}");
			Console.WriteLine($"Content: {post.Content}");
			Console.WriteLine($"Author: {post.Author}");
			Console.WriteLine($"Created At: {post.CreatedAt}");
            Console.WriteLine($"Status: {post.Status}");
            Console.WriteLine(new string('-', 30));
		}
	}

    public void ViewApprovedPosts()
    {
        List<Post> approvedPosts = postsManager.GetPostsByStatus(PostStatus.Approved);

        if (approvedPosts.Count == 0)
        {
            Console.WriteLine("No approved posts found.");
            return;
        }

        Console.WriteLine("=== Approved Posts ===");
        foreach (var post in approvedPosts)
        {
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Content: {post.Content}");
            Console.WriteLine($"Author: {post.Author}");
            Console.WriteLine($"Created At: {post.CreatedAt}");
            Console.WriteLine($"Status: {post.Status}");
            Console.WriteLine(new string('-', 40));
        }
    }


}
