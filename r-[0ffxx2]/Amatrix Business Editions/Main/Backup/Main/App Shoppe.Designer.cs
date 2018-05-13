namespace Main
{
    partial class App_Shoppe
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
            this.sllpnl = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addr = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.shproccess = new System.Windows.Forms.Timer(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.shpclse = new System.Windows.Forms.ToolStripSplitButton();
            this.amatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rstrtshp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.clseshp = new System.Windows.Forms.Timer(this.components);
            this.dectmeshp = new System.Windows.Forms.Timer(this.components);
            this.wb = new System.Windows.Forms.WebBrowser();
            this.dots = new System.Windows.Forms.Timer(this.components);
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sllpnl.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sllpnl
            // 
            this.sllpnl.BackColor = System.Drawing.Color.White;
            this.sllpnl.BackgroundImage = global::Main.Properties.Resources.bannrrageconv;
            this.sllpnl.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.sllpnl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripLabel2,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripButton4,
            this.toolStripSeparator2,
            this.addr,
            this.toolStripLabel1});
            this.sllpnl.Location = new System.Drawing.Point(0, 0);
            this.sllpnl.Name = "sllpnl";
            this.sllpnl.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.sllpnl.Size = new System.Drawing.Size(951, 25);
            this.sllpnl.TabIndex = 0;
            this.sllpnl.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(93, 22);
            this.toolStripLabel3.Text = "Internet Explorer";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(72, 22);
            this.toolStripLabel2.Text = "Powered by ";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Main.Properties.Resources.hme2;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "Home";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.Image = global::Main.Properties.Resources.envelpe_sent;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(155, 22);
            this.toolStripButton4.Text = "Submit an Advertisment";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // addr
            // 
            this.addr.BackColor = System.Drawing.Color.Transparent;
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(30, 22);
            this.addr.Text = "N.A.";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.toolStripLabel1.Text = ".";
            // 
            // shproccess
            // 
            this.shproccess.Enabled = true;
            this.shproccess.Tick += new System.EventHandler(this.shproccesstc);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.shpclse,
            this.toolStripButton5,
            this.toolStripSeparator4,
            this.toolStripLabel4});
            this.toolStrip2.Location = new System.Drawing.Point(0, 482);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(951, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripButton3.Image = global::Main.Properties.Resources.backarr;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(70, 22);
            this.toolStripButton3.Text = "Forward";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripButton2.Image = global::Main.Properties.Resources.frontarr;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(72, 22);
            this.toolStripButton2.Text = "Previous";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // shpclse
            // 
            this.shpclse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.shpclse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shpclse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amatrixToolStripMenuItem,
            this.rstrtshp});
            this.shpclse.ForeColor = System.Drawing.Color.Silver;
            this.shpclse.Image = global::Main.Properties.Resources.extfin;
            this.shpclse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shpclse.Name = "shpclse";
            this.shpclse.Size = new System.Drawing.Size(32, 22);
            this.shpclse.Text = "toolStripButton1";
            this.shpclse.MouseLeave += new System.EventHandler(this.shpclseext);
            this.shpclse.ButtonClick += new System.EventHandler(this.shpclse_Click);
            this.shpclse.MouseEnter += new System.EventHandler(this.shpclsehov);
            // 
            // amatrixToolStripMenuItem
            // 
            this.amatrixToolStripMenuItem.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.amatrixToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.amatrixToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.amatrixToolStripMenuItem.Name = "amatrixToolStripMenuItem";
            this.amatrixToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.amatrixToolStripMenuItem.Text = "Amatrix";
            // 
            // rstrtshp
            // 
            this.rstrtshp.Image = global::Main.Properties.Resources.restart;
            this.rstrtshp.ImageTransparentColor = System.Drawing.Color.White;
            this.rstrtshp.Name = "rstrtshp";
            this.rstrtshp.Size = new System.Drawing.Size(115, 22);
            this.rstrtshp.Text = "Restart";
            this.rstrtshp.Click += new System.EventHandler(this.rstrtshp_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel4.Text = "Loaded";
            this.toolStripLabel4.Visible = false;
            // 
            // clseshp
            // 
            this.clseshp.Interval = 3;
            this.clseshp.Tick += new System.EventHandler(this.clseshptc);
            // 
            // dectmeshp
            // 
            this.dectmeshp.Interval = 10;
            this.dectmeshp.Tick += new System.EventHandler(this.dectmeshp_Tick);
            // 
            // wb
            // 
            this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb.Location = new System.Drawing.Point(0, 25);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.ScriptErrorsSuppressed = true;
            this.wb.Size = new System.Drawing.Size(951, 457);
            this.wb.TabIndex = 2;
            this.wb.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.wb_ProgressChanged);
            this.wb.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wb_Navigating);
            this.wb.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wb_Navigated);
            // 
            // dots
            // 
            this.dots.Enabled = true;
            this.dots.Interval = 800;
            this.dots.Tick += new System.EventHandler(this.dots_Tick);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Main.Properties.Resources.extrns;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Cancel Navigation";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // App_Shoppe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(951, 507);
            this.Controls.Add(this.wb);
            this.Controls.Add(this.sllpnl);
            this.Controls.Add(this.toolStrip2);
            this.DoubleBuffered = true;
            this.Name = "App_Shoppe";
            this.Text = "App_Shoppe";
            this.Deactivate += new System.EventHandler(this.Appshpdev);
            this.Load += new System.EventHandler(this.App_Shoppe_Load);
            this.DoubleClick += new System.EventHandler(this.App_Shoppe_dc);
            this.Activated += new System.EventHandler(this.App_Shoppe_Activated);
            this.sllpnl.ResumeLayout(false);
            this.sllpnl.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip sllpnl;
        private System.Windows.Forms.Timer shproccess;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer clseshp;
        private System.Windows.Forms.Timer dectmeshp;
        private System.Windows.Forms.ToolStripSplitButton shpclse;
        private System.Windows.Forms.ToolStripMenuItem rstrtshp;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Timer dots;
        private System.Windows.Forms.ToolStripLabel addr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripMenuItem amatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}