﻿using System;
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
        SqlRepository sql;
        public FormMain()
        {
            InitializeComponent();
            sql = new SqlRepository();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            users = sql.GetUsers();
            listView1.Items.Clear();
            foreach (var user in users)
            {
                listView1.Items.Add(user.ToListViewItem());
            }
        }
    }
}
