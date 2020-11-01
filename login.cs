using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SEHS
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void usernameLabel1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query;
            string UID = $"{textBox1.Text}";
            string Password = $"{textBox2.Text}"; 

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                SqlCommand cmd = connection.CreateCommand();
                try
                {  
                    // same way
                    query = $"SELECT * FROM TFHR.dbo.Staff where UID='{UID}'";
                    // show where?
                    cmd.CommandText = query;
                    connection.Open();
                    cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(cmd.CommandText,
                               "SQL Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                }
                finally
                {
                    List<string> us = new List<string>();
                    List<string> ps = new List<string>();
                    string data = "";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // if wrong change to number, sleep for now
                        // use  quotes bracket for these key-indexed index.
                        us.Add(reader["UID"].ToString());
                        ps.Add(reader["Password"].ToString());
                    }
                    if (us.Contains(UID))
                    {
                        if(Password == ps[us.IndexOf(UID)])
                        {
                            MessageBox.Show($"Welcome! {UID}",
                            "Note",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                            this.Hide();
                            new Form1().Show();            
                        }
                        else
                        {
                            //wrong pw code3
                            MessageBox.Show("Wrong!");
                        }
                    } else
                    {
                        // Enter user not found code here.
                        // testing?

                        MessageBox.Show("Wrong UID");
                    }
                    cmd.Dispose();
                }
                connection.Close();
            }

            //Test

            // what's this
            if (textBox1.Text == "") {
                MessageBox.Show("Please enter your UID.");
            } else
            {
                // sql connection need tcf, this works like promise from ES6
                // but since C# is java based, so try catch is basically the things being here
                // just try to put things in tcf
                // otherwise the async will not work properly
                // tcf is an async function.
                // 
            }
        }
    }
}
