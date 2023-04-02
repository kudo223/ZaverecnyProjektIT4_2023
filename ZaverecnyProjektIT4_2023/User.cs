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
        private int v1;
        private string text1;
        private string v2;
        private string text2;
        private string text3;

        public User(int id, string nickname, byte[] passwordHash, byte[] passwordSalt, string role)
        {
            Id = id;
            Nickname = nickname;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Role = role;
        }

        public User(int v1, string text1, string v2, string text2, string text3)
        {
            this.v1 = v1;
            this.text1 = text1;
            this.v2 = v2;
            this.text2 = text2;
            this.text3 = text3;
        }

        public ListViewItem UserToListViewItem()
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
