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

namespace Wind_Forms
{
    public partial class Login : Form
    {
        private db database = new db();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Call the method to verify login credentials
            bool isValidUser = database.VerifyLogin(username, password);

            if (isValidUser)
            {
                // Successful login, open the main form or perform other actions
                this.Hide();
                Form1 mdi = new Form1();
                mdi.Show();
            }
            else
            {
                // Invalid login, show an error message
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}