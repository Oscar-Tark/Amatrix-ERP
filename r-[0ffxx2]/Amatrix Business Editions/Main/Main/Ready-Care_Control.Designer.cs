namespace Main
{
    partial class Ready_Care_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ready_Care_Control));
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.bkk_prd = new System.ComponentModel.BackgroundWorker();
            this.bkk_check = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.errorlogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.err_dtst = new Main.err_dtst();
            this.tmeclsecal = new System.Windows.Forms.Timer(this.components);
            this.dectmecal = new System.Windows.Forms.Timer(this.components);
            this.error_logTableAdapter = new Main.err_dtstTableAdapters.error_logTableAdapter();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorlogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.err_dtst)).BeginInit();
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.White;
            this.il.Images.SetKeyName(0, "shld_trn.png");
            this.il.Images.SetKeyName(1, "coins.png");
            this.il.Images.SetKeyName(2, "alarm.png");
            // 
            // bkk_prd
            // 
            this.bkk_prd.WorkerSupportsCancellation = true;
            this.bkk_prd.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_prd_DoWork);
            this.bkk_prd.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_prd_RunWorkerCompleted);
            // 
            // bkk_check
            // 
            this.bkk_check.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_check_DoWork);
            this.bkk_check.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_check_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Lavender;
            this.menuStrip1.BackgroundImage = global::Main.Properties.Resources.vvm3;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 441);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(573, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reToolStripMenuItem,
            this.toolStripSeparator2,
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Image = global::Main.Properties.Resources.file1trans;
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // reToolStripMenuItem
            // 
            this.reToolStripMenuItem.Image = global::Main.Properties.Resources.refrsh;
            this.reToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.reToolStripMenuItem.Name = "reToolStripMenuItem";
            this.reToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.reToolStripMenuItem.Text = "Renew ASL [Licence Registration] Certificates";
            this.reToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(308, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Image = global::Main.Properties.Resources.restart;
            this.restartToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Main.Properties.Resources.ex;
            this.exitToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.btn_clse_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.helpToolStripMenuItem.Image = global::Main.Properties.Resources.hlp1;
            this.helpToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::Main.Properties.Resources.threeballs;
            this.aboutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.il;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(573, 441);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage3
            // 
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(565, 414);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "Time & Date";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.ImageIndex = 1;
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(565, 414);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Text = "Currencies";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // errorlogBindingSource
            // 
            this.errorlogBindingSource.DataMember = "error_log";
            this.errorlogBindingSource.DataSource = this.err_dtst;
            // 
            // err_dtst
            // 
            this.err_dtst.DataSetName = "err_dtst";
            this.err_dtst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tmeclsecal
            // 
            this.tmeclsecal.Interval = 3;
            this.tmeclsecal.Tick += new System.EventHandler(this.tmeclsecal_Tick);
            // 
            // dectmecal
            // 
            this.dectmecal.Interval = 10;
            this.dectmecal.Tick += new System.EventHandler(this.dectmecal_Tick);
            // 
            // error_logTableAdapter
            // 
            this.error_logTableAdapter.ClearBeforeFill = true;
            // 
            // Ready_Care_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(573, 465);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Ready_Care_Control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Activated += new System.EventHandler(this.Ready_Care_Control_Activated);
            this.Deactivate += new System.EventHandler(this.Ready_Care_Control_Deactivate);
            this.Load += new System.EventHandler(this.Ready_Care_Control_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorlogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.err_dtst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList il;
        private System.ComponentModel.BackgroundWorker bkk_prd;
        private System.ComponentModel.BackgroundWorker bkk_check;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem reToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private err_dtst err_dtst;
        private System.Windows.Forms.BindingSource errorlogBindingSource;
        private Main.err_dtstTableAdapters.error_logTableAdapter error_logTableAdapter;
        private System.Windows.Forms.Timer tmeclsecal;
        private System.Windows.Forms.Timer dectmecal;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage5;

    }
}