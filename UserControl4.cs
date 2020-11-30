using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace SEHS
{
    public partial class UserControl4 : UserControl
    {
        public UserControl4()
        {
            InitializeComponent();
        }

        public partial class Table
        {
            public string LogID { get; set; }
            public string User { get; set; }
            public string DateTime { get; set; }
            public string Type { get; set; }
            public string Host { get; set; }

            public string Detail { get; set; }
            public string column(int i)
            {
                string[] col = new string[]
                {
                    this.LogID,this.User,this.DateTime,this.Type,this.Detail,this.Host
                };
                return col[i];
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableChange(object sender, EventArgs e)
        {
            List<string> txt = new List<string>
            {
                comboBox4.Text,comboBox5.Text
            };
            TFHREntities ctx = new TFHREntities();
            var loglist = tableQuery(ctx);           
            if (txt[0] != "")
            {
                PropertyInfo[] tcol = typeof(Table).GetProperties();
                List<string> col = new List<string> { };
                foreach (var x in tcol)
                {
                    col.Add(x.Name);
                }
                loglist = (checkBox2.Checked == false) ?
                    loglist.OrderBy(col[col.IndexOf(txt[0])]) : loglist.OrderBy(col[col.IndexOf(txt[0])] + " descending");
            }
            if (txt[1] != "")
            {
                textBox1.Enabled = true;
            }
            dataGridView1.DataSource = loglist.ToList();
        }

        public IQueryable<Table> tableQuery(TFHREntities ctx)
        {
            var log = ctx.Log;
            var stf = ctx.Staff;
            var loglist = (from i1 in log
                           join i2 in stf on i1.StaffID equals i2.UID
                           select new Table
                           {
                               LogID = i1.Id.ToString(),
                               User = string.Concat(i2.FirstName, " ", i2.LastName),
                               DateTime = i1.DateTime.ToString(),
                               Type = i1.Type,
                               Detail = i1.Detail,
                               Host = i1.Host
                           }
                           ); ;
            return loglist;
        }

        private void buttonQuit_Click_1(object sender, EventArgs e)
        {
            comboBox4.Text = comboBox5.Text = "";
            textBox1.Enabled = false;
        }

        private void UserControl4_Load(object sender, EventArgs e)
        {           
            TFHREntities ctx = new TFHREntities();
            var stflist = tableQuery(ctx);           
            PropertyInfo[] tcol = typeof(Table).GetProperties();
            List<string> col = new List<string> { };
            foreach (var x in tcol)
            {
                col.Add(x.Name);
            }
            comboBox4.Items.AddRange(col.ToArray());
            comboBox5.Items.AddRange(col.ToArray());           
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TFHREntities ctx = new TFHREntities();
            var stflist = tableQuery(ctx);
            if (comboBox5.Text != "" && textBox1.Text != "")
            {
                stflist = stflist.Where($"{comboBox5.Text}.Contains(\"{textBox1.Text}\")").Select(s => s);
            }
            dataGridView1.DataSource = stflist.ToList();
        }
    }
}
