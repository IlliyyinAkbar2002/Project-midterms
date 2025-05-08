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
		User user = userManager.GetUserByUsername(currentUsername);
		Console.WriteLine($"Username: {user.Username}");
		Console.WriteLine($"Password: {user.Password}");
		Console.WriteLine($"Nama: {user.Nama}");		
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

	// public void SearchPost()
	// {
	// 	Console.Write("Enter the title of the post to search: ");
	// 	string title = Console.ReadLine();
    //     var post = postsManager.GetAllPosts()
    //          .FirstOrDefault(p => p != null && p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
	// 	if (post != null)
	// 	{
	// 		Console.WriteLine($"Title: {post.Title}\nContent: {post.Content}\nAuthor: {post.Author}\nCreated At: {post.CreatedAt}");
	// 		Console.Write("Do you want to delete this post? (y/n): ");
	// 		string input = Console.ReadLine();
	// 		if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
	// 		{
	// 			postsManager.DeletePost(title);
	// 			Console.WriteLine("Post deleted successfully.");
	// 		}
	// 	}
	// 	else
	// 	{
	// 		Console.WriteLine("Post not found.");
	// 	}
	// }

	public void DeletePost()
	{
		Console.Write("Enter the title of the post to delete: ");
		string title = Console.ReadLine();
		postsManager.DeletePost(title);
		Console.WriteLine("Post deleted successfully.");
	}
}
