namespace Main
{
    partial class reparttn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reparttn));
            this.button1 = new System.Windows.Forms.Button();
            this.imgdb = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.clb = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.bkk_work = new System.ComponentModel.BackgroundWorker();
            this.bkk_work2 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.DimGray;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.imgdb;
            this.button1.Location = new System.Drawing.Point(571, 330);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Re-Partition";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imgdb
            // 
            this.imgdb.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgdb.ImageStream")));
            this.imgdb.TransparentColor = System.Drawing.Color.White;
            this.imgdb.Images.SetKeyName(0, "repartin.bmp");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // clb
            // 
            this.clb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clb.CheckOnClick = true;
            this.clb.FormattingEnabled = true;
            this.clb.HorizontalScrollbar = true;
            this.clb.Items.AddRange(new object[] {
            "Invoices",
            "Journal",
            "Cash Book",
            "Sales Book",
            "Purchase Book"});
            this.clb.Location = new System.Drawing.Point(27, 82);
            this.clb.Name = "clb";
            this.clb.ScrollAlwaysVisible = true;
            this.clb.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.clb.Size = new System.Drawing.Size(632, 227);
            this.clb.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "Before Repartitioning all Amatrix Windows will be Forcefully Closed.\r\nIf you Have" +
                " not Saved Your Work Please do So Before Re-Partitioning.\r\nAmatrix will be Close" +
                "d after Re-partitioning.";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(441, 330);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(124, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "These DataBases will be Re-Partitioned";
            // 
            // bkk_work
            // 
            this.bkk_work.WorkerSupportsCancellation = true;
            this.bkk_work.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_work_DoWork);
            this.bkk_work.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_work_RunWorkerCompleted);
            // 
            // bkk_work2
            // 
            this.bkk_work2.WorkerSupportsCancellation = true;
            this.bkk_work2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_work2_DoWork);
            this.bkk_work2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_work2_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 364);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(685, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "Effects Disabled";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = global::Main.Properties.Resources.shld;
            this.toolStripStatusLabel1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(106, 17);
            this.toolStripStatusLabel1.Text = "Effects Disabled";
            // 
            // reparttn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 386);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "reparttn";
            this.Text = "Amatrix Re-Partitioner";
            this.Load += new System.EventHandler(this.reparttn_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imgdb;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker bkk_work;
        private System.ComponentModel.BackgroundWorker bkk_work2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}