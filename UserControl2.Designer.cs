namespace SEHS
{
    partial class UserControl2
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Download_dept = new System.Windows.Forms.Button();
            this.upload_dept = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 24F);
            this.label1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(45, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Department Feeder";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(705, 380);
            this.dataGridView1.TabIndex = 8;
            // 
            // Download_dept
            // 
            this.Download_dept.Location = new System.Drawing.Point(33, 493);
            this.Download_dept.Name = "Download_dept";
            this.Download_dept.Size = new System.Drawing.Size(161, 51);
            this.Download_dept.TabIndex = 9;
            this.Download_dept.Text = "Download Dept Feeder";
            this.Download_dept.UseVisualStyleBackColor = true;
            this.Download_dept.Click += new System.EventHandler(this.button1_Click);
            // 
            // upload_dept
            // 
            this.upload_dept.Location = new System.Drawing.Point(235, 493);
            this.upload_dept.Name = "upload_dept";
            this.upload_dept.Size = new System.Drawing.Size(148, 53);
            this.upload_dept.TabIndex = 10;
            this.upload_dept.Text = "Upload Dept Feeder";
            this.upload_dept.UseVisualStyleBackColor = true;
            this.upload_dept.Click += new System.EventHandler(this.upload_dept_Click);
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.upload_dept);
            this.Controls.Add(this.Download_dept);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(878, 668);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Download_dept;
        private System.Windows.Forms.Button upload_dept;
    }
}
