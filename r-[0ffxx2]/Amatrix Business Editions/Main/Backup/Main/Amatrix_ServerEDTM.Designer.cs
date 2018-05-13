namespace Main
{
    partial class Amatrix_ServerEDTM
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
            this.clse = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tms = new System.Windows.Forms.Timer(this.components);
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // clse
            // 
            this.clse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clse.FlatAppearance.BorderSize = 0;
            this.clse.Location = new System.Drawing.Point(188, 2);
            this.clse.Name = "clse";
            this.clse.Size = new System.Drawing.Size(25, 23);
            this.clse.TabIndex = 0;
            this.clse.UseVisualStyleBackColor = true;
            this.clse.Click += new System.EventHandler(this.clse_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.White;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(183, 26);
            this.listBox1.TabIndex = 1;
            // 
            // tms
            // 
            this.tms.Enabled = true;
            this.tms.Interval = 15000;
            this.tms.Tick += new System.EventHandler(this.tms_Tick);
            // 
            // il
            // 
            this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.il.ImageSize = new System.Drawing.Size(16, 16);
            this.il.TransparentColor = System.Drawing.Color.White;
            // 
            // Amatrix_ServerEDTM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.clse);
            this.Name = "Amatrix_ServerEDTM";
            this.Size = new System.Drawing.Size(217, 27);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clse;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer tms;
        private System.Windows.Forms.ImageList il;
    }
}
