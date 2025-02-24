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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CodeCraze
{
    public partial class RegisterForm : Form
    {
       
        public RegisterForm()
        {
            InitializeComponent();
            
        }


 
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-ILQIOSCA\\SQLEXPRESS;Initial Catalog=codecraze;Integrated Security=True");

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Registration failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == textBox3.Text)
            {
                try
                {
                    con.Open();

                    using (SqlCommand com = new SqlCommand("add_loginfrm", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@username", textBox1.Text);
                        com.Parameters.AddWithValue("@pw", textBox2.Text);
                        com.ExecuteNonQuery();
                    }

                    using (SqlCommand com2 = new SqlCommand("add_Users", con))
                    {
                        com2.CommandType = CommandType.StoredProcedure;
                        com2.Parameters.AddWithValue("@Username", textBox1.Text);
                        com2.ExecuteNonQuery();
                    }

                    MessageBox.Show("Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("Password does not match, please re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '•';
                textBox3.PasswordChar = '•';
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2(textBox1.Text);
            f2.ShowDialog();
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
