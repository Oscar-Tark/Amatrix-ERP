namespace Main
{
    partial class Calendar_alrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calendar_alrm));
            this.tsalrm = new System.Windows.Forms.ToolStrip();
            this.clse = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.prevals = new System.Windows.Forms.ToolStripMenuItem();
            this.alrmclse = new System.Windows.Forms.Timer(this.components);
            this.stlrm = new System.Windows.Forms.Button();
            this.cncel = new System.Windows.Forms.Button();
            this.reas = new System.Windows.Forms.TextBox();
            this.prevtme = new System.Windows.Forms.Timer(this.components);
            this.dectmealrm = new System.Windows.Forms.Timer(this.components);
            this.calalenbl = new System.Windows.Forms.Timer(this.components);
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.tms = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tsalrm.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // tsalrm
            // 
            this.tsalrm.BackColor = System.Drawing.Color.White;
            this.tsalrm.BackgroundImage = global::Main.Properties.Resources.bannrrageconv;
            this.tsalrm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsalrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clse,
            this.toolStripButton1});
            this.tsalrm.Location = new System.Drawing.Point(0, 0);
            this.tsalrm.Name = "tsalrm";
            this.tsalrm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsalrm.Size = new System.Drawing.Size(276, 25);
            this.tsalrm.TabIndex = 0;
            this.tsalrm.Text = "toolStrip1";
            // 
            // clse
            // 
            this.clse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clse.ForeColor = System.Drawing.Color.Black;
            this.clse.Image = global::Main.Properties.Resources.extfin;
            this.clse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clse.Name = "clse";
            this.clse.Size = new System.Drawing.Size(23, 22);
            this.clse.Text = "toolStripButton1";
            this.clse.MouseLeave += new System.EventHandler(this.clse_MouseLeave);
            this.clse.MouseEnter += new System.EventHandler(this.clse_MouseEnter);
            this.clse.Click += new System.EventHandler(this.clse_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prevals});
            this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 22);
            this.toolStripButton1.Text = "Previous";
            // 
            // prevals
            // 
            this.prevals.BackColor = System.Drawing.Color.White;
            this.prevals.ForeColor = System.Drawing.Color.Black;
            this.prevals.Name = "prevals";
            this.prevals.Size = new System.Drawing.Size(197, 22);
            this.prevals.Text = "No Previous Alarms Set";
            this.prevals.Click += new System.EventHandler(this.prevals_Click);
            // 
            // alrmclse
            // 
            this.alrmclse.Interval = 3;
            this.alrmclse.Tick += new System.EventHandler(this.alrmclse_Tick);
            // 
            // stlrm
            // 
            this.stlrm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.stlrm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stlrm.BackgroundImage")));
            this.stlrm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stlrm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stlrm.ForeColor = System.Drawing.Color.Black;
            this.stlrm.Location = new System.Drawing.Point(110, 109);
            this.stlrm.Name = "stlrm";
            this.stlrm.Size = new System.Drawing.Size(61, 23);
            this.stlrm.TabIndex = 4;
            this.stlrm.Text = "Set Alarm";
            this.stlrm.UseVisualStyleBackColor = false;
            this.stlrm.Click += new System.EventHandler(this.stlrm_Click);
            // 
            // cncel
            // 
            this.cncel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cncel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cncel.BackgroundImage")));
            this.cncel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cncel.ForeColor = System.Drawing.Color.Black;
            this.cncel.Location = new System.Drawing.Point(177, 109);
            this.cncel.Name = "cncel";
            this.cncel.Size = new System.Drawing.Size(61, 23);
            this.cncel.TabIndex = 5;
            this.cncel.Text = "Cancel";
            this.cncel.UseVisualStyleBackColor = false;
            this.cncel.Click += new System.EventHandler(this.cncel_Click);
            // 
            // reas
            // 
            this.reas.BackColor = System.Drawing.Color.Lavender;
            this.reas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reas.ForeColor = System.Drawing.Color.Black;
            this.reas.Location = new System.Drawing.Point(11, 52);
            this.reas.Multiline = true;
            this.reas.Name = "reas";
            this.reas.Size = new System.Drawing.Size(227, 51);
            this.reas.TabIndex = 7;
            this.reas.Text = "Reason";
            this.reas.TextChanged += new System.EventHandler(this.reas_TextChanged);
            // 
            // prevtme
            // 
            this.prevtme.Enabled = true;
            this.prevtme.Interval = 10;
            this.prevtme.Tick += new System.EventHandler(this.prevtme_Tick);
            // 
            // dectmealrm
            // 
            this.dectmealrm.Interval = 10;
            this.dectmealrm.Tick += new System.EventHandler(this.dectmealrm_Tick);
            // 
            // calalenbl
            // 
            this.calalenbl.Enabled = true;
            this.calalenbl.Interval = 99;
            this.calalenbl.Tick += new System.EventHandler(this.calalenbltc);
            // 
            // dtp
            // 
            this.dtp.CalendarForeColor = System.Drawing.Color.Black;
            this.dtp.CustomFormat = "dd-MM-yyyy : hh-mm";
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp.Location = new System.Drawing.Point(87, 26);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(151, 20);
            this.dtp.TabIndex = 8;
            // 
            // tms
            // 
            this.tms.CalendarForeColor = System.Drawing.Color.Black;
            this.tms.CustomFormat = "hh:mm tt";
            this.tms.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tms.Location = new System.Drawing.Point(11, 26);
            this.tms.Name = "tms";
            this.tms.ShowUpDown = true;
            this.tms.Size = new System.Drawing.Size(70, 20);
            this.tms.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.reas);
            this.panel1.Controls.Add(this.dtp);
            this.panel1.Controls.Add(this.tms);
            this.panel1.Controls.Add(this.stlrm);
            this.panel1.Controls.Add(this.cncel);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 145);
            this.panel1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Set an Alarm";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::Main.Properties.Resources.shadowdwn;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(0, 19);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(276, 11);
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Main.Properties.Resources.shadowdwn;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 11);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Main.Properties.Resources.shadow___Copy;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(259, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(11, 145);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::Main.Properties.Resources.shadow___Copy___Copy;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(7, 36);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(11, 145);
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::Main.Properties.Resources.shadow;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(12, 31);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(253, 11);
            this.pictureBox4.TabIndex = 24;
            this.pictureBox4.TabStop = false;
            // 
            // Calendar_alrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(276, 188);
            this.Controls.Add(this.tsalrm);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox4);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Calendar_alrm";
            this.Text = "Calendar_alrm";
            this.Deactivate += new System.EventHandler(this.Calendar_alrm_Deactivate);
            this.Load += new System.EventHandler(this.Calendar_alrm_Load);
            this.Activated += new System.EventHandler(this.Calendar_alrm_Activated);
            this.tsalrm.ResumeLayout(false);
            this.tsalrm.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsalrm;
        private System.Windows.Forms.ToolStripButton clse;
        private System.Windows.Forms.Timer alrmclse;
        private System.Windows.Forms.Button stlrm;
        private System.Windows.Forms.Button cncel;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.TextBox reas;
        private System.Windows.Forms.ToolStripMenuItem prevals;
        private System.Windows.Forms.Timer prevtme;
        private System.Windows.Forms.Timer dectmealrm;
        private System.Windows.Forms.Timer calalenbl;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.DateTimePicker tms;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}