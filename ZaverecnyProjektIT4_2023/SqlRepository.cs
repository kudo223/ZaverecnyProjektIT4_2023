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
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var user = new User((int)sqlDataReader["Id"]
                                             , sqlDataReader["Nickname"].ToString()
                                             , (byte[])sqlDataReader["PasswordHash"]
                                             , (byte[])sqlDataReader["PasswordSalt"]
                                             , sqlDataReader["Role"].ToString()
                                             );
                            users.Add(user);
                        }
                    }
                }
                sqlConnection.Close();
            }
            return users;
        }
        public List<Employee> GetEmployees()
        {

            List<Employee> employees= new List<Employee>();


            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM [Employee]";
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var employee = new Employee((int)sqlDataReader["Id"]
                                             , (int)sqlDataReader["PersonalNumber"]
                                             , sqlDataReader["Firstname"].ToString()
                                             , sqlDataReader["Lastname"].ToString()
                                             , Convert.ToDateTime(sqlDataReader["BirthDate"])
                                             , sqlDataReader["Email"].ToString()
                                             , sqlDataReader["PhoneNumber"].ToString()
                                             );
                            employees.Add(employee);
                        }
                    }
                }
                sqlConnection.Close();
            }
            return employees;
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
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var user = new User((int)sqlDataReader["Id"]
                                             , sqlDataReader["Username"].ToString()
                                             , (byte[])sqlDataReader["PasswordHash"]
                                             , (byte[])sqlDataReader["PasswordSalt"]
                                             , sqlDataReader["Role"].ToString());
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
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            user = new User((int)sqlDataReader["Id"], sqlDataReader["Nickname"].ToString(), (byte[])sqlDataReader["PasswordHash"], (byte[])sqlDataReader["PasswordSalt"], sqlDataReader["Role"].ToString());
                        }
                    }
                }
                sqlConnection.Close();
            }
            return user;
        }
        public void Delete(string tableName, string columnName, string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = $"DELETE FROM {tableName} WHERE {columnName}={id}";
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public void Edit(int id, string nickname, string role)
        {
            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "UPDATE User" +
                        " SET Id=@id, Nickname=@nickname, Role=@role, ";
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@nickname", nickname);
                    sqlCommand.Parameters.AddWithValue("@role", role);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public void Add(int personalNumber, string firstname, string lastname, DateTime birthDate, string email, string phoneNumber)
        {
            using (SqlConnection sqlConnection = new SqlConnection(cString))
            {
                sqlConnection.Open();
                    string query = "INSERT INTO Employee (PersonalNumber, FirstName, LastName, BirthDate, Email, PhoneNumber) VALUES (@value1, @value2, @value3, @value4, @value5, @value6)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@value1", personalNumber);
                        sqlCommand.Parameters.AddWithValue("@value2", firstname);
                        sqlCommand.Parameters.AddWithValue("@value3", lastname);
                        sqlCommand.Parameters.AddWithValue("@value4", birthDate);
                        sqlCommand.Parameters.AddWithValue("@value5", email);
                        sqlCommand.Parameters.AddWithValue("@value6", phoneNumber);
                        sqlCommand.ExecuteNonQuery();
                    }
                sqlConnection.Close();
            }

        }
    }
}
