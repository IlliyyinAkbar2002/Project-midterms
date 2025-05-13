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
            return;
        }

        Console.WriteLine("=== User Ditemukan ===");
        Console.WriteLine($"Username: {user.Username}");
        Console.WriteLine($"Name: {user.Nama}");
        Console.WriteLine($"Role: {user.Role}");
        Console.WriteLine($"NIK: {user.NIK}, RT: {user.RT}, RW: {user.RW}");

        Console.WriteLine("\nEdit Options:");
        Console.WriteLine("1. Change Role");
        Console.WriteLine("2. Edit Personal Data");
        Console.WriteLine("3. Change Password");
        Console.WriteLine("x. Cancel");
        Console.Write("Choose option: ");
        string input = Console.ReadLine();

        var editOptions = new Dictionary<string, Action>
    {
        { "1", () => {
            Console.WriteLine("Select new role:");
            Console.WriteLine("1. Masyarakat");
            Console.WriteLine("2. Lurah");
            Console.WriteLine("3. Admin");
            Console.Write("Choice: ");
            string roleInput = Console.ReadLine();

            var roleMap = new Dictionary<string, Role>
            {
                { "1", Role.Masyarakat },
                { "2", Role.Lurah },
                { "3", Role.Admin }
            };

            if (roleMap.ContainsKey(roleInput))
            {
                user.Role = roleMap[roleInput];
                Console.WriteLine("Role updated.");
                userManager.SaveChanges();
            }
            else
            {
                Console.WriteLine("Invalid role selection.");
            }
        }},
        { "2", () => {
            // Edit personal data
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
        }},
        { "3", () => {
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            user.Password = newPassword;  // Assuming `Password` is a property of `User`
            Console.WriteLine("Password updated.");
            userManager.SaveChanges();
        }}
     };

        if (editOptions.ContainsKey(input))
        {
            editOptions[input]();  // Run selected action
        }
        else
        {
            Console.WriteLine("Cancelled or invalid input.");
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

    public void ReviewPendingPosts()
    {
        List<Post> pendingPosts = postsManager.GetPostsByStatus(PostStatus.Pending);

        if (pendingPosts.Count == 0)
        {
            Console.WriteLine("No pending posts to review.");
            return;
        }

        int page = 0;
        const int pageSize = 5;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Pending Posts Review ===");

            var postsToShow = pendingPosts.Skip(page * pageSize).Take(pageSize).ToList();

            for (int i = 0; i < postsToShow.Count; i++)
            {
                var post = postsToShow[i];
                Console.WriteLine($"{i + 1}. Title: {post.Title}, Author: {post.Author}, Created At: {post.CreatedAt}");
            }

            Console.WriteLine("\nOptions:");
            Console.WriteLine("Enter [1-5] to review a post");
            Console.WriteLine("N - Next Page | P - Previous Page | X - Exit");
            Console.Write("Your choice: ");
            string input = Console.ReadLine().Trim().ToUpper();

            if (input == "X")
                break;
            else if (input == "N")
            {
                if ((page + 1) * pageSize < pendingPosts.Count)
                    page++;
            }
            else if (input == "P")
            {
                if (page > 0)
                    page--;
            }
            else if (int.TryParse(input, out int index) && index >= 1 && index <= postsToShow.Count)
            {
                var selectedPost = postsToShow[index - 1];
                Console.Clear();
                Console.WriteLine($"--- Post Details ---");
                Console.WriteLine($"Title: {selectedPost.Title}");
                Console.WriteLine($"Content: {selectedPost.Content}");
                Console.WriteLine($"Author: {selectedPost.Author}");
                Console.WriteLine($"Created At: {selectedPost.CreatedAt}");
                Console.WriteLine("\n1. Approve");
                Console.WriteLine("2. Reject");
                Console.WriteLine("3. Back");
                Console.Write("Choose action: ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    postsManager.UpdatePostStatus(selectedPost.Title, PostStatus.Approved);
                    Console.WriteLine("Post approved.");
                    pendingPosts.Remove(selectedPost); // Update in memory list
                }
                else if (action == "2")
                {
                    postsManager.UpdatePostStatus(selectedPost.Title, PostStatus.Rejected);
                    Console.WriteLine("Post rejected.");
                    pendingPosts.Remove(selectedPost); // Update in memory list
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public void ReviewApprovedPosts()
    {
        List<Post> approvedPosts = postsManager.GetPostsByStatus(PostStatus.Approved);

        if (approvedPosts.Count == 0)
        {
            Console.WriteLine("No approved posts to review.");
            return;
        }

        int pageSize = 5;
        int page = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Review Approved Posts (Page " + (page + 1) + ") ===");

            var currentPage = approvedPosts
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < currentPage.Count; i++)
            {
                var post = currentPage[i];
                Console.WriteLine($"{i + 1}. {post.Title} by {post.Author} - {post.CreatedAt}");
            }

            Console.WriteLine("n - Next Page | p - Previous Page | number - Mark as Finished | x - Exit");
            Console.Write("Choose: ");
            string input = Console.ReadLine();

            if (input == "n" && (page + 1) * pageSize < approvedPosts.Count)
            {
                page++;
            }
            else if (input == "p" && page > 0)
            {
                page--;
            }
            else if (int.TryParse(input, out int index) && index >= 1 && index <= currentPage.Count)
            {
                var selectedPost = currentPage[index - 1];
                postsManager.UpdatePostStatus(selectedPost.Title, PostStatus.Finished);
                Console.WriteLine($"Post '{selectedPost.Title}' marked as Finished.");
                Thread.Sleep(1500); // Brief pause before reloading
                approvedPosts = postsManager.GetPostsByStatus(PostStatus.Approved); // Refresh list
            }
            else if (input == "x")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input.");
                Thread.Sleep(1000);
            }
        }
    }

}
