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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const string str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=D:\面試\德義\TEST2\TEST2\APP_DATA\DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection con = new SqlConnection(str);
        DataTable dt = new DataTable();
        private void Load_data_Click(object sender, EventArgs e)
        {

            //System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(str);
            //open a database
            
            string sql = "SELECT * FROM [dbo].[member]";
            SqlCommand da = new SqlCommand(sql,con);
            try {
                con.Open();
                SqlDataReader sdr=da.ExecuteReader();
                dt.Load(sdr);
               
                dataGridView1.DataSource = dt;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            if (con.State == ConnectionState.Open) { con.Close(); }
        }

        private void button1_Click(object sender, EventArgs e) //Insert
        {
            if (Isvalid())
            {
                string sql = "Insert Into [dbo].[member] " +
                    "Values(@name,@gender,@phone,@address)";
                SqlCommand da = new SqlCommand(sql, con);
                da.Parameters.AddWithValue("@name", textBox2.Text);
                da.Parameters.AddWithValue("@gender", comboBox1.SelectedText);
                da.Parameters.AddWithValue("@phone", textBox4.Text);
                da.Parameters.AddWithValue("@address", textBox5.Text);
                try
                {
                    con.Open();
                    da.ExecuteNonQuery();
                    
                    con.Close();
                    Load_data_Click(null, null);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }

        private bool Isvalid()
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Name cant not be empty");
                return false;
            }
            else return true;
        }

       
    }
}
