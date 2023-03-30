using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaverecnyProjektIT4_2023
{
    public partial class FormRegistrace : Form
    {
        SqlRepository sqlRepository = new SqlRepository();
        SqlConnection con = new SqlConnection();
        public FormRegistrace()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlRepository.CreateUser(nicknameTB.Text, passwordTB.Text);
            this.Close();
        }
    }
}
