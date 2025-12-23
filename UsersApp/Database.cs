using System.Data.SQLite;
using System.IO;

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
                    cmd.Parameters.AddWithValue("@p", user.Password);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
