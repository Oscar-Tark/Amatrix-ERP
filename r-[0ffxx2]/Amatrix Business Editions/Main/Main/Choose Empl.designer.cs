namespace Main
{
    partial class Choose_Empl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Choose_Empl));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.employeeFirstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeLastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeTimeInDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeTimeOutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoursWorkedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signatureDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dTSingatureDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bSAMDataSet = new Main.BSAMDataSet();
            this.button2 = new System.Windows.Forms.Button();
            this.signsTableAdapter = new Main.BSAMDataSetTableAdapters.SignsTableAdapter();
            this.abtclse = new System.Windows.Forms.Timer(this.components);
            this.dectmeabt = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSAMDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Whom Would You Like to Time In/Out?";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(477, 411);
            this.dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(311, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ok, Time In";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.employeeFirstNameDataGridViewTextBoxColumn,
            this.employeeLastNameDataGridViewTextBoxColumn,
            this.employeeTimeInDataGridViewTextBoxColumn,
            this.employeeTimeOutDataGridViewTextBoxColumn,
            this.hoursWorkedDataGridViewTextBoxColumn,
            this.signatureDataGridViewTextBoxColumn,
            this.dTSingatureDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.signsBindingSource;
            this.dgv.Location = new System.Drawing.Point(240, 250);
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(240, 150);
            this.dgv.TabIndex = 3;
            this.dgv.Visible = false;
            // 
            // employeeFirstNameDataGridViewTextBoxColumn
            // 
            this.employeeFirstNameDataGridViewTextBoxColumn.DataPropertyName = "Employee First Name";
            this.employeeFirstNameDataGridViewTextBoxColumn.HeaderText = "Employee First Name";
            this.employeeFirstNameDataGridViewTextBoxColumn.Name = "employeeFirstNameDataGridViewTextBoxColumn";
            // 
            // employeeLastNameDataGridViewTextBoxColumn
            // 
            this.employeeLastNameDataGridViewTextBoxColumn.DataPropertyName = "Employee Last Name";
            this.employeeLastNameDataGridViewTextBoxColumn.HeaderText = "Employee Last Name";
            this.employeeLastNameDataGridViewTextBoxColumn.Name = "employeeLastNameDataGridViewTextBoxColumn";
            // 
            // employeeTimeInDataGridViewTextBoxColumn
            // 
            this.employeeTimeInDataGridViewTextBoxColumn.DataPropertyName = "Employee Time In";
            this.employeeTimeInDataGridViewTextBoxColumn.HeaderText = "Employee Time In";
            this.employeeTimeInDataGridViewTextBoxColumn.Name = "employeeTimeInDataGridViewTextBoxColumn";
            // 
            // employeeTimeOutDataGridViewTextBoxColumn
            // 
            this.employeeTimeOutDataGridViewTextBoxColumn.DataPropertyName = "Employee Time Out";
            this.employeeTimeOutDataGridViewTextBoxColumn.HeaderText = "Employee Time Out";
            this.employeeTimeOutDataGridViewTextBoxColumn.Name = "employeeTimeOutDataGridViewTextBoxColumn";
            // 
            // hoursWorkedDataGridViewTextBoxColumn
            // 
            this.hoursWorkedDataGridViewTextBoxColumn.DataPropertyName = "Hours Worked";
            this.hoursWorkedDataGridViewTextBoxColumn.HeaderText = "Hours Worked";
            this.hoursWorkedDataGridViewTextBoxColumn.Name = "hoursWorkedDataGridViewTextBoxColumn";
            // 
            // signatureDataGridViewTextBoxColumn
            // 
            this.signatureDataGridViewTextBoxColumn.DataPropertyName = "Signature";
            this.signatureDataGridViewTextBoxColumn.HeaderText = "Signature";
            this.signatureDataGridViewTextBoxColumn.Name = "signatureDataGridViewTextBoxColumn";
            // 
            // dTSingatureDataGridViewTextBoxColumn
            // 
            this.dTSingatureDataGridViewTextBoxColumn.DataPropertyName = "DT Singature";
            this.dTSingatureDataGridViewTextBoxColumn.HeaderText = "DT Singature";
            this.dTSingatureDataGridViewTextBoxColumn.Name = "dTSingatureDataGridViewTextBoxColumn";
            // 
            // signsBindingSource
            // 
            this.signsBindingSource.DataMember = "Signs";
            this.signsBindingSource.DataSource = this.bSAMDataSet;
            // 
            // bSAMDataSet
            // 
            this.bSAMDataSet.DataSetName = "BSAMDataSet";
            this.bSAMDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(392, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Ok, Time Out";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // signsTableAdapter
            // 
            this.signsTableAdapter.ClearBeforeFill = true;
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
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(12, 442);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Open HR";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Choose_Empl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 471);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Choose_Empl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Business Studio [Choose an Employee]";
            this.Deactivate += new System.EventHandler(this.Choose_Empl_Deactivate);
            this.Load += new System.EventHandler(this.Choose_Empl_Load);
            this.Activated += new System.EventHandler(this.Choose_Empl_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSAMDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv;
        private BSAMDataSet bSAMDataSet;
        private System.Windows.Forms.BindingSource signsBindingSource;
        private Main.BSAMDataSetTableAdapters.SignsTableAdapter signsTableAdapter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeFirstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeLastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeTimeInDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeTimeOutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoursWorkedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signatureDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dTSingatureDataGridViewTextBoxColumn;
        private System.Windows.Forms.Timer abtclse;
        private System.Windows.Forms.Timer dectmeabt;
        private System.Windows.Forms.Button button3;
    }
}