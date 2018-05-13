namespace Main
{
    partial class Calculator_1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator_1));
            this.rtfclc = new System.Windows.Forms.RichTextBox();
            this.Add = new System.Windows.Forms.Button();
            this.eqs = new System.Windows.Forms.Button();
            this.calcts = new System.Windows.Forms.ToolStrip();
            this.clsecalc = new System.Windows.Forms.ToolStripButton();
            this.Divide = new System.Windows.Forms.Button();
            this.mult = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.Resulttf = new System.Windows.Forms.RichTextBox();
            this.onebtn = new System.Windows.Forms.Button();
            this.eightbtn = new System.Windows.Forms.Button();
            this.sevenbtn = new System.Windows.Forms.Button();
            this.sixbtn = new System.Windows.Forms.Button();
            this.fivebtn = new System.Windows.Forms.Button();
            this.fourbtn = new System.Windows.Forms.Button();
            this.threebtn = new System.Windows.Forms.Button();
            this.twobtn = new System.Windows.Forms.Button();
            this.zerobtn = new System.Windows.Forms.Button();
            this.ninebtn = new System.Windows.Forms.Button();
            this.cos = new System.Windows.Forms.Button();
            this.bin = new System.Windows.Forms.Button();
            this.lgtn = new System.Windows.Forms.Button();
            this.cube = new System.Windows.Forms.Button();
            this.sqr = new System.Windows.Forms.Button();
            this.power = new System.Windows.Forms.Button();
            this.loglog = new System.Windows.Forms.Button();
            this.calcdect = new System.Windows.Forms.Timer(this.components);
            this.delcalc = new System.Windows.Forms.Timer(this.components);
            this.clcenbl = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clcmnuext = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selall = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.calcts.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rtfclc
            // 
            this.rtfclc.BackColor = System.Drawing.Color.LightGray;
            this.rtfclc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfclc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfclc.ForeColor = System.Drawing.Color.DimGray;
            this.rtfclc.Location = new System.Drawing.Point(12, 34);
            this.rtfclc.Name = "rtfclc";
            this.rtfclc.Size = new System.Drawing.Size(341, 67);
            this.rtfclc.TabIndex = 0;
            this.rtfclc.Text = "";
            this.rtfclc.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Gainsboro;
            this.Add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Add.BackgroundImage")));
            this.Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Add.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.ForeColor = System.Drawing.Color.Black;
            this.Add.Location = new System.Drawing.Point(203, 153);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(33, 28);
            this.Add.TabIndex = 1;
            this.Add.Text = "+";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // eqs
            // 
            this.eqs.BackColor = System.Drawing.Color.Gainsboro;
            this.eqs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("eqs.BackgroundImage")));
            this.eqs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.eqs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.eqs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.eqs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eqs.ForeColor = System.Drawing.Color.Black;
            this.eqs.Location = new System.Drawing.Point(9, 153);
            this.eqs.Name = "eqs";
            this.eqs.Size = new System.Drawing.Size(70, 28);
            this.eqs.TabIndex = 2;
            this.eqs.Text = "CE";
            this.eqs.UseVisualStyleBackColor = false;
            this.eqs.Click += new System.EventHandler(this.equals_Click);
            // 
            // calcts
            // 
            this.calcts.BackColor = System.Drawing.Color.Transparent;
            this.calcts.BackgroundImage = global::Main.Properties.Resources.banner3blue;
            this.calcts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.calcts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.calcts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.calcts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clsecalc});
            this.calcts.Location = new System.Drawing.Point(0, 255);
            this.calcts.Name = "calcts";
            this.calcts.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.calcts.Size = new System.Drawing.Size(363, 25);
            this.calcts.TabIndex = 7;
            this.calcts.Text = "toolStrip1";
            // 
            // clsecalc
            // 
            this.clsecalc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clsecalc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clsecalc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.clsecalc.Image = global::Main.Properties.Resources.extfin;
            this.clsecalc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clsecalc.Name = "clsecalc";
            this.clsecalc.Size = new System.Drawing.Size(23, 22);
            this.clsecalc.Text = "toolStripButton2";
            this.clsecalc.MouseLeave += new System.EventHandler(this.clsecalcext);
            this.clsecalc.MouseEnter += new System.EventHandler(this.clsecalchov);
            this.clsecalc.Click += new System.EventHandler(this.clsecalc_Click);
            // 
            // Divide
            // 
            this.Divide.BackColor = System.Drawing.Color.Gainsboro;
            this.Divide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Divide.BackgroundImage")));
            this.Divide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Divide.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Divide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.Divide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.Divide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Divide.ForeColor = System.Drawing.Color.Black;
            this.Divide.Location = new System.Drawing.Point(320, 153);
            this.Divide.Name = "Divide";
            this.Divide.Size = new System.Drawing.Size(33, 28);
            this.Divide.TabIndex = 10;
            this.Divide.Text = "/";
            this.Divide.UseVisualStyleBackColor = false;
            this.Divide.Click += new System.EventHandler(this.Add_Click);
            // 
            // mult
            // 
            this.mult.BackColor = System.Drawing.Color.Gainsboro;
            this.mult.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mult.BackgroundImage")));
            this.mult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mult.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.mult.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.mult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.mult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mult.ForeColor = System.Drawing.Color.Black;
            this.mult.Location = new System.Drawing.Point(281, 153);
            this.mult.Name = "mult";
            this.mult.Size = new System.Drawing.Size(33, 28);
            this.mult.TabIndex = 11;
            this.mult.Text = "x";
            this.mult.UseVisualStyleBackColor = false;
            this.mult.Click += new System.EventHandler(this.Add_Click);
            // 
            // minus
            // 
            this.minus.BackColor = System.Drawing.Color.Gainsboro;
            this.minus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("minus.BackgroundImage")));
            this.minus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.minus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.minus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.minus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minus.ForeColor = System.Drawing.Color.Black;
            this.minus.Location = new System.Drawing.Point(242, 153);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(33, 28);
            this.minus.TabIndex = 12;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = false;
            this.minus.Click += new System.EventHandler(this.Add_Click);
            // 
            // Resulttf
            // 
            this.Resulttf.BackColor = System.Drawing.Color.LightGray;
            this.Resulttf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Resulttf.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resulttf.ForeColor = System.Drawing.Color.DimGray;
            this.Resulttf.Location = new System.Drawing.Point(12, 100);
            this.Resulttf.Name = "Resulttf";
            this.Resulttf.ReadOnly = true;
            this.Resulttf.Size = new System.Drawing.Size(341, 36);
            this.Resulttf.TabIndex = 13;
            this.Resulttf.Text = "";
            // 
            // onebtn
            // 
            this.onebtn.BackColor = System.Drawing.Color.Transparent;
            this.onebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("onebtn.BackgroundImage")));
            this.onebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.onebtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.onebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.onebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.onebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.onebtn.ForeColor = System.Drawing.Color.Black;
            this.onebtn.Location = new System.Drawing.Point(9, 221);
            this.onebtn.Name = "onebtn";
            this.onebtn.Size = new System.Drawing.Size(29, 29);
            this.onebtn.TabIndex = 17;
            this.onebtn.Text = "1";
            this.onebtn.UseVisualStyleBackColor = false;
            this.onebtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // eightbtn
            // 
            this.eightbtn.BackColor = System.Drawing.Color.Transparent;
            this.eightbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("eightbtn.BackgroundImage")));
            this.eightbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eightbtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.eightbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.eightbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.eightbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eightbtn.ForeColor = System.Drawing.Color.Black;
            this.eightbtn.Location = new System.Drawing.Point(254, 221);
            this.eightbtn.Name = "eightbtn";
            this.eightbtn.Size = new System.Drawing.Size(29, 29);
            this.eightbtn.TabIndex = 18;
            this.eightbtn.Text = "8";
            this.eightbtn.UseVisualStyleBackColor = false;
            this.eightbtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // sevenbtn
            // 
            this.sevenbtn.BackColor = System.Drawing.Color.Transparent;
            this.sevenbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sevenbtn.BackgroundImage")));
            this.sevenbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sevenbtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sevenbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.sevenbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.sevenbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sevenbtn.ForeColor = System.Drawing.Color.Black;
            this.sevenbtn.Location = new System.Drawing.Point(219, 221);
            this.sevenbtn.Name = "sevenbtn";
            this.sevenbtn.Size = new System.Drawing.Size(29, 29);
            this.sevenbtn.TabIndex = 19;
            this.sevenbtn.Text = "7";
            this.sevenbtn.UseVisualStyleBackColor = false;
            this.sevenbtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // sixbtn
            // 
            this.sixbtn.BackColor = System.Drawing.Color.Transparent;
            this.sixbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sixbtn.BackgroundImage")));
            this.sixbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sixbtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sixbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.sixbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.sixbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sixbtn.ForeColor = System.Drawing.Color.Black;
            this.sixbtn.Location = new System.Drawing.Point(184, 221);
            this.sixbtn.Name = "sixbtn";
            this.sixbtn.Size = new System.Drawing.Size(29, 29);
            this.sixbtn.TabIndex = 20;
            this.sixbtn.Text = "6";
            this.sixbtn.UseVisualStyleBackColor = false;
            this.sixbtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // fivebtn
            // 
            this.fivebtn.BackColor = System.Drawing.Color.Transparent;
            this.fivebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fivebtn.BackgroundImage")));
            this.fivebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fivebtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.fivebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.fivebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.fivebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fivebtn.ForeColor = System.Drawing.Color.Black;
            this.fivebtn.Location = new System.Drawing.Point(149, 221);
            this.fivebtn.Name = "fivebtn";
            this.fivebtn.Size = new System.Drawing.Size(29, 29);
            this.fivebtn.TabIndex = 21;
            this.fivebtn.Text = "5";
            this.fivebtn.UseVisualStyleBackColor = false;
            this.fivebtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // fourbtn
            // 
            this.fourbtn.BackColor = System.Drawing.Color.Transparent;
            this.fourbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fourbtn.BackgroundImage")));
            this.fourbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fourbtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.fourbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.fourbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.fourbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourbtn.ForeColor = System.Drawing.Color.Black;
            this.fourbtn.Location = new System.Drawing.Point(114, 221);
            this.fourbtn.Name = "fourbtn";
            this.fourbtn.Size = new System.Drawing.Size(29, 29);
            this.fourbtn.TabIndex = 22;
            this.fourbtn.Text = "4";
            this.fourbtn.UseVisualStyleBackColor = false;
            this.fourbtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // threebtn
            // 
            this.threebtn.BackColor = System.Drawing.Color.Transparent;
            this.threebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("threebtn.BackgroundImage")));
            this.threebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.threebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.threebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.threebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.threebtn.ForeColor = System.Drawing.Color.Black;
            this.threebtn.Location = new System.Drawing.Point(79, 221);
            this.threebtn.Name = "threebtn";
            this.threebtn.Size = new System.Drawing.Size(29, 29);
            this.threebtn.TabIndex = 23;
            this.threebtn.Text = "3";
            this.threebtn.UseVisualStyleBackColor = false;
            this.threebtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // twobtn
            // 
            this.twobtn.BackColor = System.Drawing.Color.Transparent;
            this.twobtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("twobtn.BackgroundImage")));
            this.twobtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.twobtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.twobtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.twobtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.twobtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.twobtn.ForeColor = System.Drawing.Color.Black;
            this.twobtn.Location = new System.Drawing.Point(44, 221);
            this.twobtn.Name = "twobtn";
            this.twobtn.Size = new System.Drawing.Size(29, 29);
            this.twobtn.TabIndex = 24;
            this.twobtn.Text = "2";
            this.twobtn.UseVisualStyleBackColor = false;
            this.twobtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // zerobtn
            // 
            this.zerobtn.BackColor = System.Drawing.Color.Transparent;
            this.zerobtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zerobtn.BackgroundImage")));
            this.zerobtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zerobtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.zerobtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.zerobtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.zerobtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zerobtn.ForeColor = System.Drawing.Color.Black;
            this.zerobtn.Location = new System.Drawing.Point(324, 221);
            this.zerobtn.Name = "zerobtn";
            this.zerobtn.Size = new System.Drawing.Size(29, 29);
            this.zerobtn.TabIndex = 25;
            this.zerobtn.Text = "0";
            this.zerobtn.UseVisualStyleBackColor = false;
            this.zerobtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // ninebtn
            // 
            this.ninebtn.BackColor = System.Drawing.Color.Transparent;
            this.ninebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ninebtn.BackgroundImage")));
            this.ninebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ninebtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ninebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.ninebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.ninebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ninebtn.ForeColor = System.Drawing.Color.Black;
            this.ninebtn.Location = new System.Drawing.Point(289, 221);
            this.ninebtn.Name = "ninebtn";
            this.ninebtn.Size = new System.Drawing.Size(29, 29);
            this.ninebtn.TabIndex = 26;
            this.ninebtn.Text = "9";
            this.ninebtn.UseVisualStyleBackColor = false;
            this.ninebtn.Click += new System.EventHandler(this.onebtn_Click);
            // 
            // cos
            // 
            this.cos.BackColor = System.Drawing.Color.Transparent;
            this.cos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cos.BackgroundImage")));
            this.cos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cos.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.cos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.cos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cos.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cos.ForeColor = System.Drawing.Color.Black;
            this.cos.Location = new System.Drawing.Point(184, 187);
            this.cos.Name = "cos";
            this.cos.Size = new System.Drawing.Size(46, 28);
            this.cos.TabIndex = 28;
            this.cos.Text = "COS";
            this.cos.UseVisualStyleBackColor = false;
            this.cos.Click += new System.EventHandler(this.cos_Click);
            // 
            // bin
            // 
            this.bin.BackColor = System.Drawing.Color.Transparent;
            this.bin.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.bin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bin.FlatAppearance.BorderSize = 0;
            this.bin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bin.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bin.ForeColor = System.Drawing.Color.White;
            this.bin.Location = new System.Drawing.Point(44, 533);
            this.bin.Name = "bin";
            this.bin.Size = new System.Drawing.Size(64, 32);
            this.bin.TabIndex = 29;
            this.bin.Text = "0101";
            this.bin.UseVisualStyleBackColor = false;
            // 
            // lgtn
            // 
            this.lgtn.BackColor = System.Drawing.Color.Transparent;
            this.lgtn.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.lgtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lgtn.FlatAppearance.BorderSize = 0;
            this.lgtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lgtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lgtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lgtn.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lgtn.ForeColor = System.Drawing.Color.White;
            this.lgtn.Location = new System.Drawing.Point(114, 508);
            this.lgtn.Name = "lgtn";
            this.lgtn.Size = new System.Drawing.Size(57, 32);
            this.lgtn.TabIndex = 31;
            this.lgtn.Text = "Log 10";
            this.lgtn.UseVisualStyleBackColor = false;
            this.lgtn.Click += new System.EventHandler(this.lgtn_Click);
            // 
            // cube
            // 
            this.cube.BackColor = System.Drawing.Color.Transparent;
            this.cube.BackgroundImage = global::Main.Properties.Resources.btnsim1;
            this.cube.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cube.FlatAppearance.BorderSize = 0;
            this.cube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cube.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cube.ForeColor = System.Drawing.Color.White;
            this.cube.Location = new System.Drawing.Point(185, 508);
            this.cube.Name = "cube";
            this.cube.Size = new System.Drawing.Size(57, 32);
            this.cube.TabIndex = 32;
            this.cube.Text = "X^3";
            this.cube.UseVisualStyleBackColor = false;
            this.cube.Click += new System.EventHandler(this.cube_Click);
            // 
            // sqr
            // 
            this.sqr.BackColor = System.Drawing.Color.Transparent;
            this.sqr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sqr.BackgroundImage")));
            this.sqr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sqr.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sqr.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.sqr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.sqr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sqr.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqr.ForeColor = System.Drawing.Color.Black;
            this.sqr.Location = new System.Drawing.Point(120, 153);
            this.sqr.Name = "sqr";
            this.sqr.Size = new System.Drawing.Size(38, 28);
            this.sqr.TabIndex = 33;
            this.sqr.Text = "X^2";
            this.sqr.UseVisualStyleBackColor = false;
            this.sqr.Click += new System.EventHandler(this.sqr_Click);
            // 
            // power
            // 
            this.power.BackColor = System.Drawing.Color.Transparent;
            this.power.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("power.BackgroundImage")));
            this.power.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.power.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.power.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.power.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.power.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.power.ForeColor = System.Drawing.Color.Black;
            this.power.Location = new System.Drawing.Point(164, 153);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(33, 28);
            this.power.TabIndex = 34;
            this.power.Text = "X^";
            this.power.UseVisualStyleBackColor = false;
            this.power.Click += new System.EventHandler(this.power_Click);
            // 
            // loglog
            // 
            this.loglog.BackColor = System.Drawing.Color.Transparent;
            this.loglog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loglog.BackgroundImage")));
            this.loglog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loglog.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.loglog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.loglog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.loglog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loglog.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loglog.ForeColor = System.Drawing.Color.Black;
            this.loglog.Location = new System.Drawing.Point(236, 187);
            this.loglog.Name = "loglog";
            this.loglog.Size = new System.Drawing.Size(41, 28);
            this.loglog.TabIndex = 35;
            this.loglog.Text = "Log";
            this.loglog.UseVisualStyleBackColor = false;
            this.loglog.Click += new System.EventHandler(this.loglog_Click);
            // 
            // calcdect
            // 
            this.calcdect.Enabled = true;
            this.calcdect.Interval = 10;
            this.calcdect.Tick += new System.EventHandler(this.calcdecttc);
            // 
            // delcalc
            // 
            this.delcalc.Interval = 3;
            this.delcalc.Tick += new System.EventHandler(this.delcalc_Tick);
            // 
            // clcenbl
            // 
            this.clcenbl.Enabled = true;
            this.clcenbl.Tick += new System.EventHandler(this.clcenbltc);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(363, 24);
            this.menuStrip1.TabIndex = 36;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clcmnuext});
            this.fileToolStripMenuItem.Image = global::Main.Properties.Resources.file1;
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // clcmnuext
            // 
            this.clcmnuext.Image = global::Main.Properties.Resources.extfin;
            this.clcmnuext.Name = "clcmnuext";
            this.clcmnuext.Size = new System.Drawing.Size(92, 22);
            this.clcmnuext.Text = "Exit";
            this.clcmnuext.Click += new System.EventHandler(this.clcmnuextclc);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selall,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.editToolStripMenuItem.Image = global::Main.Properties.Resources.edit;
            this.editToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // selall
            // 
            this.selall.Image = global::Main.Properties.Resources.SelectAll;
            this.selall.ImageTransparentColor = System.Drawing.Color.White;
            this.selall.Name = "selall";
            this.selall.Size = new System.Drawing.Size(122, 22);
            this.selall.Text = "Select All";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.cutToolStripMenuItem.Text = "Copy";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::Main.Properties.Resources.Paste;
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Paste";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Image = global::Main.Properties.Resources.hlp1;
            this.helpToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(5, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(353, 117);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(281, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 28);
            this.button1.TabIndex = 38;
            this.button1.Text = "=";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Calculator_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(363, 280);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Divide);
            this.Controls.Add(this.eqs);
            this.Controls.Add(this.loglog);
            this.Controls.Add(this.power);
            this.Controls.Add(this.sqr);
            this.Controls.Add(this.cube);
            this.Controls.Add(this.lgtn);
            this.Controls.Add(this.bin);
            this.Controls.Add(this.cos);
            this.Controls.Add(this.ninebtn);
            this.Controls.Add(this.zerobtn);
            this.Controls.Add(this.twobtn);
            this.Controls.Add(this.threebtn);
            this.Controls.Add(this.fourbtn);
            this.Controls.Add(this.fivebtn);
            this.Controls.Add(this.sixbtn);
            this.Controls.Add(this.sevenbtn);
            this.Controls.Add(this.eightbtn);
            this.Controls.Add(this.onebtn);
            this.Controls.Add(this.Resulttf);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.mult);
            this.Controls.Add(this.calcts);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.rtfclc);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Calculator_1";
            this.Text = "Calc_optn_1";
            this.Deactivate += new System.EventHandler(this.Calculator_1_Deactivate);
            this.Load += new System.EventHandler(this.Calculator_1_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Calculator_1_MouseUp);
            this.Activated += new System.EventHandler(this.Calculator_1_Activated);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Calculator_1_MouseDown);
            this.calcts.ResumeLayout(false);
            this.calcts.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfclc;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button eqs;
        private System.Windows.Forms.ToolStrip calcts;
        private System.Windows.Forms.ToolStripButton clsecalc;
        private System.Windows.Forms.Button Divide;
        private System.Windows.Forms.Button mult;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.RichTextBox Resulttf;
        private System.Windows.Forms.Button onebtn;
        private System.Windows.Forms.Button eightbtn;
        private System.Windows.Forms.Button sevenbtn;
        private System.Windows.Forms.Button sixbtn;
        private System.Windows.Forms.Button fivebtn;
        private System.Windows.Forms.Button fourbtn;
        private System.Windows.Forms.Button threebtn;
        private System.Windows.Forms.Button twobtn;
        private System.Windows.Forms.Button zerobtn;
        private System.Windows.Forms.Button ninebtn;
        private System.Windows.Forms.Button cos;
        private System.Windows.Forms.Button bin;
        private System.Windows.Forms.Button lgtn;
        private System.Windows.Forms.Button cube;
        private System.Windows.Forms.Button sqr;
        private System.Windows.Forms.Button loglog;
        private System.Windows.Forms.Timer calcdect;
        private System.Windows.Forms.Timer delcalc;
        private System.Windows.Forms.Timer clcenbl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem clcmnuext;
        private System.Windows.Forms.ToolStripMenuItem selall;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button power;
        private System.Windows.Forms.Button button1;
    }
}