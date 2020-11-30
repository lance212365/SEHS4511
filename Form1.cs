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
using System.Net;
using System.Net.Sockets;

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

        public class Table
        {
            public string UID { get; set; }
            public string Name { get; set; }
            public string Centre { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            hideAllAndShow(0);
            using (TFHREntities ctx = new TFHREntities())
            {
                var stf = ctx.Staff;
                var cc = ctx.CostCentre;
                var role = ctx.Role;
                var title = ctx.Title;
                var stflist = (from i1 in stf
                               join i2 in cc on i1.CentreID equals i2.CentreID
                               join i3 in role on i1.Role equals i3.RoleID
                               join i4 in title on i1.Title equals i4.TitleID
                               select new Table
                               {
                                   UID = i1.UID,
                                   Name = string.Concat(i4.Title1, ". ", i1.FirstName, " ", i1.LastName),
                                   Centre = i2.CentreName,
                                   Role = i3.RoleName,
                                   Status = i1.Status
                               }
                               );
                                Log l = new Log
                                {
                                    StaffID = CheckUID(buttonUserName.Text),
                                    DateTime = DateTime.Now,
                                    Type = "Read",
                                    Detail = "View User Table",
                                    Host = GetLocalIPAddress()
                                };
                                ctx.Log.Add(l);
                                ctx.SaveChanges();
                userControl1.dataGridView2.DataSource = stflist.ToList();
            }
        }

        private string CheckUID(string Name)
        {
            string UID = "";
            using (TFHREntities ctx = new TFHREntities())
            {
                var stf = ctx.Staff;
                var stflist = (from i1 in stf
                               select new
                               {
                                   UID = i1.UID,
                                   Name = string.Concat(i1.FirstName, " ", i1.LastName),
                               }
                               );
                foreach (var name in stflist)
                {
                    if (Name == name.Name)
                    {
                        UID = name.UID;
                    }
                }


            }

            return UID;
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
        private void button2_Click(object sender, EventArgs e)
        {
            hideAllAndShow(1);
            //hideAll(); // Still need to hide all usercontrol

            userControl2.dataGridView1.Show();
            //String path = Directory.GetCurrentDirectory();
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


            using (TFHREntities ctx = new TFHREntities())
            {               
                Log l = new Log
                {
                    StaffID = CheckUID(buttonUserName.Text),
                    DateTime = DateTime.Now,
                    Type = "Read",
                    Detail = "Read Seed Feeder",
                    Host = GetLocalIPAddress()
                };
                ctx.Log.Add(l);
                ctx.SaveChanges();                
            }

        }
        public class ClassName
        {
            public string Col1 { get; set; }
        }

        private DataGridViewComboBoxCell loadcombobox(int row,int col)
        {
            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
            using (TFHREntities ctx = new TFHREntities())
            {
                var stf = ctx.Staff;
                var stflist = (from i1 in stf
                               select new
                               {
                                   UID = i1.UID,
                                   Name = string.Concat(i1.FirstName, " ", i1.LastName),
                                 
                               }
                               );

                comboBoxCell.DataSource = stflist.ToList();
                comboBoxCell.DisplayMember = "Name";
                comboBoxCell.ValueMember = "UID";
            }
            return comboBoxCell;
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            hideAllAndShow(2);
            

            
            using (TFHREntities ctx = new TFHREntities())
            {
                var stf = ctx.Staff;
                var cc = ctx.Staff_Duty;
                var stflist = (from i1 in stf
                               join i2 in cc on i1.UID equals i2.StaffID
                               select new
                               {
                                   CourseCode = i2.CourseCode,
                                   CourseTile = i2.CourseName,
                                   UID = i1.UID,
                                   Classes = string.Concat(i2.AmountOF_L, "L", i2.AmountOF_T, "T"),
                                   Normal_or_OT = i2.CourseType                                   
                               }
                               ); ;

                Log l = new Log
                {
                    StaffID = CheckUID(buttonUserName.Text),
                    DateTime = DateTime.Now,
                    Type = "Read",
                    Detail = "Read Dept Feeder Data",
                    Host = GetLocalIPAddress()
                };
                ctx.Log.Add(l);
                ctx.SaveChanges();

                userControl3.dataGridView1.DataSource = stflist.ToList();
            };

        }
        private void button4_Click(object sender, EventArgs e)
        {
            hideAllAndShow(3);
            using (TFHREntities ctx = new TFHREntities())
            {
                var log = ctx.Log;
                var stf = ctx.Staff;
                var loglist = (from i1 in log
                               join i2 in stf on i1.StaffID equals i2.UID
                               select new
                               {
                                   LogID = i1.Id,
                                   User = string.Concat(i2.FirstName," ",i2.LastName),
                                   DateTime = i1.DateTime.ToString(),
                                   Type = i1.Type,
                                   Detail = i1.Detail,
                                   Host = i1.Host
                               }
                               ); ;

                Log l = new Log
                {
                    StaffID = CheckUID(buttonUserName.Text),
                    DateTime = DateTime.Now,
                    Type = "Read",
                    Detail = "Read Log",
                    Host = GetLocalIPAddress()
                };
                ctx.Log.Add(l);
                ctx.SaveChanges();

                userControl4.dataGridView1.DataSource = loglist.ToList();
            };

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

        private void buttonUserName_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
