using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCraze
{
   
    public partial class main : Form
    {
        
        public int UserID { get; set; }
        private string username;
        public static main Instance { get; private set; }
        public main()
        {
            InitializeComponent();
            
        }
        public main(string username)
        {
            InitializeComponent();
            Instance = this;
            this.username = username;
            label4.Text =  username;
        }

        private void main_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuizForm f4 = new QuizForm(username);
            f4.ShowDialog();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AccountForm f5 = new AccountForm(username);
            f5.ShowDialog();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Sign out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                RegisterForm f1 = new RegisterForm();
                f1.ShowDialog();
            }
            if (result == DialogResult.No)
            {
               
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LeaderboardsForm f6 = new LeaderboardsForm(username);
            f6.ShowDialog();
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
