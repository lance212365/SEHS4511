using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEHS
{
    public partial class Form4 : Form
    {
        public string cur;
        UserControl1 uc1;
        public Form4(UserControl1 uc,string UID)
        {
            InitializeComponent();
            uc1 = uc;
            cur = UID;
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {

            TFHREntities ctx = new TFHREntities();
            var stf = ctx.Staff;
            var rem = stf.SingleOrDefault(b => b.UID == cur);
            var usl = ctx.UserLogin.SingleOrDefault(b => b.UID == cur);
            var std = ctx.Staff_Duty.Where(b=>b.StaffID ==cur).Select(s=>s).ToArray();
            if (rem != null)
            {
                stf.Remove(rem);
                ctx.UserLogin.Remove(usl);
                ctx.Staff_Duty.RemoveRange(std);
                ctx.SaveChanges();
                MessageBox.Show("User Deleted.");
                uc1.tableChange(null, null);
                Close();
            }

        }
    }
}
