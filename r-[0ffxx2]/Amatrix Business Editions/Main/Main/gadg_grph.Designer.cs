namespace Main
{
    partial class gadg_grph
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Debit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Credit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Debit And Credit Totaled Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("General Journal", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Debit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Credit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Debit And Credit Totaled Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Cash Book", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Debit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Credit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Debit And Credit Totaled Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Purchase Book", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Credit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Sales Book", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Journal", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode12,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Debit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Credit Comparison Per Month", 1, 1);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Invoices", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Accounting", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode18});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gadg_grph));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.clse = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.boxChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invert = new System.Windows.Forms.ToolStripButton();
            this.pnl_drw = new System.Windows.Forms.Panel();
            this.tv = new System.Windows.Forms.TreeView();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.suppmgmt_dtst = new Main.suppmgmt_dtst();
            this.supplymgmtBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.supply_mgmtTableAdapter = new Main.suppmgmt_dtstTableAdapters.Supply_mgmtTableAdapter();
            this.pnl_drw1 = new System.Windows.Forms.Panel();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.bkk = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            this.pnl_drw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.suppmgmt_dtst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplymgmtBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Lavender;
            this.toolStrip1.BackgroundImage = global::Main.Properties.Resources.vvm2;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clse,
            this.toolStripButton10,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.invert});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(869, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // clse
            // 
            this.clse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clse.Image = global::Main.Properties.Resources.extfin;
            this.clse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clse.Name = "clse";
            this.clse.Size = new System.Drawing.Size(56, 22);
            this.clse.Text = "Close";
            this.clse.Click += new System.EventHandler(this.clse_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = global::Main.Properties.Resources.Save1;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton10.Text = "Save As...";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boxChartToolStripMenuItem,
            this.lineChartToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Main.Properties.Resources.Graphing_style;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "Change Graph Style";
            // 
            // boxChartToolStripMenuItem
            // 
            this.boxChartToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.boxChartToolStripMenuItem.Name = "boxChartToolStripMenuItem";
            this.boxChartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.boxChartToolStripMenuItem.Text = "Box Chart";
            this.boxChartToolStripMenuItem.Visible = false;
            this.boxChartToolStripMenuItem.Click += new System.EventHandler(this.Change_Style);
            // 
            // lineChartToolStripMenuItem
            // 
            this.lineChartToolStripMenuItem.Checked = true;
            this.lineChartToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lineChartToolStripMenuItem.Name = "lineChartToolStripMenuItem";
            this.lineChartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lineChartToolStripMenuItem.Text = "Line Chart";
            this.lineChartToolStripMenuItem.Click += new System.EventHandler(this.Change_Style);
            // 
            // invert
            // 
            this.invert.CheckOnClick = true;
            this.invert.Image = global::Main.Properties.Resources.rotat180180;
            this.invert.ImageTransparentColor = System.Drawing.Color.White;
            this.invert.Name = "invert";
            this.invert.Size = new System.Drawing.Size(57, 22);
            this.invert.Text = "Invert";
            this.invert.Click += new System.EventHandler(this.invert_Click);
            // 
            // pnl_drw
            // 
            this.pnl_drw.BackColor = System.Drawing.Color.White;
            this.pnl_drw.Controls.Add(this.tv);
            this.pnl_drw.Controls.Add(this.pictureBox1);
            this.pnl_drw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_drw.Location = new System.Drawing.Point(0, 25);
            this.pnl_drw.Name = "pnl_drw";
            this.pnl_drw.Size = new System.Drawing.Size(869, 433);
            this.pnl_drw.TabIndex = 1;
            // 
            // tv
            // 
            this.tv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.il;
            this.tv.Location = new System.Drawing.Point(0, 6);
            this.tv.Name = "tv";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "Node11";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "Debit Comparison Per Month";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "Node12";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "Credit Comparison Per Month";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "Node2";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "Debit And Credit Totaled Comparison Per Month";
            treeNode4.Name = "Node6";
            treeNode4.Text = "General Journal";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "Node13";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "Debit Comparison Per Month";
            treeNode6.ImageIndex = 1;
            treeNode6.Name = "Node14";
            treeNode6.SelectedImageIndex = 1;
            treeNode6.Text = "Credit Comparison Per Month";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Node0";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "Debit And Credit Totaled Comparison Per Month";
            treeNode8.Name = "Node7";
            treeNode8.Text = "Cash Book";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "Node15";
            treeNode9.SelectedImageIndex = 1;
            treeNode9.Text = "Debit Comparison Per Month";
            treeNode10.ImageIndex = 1;
            treeNode10.Name = "Node16";
            treeNode10.SelectedImageIndex = 1;
            treeNode10.Text = "Credit Comparison Per Month";
            treeNode11.ImageIndex = 1;
            treeNode11.Name = "Node1";
            treeNode11.SelectedImageIndex = 1;
            treeNode11.Text = "Debit And Credit Totaled Comparison Per Month";
            treeNode12.Name = "Node8";
            treeNode12.Text = "Purchase Book";
            treeNode13.ImageIndex = 1;
            treeNode13.Name = "Node18";
            treeNode13.SelectedImageIndex = 1;
            treeNode13.Text = "Credit Comparison Per Month";
            treeNode14.Name = "Node9";
            treeNode14.Text = "Sales Book";
            treeNode15.Name = "Node2";
            treeNode15.Text = "Journal";
            treeNode16.ImageIndex = 1;
            treeNode16.Name = "Node20";
            treeNode16.SelectedImageIndex = 1;
            treeNode16.Text = "Debit Comparison Per Month";
            treeNode17.ImageIndex = 1;
            treeNode17.Name = "Node19";
            treeNode17.SelectedImageIndex = 1;
            treeNode17.Text = "Credit Comparison Per Month";
            treeNode18.Name = "Node0";
            treeNode18.Text = "Invoices";
            treeNode19.Name = "Node1";
            treeNode19.Text = "Accounting";
            this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19});
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(172, 428);
            this.tv.TabIndex = 0;
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseClick);
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.White;
            this.il.Images.SetKeyName(0, "dropper.bmp");
            this.il.Images.SetKeyName(1, "Graphing.bmp");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::Main.Properties.Resources.shadowdwn;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-1, -8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(871, 15);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // suppmgmt_dtst
            // 
            this.suppmgmt_dtst.DataSetName = "suppmgmt_dtst";
            this.suppmgmt_dtst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // supplymgmtBindingSource
            // 
            this.supplymgmtBindingSource.DataMember = "Supply_mgmt";
            this.supplymgmtBindingSource.DataSource = this.suppmgmt_dtst;
            // 
            // supply_mgmtTableAdapter
            // 
            this.supply_mgmtTableAdapter.ClearBeforeFill = true;
            // 
            // pnl_drw1
            // 
            this.pnl_drw1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_drw1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pnl_drw1.Location = new System.Drawing.Point(171, 31);
            this.pnl_drw1.Name = "pnl_drw1";
            this.pnl_drw1.Size = new System.Drawing.Size(699, 428);
            this.pnl_drw1.TabIndex = 1;
            // 
            // sfd
            // 
            this.sfd.Filter = "JPEG image|*jpg";
            this.sfd.Title = "Save As Image";
            this.sfd.FileOk += new System.ComponentModel.CancelEventHandler(this.sfd_FileOk);
            // 
            // bkk
            // 
            this.bkk.WorkerSupportsCancellation = true;
            this.bkk.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_DoWork);
            // 
            // gadg_grph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnl_drw1);
            this.Controls.Add(this.pnl_drw);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "gadg_grph";
            this.Size = new System.Drawing.Size(869, 458);
            this.Load += new System.EventHandler(this.gadg_grph_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnl_drw.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.suppmgmt_dtst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplymgmtBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel pnl_drw;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private suppmgmt_dtst suppmgmt_dtst;
        private System.Windows.Forms.BindingSource supplymgmtBindingSource;
        private Main.suppmgmt_dtstTableAdapters.Supply_mgmtTableAdapter supply_mgmtTableAdapter;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Panel pnl_drw1;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStripButton clse;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem boxChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineChartToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton invert;
        private System.ComponentModel.BackgroundWorker bkk;
        //private ZedGraph.ZedGraphControl zg1;
    }
}
