using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 图书管理系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string Student_ID; // 注意，必须申明为static变量
  
        private   void button1_Click(object sender, EventArgs e)
        {
            string con = "Data source=LAPTOP-D2TETLLC\\SQLEXPRESS01;Initial Catalog=library;integrated Security=true";
            SqlConnection connection = new SqlConnection(con);
             Student_ID = textBox1.Text.Trim ();
             string Name = textBox2.Text.Trim ();
            string Password = textBox3.Text.Trim ();
            string sql = string.Format("select count (* )from student_information where Student_ID='{0}' and Name='{1}' and Password='{2}' ", Student_ID, Name, Password);
            
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                int num = (int)command.ExecuteScalar();
                if (num > 0)
                {
                    MessageBox.Show("登陆成功", "欢迎使用图书馆查询借阅系统", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Visible = false;

                }
                else
                {
                    MessageBox.Show("登陆失败" + "请重新输入", "欢迎使用图书馆查询借阅系统", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("登录失败" + ex.Message, "欢迎使用图书馆查询借阅系统", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
 
