using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

namespace SEHS
{
    public partial class login : Form
    {
        public static byte[] KEY = Encoding.ASCII.GetBytes(Properties.Settings.Default.key);
        public static byte[] IV = Encoding.ASCII.GetBytes(Properties.Settings.Default.iv);
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
            using (TFHREntities ctx = new TFHREntities())
            {
                if (UID == "" || Password == "")
                {
                    MessageBox.Show("Please enter your UID and Password.");
                }
                else
                {
                    var info = ctx.UserLogin.Where(w => w.UID == UID).Select(s => s).FirstOrDefault();
                    if (info == null)
                    {
                        MessageBox.Show("Wrong UID");
                    }
                    else
                    {
                        var realpw = Decrypt(Convert.FromBase64String(info.Password), KEY, IV);
                        if (info.UID == UID && realpw == Password)
                        {
                            Staff user = ctx.Staff.Where(w => w.UID == UID).Select(s => s).FirstOrDefault();
                            Log l = new Log
                            {
                                StaffID = user.UID,
                                DateTime = DateTime.Now,
                                Type = "login",
                                Detail = "Login Success",
                                Host = GetLocalIPAddress()
                            };
                            ctx.Log.Add(l);
                            ctx.SaveChanges();
                            string name = user.FirstName;
                            string sur = user.LastName;
                            MessageBox.Show($"Welcome! {name}!",
                            "Note",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                            this.Hide();
                            Form1 Form = new Form1();
                            Form.buttonUserName.Text = $"{name} {sur}";
                            Form.ShowDialog();
                            cTextBox1.Text = null;
                            cTextBox2.Text = null;
                            this.Show();
                        }
                        else if (realpw != Password)
                        {
                            MessageBox.Show("Wrong Password!");
                        }
                    }
                }
            }
        }

        private void cTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void cTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // sql connection need tcf, this works like promise from ES6
                // but since C# is java based, so try catch is basically the things being here
                // just try to put things in tcf
                // otherwise the async will not work properly
                // tcf is an async function.
                // 
                button1.PerformClick();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

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
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
    public static class MD5Extensions
    {
        public static string ToMD5(this string str)
        {
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將字串編碼成 UTF8 位元組陣列
                var bytes = Encoding.UTF8.GetBytes(str);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();

                return md5;
            }
        }
    }
}

//using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
//{
//    SqlCommand cmd = connection.CreateCommand();
//    try
//    {
//        query = $"SELECT * FROM TFHR.dbo.Staff where UID='{UID}'";
//        cmd.CommandText = query;
//        connection.Open();
//        cmd.ExecuteScalar();
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(cmd.CommandText,
//                   "SQL Error",
//                   MessageBoxButtons.OK,
//                   MessageBoxIcon.Error);
//    }
//    finally
//    {
//        List<string> us = new List<string>();
//        List<string> ps = new List<string>();
//        string name = "";
//        SqlDataReader reader = cmd.ExecuteReader();
//        while (reader.Read())
//        {
//            us.Add(reader["UID"].ToString());
//            ps.Add(reader["Password"].ToString());
//            name = reader["FirstName"].ToString();
//        }
//        if (us.Contains(UID))
//        {
//            if (Password == ps[us.IndexOf(UID)])
//            {
//            }
//            else
//            {
//                MessageBox.Show("Wrong Password!");
//            }
//        }
//        else
//        {
//            // Enter user not found code here.
//            // testing?

//            MessageBox.Show("Wrong UID");
//        }
//        cmd.Dispose();
//    }
//    connection.Close();
//}

//Test

// what's this