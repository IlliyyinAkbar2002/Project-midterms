using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Newtonsoft.Json;
using System.Diagnostics;

public enum PostStatus
{
    Pending,
    Approved,
    Rejected,
    Hidden,
    Finished
}

public class Post
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public PostStatus Status { get; set; }

    public Post(string title, string content, string author)
    {
        Title = title;
        Content = content;
        Author = author;
        CreatedAt = DateTime.Now;
        Status = PostStatus.Pending;  // Default status
    }
}

public class Posts
{

    private List<Post> posts;  // Daftar untuk menyimpan data post
    private string postsFilePath = "crud_masyarakat.json";  // Lokasi file JSON untuk posts

    public Posts(string? customFilePath = null)
    {
        postsFilePath = customFilePath ?? "crud_masyarakat.json";

        if (File.Exists(postsFilePath))
        {
            string json = File.ReadAllText(postsFilePath);
            posts = JsonConvert.DeserializeObject<List<Post>>(json) ?? new List<Post>();
        }
        else
        {
            posts = new List<Post>();
        }
    }

    public void AddPost(string title, string content, string author)
    {
        // Preconditions
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Title cannot be null or empty", nameof(title));
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("Content cannot be null or empty", nameof(content));
        if (string.IsNullOrEmpty(author))
            throw new ArgumentException("Author cannot be null or empty", nameof(author));
        if (posts.Any(p => p.Title == title))
            throw new InvalidOperationException("A post with this title already exists");
            
        // Method implementation
        Post newPost = new Post(title, content, author);
        posts.Add(newPost);
        SavePostsToFile();
        
        // Postconditions
        Debug.Assert(posts.Any(p => p.Title == title && p.Content == content && p.Author == author), 
            "Post was not successfully added");
    }

    public void DeletePost(string title)
    {
        Post postToDelete = posts.FirstOrDefault(p => p.Title == title);
        if (postToDelete != null)
        {
            posts.Remove(postToDelete);
            SavePostsToFile();
            Console.WriteLine("Post deleted successfully.");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
    }

    public void DeletePostByUser(string title, string username)
    {
        var postToDelete = posts.FirstOrDefault(p => p.Title == title && p.Author == username);
        if (postToDelete != null)
        {
            posts.Remove(postToDelete);
            SavePostsToFile();
            Console.WriteLine("Post deleted successfully.");
        }
        else
        {
            Console.WriteLine("You can only delete your own post or the post was not found.");
        }
    }

    public List<Post> GetAllPosts()
    {
        return posts;
    }

    public void UpdatePostStatus(string title, PostStatus newStatus)
    {
        Post postToUpdate = posts.FirstOrDefault(p => p.Title == title);
        if (postToUpdate != null)
        {
            postToUpdate.Status = newStatus;
            SavePostsToFile();
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
    }

    private void SavePostsToFile()
    {
        string json = JsonConvert.SerializeObject(posts, Formatting.Indented);
        System.IO.File.WriteAllText(postsFilePath, json);
    }

    public List<Post> GetPostsByStatus(PostStatus status)
    {
        return posts.Where(p => p.Status == status).ToList();
    }


}
