using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektIT4_2023
{
    class User
    {
        public int Id { get; }
        public string Username { get; }
        public int Role { get; set; }
        public byte[] PasswordHash { get; internal set; }
        public byte[] PasswordSalt { get; internal set; }
        SqlRepository sqlRepository = new SqlRepository();

        public User(int id, string username, string password, int role)
        {
            Id = id;
            Username = username;
            Role = role;
            sqlRepository.CreateUser(username, password);
        }

        public User(int id, string username, byte[] passwordHash, byte[] passwordSalt, int role)
        {
            Id = id;
            Username = username;
            Role = role;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
