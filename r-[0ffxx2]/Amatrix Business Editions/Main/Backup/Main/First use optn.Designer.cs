namespace Main
{
    partial class First_use_optn
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
            this.components = new System.ComponentModel.Container();
            this.lblsvepriv = new System.Windows.Forms.Label();
            this.chbnewpw = new System.Windows.Forms.CheckBox();
            this.poid = new System.Windows.Forms.TextBox();
            this.poid2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ttp = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblsvepriv
            // 
            this.lblsvepriv.AutoSize = true;
            this.lblsvepriv.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblsvepriv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblsvepriv.Location = new System.Drawing.Point(251, 147);
            this.lblsvepriv.Name = "lblsvepriv";
            this.lblsvepriv.Size = new System.Drawing.Size(159, 13);
            this.lblsvepriv.TabIndex = 9;
            this.lblsvepriv.Text = "Your Settings have Been Saved";
            this.lblsvepriv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblsvepriv.Visible = false;
            // 
            // chbnewpw
            // 
            this.chbnewpw.AutoSize = true;
            this.chbnewpw.BackColor = System.Drawing.Color.Lavender;
            this.chbnewpw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbnewpw.ForeColor = System.Drawing.Color.DimGray;
            this.chbnewpw.Location = new System.Drawing.Point(331, 40);
            this.chbnewpw.Name = "chbnewpw";
            this.chbnewpw.Size = new System.Drawing.Size(60, 17);
            this.chbnewpw.TabIndex = 8;
            this.chbnewpw.Text = "Change";
            this.chbnewpw.UseVisualStyleBackColor = false;
            this.chbnewpw.CheckedChanged += new System.EventHandler(this.chbnewpw_CheckedChanged);
            // 
            // poid
            // 
            this.poid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.poid.Enabled = false;
            this.poid.Location = new System.Drawing.Point(160, 38);
            this.poid.Name = "poid";
            this.poid.PasswordChar = '.';
            this.poid.Size = new System.Drawing.Size(165, 20);
            this.poid.TabIndex = 4;
            this.poid.TextChanged += new System.EventHandler(this.confnewpwus_Click);
            // 
            // poid2
            // 
            this.poid2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.poid2.Enabled = false;
            this.poid2.Location = new System.Drawing.Point(160, 72);
            this.poid2.Name = "poid2";
            this.poid2.PasswordChar = '.';
            this.poid2.Size = new System.Drawing.Size(165, 20);
            this.poid2.TabIndex = 5;
            this.poid2.TextChanged += new System.EventHandler(this.confnewpwus_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Lavender;
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(39, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Confirm New Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Lavender;
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(39, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "New Password";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ttp
            // 
            this.ttp.IsBalloon = true;
            this.ttp.ToolTipTitle = "Informator";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblsvepriv);
            this.panel2.Controls.Add(this.chbnewpw);
            this.panel2.Controls.Add(this.poid);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.poid2);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Location = new System.Drawing.Point(7, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 162);
            this.panel2.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Enabled = false;
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(160, 106);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '.';
            this.textBox1.Size = new System.Drawing.Size(165, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.TextChanged += new System.EventHandler(this.confnewpwus_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Lavender;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(39, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Confirm Old Password";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 27);
            this.panel1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password Settings";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Main.Properties.Resources.shadowdwn;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(452, 18);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // First_use_optn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(448, 149);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "First_use_optn";
            this.Text = "First_use_optn";
            this.Load += new System.EventHandler(this.First_use_optn_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chbnewpw;
        private System.Windows.Forms.TextBox poid;
        private System.Windows.Forms.TextBox poid2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblsvepriv;
        private System.Windows.Forms.ToolTip ttp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}