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
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;

namespace SEHS
{
    public partial class Form1 : Form
    {
        bool isToLogin = false;
        public Form1()
        {
            InitializeComponent();
            UserControl[] interfaceForm = new UserControl[] {
                userControl1,userControl2,userControl3,userControl4
            };
            hideAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideAllAndShow(0);
            using (TFHREntities ctx = new TFHREntities())
            {
                var stflist = ctx.Staff.ToList();
                userControl1.dataGridView2.DataSource = stflist;
            }
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    SqlCommand cmd = connection.CreateCommand();
            //    try
            //    {
            //        cmd.CommandText = query;
            //        connection.Open();
            //        cmd.ExecuteScalar();
            //        System.Data.DataTable dt = new System.Data.DataTable();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(dt);
            //        userControl1.dataGridView2.DataSource = dt;
            //        da.Dispose();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(cmd.CommandText,
            //                   "SQL Error",
            //                   MessageBoxButtons.OK,
            //                   MessageBoxIcon.Error);
            //    }
            //    connection.Close();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hideAllAndShow(1);
            //hideAll(); // Still need to hide all usercontrol

            userControl2.dataGridView1.Show();
            String path = Directory.GetCurrentDirectory();
            String fname = "http://dev.elderlyinhome.org/Seed.xlsx";

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fname);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[4];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = 16;
            int colCount = 16;

            // dt.Column = colCount;  
            userControl2.dataGridView1.ColumnCount = 16;
            userControl2.dataGridView1.RowCount = 16;

            //stop the loop
            bool loop = false;

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //write the value to the Grid 
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        if (xlRange.Cells[i, j].Value2.ToString() == "End")
                        {
                            loop = true;
                            break;
                        }
                        else
                        {
                            userControl2.dataGridView1.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
                        }
                    }
                    else
                    {
                        try
                        {
                            if (i > 3 && j == 7)
                            {
                                userControl2.dataGridView1.Rows[i - 1].Cells[j - 1] = loadcombobox(i, j);
                            }else if(i > 3 && j == 9)
                            {
                                userControl2.dataGridView1.Rows[i - 1].Cells[j - 1] = loadcombobox(i, j);
                            }
                            else if (i > 3 && j == 11)
                            {
                                userControl2.dataGridView1.Rows[i - 1].Cells[j - 1] = loadcombobox(i, j);
                            }
                            else if (i > 3 && j == 13)
                            {
                                userControl2.dataGridView1.Rows[i - 1].Cells[j - 1] = loadcombobox(i, j);
                            }
                            else if (i > 3 && j == 15)
                            {
                                userControl2.dataGridView1.Rows[i - 1].Cells[j - 1] = loadcombobox(i, j);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);
                        }
                    }   
                    // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");  

                    //add useful things here!     
                }
                if (loop)
                {
                    break;
                }
            }


            //cleanup  
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:  
            //  never use two dots, all COM objects must be referenced and released individually  
            //  ex: [somthing].[something].[something] is bad  

            //release com objects to fully kill excel process from running in the background  
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release  
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release  
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            

            
        }
        public class ClassName
        {
            public string Col1 { get; set; }
        }

        private DataGridViewComboBoxCell loadcombobox(int row,int col)
        {
            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
            string query = "SELECT CONCAT(FirstName,LastName)AS Name FROM TFHR.dbo.Staff";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    connection.Open();
                    cmd.ExecuteScalar();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    comboBoxCell.DataSource = dt;
                    comboBoxCell.DisplayMember = "Name";
                    comboBoxCell.ValueMember = "Name";
                    da.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(cmd.CommandText,
                               "SQL Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                }
                connection.Close();
            }
            return comboBoxCell;
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            hideAllAndShow(2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            hideAllAndShow(3);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            hideAll();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            View.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            View.BackColor = Color.Transparent;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public UserControl[] UserControlList()
        {
            return new UserControl[] {
                userControl1,userControl2,userControl3,userControl4
            };
        }
        public void hideAll()
        {
            foreach (var x in UserControlList())
            {
                x.Hide();
            }
        }
        public void hideAllAndShow(int i)
        {
            UserControl[] ucon = UserControlList();
            hideAll();
            ucon[i].Show();
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.Transparent;
        }

        private void userControl4_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            isToLogin = true;
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isToLogin == false)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
