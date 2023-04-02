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
    public partial class FormMain : Form
    {
        private List<User> users;
        private List<Employee> employees;
        SqlRepository sqlRepository;
        public FormMain()
        {
            InitializeComponent();
            sqlRepository = new SqlRepository();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadEmployees();
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
        private void LoadEmployees()
        {
            employees = sqlRepository.GetEmployees();
            listView2.Items.Clear();
            foreach (var employee in employees)
            {
                listView2.Items.Add(employee.EmployeeToListViewItem());
            }
        }
    }
}
