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
    public partial class Form1 : Form
    {
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hideAllAndShow(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hideAllAndShow(2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            hideAllAndShow(3);
        }
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}
