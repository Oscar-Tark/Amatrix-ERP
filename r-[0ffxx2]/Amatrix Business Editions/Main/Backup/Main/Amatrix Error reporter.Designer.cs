namespace Main
{
    partial class Am_err
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
            this.clseerr = new System.Windows.Forms.Button();
            this.dectmeerr = new System.Windows.Forms.Timer(this.components);
            this.clseerrr = new System.Windows.Forms.Timer(this.components);
            this.enblerr = new System.Windows.Forms.Timer(this.components);
            this.uku = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbxmssge = new System.Windows.Forms.TextBox();
            this.bkk = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clseerr
            // 
            this.clseerr.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.clseerr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clseerr.FlatAppearance.BorderSize = 0;
            this.clseerr.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.clseerr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.clseerr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clseerr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.clseerr.Location = new System.Drawing.Point(282, 156);
            this.clseerr.Name = "clseerr";
            this.clseerr.Size = new System.Drawing.Size(87, 48);
            this.clseerr.TabIndex = 0;
            this.clseerr.Text = "Close";
            this.clseerr.UseVisualStyleBackColor = true;
            this.clseerr.MouseLeave += new System.EventHandler(this.uku_MouseLeave);
            this.clseerr.Click += new System.EventHandler(this.clseerr_Click);
            this.clseerr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uku_MouseDown);
            this.clseerr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uku_MouseUp);
            this.clseerr.MouseEnter += new System.EventHandler(this.uku_MouseEnter);
            // 
            // dectmeerr
            // 
            this.dectmeerr.Interval = 10;
            this.dectmeerr.Tick += new System.EventHandler(this.dectmeerr_Tick);
            // 
            // clseerrr
            // 
            this.clseerrr.Interval = 3;
            this.clseerrr.Tick += new System.EventHandler(this.clseerrr_Tick);
            // 
            // enblerr
            // 
            this.enblerr.Enabled = true;
            this.enblerr.Tick += new System.EventHandler(this.enblerrtc);
            // 
            // uku
            // 
            this.uku.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.uku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.uku.FlatAppearance.BorderSize = 0;
            this.uku.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.uku.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.uku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uku.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uku.Location = new System.Drawing.Point(189, 156);
            this.uku.Name = "uku";
            this.uku.Size = new System.Drawing.Size(87, 48);
            this.uku.TabIndex = 4;
            this.uku.Text = "Ok";
            this.uku.UseVisualStyleBackColor = true;
            this.uku.MouseLeave += new System.EventHandler(this.uku_MouseLeave);
            this.uku.Click += new System.EventHandler(this.clseerr_Click);
            this.uku.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uku_MouseDown);
            this.uku.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uku_MouseUp);
            this.uku.MouseEnter += new System.EventHandler(this.uku_MouseEnter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(371, 164);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbxmssge);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(363, 138);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Error Message";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbxmssge
            // 
            this.tbxmssge.BackColor = System.Drawing.Color.White;
            this.tbxmssge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxmssge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxmssge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxmssge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbxmssge.Location = new System.Drawing.Point(3, 3);
            this.tbxmssge.Multiline = true;
            this.tbxmssge.Name = "tbxmssge";
            this.tbxmssge.ReadOnly = true;
            this.tbxmssge.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxmssge.Size = new System.Drawing.Size(357, 132);
            this.tbxmssge.TabIndex = 4;
            this.tbxmssge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bkk
            // 
            this.bkk.WorkerSupportsCancellation = true;
            this.bkk.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_DoWork);
            this.bkk.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_RunWorkerCompleted);
            // 
            // Am_err
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(369, 195);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.uku);
            this.Controls.Add(this.clseerr);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Am_err";
            this.Text = "Form1";
            this.Deactivate += new System.EventHandler(this.Am_err_Deactivate);
            this.Load += new System.EventHandler(this.Am_err_Load);
            this.Activated += new System.EventHandler(this.Am_err_Activated);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clseerr;
        private System.Windows.Forms.Timer dectmeerr;
        private System.Windows.Forms.Timer clseerrr;
        private System.Windows.Forms.Timer enblerr;
        private System.Windows.Forms.Button uku;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbxmssge;
        private System.ComponentModel.BackgroundWorker bkk;
    }
}