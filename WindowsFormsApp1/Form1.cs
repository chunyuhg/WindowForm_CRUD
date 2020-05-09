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
            Get_data();
        }
        const string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Database\membership\Database1.mdf;Integrated Security=True;Connect Timeout=30";
       
        SqlConnection con = new SqlConnection(str);
        DataTable dt = new DataTable();
        private int member_id;
        //Insert
        private void button1_Click(object sender, EventArgs e) 
        {
            if (Isvalid())
            {
                string sql = "Insert Into [dbo].[member] " +
                    "Values(@name,@gender,@phone,@address)";
                SqlCommand da = new SqlCommand(sql, con);
                da.Parameters.AddWithValue("@name", textBox2.Text);
                da.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                da.Parameters.AddWithValue("@phone", textBox4.Text);
                da.Parameters.AddWithValue("@address", textBox5.Text);
                try
                {
                    con.Open();
                    da.ExecuteNonQuery();

                    con.Close();
                    Get_data();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                if (con.State == ConnectionState.Open) { con.Close(); }
                
            }
        }
        //Update
        private void Update_Click(object sender, EventArgs e) {
            if (Isvalid())
            {
                string i = comboBox1.SelectedItem.ToString();
                string sql = "Update [dbo].[member] " +
                    "Set name=@name,gender=@gender,phone=@phone,address=@address  "+
                    "where member_id=@member_id";
                SqlCommand da = new SqlCommand(sql, con);
                da.Parameters.AddWithValue("@name", textBox2.Text);
                da.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                da.Parameters.AddWithValue("@phone", textBox4.Text);
                da.Parameters.AddWithValue("@address", textBox5.Text);
                da.Parameters.AddWithValue("@member_id", member_id);
                try
                {
                    con.Open();
                    da.ExecuteNonQuery();

                    con.Close();
                    Get_data();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                if (con.State == ConnectionState.Open) { con.Close(); }
              
            }
        }
        //Delete
        private void button2_Click(object sender, EventArgs e)
        {
            if (member_id > 0)
            {
                string sql = "Delete from [dbo].[member] " +
                    "where member_id=@member_id";
                SqlCommand da = new SqlCommand(sql, con);

                da.Parameters.AddWithValue("@member_id", member_id);
                try
                {
                    con.Open();
                    da.ExecuteNonQuery();

                    con.Close();
                    Get_data();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                if (con.State == ConnectionState.Open) { con.Close(); }
             
            }
        }
        //Reset
        private void button3_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
        private void Get_data()
        {
            
            string sql = "SELECT * FROM [dbo].[member]";
            SqlCommand da = new SqlCommand(sql,con);
            try {
                con.Open();
                
                SqlDataReader sdr=da.ExecuteReader();
                dt.Clear();
                dt.Load(sdr);
               
                dataGridView1.DataSource = dt;
                if (dataGridView1.RowCount > 0) { dataGridView1.Rows[0].Selected = true; }
     
                dataGridView1_CellClick(null, null);
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            if (con.State == ConnectionState.Open) { con.Close(); }
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

        private void ResetForm() {

            member_id = 0;
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount < 0) return;

            int i = dataGridView1.SelectedCells[0].RowIndex;
            member_id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
            textBox5.Text = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
            comboBox1.SelectedIndex= (dataGridView1.Rows[i].Cells[2].Value.ToString() == "F")? 0 : 1 ;
        }
       
    }
}
