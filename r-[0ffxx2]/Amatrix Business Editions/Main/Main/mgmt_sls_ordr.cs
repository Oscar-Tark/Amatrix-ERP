/*Amatrix Data Center
    Copyright (C) 2013  Oscar Arjun Singh Tark, Benjamin Jack Johnson

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_sls_ordr : Form
    {
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();

        public mgmt_sls_ordr()
        {
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(mgmt_sls_ordr_Disposed);
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        void mgmt_sls_ordr_Disposed(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            dataGridView5.DataSource = null;

            sO_InventryTableAdapter.Connection.Close();
            s_OrderTableAdapter.Connection.Close();
            prod_bulkTableAdapter.Connection.Close();

            sOrderBindingSource.EndEdit();

            sO_invntry_dtst.Clear();
            sO_dtst.Clear();
            prod_bulk_dtst.Clear();

            sO_InventryTableAdapter.Dispose();
            s_OrderTableAdapter.Dispose();
            prod_bulkTableAdapter.Dispose();

            sO_invntry_dtst.Dispose();
            sO_dtst.Dispose();
            prod_bulk_dtst.Dispose();

            sOInventryBindingSource.Dispose();
            sOrderBindingSource.Dispose();
            prodbulkBindingSource.Dispose();

            this.Disposed -= mgmt_sls_ordr_Disposed;
            bindingNavigatorAddNewItem.Click -= bindingNavigatorAddNewItem_Click;
            bkk_custs.DoWork -= bkk_custs_DoWork;
            bkk_custs.RunWorkerCompleted -= bkk_custs_RunWorkerCompleted;
            comboBox1.DropDown -= comboBox1_DropDown;
            comboBox2.DropDown -= comboBox2_DropDown;
            tbxfned.Enter -= tbxfned_Enter;
            tbxfned.Leave -= tbxfned_Leave;
            checkBox1.CheckedChanged -= checkBox1_CheckedChanged;
            checkBox1.Click -= checkBox1_Click;
            this.toolStripButton1.Click -= this.toolStripButton1_Click;
            this.bindingNavigatorPositionItem.TextChanged -= this.bindingNavigatorPositionItem_TextChanged;
            this.toolStripButton7.Click -= this.toolStripButton7_Click;
            this.dgv.CellBeginEdit -= this.dgv_CellBeginEdit;
            this.dgv.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv.DataError -= this.dgv_DataError;
            this.dgv.CellEnter -= this.dgv_CellEnter;
            this.dgv.CellContentClick -= this.dgv_CellContentClick;
            this.toolStripButton3.Click -= this.toolStripButton3_Click;
            this.textBox2.Enter -= this.textBox2_Enter;
            this.button2.Click -= this.button2_Click;
            this.toolStripButton12.Click -= this.toolStripButton12_Click;
            this.toolStripButton4.Click -= this.toolStripButton4_Click;
            this.svall.Click -= this.toolStripButton12_Click;
            this.extfle.Click -= this.extfle_Click;
            this.rstrt2.Click -= this.rstrt2_Click;
            this.button1.Click -= this.button1_Click;
            this.comboBox2.DropDown -= this.comboBox2_DropDown;
            this.bkk_tx.DoWork -= this.bkk_tx_DoWork;
            this.bkk_tx.RunWorkerCompleted -= this.bkk_tx_RunWorkerCompleted;
            this.button18.Click -= this.button18_Click;
            this.button19.Click -= this.textBox2_Enter;
            this.dataGridView5.CellMouseClick -= this.dataGridView5_CellMouseClick;
            this.button3.Click -= this.button3_Click;
            this.Load -= this.mgmt_sls_ordr_Load;

            this.components.Dispose();
            foreach (Control c in this.Controls)
            {
                c.Dispose();
            }
            GC.Collect();
        }

        private void mgmt_sls_ordr_Load(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                // TODO: This line of code loads data into the 'prod_bulk_dtst.prod_bulk' table. You can move, or remove it, as needed.
                this.prod_bulkTableAdapter.Fill(this.prod_bulk_dtst.prod_bulk);
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk");
                prod_bulk_dtst.prod_bulk.Load(dr_univ);
            }

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            try
            {
                sOrderBindingSource.EndEdit();
                if (Main.Amatrix.mgt == "")
                {
                    s_OrderTableAdapter.Update(sO_dtst);
                    sO_InventryTableAdapter.Update(sO_invntry_dtst);
                }
                else
                {
                    asql.Save(sO_dtst.S_Order, "S_Order", Main.Amatrix.mgt);
                    asql.Save(sO_invntry_dtst.SO_Inventry, "SO_Inventry", Main.Amatrix.mgt);
                }
            }
            catch (Exception erty) { }
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.Text = "";
            if (e.ColumnIndex == 1)
            {
                if (dgv.CurrentRow.Index != dgv.Rows.Count - 1)
                {
                    panel5.Visible = true;
                    panel5.Location = new Point((Cursor.Position.X - this.Location.X) - panel5.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                }
                else
                {
                    panel5.Visible = false;
                }
            }
            else if (e.ColumnIndex == 2 && dgv[1, e.RowIndex].Value != DBNull.Value)
            {
                if (dgv.CurrentRow.Index != dgv.Rows.Count - 1)
                {
                    label9.Text = "Pick Inventory";
                    panel5.Visible = true;
                    panel5.Location = new Point((Cursor.Position.X - this.Location.X) - panel5.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                }
                else
                {
                    panel5.Visible = false;
                }
            }
            else
            {
                panel5.Visible = false;
            }
        }

        private DataTable dtp = new DataTable();
        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            dtp = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse_4ConnectionString);
                string SQL = "";
                if (dgv.CurrentCell.ColumnIndex == 1)
                {
                    SQL = "SELECT * FROM Prod_mgmt";
                }
                else if (dgv.CurrentCell.ColumnIndex == 2)
                {
                    SQL = "SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + dgv.CurrentRow.Cells[1].Value.ToString() + "'";
                }
                SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
            }
            else
            {
                if (dgv.CurrentCell.ColumnIndex == 1)
                {
                    dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt", "Prod_mgmt", dtp);
                }
                else if (dgv.CurrentCell.ColumnIndex == 2)
                {
                    dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + dgv.CurrentRow.Cells[1].Value.ToString() + "'", "Prod_mgmt", dtp);
                }
            }

            comboBox2.Items.Clear();
            foreach (DataRow dri in dtp.Rows)
            {
                comboBox2.Items.Add(dri.ItemArray[1].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = dtp;
            try
            {
                if (dgv.CurrentCell.ColumnIndex == 1)
                {
                    dgv[0, dgv.CurrentRow.Index].Value = 0;
                    dgv[1, dgv.CurrentRow.Index].Value = comboBox2.Text;
                    try
                    {
                        //dgv[2, dgv.CurrentRow.Index].Value = dtp.Rows[comboBox2.SelectedIndex].ItemArray[2];
                    }
                    catch (Exception erty) { }
                    dgv[3, dgv.CurrentRow.Index].Value = textBox1.Text;
                }
                else { dgv[2, dgv.CurrentRow.Index].Value = comboBox2.Text; dgv[0, dgv.CurrentRow.Index].Value = dtp.Rows[comboBox2.SelectedIndex].ItemArray[9]; }
            }
            catch (Exception etrert) { }
            {
                /*DataRow row;
                DataTable tb = new DataTable();
                row = po_invntry_dtst.Porder_invntry.NewRow();
                row[5] = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Ticks.ToString();
                tb = po_invntry_dtst.Porder_invntry;
                tb.Rows.Add(row);
                dgv.DataSource = tb;
                dgv[0, dgv.CurrentRow.Index].Value = 0;
                dgv[1, dgv.CurrentRow.Index].Value = comboBox2.Text;
                try
                {
                    dgv[2, dgv.CurrentRow.Index].Value = dtp.Rows[comboBox2.SelectedIndex].ItemArray[2];
                }
                catch (Exception erty) { }
                dgv[3, dgv.CurrentRow.Index].Value = textBox1.Text;*/
            }
            //porder_invntryTableAdapter.Update(po_invntry_dtst);
        }

        private Color cl_tmp;
        private void dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cl_tmp = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
            }
            catch (Exception erty) { }
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
            }
            catch (Exception erty) { }
        }

        private string Product;
        public void tx(String Product_)
        {
            this.Enabled = false;
            this.Text = "LOADING...";
            Product = Product_;
            dgv.Visible = false;
            bkk_tx.RunWorkerAsync();
        }

        private void load_SQL()
        {
            if (Product != "")
            {
                sO_invntry_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                    SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM SO_Inventry WHERE [Inventory of Product] = '" + Product + "'", conn2);
                    conn2.Open();
                    SqlCeDataReader dr = cmd2.ExecuteReader();
                    sO_invntry_dtst.SO_Inventry.Load(dr);
                    conn2.Close();
                }
                else
                {
                    dr_univ = Quer("SELECT * FROM SO_Inventry WHERE [Inventory of Product] = '" + Product + "'");
                    sO_invntry_dtst.SO_Inventry.Load(dr_univ);
                }
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;

                string SQLSTR = "";
                foreach (DataGridViewRow dgvr in dgv.Rows)
                {
                    try
                    {
                        if (one_ == false)
                        {
                            SQLSTR = SQLSTR + "SELECT * FROM S_Order WHERE [Order Number] = '" + dgvr.Cells[3].Value.ToString() + "'";
                            one_ = true;
                        }
                        else
                        {
                            SQLSTR = SQLSTR + " OR [Order Number] = '" + dgvr.Cells[3].Value.ToString() + "'";
                        }
                    }
                    catch (Exception erty) { }
                }
                one_ = false;

                sO_dtst.S_Order.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn3 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(SQLSTR, conn3);
                    conn3.Open();
                    SqlCeDataReader drr = cmd.ExecuteReader();
                    sO_dtst.S_Order.Load(drr);
                    conn3.Close();
                }
                else
                {
                    dr_univ = Quer(SQLSTR);
                    sO_dtst.S_Order.Load(dr_univ);
                }
            }
            else
            {
                if (Main.Amatrix.mgt == "")
                {
                    sO_invntry_dtst.Clear();
                    SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                    SqlCeCommand cmd2 = new SqlCeCommand("SELECT distinct FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn2);
                    conn2.Open();
                    SqlCeDataReader dr = cmd2.ExecuteReader();
                    sO_invntry_dtst.SO_Inventry.Load(dr);
                }
                else
                {
                    dr_univ = Quer("SELECT distinct FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                    sO_invntry_dtst.SO_Inventry.Load(dr_univ);
                }
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
            }
        }

        SqlDataReader dr_univ;
        private SqlDataReader Quer(string Query)
        {
            SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
            SqlCommand cmd = new SqlCommand(Query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            conn.Close();
        }

        bool one_ = false;
        private void bkk_tx_DoWork(object sender, DoWorkEventArgs e)
        {
            load_SQL();
        }

        private void bkk_tx_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv.Visible = true;
            EventArgs er = new EventArgs();
            bindingNavigatorPositionItem_TextChanged(sender, er);
            this.Enabled = true;
            this.Text = "Amatrix [Compact Edition] : Sales Orders";
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                tabControl1.Enabled = false;
                tabControl2.Enabled = false;
            }
            else
            { tabControl1.Enabled = true; tabControl2.Enabled = true; }

            sO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                sO_invntry_dtst.SO_Inventry.Load(dr);
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                sO_invntry_dtst.SO_Inventry.Load(dr_univ);
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgv[5, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[5, dgv.CurrentRow.Index].Value = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Ticks.ToString();
            }
            if (dgv[3, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[3, dgv.CurrentRow.Index].Value = textBox1.Text;
            }
            if (dgv[0, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[0, dgv.CurrentRow.Index].Value = 0;
            }
            if (dgv[1, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[1, dgv.CurrentRow.Index].Value = "Add a Product";
            }
            dgv[3, dgv.CurrentRow.Index].Value = textBox1.Text;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                sO_InventryTableAdapter.Update(sO_invntry_dtst);
            }
            else
            {
                asql.Save(sO_invntry_dtst.SO_Inventry, "SO_Inventry", Main.Amatrix.mgt);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            sO_dtst.Clear();
            sO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = s_OrderTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM S_Order", conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            sO_dtst.S_Order.Load(reader);
                        }
                    }
                }
                EventArgs et = new EventArgs();

                SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn2);
                conn2.Open();
                SqlCeDataReader dr = cmd2.ExecuteReader();
                sO_invntry_dtst.SO_Inventry.Load(dr);
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
                conn2.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM S_Order");
                sO_dtst.S_Order.Load(dr_univ);

                dr_univ = Quer("SELECT * FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                sO_invntry_dtst.SO_Inventry.Load(dr_univ);
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
            }
        }

        private void extfle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rstrt2_Click(object sender, EventArgs e)
        {
            mgmt_sls_ordr sls = new mgmt_sls_ordr();
            sls.Show();
            this.Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int start = 0;
            string SqlString = "Select * From [S_Order] Where";
            foreach (DataColumn dgvc in sO_dtst.S_Order.Columns)
            {
                try
                {
                    if (dgvc.ColumnName != "Delivered")
                    {
                        if (start == 0)
                        {
                            if (tbxfned.Text.Contains(' ') == true)
                            {
                                SqlString = SqlString + " [" + dgvc.ColumnName + "] + ' ' + [" + sO_dtst.S_Order.Columns[1].ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
                            }
                            else
                            {
                                SqlString = SqlString + " [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
                            }
                            start = 1;
                        }
                        else
                        {
                            SqlString = SqlString + " OR [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%'";
                        }
                    }
                }
                catch (Exception ertty) { }
            }

            sO_dtst.Clear();
            sO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = s_OrderTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            sO_dtst.S_Order.Load(reader);
                        }
                    }
                }

                SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn2);
                conn2.Open();
                SqlCeDataReader dr = cmd2.ExecuteReader();
                sO_invntry_dtst.SO_Inventry.Load(dr);
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
                conn2.Close();
            }
            else
            {
                dr_univ = Quer(SqlString);
                sO_dtst.S_Order.Load(dr_univ);

                dr_univ = Quer("SELECT * FROM SO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                sO_invntry_dtst.SO_Inventry.Load(dr_univ);
                dgv.DataSource = sO_invntry_dtst.SO_Inventry;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.mgmt_opr opr = new Main.mgmt_opr();
            opr.Show();
            opr.tx(textBox2.Text);
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private DataTable dtp_c = new DataTable();
        private void textBox2_Enter(object sender, EventArgs e)
        {
            dtp_c = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Logs_mgmt", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView5.DataSource = dtp_c;
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Logs_mgmt", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView5.DataSource = dtp_c;
                conn.Close();
            }
            prod_box.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prod_box.Visible = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            dtp_c = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] LIKE '%" + textBox4.Text + "%'", conn);// OR [Employee Last Name] LIKE '%" + textBox11.Text + "%' OR [Employee ID] LIKE '%" + textBox11.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView5.DataSource = dtp_c;
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] LIKE '%" + textBox4.Text + "%'", conn);// OR [Employee Last Name] LIKE '%" + textBox11.Text + "%' OR [Employee ID] LIKE '%" + textBox11.Text + "%'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView5.DataSource = dtp_c;
                conn.Close();
            }
        }

        private void dataGridView5_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox2.Text = dataGridView5[0, e.RowIndex].Value.ToString();
        }

        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ClassStyle |= CS_DROPSHADOW;
                return p;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //DEPRECIATED
            Main.Doc_stdio std = new Main.Doc_stdio();
            std.tx2("sales order");
            std.Show();
            std.Compile_Print_Data2(dgv, textBox1.Text, dateTimePicker1.Value.ToString(), textBox2.Text, comboBox1.Text); // insert buyer
        }

        //new
        private void tbxfned_Enter(object sender, EventArgs e)
        {
            tbxfned.ForeColor = Color.Black;
            Font f = new Font(tbxfned.Font, FontStyle.Regular);
            tbxfned.Font = f;
        }

        private void tbxfned_Leave(object sender, EventArgs e)
        {
            tbxfned.ForeColor = Color.DimGray;
            Font f = new Font(tbxfned.Font, FontStyle.Italic);
            tbxfned.Font = f;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Indeterminate || checkBox1.CheckState == CheckState.Unchecked)
            {
                checkBox1.Enabled = true;
                tabControl1.Enabled = true;
                dgv.ReadOnly = false;
                panel5.Enabled = true;
            }
            else if (checkBox1.CheckState == CheckState.Checked)
            {
                checkBox1.Enabled = false;
                tabControl1.Enabled = false;
                dgv.ReadOnly = true;
                panel5.Enabled = false;
            }
        }

        private DataTable dtp_max = new DataTable();
        private void checkBox1_Click(object sender, EventArgs e)
        {
            EventArgs we = new EventArgs();
            toolStripButton12_Click(svall, we);
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow dgvr in dgv.Rows)
                {
                    try
                    {
                        if (Main.Amatrix.mgt == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = '" + comboBox1.Text + "', [State] = 'Sold' WHERE [Reference Number] = '" + dgvr.Cells[2].Value.ToString() + "'", conn);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [Sold To] = '" + comboBox1.Text + "', [State] = 'Sold' WHERE [Reference Number] = '" + dgvr.Cells[2].Value.ToString() + "'", "prod_bulk", dtp_custs);
                        }
                    }
                    catch (Exception erty) { }
                }
                MessageBox.Show("All Your Records in the Purchase Inventory Tab have been added to Your Inventory Browser For The Specified Product, THIS OPERATION IS NOT REVERSABLE. The Purchase Order has been Marked as Final.", "Xaria Helper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("After Selecting This Check Box All Your Records in the Purchase Inventory Tab Will be added to Your Inventory Browser, THIS OPERATION IS NOT REVERSABLE. After the Operation the current Purchase order Will be marked as Final and will become READ ONLY(You will not be Able to Write New records for the Current Purchase Order))", "Xaria Helper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            toolStripButton12_Click(svall, we);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Loading From Customer Managment...");
            try
            {
                dtp_custs.Clear();
                bkk_custs.RunWorkerAsync();
            }
            catch (Exception erty) { }
        }

        DataTable dtp_custs = new DataTable();
        private void bkk_custs_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_custs.Load(dr);
                conn.Close();
            }
            else
            {
                dtp_custs = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Customers", "Customers", dtp_custs);
            }
        }

        private void bkk_custs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (DataRow dr in dtp_custs.Rows)
            {
                comboBox1.Items.Add(dr.ItemArray[0].ToString());
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            EventArgs ev = new EventArgs();
            toolStripButton12_Click(svall, ev);
            sO_invntry_dtst.Clear();
            checkBox1.CheckState = CheckState.Unchecked;
        }
    }
}
