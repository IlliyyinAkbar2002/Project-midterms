using System;
using System.Linq;
using Project_C_.Back_end;

public class CrudHandler
{
	private Project_C_.Back_end.UserManager userManager;
	private Posts postsManager;
	private string currentUsername;
	public CrudHandler(Project_C_.Back_end.UserManager userManager, Posts postsManager, string currentUsername)
	{
		this.userManager = userManager;
		this.postsManager = postsManager;
		this.currentUsername = currentUsername;
	}

	public void ViewProfile()
	{
        Console.WriteLine("===Profile Anda===");
        User user = userManager.GetUserByUsername(currentUsername);
		Console.WriteLine($"Username: {user.Username}");
		Console.WriteLine($"Password: {user.Password}");
		Console.WriteLine($"Nama: {user.Nama}");
		Console.WriteLine($"Status: {user.Role}");
		Console.WriteLine($"NIK: {user.NIK}");
		Console.WriteLine($"RT: {user.RT}");
		Console.WriteLine($"RW: {user.RW}");
	}

    // Create Post
    public void CreatePost()
	{
		Console.Write("Title: ");
		string title = Console.ReadLine();
		Console.Write("Content: ");
		string content = Console.ReadLine();
		postsManager.AddPost(title, content, currentUsername);
		Console.WriteLine("Post created successfully.");
	}

	// View Posts
	public void ViewPost()
	{
		var allPosts = postsManager.GetAllPosts().Where(p => p != null).ToList();
		if (allPosts.Count == 0)
		{
			Console.WriteLine("No posts found.");
			return;
		}
		Console.WriteLine("All Posts:");
		foreach (var post in allPosts)
		{
			Console.WriteLine($"Title: {post.Title}\nContent: {post.Content}\nAuthor: {post.Author}\nCreated At: {post.CreatedAt}\n");
		}
	}

	public void DeletePost()
	{
		Console.Write("Enter the title of the post to delete: ");
		string title = Console.ReadLine();
		postsManager.DeletePost(title);
		
	}

    public void DeleteOwnPost()
    {
        Console.Write("Enter the title of your post to delete: ");
        string title = Console.ReadLine();
        postsManager.DeletePostByUser(title, currentUsername); // pass currentUsername
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
