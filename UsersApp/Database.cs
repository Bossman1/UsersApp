using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UsersApp
{


    internal static class Database
    {
        private static readonly string DbPath = "users_data.db";
        private static readonly string ConnectionString =  "Data Source=users_data.db;Version=3;";

        public static void Initialize()
        {
            if (!File.Exists(DbPath))
                SQLiteConnection.CreateFile(DbPath);

            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(@"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Login TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Password TEXT NOT NULL
                    );", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AddUser(User user)
        {
            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Users (Login, Email, Password) VALUES (@l,@e,@p)", con))
                {
                    cmd.Parameters.AddWithValue("@l", user.Login);
                    cmd.Parameters.AddWithValue("@e", user.Email);
                    cmd.Parameters.AddWithValue("@p", PasswordHelper.HashPassword(user.Password));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool ValidateUser(string login, string password)
        {
            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(1) FROM Users WHERE Login = @l AND Password = @p", con))
                {
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", PasswordHelper.HashPassword(password));
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        public static List<User> GetUsers()
        {
            var users = new List<User>();

            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT Id, Login, Email, Password FROM Users ORDER BY Id DESC", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),          // Id
                                Login = reader.GetString(1),      // Login
                                Email = reader.GetString(2),      // Email
                                Password = reader.GetString(3)    // Password
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }




        internal static class PasswordHelper
        {
            public static string HashPassword(string password)
            {
                using (var sha = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    byte[] hash = sha.ComputeHash(bytes);

                    var sb = new StringBuilder();
                    foreach (byte b in hash)
                        sb.Append(b.ToString("x2"));

                    return sb.ToString();
                }
            }
        }

    }
}
