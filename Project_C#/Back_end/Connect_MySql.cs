using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // Add this using directive

namespace Testing_connect_MySql
{
    internal class Connect_MySql
    {
        private string server = "localhost";
        private string database = "testdb";
        private string user = "root";
        private string password = "";
        private string port = "3306";

        public string ConnectionString
        {
            get
            {
                return $"Server={server};Database={database};User ID={user};Password={password};Port={port};";
            }
        }

        public void Connect()
        {
            using (var connection = new MySqlConnection(ConnectionString)) // Simplified namespace
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}