using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Back_end
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Nama { get; set; }

        public User(string username, string password, Role role, string nama)
        {
            Username = username;
            Password = password;
            Role = role;
            Nama = nama;
        }
    }
}
