namespace Main
{
    partial class logs_pick_prods
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.productSerialNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productReferenceIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageboxNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesOrParticularsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.owningProductIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logsprodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logs_prod2 = new Main.logs_prod2();
            this.abtclse = new System.Windows.Forms.Timer(this.components);
            this.dectmeabt = new System.Windows.Forms.Timer(this.components);
            this.logs_prodTableAdapter = new Main.logs_prod2TableAdapters.Logs_prodTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logsprodBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logs_prod2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(2, 27);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(465, 114);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_re);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(2, 163);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(465, 123);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.cel_entr);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.BackgroundImage = global::Main.Properties.Resources.BannerTRS;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Location = new System.Drawing.Point(382, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(2, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First - Pick a Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(2, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(386, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Second - Pick From Inventory (Select Rows and Click on Done to Add Products)";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productSerialNumberDataGridViewTextBoxColumn,
            this.productReferenceIDDataGridViewTextBoxColumn,
            this.packageboxNumberDataGridViewTextBoxColumn,
            this.notesOrParticularsDataGridViewTextBoxColumn,
            this.owningProductIDDataGridViewTextBoxColumn});
            this.dataGridView3.DataSource = this.logsprodBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(12, 292);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(331, 78);
            this.dataGridView3.TabIndex = 5;
            this.dataGridView3.Visible = false;
            // 
            // productSerialNumberDataGridViewTextBoxColumn
            // 
            this.productSerialNumberDataGridViewTextBoxColumn.DataPropertyName = "Product Serial Number";
            this.productSerialNumberDataGridViewTextBoxColumn.HeaderText = "Product Serial Number";
            this.productSerialNumberDataGridViewTextBoxColumn.Name = "productSerialNumberDataGridViewTextBoxColumn";
            // 
            // productReferenceIDDataGridViewTextBoxColumn
            // 
            this.productReferenceIDDataGridViewTextBoxColumn.DataPropertyName = "Product Reference ID";
            this.productReferenceIDDataGridViewTextBoxColumn.HeaderText = "Product Reference ID";
            this.productReferenceIDDataGridViewTextBoxColumn.Name = "productReferenceIDDataGridViewTextBoxColumn";
            // 
            // packageboxNumberDataGridViewTextBoxColumn
            // 
            this.packageboxNumberDataGridViewTextBoxColumn.DataPropertyName = "Package/box Number";
            this.packageboxNumberDataGridViewTextBoxColumn.HeaderText = "Package/box Number";
            this.packageboxNumberDataGridViewTextBoxColumn.Name = "packageboxNumberDataGridViewTextBoxColumn";
            // 
            // notesOrParticularsDataGridViewTextBoxColumn
            // 
            this.notesOrParticularsDataGridViewTextBoxColumn.DataPropertyName = "Notes or Particulars";
            this.notesOrParticularsDataGridViewTextBoxColumn.HeaderText = "Notes or Particulars";
            this.notesOrParticularsDataGridViewTextBoxColumn.Name = "notesOrParticularsDataGridViewTextBoxColumn";
            // 
            // owningProductIDDataGridViewTextBoxColumn
            // 
            this.owningProductIDDataGridViewTextBoxColumn.DataPropertyName = "Owning Product ID";
            this.owningProductIDDataGridViewTextBoxColumn.HeaderText = "Owning Product ID";
            this.owningProductIDDataGridViewTextBoxColumn.Name = "owningProductIDDataGridViewTextBoxColumn";
            // 
            // logsprodBindingSource
            // 
            this.logsprodBindingSource.DataMember = "Logs_prod";
            this.logsprodBindingSource.DataSource = this.logs_prod2;
            // 
            // logs_prod2
            // 
            this.logs_prod2.DataSetName = "logs_prod2";
            this.logs_prod2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // logs_prodTableAdapter
            // 
            this.logs_prodTableAdapter.ClearBeforeFill = true;
            // 
            // logs_pick_prods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(469, 323);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "logs_pick_prods";
            this.Text = "Pick Products";
            this.Deactivate += new System.EventHandler(this.logs_pick_prods_Deactivate);
            this.Load += new System.EventHandler(this.logs_pick_prods_Load);
            this.Activated += new System.EventHandler(this.logs_pick_prods_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logsprodBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logs_prod2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private logs_prod2 logs_prod2;
        private System.Windows.Forms.BindingSource logsprodBindingSource;
        private Main.logs_prod2TableAdapters.Logs_prodTableAdapter logs_prodTableAdapter;
        private System.Windows.Forms.Timer abtclse;
        private System.Windows.Forms.Timer dectmeabt;
        private System.Windows.Forms.DataGridViewTextBoxColumn productSerialNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productReferenceIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageboxNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesOrParticularsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn owningProductIDDataGridViewTextBoxColumn;
    }
}