namespace Main
{
    partial class Suppliers
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.amdtbse_6DataSet1 = new Main.Amdtbse_6DataSet1();
            this.suppliersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.suppliersTableAdapter = new Main.Amdtbse_6DataSet1TableAdapters.SuppliersTableAdapter();
            this.supplierNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierWareHouseAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierPhoneNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierPhoneNumber2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierEmailAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amdtbse_6DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.suppliersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.supplierNameDataGridViewTextBoxColumn,
            this.supplierCodeDataGridViewTextBoxColumn,
            this.supplierAddressDataGridViewTextBoxColumn,
            this.supplierWareHouseAddressDataGridViewTextBoxColumn,
            this.supplierPhoneNumberDataGridViewTextBoxColumn,
            this.supplierPhoneNumber2DataGridViewTextBoxColumn,
            this.supplierEmailAddressDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.suppliersBindingSource;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(400, 320);
            this.dgv.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(93, 338);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remove Selected";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // amdtbse_6DataSet1
            // 
            this.amdtbse_6DataSet1.DataSetName = "Amdtbse_6DataSet1";
            this.amdtbse_6DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // suppliersBindingSource
            // 
            this.suppliersBindingSource.DataMember = "Suppliers";
            this.suppliersBindingSource.DataSource = this.amdtbse_6DataSet1;
            // 
            // suppliersTableAdapter
            // 
            this.suppliersTableAdapter.ClearBeforeFill = true;
            // 
            // supplierNameDataGridViewTextBoxColumn
            // 
            this.supplierNameDataGridViewTextBoxColumn.DataPropertyName = "Supplier Name";
            this.supplierNameDataGridViewTextBoxColumn.HeaderText = "Supplier Name";
            this.supplierNameDataGridViewTextBoxColumn.Name = "supplierNameDataGridViewTextBoxColumn";
            this.supplierNameDataGridViewTextBoxColumn.Width = 93;
            // 
            // supplierCodeDataGridViewTextBoxColumn
            // 
            this.supplierCodeDataGridViewTextBoxColumn.DataPropertyName = "Supplier Code";
            this.supplierCodeDataGridViewTextBoxColumn.HeaderText = "Supplier Code";
            this.supplierCodeDataGridViewTextBoxColumn.Name = "supplierCodeDataGridViewTextBoxColumn";
            this.supplierCodeDataGridViewTextBoxColumn.Width = 90;
            // 
            // supplierAddressDataGridViewTextBoxColumn
            // 
            this.supplierAddressDataGridViewTextBoxColumn.DataPropertyName = "Supplier Address";
            this.supplierAddressDataGridViewTextBoxColumn.HeaderText = "Supplier Address";
            this.supplierAddressDataGridViewTextBoxColumn.Name = "supplierAddressDataGridViewTextBoxColumn";
            this.supplierAddressDataGridViewTextBoxColumn.Width = 102;
            // 
            // supplierWareHouseAddressDataGridViewTextBoxColumn
            // 
            this.supplierWareHouseAddressDataGridViewTextBoxColumn.DataPropertyName = "Supplier WareHouse Address";
            this.supplierWareHouseAddressDataGridViewTextBoxColumn.HeaderText = "Supplier WareHouse Address";
            this.supplierWareHouseAddressDataGridViewTextBoxColumn.Name = "supplierWareHouseAddressDataGridViewTextBoxColumn";
            this.supplierWareHouseAddressDataGridViewTextBoxColumn.Width = 122;
            // 
            // supplierPhoneNumberDataGridViewTextBoxColumn
            // 
            this.supplierPhoneNumberDataGridViewTextBoxColumn.DataPropertyName = "Supplier Phone Number";
            this.supplierPhoneNumberDataGridViewTextBoxColumn.HeaderText = "Supplier Phone Number";
            this.supplierPhoneNumberDataGridViewTextBoxColumn.Name = "supplierPhoneNumberDataGridViewTextBoxColumn";
            this.supplierPhoneNumberDataGridViewTextBoxColumn.Width = 98;
            // 
            // supplierPhoneNumber2DataGridViewTextBoxColumn
            // 
            this.supplierPhoneNumber2DataGridViewTextBoxColumn.DataPropertyName = "Supplier Phone Number 2";
            this.supplierPhoneNumber2DataGridViewTextBoxColumn.HeaderText = "Supplier Phone Number 2";
            this.supplierPhoneNumber2DataGridViewTextBoxColumn.Name = "supplierPhoneNumber2DataGridViewTextBoxColumn";
            this.supplierPhoneNumber2DataGridViewTextBoxColumn.Width = 72;
            // 
            // supplierEmailAddressDataGridViewTextBoxColumn
            // 
            this.supplierEmailAddressDataGridViewTextBoxColumn.DataPropertyName = "Supplier Email Address";
            this.supplierEmailAddressDataGridViewTextBoxColumn.HeaderText = "Supplier Email Address";
            this.supplierEmailAddressDataGridViewTextBoxColumn.Name = "supplierEmailAddressDataGridViewTextBoxColumn";
            this.supplierEmailAddressDataGridViewTextBoxColumn.Width = 93;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(337, 338);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Suppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(424, 366);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Suppliers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Suppliers";
            this.Load += new System.EventHandler(this.Suppliers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amdtbse_6DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.suppliersBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button button2;
        private Amdtbse_6DataSet1 amdtbse_6DataSet1;
        private System.Windows.Forms.BindingSource suppliersBindingSource;
        private Main.Amdtbse_6DataSet1TableAdapters.SuppliersTableAdapter suppliersTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierWareHouseAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierPhoneNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierPhoneNumber2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierEmailAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button3;
    }
}