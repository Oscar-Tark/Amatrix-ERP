namespace Main
{
    partial class mgmt_purchase_information
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.purch_info_dtst = new Main.Purch_info_dtst();
            this.purchaseInformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.purchase_InformationTableAdapter = new Main.Purch_info_dtstTableAdapters.Purchase_InformationTableAdapter();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.forProductDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forProductSerialNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchaseCostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchaseUnitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.purch_info_dtst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseInformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(337, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(93, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Remove Selected";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // purch_info_dtst
            // 
            this.purch_info_dtst.DataSetName = "Purch_info_dtst";
            this.purch_info_dtst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // purchaseInformationBindingSource
            // 
            this.purchaseInformationBindingSource.DataMember = "Purchase_Information";
            this.purchaseInformationBindingSource.DataSource = this.purch_info_dtst;
            // 
            // purchase_InformationTableAdapter
            // 
            this.purchase_InformationTableAdapter.ClearBeforeFill = true;
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.forProductDataGridViewTextBoxColumn,
            this.forProductSerialNumberDataGridViewTextBoxColumn,
            this.purchaseCostDataGridViewTextBoxColumn,
            this.purchaseUnitDataGridViewTextBoxColumn});
            this.dgv.DataSource = this.purchaseInformationBindingSource;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(400, 105);
            this.dgv.TabIndex = 8;
            // 
            // forProductDataGridViewTextBoxColumn
            // 
            this.forProductDataGridViewTextBoxColumn.DataPropertyName = "For Product";
            this.forProductDataGridViewTextBoxColumn.HeaderText = "For Product";
            this.forProductDataGridViewTextBoxColumn.Name = "forProductDataGridViewTextBoxColumn";
            this.forProductDataGridViewTextBoxColumn.Width = 87;
            // 
            // forProductSerialNumberDataGridViewTextBoxColumn
            // 
            this.forProductSerialNumberDataGridViewTextBoxColumn.DataPropertyName = "For Product Serial Number";
            this.forProductSerialNumberDataGridViewTextBoxColumn.HeaderText = "For Product Serial Number";
            this.forProductSerialNumberDataGridViewTextBoxColumn.Name = "forProductSerialNumberDataGridViewTextBoxColumn";
            this.forProductSerialNumberDataGridViewTextBoxColumn.Width = 156;
            // 
            // purchaseCostDataGridViewTextBoxColumn
            // 
            this.purchaseCostDataGridViewTextBoxColumn.DataPropertyName = "Purchase Cost";
            this.purchaseCostDataGridViewTextBoxColumn.HeaderText = "Purchase Cost";
            this.purchaseCostDataGridViewTextBoxColumn.Name = "purchaseCostDataGridViewTextBoxColumn";
            this.purchaseCostDataGridViewTextBoxColumn.Width = 101;
            // 
            // purchaseUnitDataGridViewTextBoxColumn
            // 
            this.purchaseUnitDataGridViewTextBoxColumn.DataPropertyName = "Purchase Unit";
            this.purchaseUnitDataGridViewTextBoxColumn.HeaderText = "Purchase Unit";
            this.purchaseUnitDataGridViewTextBoxColumn.Name = "purchaseUnitDataGridViewTextBoxColumn";
            this.purchaseUnitDataGridViewTextBoxColumn.Width = 99;
            // 
            // mgmt_purchase_information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 156);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mgmt_purchase_information";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Purchase Information";
            this.Load += new System.EventHandler(this.mgmt_purchase_information_Load);
            ((System.ComponentModel.ISupportInitialize)(this.purch_info_dtst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseInformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private Purch_info_dtst purch_info_dtst;
        private System.Windows.Forms.BindingSource purchaseInformationBindingSource;
        private Main.Purch_info_dtstTableAdapters.Purchase_InformationTableAdapter purchase_InformationTableAdapter;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn forProductDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn forProductSerialNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseCostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseUnitDataGridViewTextBoxColumn;

    }
}