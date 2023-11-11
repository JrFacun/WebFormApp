using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Wind_Forms
{

    internal class db
    {
        public SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=web;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public string id, type, username, password;

        public void saveData(string sqlcreate, string sqlupdate, TextBox txtid)
        {
            cmd.Connection = conn; 
            
            SqlCommand cmdCheckId = new SqlCommand("select * from tblUser where id = '" + txtid.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdCheckId);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                conn.Open();
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;    
                cmd1.CommandText = sqlupdate;

                cmd1.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Updated");
            }
            else
            {
                conn.Open();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = sqlcreate;
                cmd2.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Record Added");

            }

            conn.Close();

        }

        public void load_data(string q, ListView lv) {
            lv.Items .Clear();
            try
            {
                SqlDataReader dr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = q;
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["Username"].ToString());
                        item.SubItems.Add(dr["Password"].ToString());
                        lv.Items.Add(item); 
                    }
                }
                dr.Close();
                conn.Close();
            }catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        public void delete(string del)
        {
            conn.Open();
            SqlCommand dele = new SqlCommand(del, conn);
            dele.ExecuteNonQuery();
            conn.Close( );
            MessageBox.Show("Record Deleted");
        }
        public bool VerifyLogin(string username, string password)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblUser WHERE Username = @Username AND Password = @Password", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataReader dr = cmd.ExecuteReader();

            bool isValidUser = dr.HasRows;

            dr.Close();
            conn.Close();
            return isValidUser;
        }
    }
}
