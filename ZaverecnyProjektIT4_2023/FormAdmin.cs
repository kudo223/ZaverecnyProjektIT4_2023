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
    public partial class FormAdmin : Form
    {
        private List<User> users;
        SqlRepository sqlRepository;
        
        public FormAdmin()
        {
            InitializeComponent();
            sqlRepository = new SqlRepository();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            users = sqlRepository.GetUsers();
            listView1.Items.Clear();
            foreach (var user in users)
            {
                listView1.Items.Add(user.UserToListViewItem());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("vyberte řádek");
            }
            else
            {
                var selectedRow = listView1.SelectedItems[0];
                var columnName = listView1.Columns[0].Text;
                string selectedValue = selectedRow.SubItems[0].Text;
                selectedValue = columnName.Replace(" ", "");
                var id = selectedRow.SubItems[0].Text;

                sqlRepository.Delete("[User]", selectedValue, id);
                listView1.SelectedItems[0].Remove();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
