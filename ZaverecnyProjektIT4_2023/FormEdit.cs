using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaverecnyProjektIT4_2023
{
    public partial class FormEdit : Form
    {
        SqlRepository sqlRepository = new SqlRepository();
        public FormEdit(User user)
        {
            this.user = user;
            InitializeComponent();
            textBox1.Text = user.Id.ToString();
            textBox2.Text = user.Nickname.ToString();
            textBox3.Text = user.PasswordHash.ToString();
            textBox4.Text = user.PasswordSalt.ToString();
            textBox5.Text = user.Role.ToString();
        }

        private User user;

        private void button1_Click(object sender, EventArgs e)
        {
            var user = new User(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            sqlRepository.Edit(user);
        }
    }
}
