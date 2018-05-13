namespace Main
{
    partial class Guided_Function
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Guided_Function));
            this.Cancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx1 = new System.Windows.Forms.TextBox();
            this.add_btn = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.am = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.fin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bt_nxt = new System.Windows.Forms.Button();
            this.cbh = new System.Windows.Forms.CheckBox();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.bck = new System.Windows.Forms.Button();
            this.abtclse = new System.Windows.Forms.Timer(this.components);
            this.dectmeabt = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel.BackgroundImage")));
            this.Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Cancel.FlatAppearance.BorderSize = 0;
            this.Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Cancel.Location = new System.Drawing.Point(12, 362);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(61, 22);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(585, 342);
            this.tabControl1.TabIndex = 1;
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
            this.label2.Location = new System.Drawing.Point(103, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(375, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "This Guide Will Help You Set up a Link To a File or Application of Your Choice";
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
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.tbx1);
            this.tabPage2.Controls.Add(this.add_btn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(577, 316);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Add Your Link";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(62, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Add a Link to An application or File by Clicking on Add or Pasting its Address Be" +
                "low.";
            // 
            // tbx1
            // 
            this.tbx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx1.ForeColor = System.Drawing.Color.Gray;
            this.tbx1.Location = new System.Drawing.Point(63, 146);
            this.tbx1.Name = "tbx1";
            this.tbx1.Size = new System.Drawing.Size(379, 22);
            this.tbx1.TabIndex = 5;
            this.tbx1.Text = "Link Address";
            this.tbx1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // add_btn
            // 
            this.add_btn.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.add_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.add_btn.ForeColor = System.Drawing.Color.Black;
            this.add_btn.Location = new System.Drawing.Point(437, 146);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(75, 22);
            this.add_btn.TabIndex = 4;
            this.add_btn.Text = "Add...";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.am);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Controls.Add(this.fin);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(577, 316);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Name Your Link";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // am
            // 
            this.am.BackColor = System.Drawing.Color.WhiteSmoke;
            this.am.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("am.BackgroundImage")));
            this.am.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.am.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.am.FlatAppearance.BorderSize = 0;
            this.am.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.am.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.am.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.am.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.am.Location = new System.Drawing.Point(484, 290);
            this.am.Name = "am";
            this.am.Size = new System.Drawing.Size(90, 23);
            this.am.TabIndex = 12;
            this.am.Text = "Add More..";
            this.am.UseVisualStyleBackColor = false;
            this.am.Visible = false;
            this.am.Click += new System.EventHandler(this.fin_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(424, 149);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Use File Name";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // fin
            // 
            this.fin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fin.BackgroundImage")));
            this.fin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.fin.FlatAppearance.BorderSize = 0;
            this.fin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.fin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.fin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fin.Location = new System.Drawing.Point(238, 174);
            this.fin.Name = "fin";
            this.fin.Size = new System.Drawing.Size(90, 26);
            this.fin.TabIndex = 10;
            this.fin.Text = "Finish";
            this.fin.UseVisualStyleBackColor = false;
            this.fin.Click += new System.EventHandler(this.fin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(62, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(412, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Enter The Name of Your Link this Will Displayed in the Link Menu in the Main Wind" +
                "ow";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(63, 146);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(355, 22);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Link Name";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bt_nxt
            // 
            this.bt_nxt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bt_nxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_nxt.BackgroundImage")));
            this.bt_nxt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_nxt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bt_nxt.FlatAppearance.BorderSize = 0;
            this.bt_nxt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bt_nxt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bt_nxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_nxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bt_nxt.Location = new System.Drawing.Point(529, 362);
            this.bt_nxt.Name = "bt_nxt";
            this.bt_nxt.Size = new System.Drawing.Size(64, 22);
            this.bt_nxt.TabIndex = 2;
            this.bt_nxt.Text = "Next";
            this.bt_nxt.UseVisualStyleBackColor = false;
            this.bt_nxt.Click += new System.EventHandler(this.bt_nxt_Click);
            // 
            // cbh
            // 
            this.cbh.AutoSize = true;
            this.cbh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbh.Location = new System.Drawing.Point(88, 365);
            this.cbh.Name = "cbh";
            this.cbh.Size = new System.Drawing.Size(157, 17);
            this.cbh.TabIndex = 3;
            this.cbh.Text = "Show This Guide EveryTime";
            this.cbh.UseVisualStyleBackColor = true;
            this.cbh.Visible = false;
            this.cbh.CheckedChanged += new System.EventHandler(this.cbh_CheckedChanged);
            // 
            // OFD
            // 
            this.OFD.FileName = "File or Application";
            this.OFD.Filter = "|*.*|All Compatible Files|";
            this.OFD.FileOk += new System.ComponentModel.CancelEventHandler(this.OFD_FileOk);
            // 
            // bck
            // 
            this.bck.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bck.BackgroundImage")));
            this.bck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bck.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bck.FlatAppearance.BorderSize = 0;
            this.bck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bck.Location = new System.Drawing.Point(458, 362);
            this.bck.Name = "bck";
            this.bck.Size = new System.Drawing.Size(65, 22);
            this.bck.TabIndex = 4;
            this.bck.Text = "Back";
            this.bck.UseVisualStyleBackColor = false;
            this.bck.Click += new System.EventHandler(this.bt_nxt_Click);
            // 
            // abtclse
            // 
            this.abtclse.Interval = 3;
            this.abtclse.Tick += new System.EventHandler(this.abtclse_Tick);
            // 
            // dectmeabt
            // 
            this.dectmeabt.Interval = 10;
            this.dectmeabt.Tick += new System.EventHandler(this.dectmeabt_Tick);
            // 
            // Guided_Function
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
            this.Name = "Guided_Function";
            this.Text = "Guided_Function";
            this.Deactivate += new System.EventHandler(this.Guided_Function_Deactivate);
            this.Load += new System.EventHandler(this.Guided_Function_Load);
            this.Activated += new System.EventHandler(this.Guided_Function_Activated);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx1;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button bt_nxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cbh;
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button fin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bck;
        private System.Windows.Forms.Button am;
        private System.Windows.Forms.Timer abtclse;
        private System.Windows.Forms.Timer dectmeabt;
    }
}