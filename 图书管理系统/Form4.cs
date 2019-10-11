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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        int book_count, book_id, clickRowIndex, student_count;
        string  sql,sql2, stusql, stusql2, usql3,book_id1,book_id2,book_id3;

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new 图书管理系统.Form3();
            form3.Show();
            this.Visible = false;
        }

        DataTable dt;
            
        SqlConnection connection;
        string conn = "Data source=LAPTOP-D2TETLLC\\SQLEXPRESS01;Initial Catalog=library;integrated Security=true";
        private void Form4_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(conn);
            connection.Open();
            
            sql = string.Format("select book_id1,book_id2,book_id3 from student_information where Student_ID = '{0}' ", Form1.Student_ID);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
            dt = new DataTable();
            dataadapter.Fill(dt);
             book_id1 = dt.Rows[0][0].ToString();
             book_id2 = dt.Rows[0][1].ToString();
             book_id3 = dt.Rows[0][2].ToString();

            sql2 = string.Format("select book_ID, book_name,book_writer from book_information where book_ID ='{0}' or book_ID ='{1}' or book_ID ='{2}'   ", book_id1, book_id2, book_id3);
            SqlDataAdapter dataadapter2 = new SqlDataAdapter(sql2, connection);
            DataSet dataset = new DataSet();
            dataadapter2.Fill(dataset);
            try
            {
                dataGridView1.Columns[0].DataPropertyName = "book_ID";
                dataGridView1.Columns[1].DataPropertyName = "book_name";
                dataGridView1.Columns[2].DataPropertyName = "book_writer";
                dataGridView1.DataSource = dataset.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "显示失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           if (dt.Rows[0][0].ToString() == ""|| dt.Rows[0][0] ==null)
            {
                MessageBox.Show("请选择图书", "欢迎使用图书馆查询借阅系统", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                
                this.Close();
                Form3 form3 = new 图书管理系统.Form3();
                form3.Show();
            }
            else
            {
                book_id = (int)dataGridView1.Rows[0].Cells["Column1"].Value;
                connection.Close();
            }
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
           
                book_id = (int)dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value;

                clickRowIndex = e.RowIndex;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sql2 = string.Format("select book_ID, book_name,book_writer from book_information where book_ID ='{0}' or book_ID ='{1}' or book_ID ='{2}'   ", book_id1, book_id2, book_id3);
            SqlDataAdapter dataadapter2 = new SqlDataAdapter(sql2, connection);
            DataSet dataset = new DataSet();
            dataadapter2.Fill(dataset);

            stusql = string.Format("select counts from student_information where Student_ID = '{0}' ", Form1.Student_ID);
            connection = new SqlConnection(conn);
            connection.Open();
            SqlCommand scount = new SqlCommand(stusql, connection);
            student_count = (int)scount.ExecuteScalar();

            stusql2 = string.Format("select book_count from book_information where book_ID = '{0}' ",book_id );
            SqlCommand bcount = new SqlCommand(stusql, connection);
            book_count = (int)bcount.ExecuteScalar();

            student_count--;
            book_count++;

            string usql = string.Format("update book_information  set book_count='{0}' where book_ID='{1}' ", book_count, book_id);
            SqlCommand sqlcon = new SqlCommand(usql, connection);
            sqlcon.ExecuteNonQuery();
            sqlcon.Dispose();

            string usql2 = string.Format("update student_information  set counts='{0}' where Student_ID='{1}' ", student_count, Form1.Student_ID);
            SqlCommand sqlcon2 = new SqlCommand(usql2, connection);
            sqlcon2.ExecuteNonQuery();
            sqlcon2.Dispose();
            

            if (book_id1 == "")
            {
                book_id1 = "0";
            }
             if(book_id2 == "")
            {
                book_id2 = "0";
            }
            if(book_id3 == "")
            {
                book_id3= "0";
            }

            if (Convert.ToInt32 (book_id1)==book_id)
            {
                 usql3 = string.Format("update student_information  set  book_id1=''  where  Student_ID='{0}'  ", Form1.Student_ID);
               
            }
            if(Convert.ToInt32(book_id2) == book_id)
            {
                usql3 = string.Format("update student_information  set  book_id2=''  where  Student_ID='{0}'  ", Form1.Student_ID);
             
            }
            if(Convert.ToInt32(book_id3) == book_id)
            {
                usql3 = string.Format("update student_information  set  book_id3=''  where  Student_ID='{0}'  ", Form1.Student_ID);
            
            }
         
            SqlCommand sqlcon3 = new SqlCommand(usql3, connection);
            sqlcon3.ExecuteNonQuery();
            sqlcon3.Dispose();
            dataGridView1.Rows.RemoveAt(clickRowIndex);
            connection.Close();
          
            
        }
     
    }
}
