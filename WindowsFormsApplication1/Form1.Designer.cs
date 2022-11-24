namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnok = new System.Windows.Forms.Button();
            this.btnsource = new System.Windows.Forms.Button();
            this.radiorestore = new System.Windows.Forms.RadioButton();
            this.lblFrom = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radiobackup = new System.Windows.Forms.RadioButton();
            this.tbsource = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(164, 173);
            this.btnok.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(113, 38);
            this.btnok.TabIndex = 30;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btnsource
            // 
            this.btnsource.BackColor = System.Drawing.Color.White;
            this.btnsource.Location = new System.Drawing.Point(648, 109);
            this.btnsource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnsource.Name = "btnsource";
            this.btnsource.Size = new System.Drawing.Size(75, 38);
            this.btnsource.TabIndex = 28;
            this.btnsource.Text = "Browse";
            this.btnsource.UseVisualStyleBackColor = false;
            this.btnsource.Click += new System.EventHandler(this.btnsource_Click);
            // 
            // radiorestore
            // 
            this.radiorestore.AutoSize = true;
            this.radiorestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.radiorestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radiorestore.Location = new System.Drawing.Point(307, 35);
            this.radiorestore.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.radiorestore.Name = "radiorestore";
            this.radiorestore.Size = new System.Drawing.Size(64, 22);
            this.radiorestore.TabIndex = 13;
            this.radiorestore.Text = "&Restore";
            this.radiorestore.UseVisualStyleBackColor = false;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(26, 117);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(134, 20);
            this.lblFrom.TabIndex = 27;
            this.lblFrom.Text = "Backup File လမ္းေၾကာင္း";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radiorestore);
            this.groupBox1.Controls.Add(this.radiobackup);
            this.groupBox1.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(708, 90);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ေရြးရန္ Backup (သို႕မဟုတ္) Restore";
            // 
            // radiobackup
            // 
            this.radiobackup.AutoSize = true;
            this.radiobackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.radiobackup.Checked = true;
            this.radiobackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radiobackup.Location = new System.Drawing.Point(75, 35);
            this.radiobackup.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.radiobackup.Name = "radiobackup";
            this.radiobackup.Size = new System.Drawing.Size(60, 22);
            this.radiobackup.TabIndex = 12;
            this.radiobackup.TabStop = true;
            this.radiobackup.Text = "&Backup";
            this.radiobackup.UseVisualStyleBackColor = false;
            // 
            // tbsource
            // 
            this.tbsource.BackColor = System.Drawing.Color.White;
            this.tbsource.Location = new System.Drawing.Point(164, 111);
            this.tbsource.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tbsource.Multiline = true;
            this.tbsource.Name = "tbsource";
            this.tbsource.Size = new System.Drawing.Size(444, 36);
            this.tbsource.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(739, 224);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.btnsource);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbsource);
            this.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup  or Restore";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.Button btnsource;
        private System.Windows.Forms.RadioButton radiorestore;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radiobackup;
        private System.Windows.Forms.TextBox tbsource;
    }
}

