using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms.VisualStyles;
using System.Linq.Dynamic.Core;

namespace SEHS
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public void label1_Click(object sender, EventArgs e)
        {

        }

        public void label2_Click(object sender, EventArgs e)
        {

        }

        public void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public partial class Table
        {
            public string UID { get; set; }
            public string Name { get; set; }
            public string Centre { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
            public string column(int i)
            {
                string[] col = new string[]
                {
                    this.UID,this.Name,this.Centre,this.Role,this.Status
                };
                return col[i];
            }
        }
        private void tableChange(object sender, EventArgs e)
        {
            List<string> txt = new List<string>
            {
                comboBox1.Text,comboBox2.Text,comboBox3.Text,comboBox4.Text,comboBox5.Text,
            };
            TFHREntities ctx = new TFHREntities();
            var stflist = tableQuery(ctx);
            if (txt[0] != "")
            {
                stflist = stflist.Where(w => w.Centre == comboBox1.Text);
            }
            if (txt[1] != "")
            {
                stflist = stflist.Where(w => w.Role == comboBox2.Text);
            }
            if (txt[2] != "")
            {
                stflist = stflist.Where(w => w.Status == comboBox3.Text.ToLower());
            }
            if (txt[3] != "")
            {
                PropertyInfo[] tcol = typeof(Table).GetProperties();
                List<string> col = new List<string> { };
                foreach (var x in tcol)
                {
                    col.Add(x.Name);
                }
                stflist = stflist.OrderBy(col[col.IndexOf(txt[3])]);
            }
            if (txt[4] != "")
            {
                textBox1.Enabled = true;
            }
            dataGridView2.DataSource = stflist.ToList();
        }
        public IQueryable<Table> tableQuery(TFHREntities ctx)
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
                           });
            return stflist;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            TFHREntities ctx = new TFHREntities();
            var stflist = tableQuery(ctx);
            comboBox1.Items.AddRange(stflist.Select(s => s.Centre).Distinct().ToArray());
            comboBox2.Items.AddRange(stflist.Select(s => s.Role).Distinct().ToArray());
            comboBox3.Items.AddRange(stflist.Select(s => s.Status).Distinct().ToArray());
            PropertyInfo[] tcol = typeof(Table).GetProperties();
            List<string> col = new List<string> {};
            foreach(var x in tcol)
            {
                col.Add(x.Name);
            }
            comboBox4.Items.AddRange(col.ToArray());
            comboBox5.Items.AddRange(col.ToArray());
        }


        private void buttonQuit_Click_1(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text = comboBox5.Text= "";
            textBox1.Enabled = false;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows != null)
            {
                buttonUpdate.Enabled = true;
                buttonDeleteData.Enabled = true;
            } else
            {
                buttonUpdate.Enabled = false;
                buttonDeleteData.Enabled = false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
