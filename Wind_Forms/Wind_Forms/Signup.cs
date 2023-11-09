using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wind_Forms
{
    public partial class Signup : Form
    {
        db data = new db();
        public Signup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Clear();
            txtUsername.Focus();
        }
        private void Signup_Activated(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void Signup_Load(object sender, EventArgs e)
        {
            data.load_data("SELECT * FROM tblUser", listView1);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0){
                txtId.Text = listView1.SelectedItems[0].Text;
                txtUsername.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtPassword.Text = listView1.SelectedItems[0].SubItems[2].Text;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string qryCreate = "INSERT into tblUser values('" + txtUsername.Text + "', '" + txtPassword.Text + "')"; 
            string qryUpdate = "UPDATE tblUser set Email = '" + txtUsername.Text + "', Password = '" + txtPassword.Text + "' WHERE id = '" + txtId.Text +"'";
            data.saveData(qryCreate, qryUpdate, txtId);
            data.load_data("SELECT * from tblregister", listView1);
            txtId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (txtSearch.Text != "")
            {
                string srch = "SELECT * from tblUser WHERE Id LIKE '%" + txtSearch.Text + "%'";
                data.load_data(srch, listView1);
            }
            else
            {
                data.load_data("SELECT * FROM tblUser", listView1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            data.delete("DELETE FROM tblUser WHERE ID = " + txtId.Text);
            data.load_data("SELECT * FROM tblUser", listView1);
            txtId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
