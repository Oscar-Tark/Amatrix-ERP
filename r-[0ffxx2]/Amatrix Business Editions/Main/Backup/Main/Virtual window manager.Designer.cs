namespace Main
{
    partial class Virtual_window_manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Virtual_window_manager));
            this.actvvm = new System.Windows.Forms.Timer(this.components);
            this.decvvm = new System.Windows.Forms.Timer(this.components);
            this.tsvvm = new System.Windows.Forms.ToolStrip();
            this.cmsvvm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.orientationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpvvm = new System.Windows.Forms.ToolStripMenuItem();
            this.bttvvm = new System.Windows.Forms.ToolStripMenuItem();
            this.clseqb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.vvmclse = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.restartAmatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAmatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hmets = new System.Windows.Forms.ToolStripDropDownButton();
            this.nwam = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeAndCompanyInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.connectionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSettingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oats = new System.Windows.Forms.ToolStripDropDownButton();
            this.showDocumentStudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devts = new System.Windows.Forms.ToolStripDropDownButton();
            this.mgmtstd = new System.Windows.Forms.ToolStripMenuItem();
            this.vvmmkt = new System.Windows.Forms.ToolStripMenuItem();
            this.projectManagmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openHumanResourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCustomerManagmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProductManagmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTaskManagmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accstd = new System.Windows.Forms.ToolStripDropDownButton();
            this.accshw = new System.Windows.Forms.ToolStripMenuItem();
            this.jrnlstrt = new System.Windows.Forms.ToolStripMenuItem();
            this.ldg = new System.Windows.Forms.ToolStripMenuItem();
            this.incvd = new System.Windows.Forms.ToolStripMenuItem();
            this.csts = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.grapherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbts = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openRePartitionerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shptls = new System.Windows.Forms.ToolStripButton();
            this.tstme = new System.Windows.Forms.ToolStripLabel();
            this.clsevvm = new System.Windows.Forms.Timer(this.components);
            this.enblvvm = new System.Windows.Forms.Timer(this.components);
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsvvm.SuspendLayout();
            this.cmsvvm.SuspendLayout();
            this.SuspendLayout();
            // 
            // actvvm
            // 
            this.actvvm.Interval = 10;
            // 
            // decvvm
            // 
            this.decvvm.Interval = 10;
            this.decvvm.Tick += new System.EventHandler(this.decvvm_Tick);
            // 
            // tsvvm
            // 
            this.tsvvm.BackColor = System.Drawing.Color.Transparent;
            this.tsvvm.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.tsvvm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsvvm.ContextMenuStrip = this.cmsvvm;
            this.tsvvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsvvm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsvvm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.vvmclse,
            this.hmets,
            this.oats,
            this.devts,
            this.accstd,
            this.csts,
            this.dbts,
            this.shptls,
            this.tstme});
            this.tsvvm.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsvvm.Location = new System.Drawing.Point(0, 0);
            this.tsvvm.Name = "tsvvm";
            this.tsvvm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsvvm.Size = new System.Drawing.Size(1081, 33);
            this.tsvvm.TabIndex = 0;
            this.tsvvm.Text = "toolStrip1";
            this.tsvvm.MouseEnter += new System.EventHandler(this.vvmhov);
            this.tsvvm.MouseLeave += new System.EventHandler(this.vvmext);
            // 
            // cmsvvm
            // 
            this.cmsvvm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmsvvm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orientationToolStripMenuItem,
            this.clseqb});
            this.cmsvvm.Name = "contextMenuStrip1";
            this.cmsvvm.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsvvm.Size = new System.Drawing.Size(135, 48);
            // 
            // orientationToolStripMenuItem
            // 
            this.orientationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tpvvm,
            this.bttvvm});
            this.orientationToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.orientationToolStripMenuItem.Name = "orientationToolStripMenuItem";
            this.orientationToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.orientationToolStripMenuItem.Text = "Orientation";
            // 
            // tpvvm
            // 
            this.tpvvm.ForeColor = System.Drawing.Color.Black;
            this.tpvvm.Name = "tpvvm";
            this.tpvvm.Size = new System.Drawing.Size(114, 22);
            this.tpvvm.Text = "Top";
            this.tpvvm.Click += new System.EventHandler(this.tpvvm_Click);
            // 
            // bttvvm
            // 
            this.bttvvm.ForeColor = System.Drawing.Color.Black;
            this.bttvvm.Name = "bttvvm";
            this.bttvvm.Size = new System.Drawing.Size(114, 22);
            this.bttvvm.Text = "Bottom";
            this.bttvvm.Click += new System.EventHandler(this.bttvvm_Click);
            // 
            // clseqb
            // 
            this.clseqb.ForeColor = System.Drawing.Color.Black;
            this.clseqb.Name = "clseqb";
            this.clseqb.Size = new System.Drawing.Size(134, 22);
            this.clseqb.Text = "Close";
            this.clseqb.Click += new System.EventHandler(this.vvmclse_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(97, 30);
            this.toolStripLabel1.Text = "Quik bar               ";
            // 
            // vvmclse
            // 
            this.vvmclse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.vvmclse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vvmclse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.restartAmatrixToolStripMenuItem,
            this.closeAmatrixToolStripMenuItem});
            this.vvmclse.Image = global::Main.Properties.Resources.extfin;
            this.vvmclse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vvmclse.Name = "vvmclse";
            this.vvmclse.Size = new System.Drawing.Size(32, 30);
            this.vvmclse.Text = "toolStripButton2";
            this.vvmclse.ToolTipText = "Close : Closes the Quik Bar";
            this.vvmclse.ButtonClick += new System.EventHandler(this.vvmclse_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.LightGray;
            this.toolStripMenuItem3.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.toolStripMenuItem3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem3.Text = "Amatrix";
            // 
            // restartAmatrixToolStripMenuItem
            // 
            this.restartAmatrixToolStripMenuItem.Image = global::Main.Properties.Resources.restart;
            this.restartAmatrixToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.restartAmatrixToolStripMenuItem.Name = "restartAmatrixToolStripMenuItem";
            this.restartAmatrixToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.restartAmatrixToolStripMenuItem.Text = "Restart Amatrix";
            this.restartAmatrixToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // closeAmatrixToolStripMenuItem
            // 
            this.closeAmatrixToolStripMenuItem.Image = global::Main.Properties.Resources.ex_am;
            this.closeAmatrixToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.closeAmatrixToolStripMenuItem.Name = "closeAmatrixToolStripMenuItem";
            this.closeAmatrixToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.closeAmatrixToolStripMenuItem.Text = "Close Amatrix";
            this.closeAmatrixToolStripMenuItem.Click += new System.EventHandler(this.closeAmatrixToolStripMenuItem_Click);
            // 
            // hmets
            // 
            this.hmets.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nwam,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.restartToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.hmets.ForeColor = System.Drawing.Color.Silver;
            this.hmets.Image = global::Main.Properties.Resources.main_Mnu;
            this.hmets.ImageTransparentColor = System.Drawing.Color.White;
            this.hmets.Name = "hmets";
            this.hmets.Size = new System.Drawing.Size(69, 30);
            this.hmets.Text = "Home";
            // 
            // nwam
            // 
            this.nwam.BackColor = System.Drawing.Color.LightGray;
            this.nwam.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.nwam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nwam.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.nwam.Name = "nwam";
            this.nwam.Size = new System.Drawing.Size(145, 22);
            this.nwam.Text = "New Window";
            this.nwam.Click += new System.EventHandler(this.nwam_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.employeeAndCompanyInformationToolStripMenuItem,
            this.toolStripSeparator5,
            this.connectionSettingsToolStripMenuItem,
            this.connectionSettingsToolStripMenuItem1});
            this.settingsToolStripMenuItem.Image = global::Main.Properties.Resources.tools;
            this.settingsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.LightGray;
            this.toolStripMenuItem2.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.toolStripMenuItem2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(270, 22);
            this.toolStripMenuItem2.Text = "Settings";
            // 
            // employeeAndCompanyInformationToolStripMenuItem
            // 
            this.employeeAndCompanyInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("employeeAndCompanyInformationToolStripMenuItem.Image")));
            this.employeeAndCompanyInformationToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.employeeAndCompanyInformationToolStripMenuItem.Name = "employeeAndCompanyInformationToolStripMenuItem";
            this.employeeAndCompanyInformationToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.employeeAndCompanyInformationToolStripMenuItem.Text = "Employee and Company Information";
            this.employeeAndCompanyInformationToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(267, 6);
            // 
            // connectionSettingsToolStripMenuItem
            // 
            this.connectionSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("connectionSettingsToolStripMenuItem.Image")));
            this.connectionSettingsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.connectionSettingsToolStripMenuItem.Name = "connectionSettingsToolStripMenuItem";
            this.connectionSettingsToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.connectionSettingsToolStripMenuItem.Text = "Connection Settings";
            this.connectionSettingsToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // connectionSettingsToolStripMenuItem1
            // 
            this.connectionSettingsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("connectionSettingsToolStripMenuItem1.Image")));
            this.connectionSettingsToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.White;
            this.connectionSettingsToolStripMenuItem1.Name = "connectionSettingsToolStripMenuItem1";
            this.connectionSettingsToolStripMenuItem1.Size = new System.Drawing.Size(270, 22);
            this.connectionSettingsToolStripMenuItem1.Text = "Password Settings";
            this.connectionSettingsToolStripMenuItem1.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Image = global::Main.Properties.Resources.restart;
            this.restartToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::Main.Properties.Resources.ex;
            this.closeToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeAmatrixToolStripMenuItem_Click);
            // 
            // oats
            // 
            this.oats.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDocumentStudioToolStripMenuItem,
            this.runApplicationToolStripMenuItem,
            this.toolStripMenuItem6});
            this.oats.ForeColor = System.Drawing.Color.Silver;
            this.oats.Image = global::Main.Properties.Resources.fle_App;
            this.oats.ImageTransparentColor = System.Drawing.Color.White;
            this.oats.Name = "oats";
            this.oats.Size = new System.Drawing.Size(129, 30);
            this.oats.Text = "Document Studio";
            // 
            // showDocumentStudioToolStripMenuItem
            // 
            this.showDocumentStudioToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.showDocumentStudioToolStripMenuItem.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.showDocumentStudioToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.showDocumentStudioToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.showDocumentStudioToolStripMenuItem.Name = "showDocumentStudioToolStripMenuItem";
            this.showDocumentStudioToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.showDocumentStudioToolStripMenuItem.Text = "Document Studio";
            // 
            // runApplicationToolStripMenuItem
            // 
            this.runApplicationToolStripMenuItem.Image = global::Main.Properties.Resources.Console;
            this.runApplicationToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.runApplicationToolStripMenuItem.Name = "runApplicationToolStripMenuItem";
            this.runApplicationToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.runApplicationToolStripMenuItem.Text = "Open Document Studio";
            this.runApplicationToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // devts
            // 
            this.devts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mgmtstd,
            this.vvmmkt,
            this.projectManagmentToolStripMenuItem,
            this.openHumanResourcesToolStripMenuItem,
            this.openCustomerManagmentToolStripMenuItem,
            this.openProductManagmentToolStripMenuItem,
            this.openTaskManagmentToolStripMenuItem});
            this.devts.ForeColor = System.Drawing.Color.Silver;
            this.devts.Image = global::Main.Properties.Resources.mgmt_App;
            this.devts.ImageTransparentColor = System.Drawing.Color.White;
            this.devts.Name = "devts";
            this.devts.Size = new System.Drawing.Size(138, 30);
            this.devts.Text = "Managment Studio";
            // 
            // mgmtstd
            // 
            this.mgmtstd.BackColor = System.Drawing.Color.LightGray;
            this.mgmtstd.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.mgmtstd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mgmtstd.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.mgmtstd.Name = "mgmtstd";
            this.mgmtstd.Size = new System.Drawing.Size(226, 22);
            this.mgmtstd.Text = "Managment Studio";
            // 
            // vvmmkt
            // 
            this.vvmmkt.Image = ((System.Drawing.Image)(resources.GetObject("vvmmkt.Image")));
            this.vvmmkt.ImageTransparentColor = System.Drawing.Color.White;
            this.vvmmkt.Name = "vvmmkt";
            this.vvmmkt.Size = new System.Drawing.Size(226, 22);
            this.vvmmkt.Text = "Open Logistics Managment";
            this.vvmmkt.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // projectManagmentToolStripMenuItem
            // 
            this.projectManagmentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("projectManagmentToolStripMenuItem.Image")));
            this.projectManagmentToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.projectManagmentToolStripMenuItem.Name = "projectManagmentToolStripMenuItem";
            this.projectManagmentToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.projectManagmentToolStripMenuItem.Text = "Open Project Managment";
            this.projectManagmentToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // openHumanResourcesToolStripMenuItem
            // 
            this.openHumanResourcesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openHumanResourcesToolStripMenuItem.Image")));
            this.openHumanResourcesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.openHumanResourcesToolStripMenuItem.Name = "openHumanResourcesToolStripMenuItem";
            this.openHumanResourcesToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.openHumanResourcesToolStripMenuItem.Text = "Open Human Resources";
            this.openHumanResourcesToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // openCustomerManagmentToolStripMenuItem
            // 
            this.openCustomerManagmentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openCustomerManagmentToolStripMenuItem.Image")));
            this.openCustomerManagmentToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.openCustomerManagmentToolStripMenuItem.Name = "openCustomerManagmentToolStripMenuItem";
            this.openCustomerManagmentToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.openCustomerManagmentToolStripMenuItem.Text = "Open Customer Managment";
            this.openCustomerManagmentToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // openProductManagmentToolStripMenuItem
            // 
            this.openProductManagmentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openProductManagmentToolStripMenuItem.Image")));
            this.openProductManagmentToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.openProductManagmentToolStripMenuItem.Name = "openProductManagmentToolStripMenuItem";
            this.openProductManagmentToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.openProductManagmentToolStripMenuItem.Text = "Open Product Managment";
            this.openProductManagmentToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // openTaskManagmentToolStripMenuItem
            // 
            this.openTaskManagmentToolStripMenuItem.Image = global::Main.Properties.Resources.Console;
            this.openTaskManagmentToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.openTaskManagmentToolStripMenuItem.Name = "openTaskManagmentToolStripMenuItem";
            this.openTaskManagmentToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.openTaskManagmentToolStripMenuItem.Text = "External Applications";
            this.openTaskManagmentToolStripMenuItem.Click += new System.EventHandler(this.openTaskManagmentToolStripMenuItem_Click);
            // 
            // accstd
            // 
            this.accstd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accshw,
            this.jrnlstrt,
            this.ldg,
            this.incvd});
            this.accstd.ForeColor = System.Drawing.Color.Silver;
            this.accstd.Image = global::Main.Properties.Resources.Acc_App;
            this.accstd.ImageTransparentColor = System.Drawing.Color.White;
            this.accstd.Name = "accstd";
            this.accstd.Size = new System.Drawing.Size(135, 30);
            this.accstd.Text = "Accounting Studio";
            // 
            // accshw
            // 
            this.accshw.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.accshw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.accshw.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.accshw.Name = "accshw";
            this.accshw.Size = new System.Drawing.Size(173, 22);
            this.accshw.Text = "Accounting Studio";
            // 
            // jrnlstrt
            // 
            this.jrnlstrt.Image = ((System.Drawing.Image)(resources.GetObject("jrnlstrt.Image")));
            this.jrnlstrt.ImageTransparentColor = System.Drawing.Color.White;
            this.jrnlstrt.Name = "jrnlstrt";
            this.jrnlstrt.Size = new System.Drawing.Size(173, 22);
            this.jrnlstrt.Text = "Open Journal";
            this.jrnlstrt.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // ldg
            // 
            this.ldg.Image = ((System.Drawing.Image)(resources.GetObject("ldg.Image")));
            this.ldg.ImageTransparentColor = System.Drawing.Color.White;
            this.ldg.Name = "ldg";
            this.ldg.Size = new System.Drawing.Size(173, 22);
            this.ldg.Text = "Open Ledger";
            this.ldg.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // incvd
            // 
            this.incvd.Image = ((System.Drawing.Image)(resources.GetObject("incvd.Image")));
            this.incvd.ImageTransparentColor = System.Drawing.Color.White;
            this.incvd.Name = "incvd";
            this.incvd.Size = new System.Drawing.Size(173, 22);
            this.incvd.Text = "Open Invoices";
            this.incvd.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // csts
            // 
            this.csts.BackColor = System.Drawing.Color.Transparent;
            this.csts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.csts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.grapherToolStripMenuItem});
            this.csts.ForeColor = System.Drawing.Color.Silver;
            this.csts.Image = global::Main.Properties.Resources.bus_App;
            this.csts.ImageTransparentColor = System.Drawing.Color.White;
            this.csts.Name = "csts";
            this.csts.Size = new System.Drawing.Size(118, 30);
            this.csts.Text = "Business Studio";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.toolStripMenuItem4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem4.Text = "Business Studio";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItem5.Text = "Employee Time In/Out";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // grapherToolStripMenuItem
            // 
            this.grapherToolStripMenuItem.Image = global::Main.Properties.Resources.Console;
            this.grapherToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.grapherToolStripMenuItem.Name = "grapherToolStripMenuItem";
            this.grapherToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.grapherToolStripMenuItem.Text = "Grapher";
            this.grapherToolStripMenuItem.Click += new System.EventHandler(this.grapherToolStripMenuItem_Click);
            // 
            // dbts
            // 
            this.dbts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.openRePartitionerToolStripMenuItem});
            this.dbts.ForeColor = System.Drawing.Color.Silver;
            this.dbts.Image = global::Main.Properties.Resources.dbs_App;
            this.dbts.ImageTransparentColor = System.Drawing.Color.White;
            this.dbts.Name = "dbts";
            this.dbts.Size = new System.Drawing.Size(124, 30);
            this.dbts.Text = "Data Base Studio";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.toolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem1.Text = "Data Base Studio";
            // 
            // openRePartitionerToolStripMenuItem
            // 
            this.openRePartitionerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openRePartitionerToolStripMenuItem.Image")));
            this.openRePartitionerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.openRePartitionerToolStripMenuItem.Name = "openRePartitionerToolStripMenuItem";
            this.openRePartitionerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openRePartitionerToolStripMenuItem.Text = "Open Re-Partitioner";
            this.openRePartitionerToolStripMenuItem.Click += new System.EventHandler(this.employeeAndCompanyInformationToolStripMenuItem_Click);
            // 
            // shptls
            // 
            this.shptls.ForeColor = System.Drawing.Color.Silver;
            this.shptls.Image = global::Main.Properties.Resources.shp_App;
            this.shptls.ImageTransparentColor = System.Drawing.Color.White;
            this.shptls.Name = "shptls";
            this.shptls.Size = new System.Drawing.Size(67, 30);
            this.shptls.Text = "Shoppe";
            this.shptls.Click += new System.EventHandler(this.shptlsclc);
            // 
            // tstme
            // 
            this.tstme.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tstme.BackgroundImage = global::Main.Properties.Resources.bannrrageconv;
            this.tstme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstme.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tstme.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tstme.Image = ((System.Drawing.Image)(resources.GetObject("tstme.Image")));
            this.tstme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tstme.Name = "tstme";
            this.tstme.Size = new System.Drawing.Size(96, 30);
            this.tstme.Text = "Retrieving Time..";
            this.tstme.Click += new System.EventHandler(this.tstme_Click);
            // 
            // clsevvm
            // 
            this.clsevvm.Interval = 3;
            this.clsevvm.Tick += new System.EventHandler(this.clsevvmtk);
            // 
            // enblvvm
            // 
            this.enblvvm.Enabled = true;
            this.enblvvm.Interval = 99;
            this.enblvvm.Tick += new System.EventHandler(this.enblvvm_Tick);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = global::Main.Properties.Resources.Console;
            this.toolStripMenuItem6.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(227, 22);
            this.toolStripMenuItem6.Text = "Open The Document Creator";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // Virtual_window_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1081, 33);
            this.ControlBox = false;
            this.Controls.Add(this.tsvvm);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Virtual_window_manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Virtual_window_manager";
            this.Load += new System.EventHandler(this.Virtual_window_manager_Load);
            this.MouseEnter += new System.EventHandler(this.vvmhov);
            this.MouseLeave += new System.EventHandler(this.vvmext);
            this.tsvvm.ResumeLayout(false);
            this.tsvvm.PerformLayout();
            this.cmsvvm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer actvvm;
        private System.Windows.Forms.Timer decvvm;
        private System.Windows.Forms.ToolStrip tsvvm;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ContextMenuStrip cmsvvm;
        private System.Windows.Forms.ToolStripMenuItem orientationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tpvvm;
        private System.Windows.Forms.ToolStripMenuItem bttvvm;
        private System.Windows.Forms.ToolStripMenuItem clseqb;
        private System.Windows.Forms.Timer clsevvm;
        private System.Windows.Forms.Timer enblvvm;
        private System.Windows.Forms.ToolStripLabel tstme;
        private System.Windows.Forms.ToolStripButton shptls;
        private System.Windows.Forms.ToolStripSplitButton vvmclse;
        private System.Windows.Forms.ToolStripMenuItem closeAmatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton hmets;
        private System.Windows.Forms.ToolStripMenuItem nwam;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton oats;
        private System.Windows.Forms.ToolStripMenuItem showDocumentStudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton devts;
        private System.Windows.Forms.ToolStripMenuItem mgmtstd;
        private System.Windows.Forms.ToolStripMenuItem vvmmkt;
        private System.Windows.Forms.ToolStripDropDownButton accstd;
        private System.Windows.Forms.ToolStripMenuItem accshw;
        private System.Windows.Forms.ToolStripMenuItem jrnlstrt;
        private System.Windows.Forms.ToolStripMenuItem ldg;
        private System.Windows.Forms.ToolStripMenuItem incvd;
        private System.Windows.Forms.ToolStripDropDownButton dbts;
        private System.Windows.Forms.ToolStripMenuItem runApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectManagmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openHumanResourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCustomerManagmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProductManagmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openRePartitionerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartAmatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employeeAndCompanyInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem connectionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionSettingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton csts;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openTaskManagmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem grapherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

    }
}