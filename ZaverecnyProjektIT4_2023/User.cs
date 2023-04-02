using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaverecnyProjektIT4_2023
{
    public class User
    {
        public int Id { get; }
        public string Nickname { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; internal set; }
        public byte[] PasswordSalt { get; internal set; }
        SqlRepository sqlRepository = new SqlRepository();



        public User(int id, string nickname, byte[] passwordHash, byte[] passwordSalt, string role)
        {
            Id = id;
            Nickname = nickname;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Role = role;
        }
        public ListViewItem ToListViewItem()
        {
            return new ListViewItem(new string[] { Id.ToString(), Nickname, Role });
        }
        public bool VerifyPassword(string password)
        {
            byte[] hash;
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            return hash.SequenceEqual(PasswordHash);
        }
    }
}
