namespace SEHS
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.loginBox1 = new System.Windows.Forms.GroupBox();
            this.passwordLabel1 = new System.Windows.Forms.Label();
            this.usernameLabel1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.loginBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginBox1
            // 
            this.loginBox1.Controls.Add(this.button1);
            this.loginBox1.Controls.Add(this.passwordLabel1);
            this.loginBox1.Controls.Add(this.usernameLabel1);
            this.loginBox1.Controls.Add(this.textBox2);
            this.loginBox1.Controls.Add(this.textBox1);
            this.loginBox1.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.loginBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.loginBox1.Location = new System.Drawing.Point(183, 163);
            this.loginBox1.Name = "loginBox1";
            this.loginBox1.Size = new System.Drawing.Size(458, 271);
            this.loginBox1.TabIndex = 1;
            this.loginBox1.TabStop = false;
            this.loginBox1.Text = "Login";
            // 
            // passwordLabel1
            // 
            this.passwordLabel1.AutoSize = true;
            this.passwordLabel1.Location = new System.Drawing.Point(45, 129);
            this.passwordLabel1.Name = "passwordLabel1";
            this.passwordLabel1.Size = new System.Drawing.Size(129, 30);
            this.passwordLabel1.TabIndex = 3;
            this.passwordLabel1.Text = "Password:";
            // 
            // usernameLabel1
            // 
            this.usernameLabel1.AutoSize = true;
            this.usernameLabel1.Location = new System.Drawing.Point(34, 57);
            this.usernameLabel1.Name = "usernameLabel1";
            this.usernameLabel1.Size = new System.Drawing.Size(140, 30);
            this.usernameLabel1.TabIndex = 2;
            this.usernameLabel1.Text = "Username:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(180, 126);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(239, 37);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(180, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 37);
            this.textBox1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SEHS.Properties.Resources.pictureBox2_Image;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(305, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 165);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(170, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 43);
            this.button1.TabIndex = 4;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.loginBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "login";
            this.Text = "login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.loginBox1.ResumeLayout(false);
            this.loginBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox loginBox1;
        private System.Windows.Forms.Label passwordLabel1;
        private System.Windows.Forms.Label usernameLabel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}