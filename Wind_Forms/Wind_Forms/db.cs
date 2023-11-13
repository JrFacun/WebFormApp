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
            // Initialize a command object with the database connection
            cmd.Connection = conn;

            // Create a SqlCommand to check if a record with the provided ID exists in the 'tblUser' table
            SqlCommand cmdCheckId = new SqlCommand("select * from tblUser where id = '" + txtid.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdCheckId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Check if any records match the provided ID
            if (dt.Rows.Count > 0)
            {
                conn.Open();
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = sqlupdate; // SQL command for updating a record

                cmd1.ExecuteNonQuery(); // Execute the SQL update command
                conn.Close();
                MessageBox.Show("Record Updated");
            }
            else
            {
                conn.Open();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = sqlcreate; // SQL command for creating a new record

                cmd2.ExecuteNonQuery(); // Execute the SQL create command
                conn.Close();
                MessageBox.Show("Record Added");
            }

            conn.Close();
        }

        public void load_data(string q, ListView lv)
        {
            lv.Items.Clear();
            try
            {
                SqlDataReader dr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = q; // The 'q' parameter contains the SQL query

                conn.Open();
                dr = cmd.ExecuteReader();

                // Check if there are rows returned by the query
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        // Create a ListViewItem to display the retrieved data in a ListView
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["Username"].ToString());
                        item.SubItems.Add(dr["Password"].ToString());
                        lv.Items.Add(item); // Add the ListViewItem to the ListView
                    }
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception e)
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
            conn.Open(); // Open the database connection

            // Create a SqlCommand to check if a user with the provided username and password exists in the 'tblUser' table
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblUser WHERE Username = @Username AND Password = @Password", conn);

            // Add parameters to the SqlCommand to prevent SQL injection
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            // Execute the SQL query and retrieve data
            SqlDataReader dr = cmd.ExecuteReader();

            // Check if the SqlDataReader has rows, indicating a valid user
            bool isValidUser = dr.HasRows;

            dr.Close(); // Close the SqlDataReader
            conn.Close(); // Close the database connection
            return isValidUser; // Return whether the user is valid (true) or not (false)
        }
    }
}
