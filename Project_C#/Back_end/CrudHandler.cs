using System;

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
		var allPosts = postsManager.GetAllPosts();
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
}
