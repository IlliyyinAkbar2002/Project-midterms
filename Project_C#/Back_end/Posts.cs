using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Post
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }

    public Post(string title, string content, string author)
    {
        Title = title;
        Content = content;
        Author = author;
        CreatedAt = DateTime.Now;
    }
}

public class Posts
{
    private List<Post> posts;  // Daftar untuk menyimpan data post
    private const string postsFilePath = "crud_masyarakat.json";  // Lokasi file JSON untuk posts
    public Posts()
    {
        // **Precondition**: Jika file tidak ada, buat file baru atau baca dari file JSON
        if (File.Exists(postsFilePath))
        {
            string json = File.ReadAllText(postsFilePath);
            posts = JsonConvert.DeserializeObject<List<Post>>(json);
        }
        else
        {
            posts = new List<Post>();  // Jika file tidak ada, buat daftar kosong
        }
    }

    public void AddPost(string title, string content, string author)
    {
        Post newPost = new Post(title, content, author);
        posts.Add(newPost);
        SavePostsToFile();
    }

    public List<Post> GetAllPosts()
    {
        return posts;
    }

    private void SavePostsToFile()
    {
        string json = JsonConvert.SerializeObject(posts, Formatting.Indented);
        System.IO.File.WriteAllText(postsFilePath, json);
    }
}
