using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektIT4_2023
{
    public class SqlRepository
    {
        private string cString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZaverecnaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<User> GetUsers()
        {
            
            List<User> users = new List<User>();
            
            
            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [User]";
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User((int)reader["Id"]
                                             , reader["Nickname"].ToString()
                                             , (byte[])reader["PasswordHash"]
                                             , (byte[])reader["PasswordSalt"]
                                             , reader["Role"].ToString()
                                             );
                            users.Add(user);
                        }
                    }
                }
                sqlConnection.Close();
            }
            return users;
        }
        public void CreateUser(string nickname, string password)
        {
            byte[] salt, hash;

            HMACSHA512 hmac = new HMACSHA512();

            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            salt = hmac.Key;


            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO [User] (Nickname, PasswordHash, PasswordSalt) VALUES (@nickname, @hash, @salt)";
                    sqlCommand.Parameters.AddWithValue("nickname", nickname);
                    sqlCommand.Parameters.AddWithValue("hash", hash);
                    sqlCommand.Parameters.AddWithValue("salt", salt);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }

        }
        public List<User> GetUsers(string searchString)
        {
            List<User> users = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM User WHERE Nickname LIKE @Search";
                    sqlCommand.Parameters.AddWithValue("Search", "%" + searchString + "%");
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User((int)reader["Id"]
                                             , reader["Username"].ToString()
                                             , (byte[])reader["PasswordHash"]
                                             , (byte[])reader["PasswordSalt"]
                                             , reader["Role"].ToString());
                            users.Add(user);
                        }
                    }
                }
                sqlConnection.Close();
            }
            return users;
        }
        public User GetUser(string nickname)
        {
            User user = null;
            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "select * from [User] where Nickname=@Nickname";
                    sqlCommand.Parameters.AddWithValue("Nickname", nickname);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User((int)reader["Id"],reader["Nickname"].ToString(), (byte[])reader["PasswordHash"], (byte[])reader["PasswordSalt"], reader["Role"].ToString());
                        }
                    }
                }
                sqlConnection.Close();
            }
            return user;
        }
    }
}
