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
    public partial class FormAdd : Form
    {
        SqlRepository sqlRepository = new SqlRepository();
        public FormAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlRepository.Add((int)numericUpDown1.Value, textBox2.Text, textBox3.Text, dateTimePicker1.Value, textBox5.Text, textBox6.Text);
            this.Close();

        }
    }
}
