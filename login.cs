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
            cTextBox1.BackColor = this.BackColor;
            cTextBox2.BackColor = this.BackColor;     

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query;
            string UID = $"{cTextBox1.Text}";
            string Password = $"{cTextBox2.Text}";

            // what's this
            if (cTextBox1.Text == "")
            {
                MessageBox.Show("Please enter your UID.");
            } else if (cTextBox2.Text == "") {
                MessageBox.Show("Please enter your Password.");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    try
                    {
                        query = $"SELECT * FROM TFHR.dbo.Staff where UID='{UID}'";
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
                        string name = "";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            us.Add(reader["UID"].ToString());
                            ps.Add(reader["Password"].ToString());
                            name = reader["FirstName"].ToString();
                        }
                        if (us.Contains(UID))
                        {
                            if (Password == ps[us.IndexOf(UID)])
                            {
                                MessageBox.Show($"Welcome! {name}!",
                                "Note",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                                this.Hide();
                                Form1 form1 = new Form1();
                                form1.ShowDialog();
                                form1 = null;
                                Show();
                            }
                            else
                            {
                                MessageBox.Show("Wrong Password!");
                            }
                        }
                        else
                        {
                            // Enter user not found code here.
                            // testing?

                            MessageBox.Show("Wrong UID");
                        }
                        cmd.Dispose();
                    }
                    connection.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void cTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void cTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
