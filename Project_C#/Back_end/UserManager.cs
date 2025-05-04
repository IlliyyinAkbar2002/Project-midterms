using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;  // Pastikan Anda menambahkan referensi ke Newtonsoft.Json

namespace Project_C_.Back_end
{
    public class UserManager
    {
        private List<User> users;
        private List<Post> posts;  // Daftar untuk menyimpan data post
        private const string usersFilePath = "users.json";  // Lokasi file JSON
        private const string postsFilePath = "crud_masyarakat.json";  // Lokasi file JSON untuk posts

        public UserManager()
        {
            // **Precondition**: Jika file tidak ada, buat file baru atau baca dari file JSON
            if (File.Exists(usersFilePath))
            {
                string json = File.ReadAllText(usersFilePath);
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                users = new List<User>();  // Jika file tidak ada, buat daftar kosong
            }
        }

        // **Design by Contract (DbC)** - Menambahkan preconditions untuk validasi input
        public User Authenticate(string username, string password)
        {
            // **Precondition**: Pastikan username dan password tidak kosong
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Username and password cannot be empty.");

            // **Precondition**: Pastikan username dan password valid
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
                throw new InvalidOperationException("Invalid username or password.");

            return user;
        }

        // **Design by Contract (DbC)** - Menambahkan preconditions untuk validasi input
        public void Register(string username, string password, Role role)
        {
            // **Precondition**: Pastikan username dan password tidak kosong
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Username and password cannot be empty.");

            // **Precondition**: Pastikan username belum terdaftar
            if (users.Any(u => u.Username == username))
                throw new ArgumentException("Username already exists.");

            // **Postcondition**: Pengguna baru ditambahkan ke daftar pengguna
            User newUser = new User(username, password, role);
            users.Add(newUser);
            SaveUsersToFile();  // Simpan data pengguna ke file JSON
        }

        // **Design by Contract (DbC)** - Menambahkan postcondition setelah data disimpan
        private void SaveUsersToFile()
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFilePath, json);
        }
    }
}
