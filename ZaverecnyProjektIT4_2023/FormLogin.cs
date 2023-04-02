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
    public partial class FormLogin : System.Windows.Forms.Form
    {
        SqlRepository sql;
        public FormLogin()
        {
            InitializeComponent();
            sql = new SqlRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = sql.GetUser(nicknameTB.Text.Trim());

            if (user != null)
            {
                if (user.VerifyPassword(passwordTB.Text))
                {
                    if (user.Role == "admin")
                    {
                        this.Hide();
                        FormAdmin formAdmin = new FormAdmin();
                        formAdmin.Show();
                        return;
                    }
                    else
                    {
                        this.Hide();
                        FormMain formMain = new FormMain();
                        formMain.Show();
                        return;
                    }

                }


            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormRegistrace formRegistrace = new FormRegistrace();
            formRegistrace.ShowDialog();
        }
    
    }
}
