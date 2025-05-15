using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Newtonsoft.Json;

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
    
    //public Posts()
    //{
    //    // **Precondition**: Jika file tidak ada, buat file baru atau baca dari file JSON
    //    if (File.Exists(postsFilePath))
    //    {
    //        string json = File.ReadAllText(postsFilePath);
    //        posts = JsonConvert.DeserializeObject<List<Post>>(json);
    //    }
    //    else
    //    {
    //        posts = new List<Post>();  // Jika file tidak ada, buat daftar kosong
    //    }
    //}

    public void AddPost(string title, string content, string author)
    {
        Post newPost = new Post(title, content, author);
        posts.Add(newPost);
        SavePostsToFile();
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
