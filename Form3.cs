using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Dynamic.Core;

namespace SEHS
{
    public partial class Form3 : Form
    {
        UserControl1 uc1;
        public Form3(UserControl1 uc)
        {
            InitializeComponent();
            uc1 = uc;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void closeUpdate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdateData_Click(object sender, EventArgs e)
        {
            TFHREntities ctx = new TFHREntities();
            var stf = ctx.Staff;
            var titles = ctx.Title;
            var centres = ctx.CostCentre;
            var roles = ctx.Role;
            List<string> texts = new List<string> {
                fname.Text,lname.Text,titleb.Text,centerb.Text,trainerb.Text,statuses.Text
            };
            var validate = true;
            texts.ForEach((string x) =>
            {
                if (x == "")
                {
                    validate = false;
                }
            });
            if (validate == true)
            {
                var result = stf.SingleOrDefault(b => b.UID == uidk.Text);
                if (result != null)
                {
                    result.FirstName = texts[0];
                    result.LastName = texts[1];
                    result.Title = titles.Where($"Title1 = \"{texts[2]}\"").Select(s => s.TitleID).FirstOrDefault().ToString();
                    result.CentreID = centres.Where($"CentreName = \"{texts[3]}\"").Select(a => a.CentreID).FirstOrDefault().ToString();
                    result.Role = roles.Where($"RoleName = \"{texts[4]}\"").Select(d => d.RoleID).FirstOrDefault().ToString();
                    result.Status = texts[5];
                    ctx.SaveChanges();
                    MessageBox.Show("Updated Database.");
                    uc1.tableChange(null, null);
                    this.Close();
                }
            } 
            else
            {
                MessageBox.Show("You missed the field!! Please fill all the blank field!");
            }
        }
    }
}
