﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 图书管理系统
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new 图书管理系统.Form2 ();
            form2.Show ();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new 图书管理系统.Form4();
            form4.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new 图书管理系统.Form1();
            form1.Show();
            this.Visible = false;
        }
    }
}
