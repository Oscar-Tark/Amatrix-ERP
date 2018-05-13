namespace Main
{
    partial class Helper
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Opening an Application");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Adding Links");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Changing Settings");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Using Amatrix", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Opening an Application");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Document Studio");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Managment Studio");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Accounting Studio");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Business Studio");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("DataBase Studio");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Shoppe");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Using The Applications", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Using The Internal DataBase");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Using Your Server For our DataBase");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Using The Network Facilities", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Windows Calculator");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Calendar");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("WorkBook");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Screen Snip");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Using The Utilities", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Legalities");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Helper));
            this.tthlp = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.dectmeabt = new System.Windows.Forms.Timer(this.components);
            this.abtclse = new System.Windows.Forms.Timer(this.components);
            this.tv1 = new System.Windows.Forms.TreeView();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.clsehlper = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.effectsDisabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectsDisabledToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(713, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 13;
            // 
            // dectmeabt
            // 
            this.dectmeabt.Interval = 10;
            this.dectmeabt.Tick += new System.EventHandler(this.dectmeabt_Tick);
            // 
            // abtclse
            // 
            this.abtclse.Interval = 3;
            this.abtclse.Tick += new System.EventHandler(this.abtclse_Tick);
            // 
            // tv1
            // 
            this.tv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tv1.FullRowSelect = true;
            this.tv1.ImageIndex = 0;
            this.tv1.ImageList = this.il;
            this.tv1.Location = new System.Drawing.Point(0, 24);
            this.tv1.Name = "tv1";
            treeNode1.BackColor = System.Drawing.Color.Transparent;
            treeNode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode1.Name = "Node8";
            treeNode1.Text = "Opening an Application";
            treeNode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode2.Name = "Node9";
            treeNode2.Text = "Adding Links";
            treeNode3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode3.Name = "Node11";
            treeNode3.Text = "Changing Settings";
            treeNode4.BackColor = System.Drawing.Color.Transparent;
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Node0";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "Using Amatrix";
            treeNode5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode5.Name = "Node12";
            treeNode5.Text = "Opening an Application";
            treeNode6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode6.Name = "Node15";
            treeNode6.Text = "Document Studio";
            treeNode7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode7.Name = "Node14";
            treeNode7.Text = "Managment Studio";
            treeNode8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode8.Name = "Node13";
            treeNode8.Text = "Accounting Studio";
            treeNode9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode9.Name = "Node17";
            treeNode9.Text = "Business Studio";
            treeNode10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode10.Name = "Node18";
            treeNode10.Text = "DataBase Studio";
            treeNode11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode11.Name = "Node19";
            treeNode11.Text = "Shoppe";
            treeNode12.ImageIndex = 1;
            treeNode12.Name = "Node1";
            treeNode12.SelectedImageIndex = 1;
            treeNode12.Text = "Using The Applications";
            treeNode13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode13.Name = "Node20";
            treeNode13.Text = "Using The Internal DataBase";
            treeNode14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode14.Name = "Node21";
            treeNode14.Text = "Using Your Server For our DataBase";
            treeNode15.ImageIndex = 1;
            treeNode15.Name = "Node2";
            treeNode15.SelectedImageIndex = 1;
            treeNode15.Text = "Using The Network Facilities";
            treeNode16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode16.Name = "Node22";
            treeNode16.Text = "Windows Calculator";
            treeNode17.BackColor = System.Drawing.Color.White;
            treeNode17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode17.Name = "Node23";
            treeNode17.Text = "Calendar";
            treeNode18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode18.Name = "Node24";
            treeNode18.Text = "WorkBook";
            treeNode19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode19.Name = "Node25";
            treeNode19.Text = "Screen Snip";
            treeNode20.ImageIndex = 1;
            treeNode20.Name = "Node3";
            treeNode20.SelectedImageIndex = 1;
            treeNode20.Text = "Using The Utilities";
            treeNode21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            treeNode21.ImageIndex = 0;
            treeNode21.Name = "Node10";
            treeNode21.Text = "Legalities";
            this.tv1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode12,
            treeNode15,
            treeNode20,
            treeNode21});
            this.tv1.SelectedImageIndex = 0;
            this.tv1.ShowPlusMinus = false;
            this.tv1.ShowRootLines = false;
            this.tv1.Size = new System.Drawing.Size(230, 427);
            this.tv1.TabIndex = 26;
            this.tv1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv1_AfterSelect);
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.White;
            this.il.Images.SetKeyName(0, "hlp.bmp");
            this.il.Images.SetKeyName(1, "index.bmp");
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(233, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(697, 426);
            this.webBrowser1.TabIndex = 28;
            // 
            // clsehlper
            // 
            this.clsehlper.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clsehlper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clsehlper.Image = global::Main.Properties.Resources.extfin;
            this.clsehlper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clsehlper.Name = "clsehlper";
            this.clsehlper.Size = new System.Drawing.Size(23, 22);
            this.clsehlper.Text = "toolStripButton3";
            this.clsehlper.MouseLeave += new System.EventHandler(this.clseexthlp);
            this.clsehlper.MouseEnter += new System.EventHandler(this.clsehovhlp);
            this.clsehlper.Click += new System.EventHandler(this.clseclseclck);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.BackgroundImage = global::Main.Properties.Resources.bannrrageconv;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clsehlper,
            this.toolStripLabel1,
            this.effectsDisabledToolStripMenuItem});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(930, 25);
            this.toolStrip2.TabIndex = 15;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.ForeColor = System.Drawing.Color.LightSlateGray;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(76, 22);
            this.toolStripLabel1.Text = "Amatrix Help";
            // 
            // effectsDisabledToolStripMenuItem
            // 
            this.effectsDisabledToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.effectsDisabledToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.effectsDisabledToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effectsDisabledToolStripMenuItem1});
            this.effectsDisabledToolStripMenuItem.Image = global::Main.Properties.Resources.shld;
            this.effectsDisabledToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.effectsDisabledToolStripMenuItem.Name = "effectsDisabledToolStripMenuItem";
            this.effectsDisabledToolStripMenuItem.Size = new System.Drawing.Size(28, 25);
            this.effectsDisabledToolStripMenuItem.Text = "Notification";
            // 
            // effectsDisabledToolStripMenuItem1
            // 
            this.effectsDisabledToolStripMenuItem1.Image = global::Main.Properties.Resources.shld;
            this.effectsDisabledToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.White;
            this.effectsDisabledToolStripMenuItem1.Name = "effectsDisabledToolStripMenuItem1";
            this.effectsDisabledToolStripMenuItem1.Size = new System.Drawing.Size(290, 22);
            this.effectsDisabledToolStripMenuItem1.Text = "Effects Disabled to Enhance Visual Clarity";
            // 
            // Helper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(930, 453);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.tv1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.label6);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(1024, 800);
            this.Name = "Helper";
            this.Text = "Amatrix Helper [Version 1.0]";
            this.Deactivate += new System.EventHandler(this.hldec);
            this.Load += new System.EventHandler(this.Helper_Load);
            this.Activated += new System.EventHandler(this.hlpact);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip tthlp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer dectmeabt;
        private System.Windows.Forms.Timer abtclse;
        private System.Windows.Forms.TreeView tv1;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripButton clsehlper;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem effectsDisabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectsDisabledToolStripMenuItem1;


    }
}