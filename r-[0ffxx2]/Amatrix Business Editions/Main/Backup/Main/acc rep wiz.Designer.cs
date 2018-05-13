namespace Main
{
    partial class acc_rep_wiz
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Journal", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Ledger", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Invoices", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Minimal Reports", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Simple Journal", "wizwin.bmp");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Professional Journal", 0);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Simple Ledger", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Professional Ledger", 0);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Simple Invoice", 0);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Professional Invoice", 0);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Credit/Debit", 0);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Bank Widthdraws", 0);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Assets", 0);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Data Information", 0);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Balance Sheet", 0);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Cash Flow Report", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(acc_rep_wiz));
            this.cbh = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lv = new System.Windows.Forms.ListView();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cb = new System.Windows.Forms.CheckBox();
            this.lbl_fin1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.bt_nxt = new System.Windows.Forms.Button();
            this.bck = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // cbh
            // 
            this.cbh.AutoSize = true;
            this.cbh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbh.Location = new System.Drawing.Point(108, 367);
            this.cbh.Name = "cbh";
            this.cbh.Size = new System.Drawing.Size(157, 17);
            this.cbh.TabIndex = 8;
            this.cbh.Text = "Show This Guide EveryTime";
            this.cbh.UseVisualStyleBackColor = true;
            this.cbh.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(585, 342);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(577, 316);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Start-Up";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(167, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "This Guide Will Help You Set up and Print a Report";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Main.Properties.Resources.betatech;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(173, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(233, 161);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lv);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(577, 316);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Choose a Template";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lv
            // 
            this.lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Journal";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Ledger";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "Invoices";
            listViewGroup3.Name = "listViewGroup3";
            listViewGroup4.Header = "Minimal Reports";
            listViewGroup4.Name = "listViewGroup4";
            this.lv.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup2;
            listViewItem4.Group = listViewGroup2;
            listViewItem5.Group = listViewGroup3;
            listViewItem6.Group = listViewGroup3;
            listViewItem7.Group = listViewGroup4;
            listViewItem8.Group = listViewGroup4;
            listViewItem9.Group = listViewGroup4;
            listViewItem10.Group = listViewGroup4;
            this.lv.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.lv.LargeImageList = this.il;
            this.lv.Location = new System.Drawing.Point(3, 3);
            this.lv.MultiSelect = false;
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(571, 310);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "wizwin.bmp");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pictureBox2);
            this.tabPage4.Controls.Add(this.cb);
            this.tabPage4.Controls.Add(this.lbl_fin1);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(577, 316);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Finish";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::Main.Properties.Resources.wizwin;
            this.pictureBox2.Location = new System.Drawing.Point(273, 143);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 30);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // cb
            // 
            this.cb.AutoSize = true;
            this.cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb.Location = new System.Drawing.Point(171, 186);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(264, 17);
            this.cb.TabIndex = 12;
            this.cb.Text = "Print all Entries also along with the Summary Report";
            this.cb.UseVisualStyleBackColor = true;
            this.cb.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // lbl_fin1
            // 
            this.lbl_fin1.AutoSize = true;
            this.lbl_fin1.Location = new System.Drawing.Point(320, 152);
            this.lbl_fin1.Name = "lbl_fin1";
            this.lbl_fin1.Size = new System.Drawing.Size(33, 13);
            this.lbl_fin1.TabIndex = 11;
            this.lbl_fin1.Text = "None";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Template Chosen : ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(239, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 46);
            this.button1.TabIndex = 9;
            this.button1.Text = "Finish";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.MouseLeave += new System.EventHandler(this.bt_nxt_MouseLeave);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseUp);
            this.button1.MouseEnter += new System.EventHandler(this.bt_nxt_MouseEnter);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.Transparent;
            this.Cancel.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Cancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Cancel.FlatAppearance.BorderSize = 0;
            this.Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Cancel.Location = new System.Drawing.Point(12, 351);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(90, 46);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.MouseLeave += new System.EventHandler(this.bt_nxt_MouseLeave);
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            this.Cancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseDown);
            this.Cancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseUp);
            this.Cancel.MouseEnter += new System.EventHandler(this.bt_nxt_MouseEnter);
            // 
            // bt_nxt
            // 
            this.bt_nxt.BackColor = System.Drawing.Color.Transparent;
            this.bt_nxt.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.bt_nxt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bt_nxt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bt_nxt.FlatAppearance.BorderSize = 0;
            this.bt_nxt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bt_nxt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bt_nxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_nxt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.bt_nxt.Location = new System.Drawing.Point(507, 351);
            this.bt_nxt.Name = "bt_nxt";
            this.bt_nxt.Size = new System.Drawing.Size(90, 46);
            this.bt_nxt.TabIndex = 7;
            this.bt_nxt.Text = "Next";
            this.bt_nxt.UseVisualStyleBackColor = false;
            this.bt_nxt.MouseLeave += new System.EventHandler(this.bt_nxt_MouseLeave);
            this.bt_nxt.Click += new System.EventHandler(this.bt_nxt_Click);
            this.bt_nxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseDown);
            this.bt_nxt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseUp);
            this.bt_nxt.MouseEnter += new System.EventHandler(this.bt_nxt_MouseEnter);
            // 
            // bck
            // 
            this.bck.BackColor = System.Drawing.Color.Transparent;
            this.bck.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.bck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bck.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bck.FlatAppearance.BorderSize = 0;
            this.bck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bck.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.bck.Location = new System.Drawing.Point(414, 351);
            this.bck.Name = "bck";
            this.bck.Size = new System.Drawing.Size(90, 46);
            this.bck.TabIndex = 9;
            this.bck.Text = "Back";
            this.bck.UseVisualStyleBackColor = false;
            this.bck.MouseLeave += new System.EventHandler(this.bt_nxt_MouseLeave);
            this.bck.Click += new System.EventHandler(this.bt_nxt_Click);
            this.bck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseDown);
            this.bck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_nxt_MouseUp);
            this.bck.MouseEnter += new System.EventHandler(this.bt_nxt_MouseEnter);
            // 
            // acc_rep_wiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(609, 396);
            this.Controls.Add(this.cbh);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.bt_nxt);
            this.Controls.Add(this.bck);
            this.Name = "acc_rep_wiz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "acc_rep_wiz";
            this.Load += new System.EventHandler(this.acc_rep_wiz_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbh;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button bt_nxt;
        private System.Windows.Forms.Button bck;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cb;
        private System.Windows.Forms.Label lbl_fin1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}