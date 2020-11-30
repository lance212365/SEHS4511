namespace SEHS
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.View = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonUserName = new System.Windows.Forms.Button();
            this.userControl4 = new SEHS.UserControl4();
            this.userControl3 = new SEHS.UserControl3();
            this.userControl2 = new SEHS.UserControl2();
            this.userControl1 = new SEHS.UserControl1();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // View
            // 
            this.View.BackColor = System.Drawing.Color.Transparent;
            this.View.Cursor = System.Windows.Forms.Cursors.Hand;
            this.View.FlatAppearance.BorderSize = 0;
            this.View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.View.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.View.ForeColor = System.Drawing.Color.Transparent;
            this.View.Image = ((System.Drawing.Image)(resources.GetObject("View.Image")));
            this.View.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.View.Location = new System.Drawing.Point(0, 155);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(275, 73);
            this.View.TabIndex = 0;
            this.View.Text = "View User Table";
            this.View.UseVisualStyleBackColor = false;
            this.View.Click += new System.EventHandler(this.button1_Click);
            this.View.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.View.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.buttonLogout);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.View);
            this.panel1.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Location = new System.Drawing.Point(-2, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 730);
            this.panel1.TabIndex = 1;
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.Color.DarkRed;
            this.buttonLogout.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonLogout.FlatAppearance.BorderSize = 0;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Berlin Sans FB", 16F);
            this.buttonLogout.ForeColor = System.Drawing.Color.Transparent;
            this.buttonLogout.Location = new System.Drawing.Point(0, 619);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(275, 73);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(275, 119);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.Location = new System.Drawing.Point(0, 453);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(275, 85);
            this.button4.TabIndex = 3;
            this.button4.Text = "Log";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.MouseEnter += new System.EventHandler(this.button4_MouseEnter);
            this.button4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.Location = new System.Drawing.Point(0, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(275, 98);
            this.button3.TabIndex = 2;
            this.button3.Text = "Generate Central Feeder File";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
            this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.Location = new System.Drawing.Point(3, 252);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(272, 91);
            this.button2.TabIndex = 1;
            this.button2.Text = "update department feeder";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            this.button2.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(150, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(734, 609);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.userControl4);
            this.panel2.Controls.Add(this.userControl3);
            this.panel2.Controls.Add(this.userControl2);
            this.panel2.Controls.Add(this.userControl1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(273, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1078, 730);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonUserName);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1078, 54);
            this.panel3.TabIndex = 7;
            // 
            // buttonUserName
            // 
            this.buttonUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.buttonUserName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonUserName.FlatAppearance.BorderSize = 0;
            this.buttonUserName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
            this.buttonUserName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUserName.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.buttonUserName.ForeColor = System.Drawing.Color.CadetBlue;
            this.buttonUserName.Image = ((System.Drawing.Image)(resources.GetObject("buttonUserName.Image")));
            this.buttonUserName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUserName.Location = new System.Drawing.Point(840, 0);
            this.buttonUserName.Name = "buttonUserName";
            this.buttonUserName.Size = new System.Drawing.Size(238, 54);
            this.buttonUserName.TabIndex = 2;
            this.buttonUserName.Text = "button1";
            this.buttonUserName.UseVisualStyleBackColor = false;
            // 
            // userControl4
            // 
            this.userControl4.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.userControl4.BackColor = System.Drawing.Color.Transparent;
            this.userControl4.Location = new System.Drawing.Point(0, 0);
            this.userControl4.Name = "userControl4";
            this.userControl4.Size = new System.Drawing.Size(1078, 730);
            this.userControl4.TabIndex = 1;
            // 
            // userControl3
            // 
            this.userControl3.BackColor = System.Drawing.Color.Transparent;
            this.userControl3.Location = new System.Drawing.Point(0, 0);
            this.userControl3.Name = "userControl3";
            this.userControl3.Size = new System.Drawing.Size(1078, 727);
            this.userControl3.TabIndex = 5;
            // 
            // userControl2
            // 
            this.userControl2.BackColor = System.Drawing.Color.Transparent;
            this.userControl2.Location = new System.Drawing.Point(0, 0);
            this.userControl2.Name = "userControl2";
            this.userControl2.Size = new System.Drawing.Size(1278, 830);
            this.userControl2.TabIndex = 4;
            // 
            // userControl1
            // 
            this.userControl1.BackColor = System.Drawing.Color.Transparent;
            this.userControl1.Location = new System.Drawing.Point(0, 0);
            this.userControl1.Name = "userControl1";
            this.userControl1.Size = new System.Drawing.Size(1075, 730);
            this.userControl1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.CadetBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.Name = "Form1";
            this.Text = "Talent Farm Data Manament System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button View;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button buttonUserName;
        public System.Windows.Forms.Panel panel1;
        public UserControl1 userControl1;
        public UserControl2 userControl2;
        public UserControl3 userControl3;
        public UserControl4 userControl4;
    }
}

