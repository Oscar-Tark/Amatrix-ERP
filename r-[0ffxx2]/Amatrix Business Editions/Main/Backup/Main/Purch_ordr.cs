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
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AMG_Fle
{
    public partial class Purch_ordr : Form
    {
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public Purch_ordr()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Disposed += new EventHandler(Purch_ordr_Disposed);
            if (Main.Amatrix.mgt != "")
            {
                try
                { Main.Security_PWD pwd = new Main.Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
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

        void Purch_ordr_Disposed(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            dtp.Clear();
            dtp_max.Clear();

            dtp.Dispose();
            dtp_max.Dispose();

            pO_InventryTableAdapter.Connection.Close();
            p_OrderTableAdapter.Connection.Close();
            prod_bulkTableAdapter.Connection.Close();

            pOrderBindingSource.EndEdit();

            pO_invntry_dtst.Clear();
            purchase_ordr.Clear();
            amdtbse_4DataSet1.Clear();

            pOrderBindingSource.Dispose();

            pO_InventryTableAdapter.Dispose();
            p_OrderTableAdapter.Dispose();
            prod_bulkTableAdapter.Dispose();

            pO_invntry_dtst.Dispose();
            purchase_ordr.Dispose();
            amdtbse_4DataSet1.Dispose();

            toolStripButton2.Click -= toolStripButton2_Click_2;
            this.Disposed -= Purch_ordr_Disposed;
            this.toolStripButton12.Click -=this.svall_Click;
            this.toolStripButton4.Click -=this.toolStripButton4_Click;
            this.svall.Click -=this.svall_Click;
            this.extfle.Click -=this.extfle_Click;
            this.rstrt2.Click -=this.rstrt2_Click;
            this.bindingNavigatorAddNewItem.Click -=this.bindingNavigatorAddNewItem_Click;
            this.bindingNavigatorDeleteItem.Click -=this.bindingNavigatorDeleteItem_Click;
            this.toolStripButton1.Click -=this.toolStripButton1_Click;
            this.bindingNavigatorPositionItem.TextChanged -=this.bindingNavigatorPositionItem_TextChanged;
            this.toolStripButton7.Click -=this.toolStripButton7_Click;
            this.tbxfned.Leave -=this.tbxfned_Leave;
            this.tbxfned.Enter -=this.tbxfned_Enter;
            this.dgv.CellBeginEdit -=this.dgv_CellBeginEdit;
            this.dgv.CellMouseLeave -=this.dgv_CellMouseLeave;
            this.dgv.CellMouseEnter -=this.dgv_CellMouseEnter;
            this.dgv.DataError -=this.dgv_DataError;
            this.dgv.CellEnter -=this.dgv_CellEnter;
            this.toolStripButton3.Click -=this.toolStripButton3_Click;
            //this.toolStripButton2.Click -=this.toolStripButton2_Click_1;
            this.checkBox1.Click -=this.checkBox1_Click;
            this.checkBox1.CheckedChanged -=this.checkBox1_CheckedChanged;
            this.textBox2.Enter -=this.textBox2_Enter;
            this.textBox1.TextChanged -=this.textBox1_TextChanged;
            this.button2.Click -=this.button2_Click;
            this.button1.Click -=this.button1_Click;
            this.comboBox2.DropDown -=this.comboBox2_DropDown;
            this.bkk_tx.DoWork -=this.bkk_tx_DoWork;
            this.bkk_tx.RunWorkerCompleted -=this.bkk_tx_RunWorkerCompleted;
            this.button18.Click -=this.button18_Click;
            this.button19.Click -=this.textBox2_Enter;
            this.dataGridView5.CellMouseClick -=this.dataGridView5_CellMouseClick;
            this.button3.Click -=this.button3_Click;
            this.Load -=this.Purch_ordr_Load;
            this.FormClosing -=this.Purch_ordr_FormClosing;

            this.components.Dispose();
            foreach (Control c in this.Controls)
            {
                c.Dispose();
            }
            GC.Collect();
        }

        private void Purch_ordr_Load(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                this.prod_bulkTableAdapter.Fill(this.amdtbse_4DataSet1.prod_bulk);
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk");
                amdtbse_4DataSet1.prod_bulk.Load(dr_univ);
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

        private void svall_Click(object sender, EventArgs e)
        {
            try
            {
                pOrderBindingSource.EndEdit();
                if (Main.Amatrix.mgt == "")
                {
                    p_OrderTableAdapter.Update(purchase_ordr);
                    pO_InventryTableAdapter.Update(pO_invntry_dtst);
                }
                else
                {
                    asql.Save(purchase_ordr.P_Order, "P_Order", Main.Amatrix.mgt);
                    asql.Save(pO_invntry_dtst.PO_Inventry, "PO_Inventry", Main.Amatrix.mgt);
                }
            }
            catch (Exception erty) { }
        }

        private void Purch_ordr_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                pOrderBindingSource.EndEdit();
                if (Main.Amatrix.mgt == "")
                {
                    p_OrderTableAdapter.Update(purchase_ordr);
                    pO_InventryTableAdapter.Update(pO_invntry_dtst);
                }
                else
                {
                    asql.Save(purchase_ordr.P_Order, "P_Order", Main.Amatrix.mgt);
                    asql.Save(pO_invntry_dtst.PO_Inventry, "PO_Inventry", Main.Amatrix.mgt);
                }
            }
            catch (Exception erty) { }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgv[6, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[6, dgv.CurrentRow.Index].Value = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Ticks.ToString();
            }
            if (dgv[4, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[4, dgv.CurrentRow.Index].Value = textBox1.Text;
            }
            if (dgv[0, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[0, dgv.CurrentRow.Index].Value = 0;
            }
            if (dgv[1, dgv.CurrentRow.Index].Value == DBNull.Value)
            {
                dgv[1, dgv.CurrentRow.Index].Value = "Add a Product";
            }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                if (dgvr.Index < dgv.Rows.Count - 1)
                {
                    dgvr.Cells[4].Value = textBox1.Text;
                }
            }
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
            pO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                pO_invntry_dtst.PO_Inventry.Load(dr);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                pO_invntry_dtst.PO_Inventry.Load(dr_univ);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
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
                SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt", "Prod_mgmt", dtp);
            }

            comboBox2.Items.Clear();
            foreach (DataRow dri in dtp.Rows)
            {
                try
                {
                    comboBox2.Items.Add(dri.ItemArray[1].ToString());
                }
                catch (Exception erty) { }
            }
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.Text = "";
            if (e.ColumnIndex == 1)
            {
                //if (dgv.CurrentRow.Index != dgv.Rows.Count - 1)
                //{
                    label9.Text = "Pick a Product";
                    panel5.Visible = true;
                    panel5.Location = new Point((Cursor.Position.X - this.Location.X) - panel5.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                //}
                //else
                //{
                    //panel5.Visible = false;
                //}
            }
            else
            {
                panel5.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentCell.ColumnIndex == 1)
                {
                    //dgv[0, dgv.CurrentRow.Index].Value = dtp.Rows[comboBox2.SelectedIndex].ItemArray[14];
                    dgv[2, dgv.CurrentRow.Index].Value = comboBox2.Text;
                    
                    try
                    {
                        dgv[1, dgv.CurrentRow.Index].Value = dtp.Rows[comboBox2.SelectedIndex].ItemArray[0];
                    }
                    catch (Exception erty) { }
                    //dgv[3, dgv.CurrentRow.Index].Value = comboBox1.Text;
                }
                else { dgv[1, dgv.CurrentRow.Index].Value = comboBox2.Text; }
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

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int start = 0;
            string SqlString = "Select * From [P_Order] Where";
            foreach (DataColumn dgvc in purchase_ordr.P_Order.Columns)
            {
                try
                {
                    if (dgvc.ColumnName != "Delivered")
                    {
                        if (start == 0)
                        {
                            if (tbxfned.Text.Contains(' ') == true)
                            {
                                SqlString = SqlString + " [" + dgvc.ColumnName + "] + ' ' + [" + purchase_ordr.P_Order.Columns[1].ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
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

            purchase_ordr.Clear();
            pO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = p_OrderTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            purchase_ordr.P_Order.Load(reader);
                        }
                    }
                }

                SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn2);
                conn2.Open();
                SqlCeDataReader dr = cmd2.ExecuteReader();
                pO_invntry_dtst.PO_Inventry.Load(dr);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
                conn2.Close();
            }
            else
            {
                dr_univ = Quer(SqlString);
                purchase_ordr.P_Order.Load(dr_univ);

                dr_univ = Quer("SELECT * FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                pO_invntry_dtst.PO_Inventry.Load(dr_univ);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
            }
        }

        private string Product, Supplier;
        public void tx(string Product_, string supplier)
        {
            this.Enabled = false;
            this.Text = "LOADING...";
            Product = Product_;
            Supplier = supplier;
            bkk_tx.RunWorkerAsync();
        }

         bool one_ = false;
         private void load_SQL()
         {
             if (Product != "")
             {
                 pO_invntry_dtst.Clear();
                 if (Main.Amatrix.mgt == "")
                 {
                     SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                     SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM PO_Inventry WHERE [Product ID Number] = '" + Product + "'", conn2);
                     conn2.Open();
                     SqlCeDataReader dr = cmd2.ExecuteReader();
                     pO_invntry_dtst.PO_Inventry.Load(dr);
                     conn2.Close();
                 }
                 else
                 {
                     dr_univ = Quer("SELECT * FROM PO_Inventry WHERE [Product ID Number] = '" + Product + "'");
                     pO_invntry_dtst.PO_Inventry.Load(dr_univ);
                 }
                 dataGridView3.DataSource = pO_invntry_dtst.PO_Inventry;

                 string SQLSTR = "";
                 foreach (DataGridViewRow dgvr in dataGridView3.Rows)
                 {
                     try
                     {
                         if (one_ == false)
                         {
                             SQLSTR = SQLSTR + "SELECT * FROM P_Order WHERE [Order Number] = '" + dgvr.Cells[3].Value.ToString() + "'";
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

                 try
                 {
                     purchase_ordr.P_Order.Clear();
                     if (Main.Amatrix.mgt == "")
                     {
                         SqlCeConnection conn3 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                         SqlCeCommand cmd = new SqlCeCommand(SQLSTR, conn3);
                         conn3.Open();
                         SqlCeDataReader drr = cmd.ExecuteReader();
                         purchase_ordr.P_Order.Load(drr);
                         conn3.Close();
                     }
                     else
                     {
                         dr_univ = Quer(SQLSTR);
                         purchase_ordr.P_Order.Load(dr_univ);
                     }
                 }
                 catch (Exception erty) { }
             }
             else
             {
                 if (Main.Amatrix.mgt == "")
                 {
                     pO_invntry_dtst.Clear();
                     SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                     SqlCeCommand cmd2 = new SqlCeCommand("SELECT distinct FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn2);
                     conn2.Open();
                     SqlCeDataReader dr = cmd2.ExecuteReader();
                     pO_invntry_dtst.PO_Inventry.Load(dr);
                     dgv.DataSource = pO_invntry_dtst.PO_Inventry;
                 }
                 else 
                 {
                     dr_univ = Quer("SELECT distinct FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                     pO_invntry_dtst.PO_Inventry.Load(dr_univ);
                     dgv.DataSource = pO_invntry_dtst.PO_Inventry;
                 }
             }
         }

         bool added = false;
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            EventArgs ev = new EventArgs();
            added = true;
            svall_Click(svall, ev);
            pO_invntry_dtst.Clear();
            checkBox1.CheckState = CheckState.Unchecked;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            purchase_ordr.Clear();
            pO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = p_OrderTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM P_Order", conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            purchase_ordr.P_Order.Load(reader);
                        }
                    }
                }
                EventArgs et = new EventArgs();

                SqlCeConnection conn2 = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn2);
                conn2.Open();
                SqlCeDataReader dr = cmd2.ExecuteReader();
                pO_invntry_dtst.PO_Inventry.Load(dr);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
                conn2.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM P_Order");
                purchase_ordr.P_Order.Load(dr_univ);

                dr_univ = Quer("SELECT * FROM PO_Inventry WHERE [For Purchase Order] = '" + textBox1.Text + "'");
                pO_invntry_dtst.PO_Inventry.Load(dr_univ);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            pO_invntry_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse5ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM PO_Inventry", conn);// WHERE [For Purchase Order] = '" + textBox1.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                pO_invntry_dtst.PO_Inventry.Load(dr);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM PO_Inventry");
                pO_invntry_dtst.PO_Inventry.Load(dr_univ);
                dgv.DataSource = pO_invntry_dtst.PO_Inventry;
            }
        }

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

        private DataTable dtp_max = new DataTable(); int i;
        private void checkBox1_Click(object sender, EventArgs e)
        {
            svall_Click(svall, e);
            if (checkBox1.Checked == true)
            {
                dtp_max.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT max([Serial Number]) FROM prod_bulk", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    dtp_max.Load(dr);
                    conn.Close();
                }
                else
                {
                    dtp_max = basql.Execute(Main.Amatrix.mgt, "SELECT max([Serial Number]) FROM prod_bulk", "prod_bulk", dtp_max);
                }
                //...
                try
                {
                    i = Convert.ToInt32(dtp_max.Rows[0].ItemArray[0]);
                    i = i + 1;
                }
                catch (Exception erty) { i = 1; }
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Main.Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("", conn);
                    conn.Open();
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        try
                        {
                            try
                            {
                                if (dgvr.Cells[3].Value == DBNull.Value)
                                {
                                    dgvr.Cells[3].Value = "Unreferenced Item";
                                }
                            }
                            catch (Exception erty) { }
                            cmd.CommandText = "INSERT INTO prod_bulk VALUES(" + i.ToString() + ",'" + dgvr.Cells[3].Value.ToString() + "','" + dgvr.Cells[2].Value + "','','','','','','','" + dgvr.Cells[0].Value + "','','','','','','','','','')";
                            cmd.ExecuteReader();
                            i = i + 1;
                        }
                        catch (Exception erty) { }
                    }
                    conn.Close();
                }
                else
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        try
                        {
                            try
                            {
                                if (dgvr.Cells[3].Value == DBNull.Value)
                                {
                                    dgvr.Cells[3].Value = "Unreferenced Item";
                                }
                            }
                            catch (Exception erty) { }
                            basql.Execute(Main.Amatrix.mgt, "INSERT INTO prod_bulk VALUES(" + i.ToString() + ",'" + dgvr.Cells[3].Value.ToString() + "','" + dgvr.Cells[2].Value + "','','','','','','','" + dgvr.Cells[0].Value + "','','','','','','','','','')", "prod_bulk", dtp);
                            i = i + 1;
                        }
                        catch (Exception erty) { }
                    }
                }
                svall_Click(svall,e);
                MessageBox.Show("All Your Records in the Purchase Inventory Tab have been added to Your Inventory Browser For The Specified Product, THIS OPERATION IS NOT REVERSABLE. The Purchase Order has been Marked as Final.", "Xaria Helper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("After Selecting This Check Box All Your Records in the Purchase Inventory Tab Will be added to Your Inventory Browser, THIS OPERATION IS NOT REVERSABLE. After the Operation the current Purchase order Will be marked as Final and will become READ ONLY(You will not be Able to Write New records for the Current Purchase Order))", "Xaria Helper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                try
                {
                    dgv.Rows.Remove(dgvr);
                }
                catch (Exception ertyu) { }
            }
            if (Main.Amatrix.mgt == "")
            {
                pO_InventryTableAdapter.Update(pO_invntry_dtst);
            }
            else
            {
                asql.Save(pO_invntry_dtst.PO_Inventry, "PO_Inventry", Main.Amatrix.mgt);
            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            //toolTip1.Show("hjjh", this);
        }

        private void extfle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rstrt2_Click(object sender, EventArgs e)
        {
            Purch_ordr ordr = new Purch_ordr();
            ordr.Show();
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                pO_InventryTableAdapter.Update(pO_invntry_dtst);
            }
            else
            {
                asql.Save(pO_invntry_dtst.PO_Inventry, "PO_Inventry", Main.Amatrix.mgt);
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void bkk_tx_DoWork(object sender, DoWorkEventArgs e)
        {
            load_SQL();
        }

        private void bkk_tx_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EventArgs er = new EventArgs();
            bindingNavigatorPositionItem_TextChanged(sender, er);
            this.Enabled = true;
            this.Text = "Amatrix [Compact Edition] : Purchase Orders";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Main.Doc_stdio std = new Main.Doc_stdio();
            std.tx2("purchase order");
            std.Show();
            std.set_P(dgv, textBox1.Text, dateTimePicker1.Value.ToString(), textBox2.Text);
            //std.Compile_Print_Data(dgv, textBox1.Text, dateTimePicker1.Value.ToString(), textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.mgmt_opr opr = new Main.mgmt_opr();
            opr.Show();
            opr.tx(textBox2.Text);
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

        private void dataGridView5_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox2.Text = dataGridView5[0, e.RowIndex].Value.ToString();
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

        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();
        private void toolStripButton2_Click_2(object sender, EventArgs e)
        {
            DataTable dtp_nfo = new DataTable();
            string business, address, eml, co_num;
            if (Main.Amatrix.doc == "")
            {
                dtp_nfo = scsql.Execute(Main.Properties.Settings.Default.Misc_DBConnectionString, "SELECT * FROM co_nfo");
                
                business = dtp_nfo.Rows[0].ItemArray[0].ToString();
                address = dtp_nfo.Rows[0].ItemArray[3].ToString();
                eml = dtp_nfo.Rows[0].ItemArray[4].ToString();
                co_num = dtp_nfo.Rows[0].ItemArray[2].ToString();
            }
            else
            {
                dtp_nfo = basql.Execute(Main.Amatrix.doc, "SELECT * FROM co_nfo", "", dtp_nfo);

                business = dtp_nfo.Rows[0].ItemArray[0].ToString();
                address = dtp_nfo.Rows[0].ItemArray[4].ToString();
                eml = dtp_nfo.Rows[0].ItemArray[8].ToString();
                co_num = dtp_nfo.Rows[0].ItemArray[2].ToString();
            }
            dtp_nfo.Clear(); dtp_nfo.Dispose();

            //get supplier info
            DataTable dtp_supli = new DataTable();
            string address_spli = "";
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    dtp_supli = scsql.Execute(Main.Properties.Settings.Default.Amdtbse_6ConnectionString, "SELECT * FROM Suppliers WHERE [Supplier Name] = '" + Supplier + "'");
                }
                else
                {
                    dtp_supli = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Suppliers WHERE [Supplier Name] = '" + Supplier + "'", "", dtp_supli);
                }
                address_spli = dtp_supli.Rows[0].ItemArray[2].ToString();
                dtp_supli.Clear();
                dtp_supli.Dispose();
            }
            catch (Exception erty) { }

            string message = "Purchase Order: \n\n\n" + "Deliver To: " + business + "\n" + "At Address: " + address + "\n" + "Phone Number: " + co_num + "\n" + "Email: " + eml + "\n\n\n" + "Items:" + "\n" + "----------------------------------------------" + "\n\n";

            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                try
                {
                    message = message + "Product Name:[" + dgvr.Cells[1].Value.ToString() + "]" + ", Bar Code:[" + dgvr.Cells[0].Value.ToString() + "], Product ID Number:[" + dgvr.Cells[2].Value.ToString() + "], Quantity Needed:[" + dgvr.Cells[7].Value.ToString() + "]" + "\n";
                }
                catch (Exception erty) { }
            }

            Main.eml_send eml__ = new Main.eml_send();
            eml__.Send_to(address_spli, message, business, "Purchase Order From " + business);
        }
    }
}