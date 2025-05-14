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
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "123", "01", "01");
            var user = userManager.Authenticate("testuser", "password");
            Assert.IsNotNull(user);
            Assert.AreEqual("testuser", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Authenticate_ThrowsError_WhenPasswordIncorrect()
        {
            userManager.Register("testuser", "password", Role.Masyarakat, "Nama", "123", "01", "01");
            userManager.Authenticate("testuser", "wrongpassword");
        }

        [TestMethod]
        public void Register_AddsUserSuccessfully()
        {
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "999", "01", "01");
            var user = userManager.GetUserByUsername("user1");
            Assert.IsNotNull(user);
            Assert.AreEqual("user1", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Throws_WhenDuplicateUsername()
        {
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "999", "01", "01");
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "888", "01", "01");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Throws_WhenDuplicateNIK()
        {
            userManager.Register("user1", "pw", Role.Masyarakat, "Nama", "999", "01", "01");
            userManager.Register("user2", "pw", Role.Masyarakat, "Nama", "999", "01", "01");
        }

        [TestMethod]
        public void GetAllUsers_ReturnsCorrectUserList()
        {
            userManager.Register("user1", "pass1", Role.Masyarakat, "Nama1", "111", "001", "002");
            userManager.Register("user2", "pass2", Role.Masyarakat, "Nama2", "222", "003", "004");

            var allUsers = userManager.GetAllUsers();

            Assert.AreEqual(2, allUsers.Count);
            Assert.IsTrue(allUsers.Any(u => u.Username == "user1"));
            Assert.IsTrue(allUsers.Any(u => u.Username == "user2"));
        }

        [TestMethod]
        public void GetUserByNIK_ReturnsCorrectUser()
        {
            userManager.Register("user1", "pass1", Role.Masyarakat, "Nama1", "999123", "001", "002");

            var user = userManager.GetUserByNIK("999123");

            Assert.IsNotNull(user);
            Assert.AreEqual("user1", user.Username);
        }

    }
}
