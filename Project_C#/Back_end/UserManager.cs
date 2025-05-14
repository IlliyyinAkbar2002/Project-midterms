using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;  

namespace Project_C_.Back_end
{
    public class UserManager
    {
        private List<User> users;
        private string usersFilePath;

        public UserManager(string? filePath = null)
        {
            usersFilePath = filePath ?? "users.json"; // default jika tidak diset
            if (File.Exists(usersFilePath))
            {
                string json = File.ReadAllText(usersFilePath);
                users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
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
        public void Register(string username, string password, Role role, string nama, string nik, string rt, string rw)
        {
            // **Precondition**: Pastikan username dan password tidak kosong
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Username and password cannot be empty.");

            // **Precondition**: Pastikan username belum terdaftar
            if (users.Any(u => u.Username == username))
                throw new ArgumentException("Username already exists.");

            // **Precondition**: Pastikan nik belum terdaftar
            if (users.Any(u => u.NIK == nik))
                throw new ArgumentException("Nik sudah terpakai.");

            // **Postcondition**: Pengguna baru ditambahkan ke daftar pengguna
            User newUser = new User(username, password, role, nama, nik, rt, rw);
            users.Add(newUser);
            SaveUsersToFile();  // Simpan data pengguna ke file JSON
        }

        // **Design by Contract (DbC)** - Menambahkan postcondition setelah data disimpan
        private void SaveUsersToFile()
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFilePath, json);
        }

        // get a user by username
        public User GetUserByUsername(string username)
        {
            return users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public void SaveChanges()
        {
            SaveUsersToFile(); 
        }

        public User GetUserByNIK(string NIK)
        {
            return users.FirstOrDefault(u => u.NIK == NIK);
        }


    }
}
