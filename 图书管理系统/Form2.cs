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
   
    public partial class Form2 : Form
    {
        


        public Form2()
        {
            InitializeComponent();
        }
        int book_count, book_id, clickRowIndex, student_count;
        string keyword, sql, stusql, usql3;
        SqlConnection connection;
        string conn = "Data source=LAPTOP-D2TETLLC\\SQLEXPRESS01;Initial Catalog=library;integrated Security=true";

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new 图书管理系统.Form3();
            form3.Show();
            this.Visible = false;
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            sql = string.Format("select * from book_information where book_name like '%{0}%' or  book_writer like   '%{1}%' ", keyword, keyword);
            stusql = string.Format("select counts from student_information where Student_ID = '{0}' ", Form1 .Student_ID );
            connection = new SqlConnection(conn);
            connection.Open();
             SqlCommand scount = new SqlCommand(stusql, connection);
             student_count = (int)scount.ExecuteScalar();
          
            try
            {

                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
                DataSet dataset = new DataSet("library");
                dataadapter.Fill(dataset);
                dataGridView1.Columns[0].DataPropertyName = "book_ID";
                dataGridView1.Columns[1].DataPropertyName = "book_name";
                dataGridView1.Columns[2].DataPropertyName = "book_writer";
                dataGridView1.Columns[3].DataPropertyName = "book_count";
                dataGridView1.DataSource = dataset.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "显示失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            book_count = (int)dataGridView1.Rows[0].Cells["Column4"].Value;//若第一行就说所选项可不点击，直接借阅
            book_id = (int)dataGridView1.Rows[0].Cells["Column1"].Value;
            connection.Close();
        }

 

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
            if (e.RowIndex >= 0)
            {
                book_count = (int)dataGridView1.Rows[e.RowIndex].Cells["Column4"].Value;
                book_id = (int)dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value;       
                clickRowIndex = e.RowIndex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            if (book_count > 0)
            {
                book_count--;           
            }
            else
            {
                MessageBox.Show("暂无库存", "借阅失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (student_count < 3)
            {
                student_count++;

                connection.Open();
                Form1 form1 = new Form1();
                string usql = string.Format("update book_information  set book_count='{0}' where book_ID='{1}' ", book_count, book_id);
                SqlCommand sqlcon = new SqlCommand(usql, connection);
                sqlcon.ExecuteNonQuery();
                sqlcon.Dispose();

                string usql2 = string.Format("update student_information  set counts='{0}' where Student_ID='{1}' ", student_count, Form1.Student_ID);
                SqlCommand sqlcon2 = new SqlCommand(usql2, connection);
                sqlcon2.ExecuteNonQuery();
                sqlcon2.Dispose();



                string nsql = string.Format("select * from student_information where Student_ID='{0}'", Form1.Student_ID);
                SqlDataAdapter data = new SqlDataAdapter(nsql, connection);
                DataTable dt = new DataTable();
                data.Fill(dt);
                
                if (dt.Rows[0].IsNull("book_id1" ))
                {
                   usql3 = string.Format("update student_information  set book_id1='{0}' where Student_ID='{1}' ", book_id, Form1.Student_ID);
                }
               else if (dt.Rows[0].IsNull("book_id2"))
                    {
                  usql3 = string.Format("update student_information  set book_id2='{0}' where Student_ID='{1}' ", book_id, Form1.Student_ID);
                }
                else if (dt.Rows[0].IsNull("book_id3"))
                    {
                 usql3 = string.Format("update student_information  set book_id3='{0}' where Student_ID='{1}' ", book_id, Form1.Student_ID);
                }


                if(dt.Rows [0]["book_id1"].ToString() == "")
                {
                    usql3 = string.Format("update student_information  set book_id1='{0}' where Student_ID='{1}' ", book_id, Form1.Student_ID);
                }
                else if (dt.Rows[0]["book_id2"].ToString() == "")
                {
                    usql3 = string.Format("update student_information  set book_id2='{0}' where Student_ID='{1}' ", book_id, Form1.Student_ID);
                }
                else if (dt.Rows[0]["book_id3"].ToString() == "")
                {
                    usql3 = string.Format("update student_information  set book_id3='{0}' where Student_ID='{1}' ", book_id, Form1.Student_ID);
                }

                SqlCommand sqlcon3 = new SqlCommand(usql3, connection);
                sqlcon3.ExecuteNonQuery();
                sqlcon3.Dispose();
                try
                {
                    dataGridView1.Rows[clickRowIndex].Cells["Column4"].Value = book_count;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "显示失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("借书数量超出额度", "借阅失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
         
             
            }
        
         }
               
         
            
        }

      

