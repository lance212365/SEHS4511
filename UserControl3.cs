using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace SEHS
{
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }

        private void Download_Central_Feeder_Click(object sender, EventArgs e)
        {
            Excel.Application NewApp = new Excel.Application();
            Excel.Workbook oXWbk = NewApp.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
            Excel.Worksheet oWSht = (Excel.Worksheet)oXWbk.ActiveSheet;

            int colcount = dataGridView1.Columns.Count;
            int rowcount = dataGridView1.Rows.Count;

            for(int i = 0;i < rowcount+1; i++)
            {
                for(int j = 0; j < colcount; j++)
                {
                    if(i == 0 && j==0)
                    {
                        oWSht.Cells[i + 1, j + 1] = "CourseCode";
                    }else if(i == 0 && j == 1)
                    {
                        oWSht.Cells[i + 1, j + 1] = "CourseTitle";
                    }
                    else if (i == 0 && j == 2)
                    {
                        oWSht.Cells[i + 1, j + 1] = "User ID";
                    }
                    else if (i == 0 && j == 3)
                    {
                        oWSht.Cells[i + 1, j + 1] = "Classes";
                    }
                    else if (i == 0 && j == 4)
                    {
                        oWSht.Cells[i + 1, j + 1] = "Normal or OT";
                    }
                    else
                    {
                        oWSht.Cells[i + 1, j + 1] = dataGridView1.Rows[i-1].Cells[j].Value.ToString();
                    }                   
                }
            }

            oXWbk.SaveAs("C:\\Users\\Jimwa\\Desktop\\Central_Feeder", Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlShared, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            oXWbk.Close();
           
            NewApp.Application.Quit();

           
            Marshal.ReleaseComObject(oXWbk);
           
            Marshal.ReleaseComObject(NewApp);
            using (TFHREntities ctx = new TFHREntities())
            {
                Form1 parentForm = (this.ParentForm as Form1);

                var form1 = parentForm.buttonUserName;
                Log l = new Log
                {

                    StaffID = CheckUID(form1.Text),
                    DateTime = DateTime.Now,
                    Type = "Export",
                    Detail = $"Export Dept Feeder at C:\\Users\\Jimwa\\Desktop\\Central_Feeder",
                    Host = GetLocalIPAddress()
                };
                ctx.Log.Add(l);

                ctx.SaveChanges();
            }
            DialogResult a = MessageBox.Show("done");
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
    }
}
