using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Csharp_Test.Back_end
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    namespace Project_Csharp_Test.Back_end
    {
        [TestClass]
        public class PostsTests
        {
            private string testFilePath;
            private Posts postsManager;

            [TestInitialize]
            public void Setup()
            {
                testFilePath = $"test_posts_{Guid.NewGuid()}.json";
                File.WriteAllText(testFilePath, "[]");
                postsManager = new Posts(testFilePath);
            }

            [TestCleanup]
            public void Cleanup()
            {
                if (File.Exists(testFilePath))
                    File.Delete(testFilePath);
            }

            [TestMethod]
            public void AddPost_ShouldAddNewPost()
            {
                postsManager.AddPost("Judul Tes", "Isi konten", "user1");

                var allPosts = postsManager.GetAllPosts();
                Assert.AreEqual(1, allPosts.Count);
                Assert.AreEqual("Judul Tes", allPosts[0].Title);
                Assert.AreEqual(PostStatus.Pending, allPosts[0].Status);
            }

            [TestMethod]
            public void DeletePost_ShouldRemovePost()
            {
                postsManager.AddPost("Hapus Ini", "Konten", "userX");
                postsManager.DeletePost("Hapus Ini");

                var remaining = postsManager.GetAllPosts();
                Assert.AreEqual(0, remaining.Count);
            }

            [TestMethod]
            public void DeletePostByUser_ShouldRemoveOwnPost()
            {
                postsManager.AddPost("Milikku", "Konten", "userABC");
                postsManager.DeletePostByUser("Milikku", "userABC");

                var remaining = postsManager.GetAllPosts();
                Assert.AreEqual(0, remaining.Count);
            }

            [TestMethod]
            public void DeletePostByUser_ShouldNotRemoveOthersPost()
            {
                postsManager.AddPost("Bukan Milikmu", "Konten", "owner1");
                postsManager.DeletePostByUser("Bukan Milikmu", "userLain");

                var remaining = postsManager.GetAllPosts();
                Assert.AreEqual(1, remaining.Count); // tidak terhapus
            }

            [TestMethod]
            public void UpdatePostStatus_ShouldChangeStatus()
            {
                postsManager.AddPost("Status Check", "Konten", "user2");
                postsManager.UpdatePostStatus("Status Check", PostStatus.Approved);

                var post = postsManager.GetAllPosts().First();
                Assert.AreEqual(PostStatus.Approved, post.Status);
            }

            [TestMethod]
            public void GetPostsByStatus_ShouldReturnFilteredList()
            {
                postsManager.AddPost("Post1", "Konten", "user1");
                postsManager.AddPost("Post2", "Konten", "user2");
                postsManager.UpdatePostStatus("Post2", PostStatus.Approved);

                var pending = postsManager.GetPostsByStatus(PostStatus.Pending);
                var approved = postsManager.GetPostsByStatus(PostStatus.Approved);

                Assert.AreEqual(1, pending.Count);
                Assert.AreEqual("Post1", pending[0].Title);
                Assert.AreEqual(1, approved.Count);
                Assert.AreEqual("Post2", approved[0].Title);
            }
        }
    }

}
