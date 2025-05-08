using System;
using System.Linq;
using System.Collections.Generic;

public class CrudAdmin
{
	private Posts postsManager;

	public CrudAdmin()
	{
		postsManager = new Posts();
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
			Console.WriteLine(new string('-', 30));
		}
	}
}
