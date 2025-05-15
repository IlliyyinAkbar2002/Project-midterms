using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_C_.Back_end;
using System;

namespace Project_C_Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager userManager;
        private string testFilePath;

        [TestInitialize]
        public void Setup()
        {
            testFilePath = $"test_users_{Guid.NewGuid()}.json"; // Buat file unik per test
            File.WriteAllText(testFilePath, "[]");
            userManager = new UserManager(testFilePath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
        }

        [TestMethod]
        public void Authenticate_ReturnsUser_WhenCredentialsAreCorrect()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "1234567890123456", "01", "01");

            var user = userManager.Authenticate("testuser", "password");
            Assert.IsNotNull(user);
            Assert.AreEqual("testuser", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Authenticate_ThrowsError_WhenPasswordIncorrect()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "1234567890123456", "01", "01");


            userManager.Authenticate("testuser", "wrongpassword");
        }

        [TestMethod]
        public void Register_AddsUserSuccessfully()
        {
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "1111222233334444", "01", "01");

            var user = userManager.GetUserByUsername("user1");
            Assert.IsNotNull(user);
            Assert.AreEqual("user1", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Throws_WhenDuplicateUsername()
        {
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "1111222233334444", "01", "01");

            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "1010101010201020", "01", "01");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Throws_WhenDuplicateNIK()
        {
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "9999111122223333", "01", "01");
            userManager.Register("user2", "pw", Role.Masyarakat, "Nama", "9999111122223333", "01", "01"); 

        }

        [TestMethod]
        public void GetAllUsers_ReturnsCorrectUserList()
        {
            userManager.Register("user1", "pass1", Role.Masyarakat, "Nama1", "1111222233334444", "01", "02");
            userManager.Register("user2", "pass2", Role.Masyarakat, "Nama2", "2222333344445555", "03", "04");


            var allUsers = userManager.GetAllUsers();

            Assert.AreEqual(2, allUsers.Count);
            Assert.IsTrue(allUsers.Any(u => u.Username == "user1"));
            Assert.IsTrue(allUsers.Any(u => u.Username == "user2"));
        }

        [TestMethod]
        public void GetUserByNIK_ReturnsCorrectUser()
        {
            userManager.Register("user1", "pass1", Role.Masyarakat, "Nama1", "9999888877776666", "01", "02");

            var user = userManager.GetUserByNIK("9999888877776666");


            Assert.IsNotNull(user);
            Assert.AreEqual("user1", user.Username);
        }

    }
}
