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
    public partial class Formcreate : Form
    {
        UserControl1 uc1;
        public Formcreate(UserControl1 uc)
        {
            InitializeComponent();
            uc1 = uc;
        }
        private void closeUpdate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Formcreate_Load(object sender, EventArgs e)
        {

            TFHREntities ctx = new TFHREntities();
            var stf = ctx.Staff;

            titleb.Items.AddRange(ctx.Title.Select(s => s.Title1).Distinct().ToArray());
            centerb.Items.AddRange(ctx.CostCentre.Select(s => s.CentreName).Distinct().ToArray());
            trainerb.Items.AddRange(ctx.Role.Select(s => s.RoleName).Distinct().ToArray());

        }
        public class Vali
        {
            public string Text { get; set; }
            public string Element { get; set; }
            public Vali(string text, string element)
            {
                Text = text;
                Element = element;
            }
        }
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            
            List<Vali> Valis = new List<Vali>()
            {
                new Vali(titleb.Text,"Title"),
                new Vali(fname.Text,"First Name"),
                new Vali(lname.Text,"Last Name"),
                new Vali(centerb.Text,"Centre"),
                new Vali(trainerb.Text,"Trainer Type"),
                new Vali(password1.Text.ToString(),"Password"),
                new Vali(password2.Text.ToString(),"Re-password")
            };
            string missing = "";
            List<string> data = new List<string>() { };
            foreach (var v in Valis)
            {
                if (v.Text == "")
                {
                    missing += $"{v.Element} required.\n";
                }
                data.Add(v.Text);
            }
            if(missing != "")
            {
                MessageBox.Show(missing);
            }
            else if(Valis[5].Text != Valis[6].Text)
            {
                MessageBox.Show("Password mismatch.");
            }
            else
            {
                new FormConfirmation(this,uc1,data).ShowDialog();
            }

        }
    }
}
