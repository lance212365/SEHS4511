using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SEHS
{
    public partial class UserControl2 : UserControl
    {  
        public UserControl2()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
                
            Excel.Application XApp = new Excel.Application();
            Excel.Application NewApp = new Excel.Application();
            object missing = System.Reflection.Missing.Value;

            //var relation = @"..\..\";
            //var iFilePath = Path.Combine(Environment.CurrentDirectory, relation, "Dept_Seed.xlsx");
            var iFilePath = "http://dev.elderlyinhome.org/Seed1.xlsx";

            const int firstSubjectLine = 1;
            //var oFilePath = Path.Combine(Environment.CurrentDirectory, relation, "Dept_Feeder.xlsx");
            //var oFilePath = "E:\\GP C# Template with Seed, Feeder verified at HHB1202 on 1030\\ConAppExcel_1030_HHB1202\\Dept_Feeder - Empty.xlsx";

            // Ref: https://docs.microsoft.com/en-us/office/vba/api/excel.workbooks.open
            Excel.Workbook iXWbk = XApp.Workbooks.Open(iFilePath); ; // open in read-only mode

            Excel.Workbook oXWbk = NewApp.Workbooks.Add(Excel.XlSheetType.xlWorksheet);

            Excel.Worksheet iWSht = iXWbk.Worksheets[4];
            Excel.Worksheet oWSht = (Excel.Worksheet)oXWbk.ActiveSheet;

            Excel.Range iRange = iWSht.UsedRange;
            int RowCount = 16;
            //int RowCount = iRange.Rows.Count;  // Up to 3000+
            int ColumnCount = iRange.Columns.Count;
            //Console.WriteLine("RowCount = " + RowCount);
            //Console.WriteLine("ColumnCount = " + ColumnCount);
            //Console.WriteLine("Enter 'Y' to continue.");
            //if (Console.ReadLine() != "Y")
            //{
            //    goto cleanUp;
            //}

            //stop the loop
            //bool loop = false;

            //var oRow = 1; // Keep track of number of rows in the output file
            //int oCol = 1; // Keep track of number of columns in the output file

            var flatList = "";
            var list = new System.Collections.Generic.List<string>();

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
                    list.Add(name.Name);
                }

                flatList = string.Join(",", list.ToArray());
            }

            for (int r = firstSubjectLine; r <= RowCount; r++)
            {
                //oCol = 1; // Restart for every row
                for (int c = 1; c <= ColumnCount; c++)
                {
                    dynamic cell = iRange.Cells[r, c];
                    try
                    {
                        if (r > 3 && c == 7 || r > 3 && c == 9 || r > 3 && c == 11 || r > 3 && c == 13 || r > 3 && c == 15) // Column G = 7th column, Column I = 9th column,... 11, 13, 15
                        {
                            //content = content.Trim();
                            //Console.WriteLine("Just read a cell at row " + r + " column " + c);
                            //oWSht.Cells[oRow, oCol] = "dropdown list here!";

                            oWSht.Cells[r, c].Validation.Delete();
                            oWSht.Cells[r, c].Validation.Add(
                               Excel.XlDVType.xlValidateList,
                               Excel.XlDVAlertStyle.xlValidAlertInformation,
                               Excel.XlFormatConditionOperator.xlBetween,
                               flatList,
                               Type.Missing);

                            oWSht.Cells[r, c].Validation.IgnoreBlank = true;
                            oWSht.Cells[r, c].Validation.InCellDropdown = true;
                        }
                        else
                        {
                            oWSht.Cells[r, c] = cell.Value2;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        string caption = "Error Detected in Input";

                        String errorMessage;
                        errorMessage = "Error: ";
                        errorMessage = String.Concat(errorMessage, ex.Message);
                        errorMessage = String.Concat(errorMessage, " Line: ");
                        errorMessage = String.Concat(errorMessage, ex.Source);
                        //Console.WriteLine(errorMessage, "Error");
                        result = MessageBox.Show(errorMessage, caption, buttons);

                    } // end catch
                }// end for c
            }


            //iXWbk.Save();
            iXWbk.Close();
                oXWbk.SaveAs("D:\\Dept_Feeder",Excel.XlFileFormat.xlWorkbookDefault,Type.Missing,Type.Missing,true,false,Excel.XlSaveAsAccessMode.xlShared,Excel.XlSaveConflictResolution.xlLocalSessionChanges,Type.Missing,Type.Missing);
                oXWbk.Close();
                XApp.Application.Quit();
                NewApp.Application.Quit();

            Marshal.ReleaseComObject(iXWbk);
                Marshal.ReleaseComObject(oXWbk);
                Marshal.ReleaseComObject(XApp);
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
                    Detail = $"Export Dept Feeder at D:\\Dept_Feeder",
                    Host = GetLocalIPAddress()
                };
                ctx.Log.Add(l);
                ctx.SaveChanges();
            }
            DialogResult a = MessageBox.Show("done");
        }

        private void upload_dept_Click(object sender, EventArgs e)
        {
            Excel.Application XApp = new Excel.Application();
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = Path.GetExtension(filePath); //get the file extension  
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        Excel.Workbook iXWbk = XApp.Workbooks.Open(filePath); ;
                        Excel.Worksheet iWSht = iXWbk.Worksheets["Sheet1"];
                        Excel.Range xlRange = iWSht.UsedRange;

                        int rowCount = 16;
                        int colCount = xlRange.Columns.Count; ;

                        //stop the loop
                        bool loop = false;

                        ArrayList dutylist = new ArrayList();

                        var CourseCode = "";
                        var CourseTitle = "";

                        for (int i = 1; i <= rowCount; i++)
                        {
                            int AmountOF_UNITA = 0;
                            int AmountOF_HQ = 0;
                            int AmountOF_HQPT = 0;
                            var StaffID = "";
                            var OTStaffID = "";
                            var TempStaffID = "";
                            var CTStaffID = "";
                            int TAmountOF_L = 0;
                            int TAmountOF_T = 0;
                            int OAmountOF_L = 0;
                            int OAmountOF_T = 0;
                            int CTAmountOF_L = 0;
                            int CTAmountOF_T = 0;
                            int AmountOF_L = 0;
                            int AmountOF_T = 0;
                            //var duty = {}
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
                                        if(i >3 && j == 1){
                                            CourseCode = xlRange.Cells[i, j].Value2.ToString();
                                        }
                                        else if(i>3 && j == 2)
                                        {
                                            CourseTitle = xlRange.Cells[i, j].Value2.ToString();
                                        }
                                        else if(i>3 && j == 3)
                                        {
                                            AmountOF_UNITA = Convert.ToInt32(xlRange.Cells[i, j].Value2);
                                        }
                                        else if (i > 3 && j == 4)
                                        {
                                            AmountOF_HQ = Convert.ToInt32(xlRange.Cells[i, j].Value2);
                                        }
                                        else if (i > 3 && j == 5)
                                        {
                                            AmountOF_HQPT = Convert.ToInt32(xlRange.Cells[i, j].Value2);
                                        }
                                        else if (i > 3 && j == 7)
                                        {
                                            StaffID = CheckUID(xlRange.Cells[i, j].Value2.ToString());                                       
                                        }
                                        else if (i > 3 && j == 8)
                                        {
                                            char[] delimiterChars = { 'L', 'T' };
                                            string[] words = xlRange.Cells[i, j].Value2.ToString().Split(delimiterChars);
                                            AmountOF_L = int.Parse(words[0]);
                                            AmountOF_T = int.Parse(words[1]);
                                        }
                                        else if (i > 3 && j == 9)
                                        {
                                            StaffID = CheckUID(xlRange.Cells[i, j].Value2.ToString());                                          
                                        }
                                        else if (i > 3 && j == 10)
                                        {
                                            char[] delimiterChars = { 'L', 'T' };
                                            string[] words = xlRange.Cells[i, j].Value2.ToString().Split(delimiterChars);
                                            AmountOF_L = int.Parse(words[0]);
                                            AmountOF_T = int.Parse(words[1]);
                                        }
                                        else if (i > 3 && j == 11)
                                        {
                                            TempStaffID = CheckUID(xlRange.Cells[i, j].Value2.ToString());                                          
                                        }
                                        else if (i > 3 && j == 12)
                                        {
                                            char[] delimiterChars = { 'L', 'T' };
                                            string[] words = xlRange.Cells[i, j].Value2.ToString().Split(delimiterChars);
                                            TAmountOF_L = int.Parse(words[0]); ;
                                            TAmountOF_T = int.Parse(words[1]);
                                        }
                                        else if (i > 3 && j == 13)
                                        {
                                            OTStaffID = CheckUID(xlRange.Cells[i, j].Value2.ToString());                                      
                                        }
                                        else if (i > 3 && j == 14)
                                        {
                                            char[] delimiterChars = { 'L', 'T' };
                                            string[] words = xlRange.Cells[i, j].Value2.ToString().Split(delimiterChars);
                                            OAmountOF_L = int.Parse(words[0]);
                                            OAmountOF_T = int.Parse(words[1]);
                                        }
                                        else if (i > 3 && j == 15)
                                        {
                                            CTStaffID = CheckUID(xlRange.Cells[i, j].Value2.ToString());                                         
                                        }
                                        else if (i > 3 && j == 16)
                                        {
                                            char[] delimiterChars = { 'L', 'T' };
                                            string[] words = xlRange.Cells[i, j].Value2.ToString().Split(delimiterChars);
                                            CTAmountOF_L = int.Parse(words[0]);
                                            CTAmountOF_T = int.Parse(words[1]);
                                        }
                                    }
                                }
                                                               
                            }
                            if (loop)
                            {
                                break;
                            }
                            if( StaffID != "")
                            {
                                int Hours = AmountOF_L * 2 + AmountOF_T;
                                dutylist.Add(new Duty(StaffID, Hours, "Normal", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, AmountOF_L, AmountOF_T));
                            }
                            if (TempStaffID != "")
                            {
                                int Hours = TAmountOF_L * 2 + TAmountOF_T;
                                dutylist.Add(new Duty(TempStaffID, Hours, "Temp", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, TAmountOF_L, TAmountOF_T));
                            }
                            if (OTStaffID != "")
                            {
                                int Hours = OAmountOF_L * 2 + OAmountOF_T;
                                dutylist.Add(new Duty(OTStaffID, Hours, "OT", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, OAmountOF_L, OAmountOF_T));
                            }
                            if (CTStaffID != "")
                            {
                                int Hours = CTAmountOF_L * 2 + CTAmountOF_T;
                                dutylist.Add(new Duty(CTStaffID, Hours, "CT", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, CTAmountOF_L, CTAmountOF_T));
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
                        Marshal.ReleaseComObject(iWSht);

                        //close and release  
                        iXWbk.Close();
                        Marshal.ReleaseComObject(iXWbk);

                        //quit and release  
                        XApp.Quit();
                        Marshal.ReleaseComObject(XApp);

                        using (TFHREntities ctx = new TFHREntities())
                        {
                            Form1 parentForm = (this.ParentForm as Form1);

                            var form1 = parentForm.buttonUserName;
                            Log l = new Log
                            {

                                StaffID = CheckUID(form1.Text),
                                DateTime = DateTime.Now,
                                Type = "Upload",
                                Detail = $"Upload Dept Feeder and insert record",
                                Host = GetLocalIPAddress()
                            };
                            ctx.Log.Add(l);

                            ctx.SaveChanges();

                            foreach (Duty i in dutylist)
                            {
                                try
                                {
                                    Staff_Duty d = new Staff_Duty
                                    {                                     
                                        StaffID = i.StaffID,
                                        Hours = i.Hours,
                                        CourseType = i.CourseType,
                                        CourseCode = i.CourseCode,
                                        CourseName = i.CourseName,
                                        AmountOF_UNITA = i.AmountOF_UNITA,
                                        AmountOF_HQ = i.AmountOF_HQ,
                                        AmountOF_HQPT = i.AmountOF_HQPT,
                                        AmountOF_L = i.AmountOF_L,
                                        AmountOF_T = i.AmountOF_T
                                    };
                                                                    

                                    Log l2 = new Log
                                    {

                                        StaffID = CheckUID(form1.Text),
                                        DateTime = DateTime.Now,
                                        Type = "Insert",
                                        Detail = $"Inser record ( {i.StaffID},{i.Hours}{i.CourseType},{i.CourseCode},{i.CourseName},{i.AmountOF_UNITA},{i.AmountOF_HQ},{i.AmountOF_HQPT},{i.AmountOF_L},{i.AmountOF_T})",
                                        Host = GetLocalIPAddress()
                                    };
                                    ctx.Log.Add(l2);                                                                     
                                    ctx.Staff_Duty.Add(d);
                                    ctx.SaveChanges();
                                    MessageBox.Show("Done Insert success");
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }                               
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
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
                    if(Name == name.Name)
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

        private void Save_Click(object sender, EventArgs e)
        {
            int rowCount = dataGridView1.Rows.Count;
            int colCount = dataGridView1.Columns.Count; ;

            //stop the loop
            bool loop = false;

            ArrayList dutylist = new ArrayList();

            var CourseCode = "";
            var CourseTitle = "";

            for (int i = 0; i < rowCount; i++)
            {
                int AmountOF_UNITA = 0;
                int AmountOF_HQ = 0;
                int AmountOF_HQPT = 0;
                var StaffID = "";
                var OTStaffID = "";
                var TempStaffID = "";
                var CTStaffID = "";
                int TAmountOF_L = 0;
                int TAmountOF_T = 0;
                int OAmountOF_L = 0;
                int OAmountOF_T = 0;
                int CTAmountOF_L = 0;
                int CTAmountOF_T = 0;
                int AmountOF_L = 0;
                int AmountOF_T = 0;
                //var duty = {}
                for (int j = 0; j < colCount; j++)
                {
                    //write the value to the Grid  
                    var a = dataGridView1.Rows[i].Cells[j].Value;
                    if (!(Boolean)String.IsNullOrWhiteSpace((string)dataGridView1.Rows[i].Cells[j].Value))
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "End")
                        {
                            loop = true;
                            break;
                        }
                        else
                        {
                            if (i > 2 && j == 0)
                            {
                                CourseCode = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 1)
                            {
                                CourseTitle = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 2)
                            {
                                AmountOF_UNITA = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                            }
                            else if (i > 2 && j == 3)
                            {
                                AmountOF_HQ = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                            }
                            else if (i > 2 && j == 4)
                            {
                                AmountOF_HQPT = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                            }
                            else if (i > 2 && j == 6)
                            {
                                StaffID = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 7)
                            {
                                char[] delimiterChars = { 'L', 'T' };
                                string[] words = dataGridView1.Rows[i].Cells[j].Value.ToString().Split(delimiterChars);
                                AmountOF_L = int.Parse(words[0]);
                                AmountOF_T = int.Parse(words[1]);
                            }
                            else if (i > 2 && j == 8)
                            {
                                StaffID = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 9)
                            {
                                char[] delimiterChars = { 'L', 'T' };
                                string[] words = dataGridView1.Rows[i].Cells[j].Value.ToString().Split(delimiterChars);
                                AmountOF_L = int.Parse(words[0]);
                                AmountOF_T = int.Parse(words[1]);
                            }
                            else if (i > 2 && j == 10)
                            {
                                TempStaffID =dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 11)
                            {
                                char[] delimiterChars = { 'L', 'T' };
                                string[] words = dataGridView1.Rows[i].Cells[j].Value.ToString().Split(delimiterChars);
                                TAmountOF_L = int.Parse(words[0]); ;
                                TAmountOF_T = int.Parse(words[1]);
                            }
                            else if (i > 2 && j == 12)
                            {
                                OTStaffID = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 13)
                            {
                                char[] delimiterChars = { 'L', 'T' };
                                string[] words = dataGridView1.Rows[i].Cells[j].Value.ToString().Split(delimiterChars);
                                OAmountOF_L = int.Parse(words[0]);
                                OAmountOF_T = int.Parse(words[1]);
                            }
                            else if (i > 2 && j == 14)
                            {
                                CTStaffID =dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (i > 2 && j == 15)
                            {
                                char[] delimiterChars = { 'L', 'T' };
                                string[] words = dataGridView1.Rows[i].Cells[j].Value.ToString().Split(delimiterChars);
                                CTAmountOF_L = int.Parse(words[0]);
                                CTAmountOF_T = int.Parse(words[1]);
                            }
                        }
                    }                    
                }
                if (loop)
                {
                    break;
                }
                if (StaffID != "")
                {
                    int Hours = AmountOF_L * 2 + AmountOF_T;
                    dutylist.Add(new Duty(StaffID, Hours, "Normal", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, AmountOF_L, AmountOF_T));
                }
                if (TempStaffID != "")
                {
                    int Hours = TAmountOF_L * 2 + TAmountOF_T;
                    dutylist.Add(new Duty(TempStaffID, Hours, "Temp", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, TAmountOF_L, TAmountOF_T));
                }
                if (OTStaffID != "")
                {
                    int Hours = OAmountOF_L * 2 + OAmountOF_T;
                    dutylist.Add(new Duty(OTStaffID, Hours, "OT", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, OAmountOF_L, OAmountOF_T));
                }
                if (CTStaffID != "")
                {
                    int Hours = CTAmountOF_L * 2 + CTAmountOF_T;
                    dutylist.Add(new Duty(CTStaffID, Hours, "CT", CourseCode, CourseTitle, AmountOF_UNITA, AmountOF_HQ, AmountOF_HQPT, CTAmountOF_L, CTAmountOF_T));
                }
            }           

            using (TFHREntities ctx = new TFHREntities())
            {
                Form1 parentForm = (this.ParentForm as Form1);

                var form1 = parentForm.buttonUserName;
                Log l = new Log
                {

                    StaffID = CheckUID(form1.Text),
                    DateTime = DateTime.Now,
                    Type = "Save",
                    Detail = $"Save Data Grid View",
                    Host = GetLocalIPAddress()
                };
                ctx.Log.Add(l);

                ctx.SaveChanges();
                foreach (Duty i in dutylist)
                {
                    try
                    {
                        Staff_Duty d = new Staff_Duty
                        {
                            StaffID = i.StaffID,
                            Hours = i.Hours,
                            CourseType = i.CourseType,
                            CourseCode = i.CourseCode,
                            CourseName = i.CourseName,
                            AmountOF_UNITA = i.AmountOF_UNITA,
                            AmountOF_HQ = i.AmountOF_HQ,
                            AmountOF_HQPT = i.AmountOF_HQPT,
                            AmountOF_L = i.AmountOF_L,
                            AmountOF_T = i.AmountOF_T
                        };

                        Log l2 = new Log
                        {

                            StaffID = CheckUID(form1.Text),
                            DateTime = DateTime.Now,
                            Type = "Insert",
                            Detail = $"Inser record ( {i.StaffID},{i.Hours}{i.CourseType},{i.CourseCode},{i.CourseName},{i.AmountOF_UNITA},{i.AmountOF_HQ},{i.AmountOF_HQPT},{i.AmountOF_L},{i.AmountOF_T})",
                            Host = GetLocalIPAddress()
                        };
                        ctx.Log.Add(l2);
                        ctx.Staff_Duty.Add(d);
                        ctx.SaveChanges();
                        MessageBox.Show("Save Success");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
    }

    public class Duty
    {
        public string DudyID; 
        public string StaffID;
        public int Hours;
        public string CourseType;
        public string CourseCode;
        public string CourseName;
        public int AmountOF_UNITA;
        public int AmountOF_HQ;
        public int AmountOF_HQPT;
        public int AmountOF_L;
        public int AmountOF_T;

        public Duty(string StaffID, int Hours, string CourseType, string CourseCode, string CourseName , int AmountOF_UNITA, int AmountOF_HQ, int AmountOF_HQPT, int AmountOF_L, int AmountOF_T) 
        {
            this.StaffID = StaffID;
            this.Hours = Hours;
            this.CourseType = CourseType;
            this.CourseCode = CourseCode;
            this.CourseName = CourseName;
            this.AmountOF_UNITA = AmountOF_UNITA;
            this.AmountOF_HQ = AmountOF_HQ;
            this.AmountOF_HQPT = AmountOF_HQPT;
            this.AmountOF_L = AmountOF_L;
            this.AmountOF_T = AmountOF_T;
        }
        
    }
}



