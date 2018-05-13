namespace Astreous_Device_Module
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.tsmn = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.clse_mn = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem82 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.mnutll = new System.Windows.Forms.ToolStripDropDownButton();
            this.aSLLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyright2011OscarArjunSinghTarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mov = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tsmn.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "searchbsc.bmp");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.tsmn);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 110);
            this.panel1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.DimGray;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.ImageIndex = 0;
            this.button3.ImageList = this.imageList1;
            this.button3.Location = new System.Drawing.Point(350, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Find Printers";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tsmn
            // 
            this.tsmn.BackColor = System.Drawing.Color.LightGray;
            this.tsmn.BackgroundImage = global::Main.Properties.Resources.banntransp2;
            this.tsmn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsmn.Enabled = false;
            this.tsmn.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsmn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.clse_mn,
            this.toolStripSeparator44,
            this.toolStripButton8,
            this.toolStripButton9,
            this.mnutll});
            this.tsmn.Location = new System.Drawing.Point(0, 0);
            this.tsmn.Name = "tsmn";
            this.tsmn.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsmn.Size = new System.Drawing.Size(462, 25);
            this.tsmn.TabIndex = 16;
            this.tsmn.Text = "toolStrip1";
            this.tsmn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tsmn_MouseUp);
            this.tsmn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsmn_MouseDown);
            this.tsmn.Click += new System.EventHandler(this.mov_clc);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            this.toolStripLabel1.Text = "toolStripButton1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(185, 22);
            this.toolStripLabel2.Tag = "1";
            this.toolStripLabel2.Text = "Astreous Remote Device Manager";
            // 
            // clse_mn
            // 
            this.clse_mn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clse_mn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clse_mn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem82});
            this.clse_mn.Image = global::Main.Properties.Resources.extfin;
            this.clse_mn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clse_mn.Name = "clse_mn";
            this.clse_mn.Size = new System.Drawing.Size(32, 22);
            this.clse_mn.Text = "toolStripButton1";
            this.clse_mn.ToolTipText = "Close Xaria including all related gadgets";
            this.clse_mn.ButtonClick += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem82
            // 
            this.toolStripMenuItem82.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem82.Name = "toolStripMenuItem82";
            this.toolStripMenuItem82.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItem82.Text = "Restart";
            this.toolStripMenuItem82.Click += new System.EventHandler(this.toolStripMenuItem82_Click);
            // 
            // toolStripSeparator44
            // 
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            this.toolStripSeparator44.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Enabled = false;
            this.toolStripButton8.Image = global::Main.Properties.Resources.max;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "toolStripButton1";
            this.toolStripButton8.ToolTipText = "Maximize Xaria";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::Main.Properties.Resources.minmze1;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "toolStripButton3";
            this.toolStripButton9.ToolTipText = "Minimize Xaria";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // mnutll
            // 
            this.mnutll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnutll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aSLLicenseToolStripMenuItem,
            this.fileToolStripMenuItem1});
            this.mnutll.Image = global::Main.Properties.Resources.mnu1;
            this.mnutll.ImageTransparentColor = System.Drawing.Color.White;
            this.mnutll.Name = "mnutll";
            this.mnutll.Size = new System.Drawing.Size(29, 22);
            this.mnutll.Text = "toolStripButton4";
            this.mnutll.ToolTipText = "Menu";
            // 
            // aSLLicenseToolStripMenuItem
            // 
            this.aSLLicenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyright2011OscarArjunSinghTarkToolStripMenuItem});
            this.aSLLicenseToolStripMenuItem.Name = "aSLLicenseToolStripMenuItem";
            this.aSLLicenseToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aSLLicenseToolStripMenuItem.Text = "ASL License";
            // 
            // thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem
            // 
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem.ForeColor = System.Drawing.Color.DimGray;
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem.Name = "thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem";
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem.Size = new System.Drawing.Size(413, 22);
            this.thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem.Text = "This Program is Protected by the ASL Small Program Amendment";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(410, 6);
            // 
            // copyright2011OscarArjunSinghTarkToolStripMenuItem
            // 
            this.copyright2011OscarArjunSinghTarkToolStripMenuItem.Name = "copyright2011OscarArjunSinghTarkToolStripMenuItem";
            this.copyright2011OscarArjunSinghTarkToolStripMenuItem.Size = new System.Drawing.Size(413, 22);
            this.copyright2011OscarArjunSinghTarkToolStripMenuItem.Text = "© Copyright 2011 Oscar Arjun Singh Tark";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Image = global::Main.Properties.Resources.extfin;
            this.fileToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.fileToolStripMenuItem1.Text = "Exit";
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.DimGray;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(25, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Search For IP Addresses";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.DimGray;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageIndex = 0;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(178, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Search For Device Names";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(10, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Result of Your Search";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Lavender;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;
            this.richTextBox1.Location = new System.Drawing.Point(12, 128);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(440, 144);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // mov
            // 
            this.mov.Interval = 2;
            this.mov.Tick += new System.EventHandler(this.mov_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(464, 110);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Astreous Remote Device Manger";
            this.Deactivate += new System.EventHandler(this.mov_clc);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsmn.ResumeLayout(false);
            this.tsmn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip tsmn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSplitButton clse_mn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem82;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator44;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripDropDownButton mnutll;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem aSLLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thisProgramIsProtectedByTheASLSmallProgramAmendmentToolStripMenuItem;
        private System.Windows.Forms.Timer mov;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyright2011OscarArjunSinghTarkToolStripMenuItem;
        private System.Windows.Forms.Button button3;
    }
}

