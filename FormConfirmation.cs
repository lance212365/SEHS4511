using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace SEHS
{
    public partial class FormConfirmation : Form
    {
        List<string> validate;
        public static byte[] KEY = Encoding.ASCII.GetBytes(Properties.Settings.Default.key);
        public static byte[] IV = Encoding.ASCII.GetBytes(Properties.Settings.Default.iv);
        public TFHREntities ctx = new TFHREntities();
        UserControl1 uc1;
        Formcreate fce;
        public FormConfirmation(Formcreate fc,UserControl1 uc,List<string> valis)
        {
            InitializeComponent();
            uc1 = uc;
            fce = fc;
            validate = valis;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void FormConfirmation_Load(object sender, EventArgs e)
        {
            flnamev.Text = $"{validate[0]} {validate[1]} {validate[2]}";
            centrev.Text = $"{validate[3]}";
            trainerv.Text = $"{validate[4]}";
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            var getter = new Regex(@"\d+");
            var latestuid = ctx.Staff.OrderByDescending(o => o.UID).Select(s => s.UID).FirstOrDefault().ToString();
            var count = (Convert.ToInt32(getter.Match(latestuid).Value)+1).ToString();
            var password = validate[5];

            var title = ctx.Title.Where($"Title1 = \"{validate[0]}\"").Select(s => s.TitleID).FirstOrDefault().ToString();
            var role = ctx.Role.Where($"RoleName = \"{validate[4]}\"").Select(s => s.RoleID).FirstOrDefault().ToString();
            var centerid = ctx.CostCentre.Where($"CentreName = \"{ validate[3]}\"").Select(s => s.CentreID).FirstOrDefault().ToString();

            var staff = new Staff()
            {
                UID = $"UID{count}",
                FirstName = validate[1],
                LastName = validate[2],
                Title = title,
                Role = role,
                CentreID = centerid,
                Status = "active"
            };
            var userlogin = new UserLogin() {

                UID = $"UID{count}",
                Password = Convert.ToBase64String(Encrypt(password, KEY, IV))
            };
            ctx.Staff.Add(staff);
            ctx.UserLogin.Add(userlogin);
            ctx.SaveChanges();
            MessageBox.Show("User Inserted.");
            uc1.tableChange(null, null);
            fce.Close();
            Close();
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
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
}
