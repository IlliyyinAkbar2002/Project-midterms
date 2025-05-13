using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_.Back_end;

namespace Project_Csharp_Test.Back_end
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager userManager;
        private string testFilePath;

        // Buat file unik untuk setiap test agar tidak bentrok
        private string GetUniqueTestFilePath()
        {
            return Path.Combine(Path.GetTempPath(), $"test_users_{Guid.NewGuid()}.json");
        }

        [TestInitialize]
        public void Setup()
        {
            testFilePath = GetUniqueTestFilePath();
            File.WriteAllText(testFilePath, "[]"); // Buat file kosong

            userManager = new UserManagerTestable(testFilePath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Hapus file setelah test selesai
            if (File.Exists(testFilePath))
            {
                try
                {
                    File.Delete(testFilePath);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Cleanup gagal: " + ex.Message);
                }
            }
        }

        [TestMethod]
        public void Register_AddsNewUserSuccessfully()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "1234567890", "001", "002");

            var user = userManager.GetUserByUsername("testuser");
            Assert.IsNotNull(user);
            Assert.AreEqual("testuser", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_ThrowsError_WhenUsernameExists()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "1234567890", "001", "002");
            userManager.Register("testuser", "password2", Role.Masyarakat, "Nama", "1234567891", "001", "002");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_ThrowsError_WhenNikExists()
        {
            userManager.Register("user1", "password", Role.Masyarakat, "Nama", "123", "001", "002");
            userManager.Register("user2", "password", Role.Masyarakat, "Nama", "123", "001", "002"); // NIK sama
        }

        [TestMethod]
        public void Authenticate_ReturnsUser_WhenCredentialsAreCorrect()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "1234567890", "001", "002");
            var user = userManager.Authenticate("testuser", "password");

            Assert.IsNotNull(user);
            Assert.AreEqual("testuser", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Authenticate_ThrowsError_WhenPasswordIncorrect()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "1234567890", "001", "002");
            userManager.Authenticate("testuser", "wrongpassword");
        }
    }

    // Subclass untuk testing: override file dan inisialisasi data
    public class UserManagerTestable : UserManager
    {
        public UserManagerTestable(string filePath)
        {
            typeof(UserManager)
                .GetField("usersFilePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, filePath);

            typeof(UserManager)
                .GetField("users", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(this, new System.Collections.Generic.List<User>());
        }
    }
}
