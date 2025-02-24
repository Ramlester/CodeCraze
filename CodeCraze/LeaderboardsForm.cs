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
    public partial class LeaderboardsForm : Form
    {
        private string username;

        public LeaderboardsForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        public LeaderboardsForm()
        {
            InitializeComponent();
            
        }

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-ILQIOSCA\\SQLEXPRESS;Initial Catalog=codecraze;Integrated Security=True");

        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("exec dbo.view_lb", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void LeaderboardsForm_Load(object sender, EventArgs e)
        {
            LoadLeaderboards();
        }
        void LoadLeaderboards()
        {
            try
            {
                SqlCommand com = new SqlCommand("exec dbo.view_lb", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.acc_Search '" + textBox1.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
