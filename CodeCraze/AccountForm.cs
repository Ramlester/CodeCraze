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

namespace CodeCraze
{
    public partial class AccountForm : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-ILQIOSCA\\SQLEXPRESS;Initial Catalog=codecraze;Integrated Security=True");
        
        private string username;
        private DataTable accountData;
        public AccountForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        public AccountForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                main.Instance.Show();
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];


                textBox1.Text = selectedRow.Cells["Username"].Value.ToString();
                textBox2.Text = selectedRow.Cells["First Name"].Value.ToString();
                textBox3.Text = selectedRow.Cells["Last Name"].Value.ToString(); 
            }
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            LoadAccount();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        void LoadAccount()
        {
            try
            {
                con.Open();

               
                SqlCommand com = new SqlCommand("exec dbo.view_Acc", con);

                SqlDataAdapter da = new SqlDataAdapter(com);
                accountData = new DataTable(); 

                da.Fill(accountData);

                DataView dv = accountData.DefaultView;
                dv.RowFilter = $"Username = '{username}'";

                dataGridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("exec dbo.update_Acc '" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully saved.");
                LoadAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    con.Open();

                    SqlCommand com = new SqlCommand("delete_Acc", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@username", textBox1.Text);

                    com.ExecuteNonQuery();
                    MessageBox.Show("Successfully deleted.");
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

                
                this.Close();
                RegisterForm registerForm = new RegisterForm();
                registerForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
