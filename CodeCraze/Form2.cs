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
    public partial class Form2 : Form
    {

        private string username;
        public Form2()
        {   
            InitializeComponent();  
        }

        public Form2(string username)
        {
            InitializeComponent();
            this.username = username;
            label5.Text = username;
        }


        SqlConnection con = new SqlConnection("Data Source=LAPTOP-ILQIOSCA\\SQLEXPRESS;Initial Catalog=codecraze;Integrated Security=True");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT * FROM loginfrm where username = '" + Username.Text + "' AND pw = '" + password.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {

                this.Hide();
                main f3 = new main(Username.Text);
                f3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Log In Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Username.Text = "";
                password.Text = "";
                Username.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Username.Text = "";
            password.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            RegisterForm f1 = new RegisterForm();
            f1.ShowDialog();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                password.PasswordChar = '\0';
            }
            else
            {
                password.PasswordChar = '•';
            }  
        }
     
    }
}
