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
            object missing = System.Reflection.Missing.Value;

            //var relation = @"..\..\";
            //var iFilePath = Path.Combine(Environment.CurrentDirectory, relation, "Dept_Seed.xlsx");
            var iFilePath = "http://dev.elderlyinhome.org/Seed1.xlsx";

            const int firstSubjectLine = 1;
            //var oFilePath = Path.Combine(Environment.CurrentDirectory, relation, "Dept_Feeder.xlsx");
            var oFilePath = "E:\\GP C# Template with Seed, Feeder verified at HHB1202 on 1030\\ConAppExcel_1030_HHB1202\\Dept_Feeder - Empty.xlsx";

            // Ref: https://docs.microsoft.com/en-us/office/vba/api/excel.workbooks.open
            Excel.Workbook iXWbk = XApp.Workbooks.Open(iFilePath); ; // open in read-only mode

            Excel.Workbook oXWbk = XApp.Workbooks.Open(oFilePath); ;

            Excel.Worksheet iWSht = iXWbk.Worksheets[4];
            Excel.Worksheet oWSht = oXWbk.Worksheets["Sheet1"];

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
                oXWbk.Save();
                oXWbk.Close();
                XApp.Application.Quit();

                Marshal.ReleaseComObject(iXWbk);
                Marshal.ReleaseComObject(oXWbk);
                Marshal.ReleaseComObject(XApp);
            DialogResult a = MessageBox.Show("done");
        }
       
    }
}

/*for (int r = firstSubjectLine; r <= RowCount; r++, oRow++)
{
    oRow += 1;
    oCol = 1; // Restart for every row
    for (int c = 1; c <= ColumnCount; c++)
    {
        dynamic cell = iRange.Cells[r, c];
        try
        {
            oCol += 1;
            if (c == 7 || c == 9 || c == 11 || c == 13 || c == 15) // Column G = 7th column, Column I = 9th column,... 11, 13, 15
            {
                //content = content.Trim();
                //Console.WriteLine("Just read a cell at row " + r + " column " + c);
                //oWSht.Cells[oRow, oCol] = "dropdown list here!";

                oWSht.Cells[oRow, oCol].Validation.Delete();
                oWSht.Cells[oRow, oCol].Validation.Add(
                   Excel.XlDVType.xlValidateList,
                   Excel.XlDVAlertStyle.xlValidAlertInformation,
                   Excel.XlFormatConditionOperator.xlBetween,
                   flatList,
                   Type.Missing);

                oWSht.Cells[oRow, oCol].Validation.IgnoreBlank = true;
                oWSht.Cells[oRow, oCol].Validation.InCellDropdown = true;
            }
            else
            {
                oWSht.Cells[oRow, oCol] = cell.Value2;
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
}*/ // end for r

//Console.WriteLine("Press ENTER to continue");
//Console.ReadLine();

