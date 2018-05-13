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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class gadg_custombook : UserControl
    {
        private int howmany;
        private int maxm;
        private ArrayList aund = new ArrayList();
        private ArrayList aundC = new ArrayList();
        private ArrayList aundR = new ArrayList();
        private ArrayList copycutpaste = new ArrayList();

        public gadg_custombook()
        {
            InitializeComponent();
            this.BringToFront();
            this.Disposed += new EventHandler(gadg_custombook_Disposed);
        }

        void gadg_custombook_Disposed(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            dst2.Clear();
            dt2.Clear();
            dt.Clear();

            this.Disposed -= gadg_custombook_Disposed;
            this.checkBox1.CheckedChanged -= cbx_conn_custm_TextChanged;
            this.textBox1.Leave -= cbx_conn_custm_TextChanged;
            this.cnn.Click -= cnn_Click;
            this.nb.Click -= nb_Click;
            this.cbx_conn_custm.DropDown -= cbx_conn_custm_DropDown;
            this.cbx_conn_custm.SelectedIndexChanged -= cbx_conn_custm_TextChanged;
            this.dgv.CellBeginEdit -= dgv_CellBeginEdit;
            this.dgv.CellMouseLeave -= dgv_CellMouseLeave;
            this.dgv.CellMouseEnter -= dgv_CellMouseEnter;
            this.dgv.DataError -= dgv_DataError;
            this.dgv.CellEnter -= dgv_CellEnter;
            this.undoToolStripMenuItem.Click -= und_Click;
            this.copyToolStripMenuItem.Click -= cpy_Click;
            this.cutToolStripMenuItem.Click -= ctat_Click;
            this.pasteToolStripMenuItem.Click -= pst_Click;
            this.selectAllToolStripMenuItem.Click -= selall_Click;
            this.shw_here.Click -= shw_here_Click;
            this.showAllToolStripMenuItem1.Click -= showAllToolStripMenuItem1_Click;
            this.Eqto.Click -= Queries;
            this.ntEqto.Click -= Queries;
            this.btw.Click -= Queries;
            this.gt.Click -= Queries;
            this.lt.Click -= Queries;
            this.stw.Click -= Queries;
            this.summedUpToolStripMenuItem.Click -= Queries;
            this.averagedToolStripMenuItem.Click -= Queries;
            this.dgvupall.Click -= dgvupall_Click;
            this.dgvupone.ButtonClick -= dgvupall_Click;
            this.uptxtdgv.TextChanged -= uptxtdgv_TextChanged;
            this.dgvleftall.Click -= dgvupall_Click;
            this.dgvleftone.Click -= dgvupall_Click;
            this.dgvrightone.Click -= dgvupall_Click;
            this.dgvrightall.Click -= dgvupall_Click;
            this.dgvdownone.ButtonClick -= dgvupall_Click;
            this.dgvtxtleft.TextChanged -= uptxtdgv_TextChanged;
            this.dgvdownall.Click -= dgvupall_Click;
            this.delete.ButtonClick -= delete_ButtonClick;
            this.del_all.Click -= del_all_Click;
            this.new_rw.Click -= new_rw_Click;
            this.menuStrip1.Click -= tswin_Click;
            this.saveToolStripMenuItem.Click -= sve_Click;
            this.closeToolStripMenuItem.Click -= clse_Click;
            this.und.Click -= und_Click;
            this.cpy.Click -= cpy_Click;
            this.ctat.Click -= ctat_Click;
            this.pst.Click -= pst_Click;
            this.selall.Click -= selall_Click;
            this.cnt.Click -= cnt_Click;
            this.nxtst.Click -= nxtst_Click;
            this.shw_here2.Click -= shw_here_Click;
            this.prev.Click -= prev_Click;
            this.stwin.Click -= tswin_Click;
            this.reszz.ButtonClick -= reszz_ButtonClick;
            this.onlyWidthToolStripMenuItem.Click -= simjrnwth_Click;
            this.onlyHeightToolStripMenuItem.Click -= simjrnhgt_Click;
            this.mvewin.ButtonClick -= tswin_DoubleClick;
            this.movtolef.Click -= movtolef_clc;
            this.toRightToolStripMenuItem.Click -= movtor;
            this.tobott.Click -= tobott_Click;
            this.totop.Click -= totop_Click;
            this.freesty.Click -= freesty_Click;
            this.tswin.DoubleClick -= tswin_DoubleClick;
            this.tswin.Click -= tswin_Click;
            this.clse.Click -= clse_Click;
            this.dvgpndoc.Click -= dvgpndoc_Click;
            this.svewin3.Click -= sve_Click;
            this.printDataToolStripMenuItem1.Click -= printDataToolStripMenuItem1_Click;
            this.sve.Click -= sve_Click;
            this.dgvwintic.Tick -= dgvwintic_Tick;
            this.tmex.Tick -= tmex_Tick;
            this.printToolStripMenuItem.Click -= printDataToolStripMenuItem1_Click;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void init()
        {
            if(this.Parent.Text.ToLower().Contains("(host)"))
            {
                tsttl.Text = "Custom Book (Client Mode)";
            }
        }

        private void dgvwintic_Tick(object sender, EventArgs e)
        {
            dvgpndoc.ToolTipText = "Maximize";
            this.Dock = DockStyle.None;
            try
            {
                this.Location = new Point((Cursor.Position.X - this.Parent.Parent.Location.X) - (this.Size.Width / 2), Cursor.Position.Y - this.Parent.Parent.Location.Y - 60);
            }
            catch (Exception erty)
            {
                this.Location = new Point((Cursor.Position.X - this.Parent.Location.X) - (this.Size.Width / 2), Cursor.Position.Y - this.Parent.Location.Y - 60);
            }
        }

        private void tswin_DoubleClick(object sender, EventArgs e)
        {
            this.BringToFront();
            dgvwintic.Start();
        }

        private void tswin_Click(object sender, EventArgs e)
        {
            try
            {
                dgvwintic.Stop();
                tmex.Stop();
            }
            catch (Exception exct)
            { }
            this.BringToFront();
        }

        private void clse_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private int up_down = 0;
        private void dvgpndoc_Click(object sender, EventArgs e)
        {
            if (up_down == 0)
            {
                this.Dock = DockStyle.Fill;
                up_down = 1;
                dvgpndoc.ToolTipText = "Window";
            }
            else if (up_down == 1)
            {
                this.Dock = DockStyle.None;
                up_down = 0;
                dvgpndoc.ToolTipText = "Maximize";
            }
        }

        private void nb_Click(object sender, EventArgs e)
        {
            wizard_newconn_bkk_cust cst = new wizard_newconn_bkk_cust();
            cst.Show();
        }

        private DataSet dbtDataSet = new DataSet();
        private string loc, str3, connxt;
        private void cbx_conn_custm_TextChanged(object sender, EventArgs e)
        {
            choosebook.Items.Clear();
            try
            {
                dbtDataSet.Clear();
                if (cbx_conn_custm.Text.ToLower() == "internal data-base")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.UCBConnectionString );
                    SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES", conn);
                    dataAdapter.Fill(dbtDataSet);
                    dgv.DataSource = dbtDataSet.Tables[0];
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        try
                        {
                            choosebook.Items.Add(dgvr.Cells[2].Value.ToString());
                        }
                        catch (Exception erty34) { }
                    }
                    conn.Close();
                    dataAdapter.Dispose();
                    conn.Dispose();
                    dgv.DataSource = null;
                }
                else
                {
                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_2ConnectionString);
                    SqlCeCommand comm = conn2.CreateCommand();
                    comm.CommandText = "SELECT * FROM [DB_PATH_LOCAL]";
                    conn2.Open();
                    dr = comm.ExecuteReader();
                    dt.Load(dr, LoadOption.PreserveChanges);
                    dgv.DataSource = dt;
                    foreach (DataGridViewRow drw in dgv.Rows)
                    {
                        str = (string)dgv[0, drw.Index].Value;
                        str2 = (string)dgv[1, drw.Index].Value;
                        try
                        {
                            str3 = (string)dgv[2, drw.Index].Value;
                        }
                        catch (Exception erty) { }
                        if (str == cbx_conn_custm.Text)
                        {
                            loc = str2;
                            /*if (str3 != "")
                            {
                                loc = loc + " , Password= '" + str3 +"'";
                            }*/
                            break;
                        }
                        else if (str.ToLower() == "not available" && str2 == cbx_conn_custm.Text)
                        {
                            loc = str2;
                            /*if (str3 != "")
                            {
                                loc = loc + " , Password= '" + str3 + "'";
                            }*/
                            break;
                        }
                    }
                    conn2.Close();
                    dgv.DataSource = null;

                    //-----------------------------------------------------------------------------------------------------------------------

                    connxt = "DataSource='" + loc + "'";
                    if (checkBox1.Checked == true)
                    {
                    }
                    else
                    {
                        connxt = connxt + "; Password='" + textBox1.Text + "'";
                    }
                    try
                    {
                        SqlCeConnection conn = new SqlCeConnection(connxt);
                        SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES", conn);
                        dataAdapter.Fill(dbtDataSet);
                        dgv.DataSource = dbtDataSet.Tables[0];
                        foreach (DataGridViewRow dgvr in dgv.Rows)
                        {
                            try
                            {
                                choosebook.Items.Add(dgvr.Cells[2].Value.ToString());
                            }
                            catch (Exception erty34) { }
                        }
                        conn.Close();
                        dataAdapter.Dispose();
                        conn.Dispose();
                        dgv.DataSource = null;
                        textBox1.BackColor = Color.Silver;
                    }
                    catch (Exception erty) { textBox1.BackColor = Color.Orange; }
                }
            }
            catch (Exception erty) { }
        }

        private DataTable dt = new DataTable();
        private SqlCeDataReader dr;
        string str, str2;
        private void cbx_conn_custm_DropDown(object sender, EventArgs e)
        {
            cbx_conn_custm.Items.Clear();
            choosebook.Items.Clear();
            dt.Rows.Clear();
            cbx_conn_custm.Items.Add("Internal Data-Base");

            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_2ConnectionString);
            SqlCeCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM [DB_PATH_LOCAL]";
            conn.Open();
            dr = comm.ExecuteReader();
            dt.Load(dr, LoadOption.PreserveChanges);
            dgv.DataSource = dt;
            foreach (DataGridViewRow drw in dgv.Rows)
            {
                str = (string)dgv[0, drw.Index].Value;
                str2 = (string)dgv[1, drw.Index].Value;
                if (/*dgv[0, drw.Index].Value.ToString()*/str == "NOT AVAILABLE" || dgv[0, drw.Index].Value == DBNull.Value)
                {
                    cbx_conn_custm.Items.Add(/*drw.Cells[1].Value.ToString()*/str2);
                }
                else if (/*dgv[0, drw.Index].Value != "NOT AVAILABLE"*/str != "NOT AVAILABLE" || dgv[0, drw.Index].Value != DBNull.Value)
                {
                    try
                    {
                        cbx_conn_custm.Items.Add(str);
                    }
                    catch (Exception erty) { }
                }
            }
            dt.Clear();
            dr.Dispose();
            conn.Close();
            comm.Dispose();
            dgv.DataSource = null;
        }

        private void cnn_Click(object sender, EventArgs e)
        {
            if (cbx_conn_custm.Text != "Internal Data-Base")
            {
                dt.Clear();
                refresh_dgv();
                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_2ConnectionString);
                SqlCeCommand comm = conn2.CreateCommand();
                comm.CommandText = "SELECT * FROM [DB_PATH_LOCAL]";
                conn2.Open();
                dr = comm.ExecuteReader();
                dt.Load(dr, LoadOption.PreserveChanges);
                dgv.DataSource = dt;
                str = new string(' ', 1);
                str2 = new string(' ', 1);
                string str3 = new string(' ', 1);
                foreach (DataGridViewRow drw in dgv.Rows)
                {
                    str = (string)dgv[0, drw.Index].Value;
                    str2 = (string)dgv[1, drw.Index].Value;
                    if (str == cbx_conn_custm.Text)
                    {
                        loc = str2;
                        break;
                    }
                    else if (str.ToLower() == "not available")
                    {
                        if (str2 == cbx_conn_custm.Text)
                        {
                            loc = str2;
                            break;
                        }
                    }
                    else { loc = cbx_conn_custm.Text; }
                }
                conn2.Close();
                dt.Clear();
                dgv.DataSource = null;
                startup(loc, choosebook.Text);
            }
            else if (cbx_conn_custm.Text == "Internal Data-Base")
            { startup("Local", choosebook.Text); }
        }

        private DataTable dt2 = new DataTable();
        private DataSet dst2 = new DataSet();
        private SqlCeDataAdapter da = new SqlCeDataAdapter();
        private string tblstr;
        private void startup(string loc, string tabl)
        {
            dgv.Select();
            tblstr = choosebook.Text;
            if (loc != "Local")
            {
                connxt = "DataSource='" + loc + "'";
                if (checkBox1.Checked == true)
                {
                }
                else
                {
                    connxt = connxt + "; Password='" + textBox1.Text + "'";
                }
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();

                try
                {
                conny.Open();
                commy.CommandText = "SELECT * FROM [" + tabl + "] WHERE [Serial Number] >= 1 AND [Serial Number] <= 100";
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2);
                dgv.DataSource = dt2;
                conny.Close();
                commy.Dispose();
                }
                catch (Exception erty) { }
                panel2.Visible = false;
            }
            else if(loc == "Local")
            {
                SqlCeDataReader dr2;
                dst2.Clear();
                connxt = Properties.Settings.Default.UCBConnectionString;
                SqlCeConnection conny = new SqlCeConnection(Properties.Settings.Default.UCBConnectionString);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                conny.Open();
                commy.CommandText = "SELECT * FROM [" + tabl + "] WHERE [Serial Number] >= 1 AND [Serial Number] <= 100";
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2, LoadOption.PreserveChanges);
                dgv.DataSource = dt2;
                conny.Close();
                commy.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Opening the Current Book."); }
                panel2.Visible = false;
            }
            try
            {
                dgv.Columns[0].ReadOnly = true;
            }
            catch (Exception erty) { }
            numoff();
        }

        private void numoff()
        {
            try
            {
                SqlCeConnection mySqlConnection = new SqlCeConnection(connxt);
                SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();

                mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM [" + choosebook.Text + "]";
                mySqlConnection.Open();

                howmany = (int)mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();

                //max
                SqlCeConnection mySqlConnection3 = new SqlCeConnection(connxt);
                SqlCeCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM [" + choosebook.Text + "]";
                mySqlConnection3.Open();

                maxm = (int)mySqlCommand3.ExecuteScalar();
                mySqlConnection3.Close();

                numbrw.Text = howmany.ToString();
                try
                {
                    huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
                }
                catch (Exception erty) { huntme.Text = smallest.ToString() + "N.A. -" + biggest.ToString() + " N.A."; }
            }
            catch (Exception erty) { }
        }

        private void sve_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)dgv.DataSource;
            DataTable table2 = new DataTable();

            using (var con = new SqlCeConnection(connxt))
            using (var adapter = new SqlCeDataAdapter("SELECT * FROM [" + choosebook.Text + "]", con))
            using (new SqlCeCommandBuilder(adapter))
            {
                adapter.Fill(table2);
                con.Open();
                adapter.Update(table);
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Am_err ner = new Am_err();
            ner.tx(e.Exception.ToString());
        }

        private int biggest = 100;
        private int smallest = 1;
        private void nxtst_Click(object sender, EventArgs e)
        {
            smallest = biggest;
            biggest = biggest + 100;

            SqlCeDataReader dr2;
            dt2.Clear(); dt2.Columns.Clear();
            SqlCeConnection conny = new SqlCeConnection(connxt);
            SqlCeCommand commy = conny.CreateCommand();
            try
            {
                conny.Open();
                commy.CommandText = "SELECT * FROM [" + choosebook.Text + "] WHERE [Serial Number] >= " + smallest.ToString() + " AND [Serial Number] <= " + biggest.ToString();
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2, LoadOption.PreserveChanges);
                dgv.DataSource = null;
                dgv.Rows.Clear();
                dgv.DataSource = dt2;
                dgv.DataSource = dt2;
                conny.Close();
                commy.Dispose();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Opening the Current Book."); }
            try
            {
                huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
            }
            catch (Exception erty) { huntme.Text = "N.A. -" + " N.A."; }
        }

        private void prev_Click(object sender, EventArgs e)
        {
            try
            {
                biggest = (int)dgv.Rows[0].Cells[0].Value;
                smallest = biggest - 100;
            }
            catch (Exception erty) { biggest = biggest - 100; smallest = biggest - 100; }

            SqlCeDataReader dr2;
            dt2.Clear(); dt2.Columns.Clear();
            SqlCeConnection conny = new SqlCeConnection(connxt);
            SqlCeCommand commy = conny.CreateCommand();
            try
            {
                conny.Open();
                commy.CommandText = "SELECT * FROM [" + choosebook.Text + "] WHERE [Serial Number] >= " + smallest.ToString() + " AND [Serial Number] <= " + biggest.ToString();
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2, LoadOption.PreserveChanges);
                refresh_dgv();
                conny.Close();
                commy.Dispose();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Opening the Current Book."); }
            try
            {
                huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
            }
            catch (Exception erty) { huntme.Text = "N.A. -" + " N.A."; }
        }

        private void cnt_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void cpy_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
            }
            catch (Exception erty) { }
        }

        private void ctat_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
                dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = DBNull.Value;
            }
            catch (Exception erty) { }
        }

        private void pst_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
            }
            catch (Exception erty) { Am_err mer = new Am_err(); mer.tx(erty.Message); } 
        }

        private void delete_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Text = dgv.SelectedRows.Count.ToString();
                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                        else
                        {
                            dgv.Rows.Remove(dgvr);
                        }
                    }
                }
            }
            catch (Exception erty) { }
            refresh_dgv();
        }

        private void new_rw_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.SelectedCells.Count > 0)
                {
                    foreach (DataGridViewCell dgvc in dgv.SelectedCells)
                    {
                        if (Convert.ToInt32(dgvc.OwningRow.Cells[0].Value) == 1)
                        { }
                        else
                        {
                            try
                            {
                                dgvc.Value = DBNull.Value;
                            }
                            catch (Exception erty) { }
                        }
                    }
                }
            }
            catch (Exception erty) { }
            refresh_dgv();
        }

        private void refresh_dgv()
        {
            dgv.Refresh();
        }

        private void selall_Click(object sender, EventArgs e)
        {
            dgv.SelectAll();
        }

        private void del_all_Click(object sender, EventArgs e)
        {
            SqlCeDataReader dr2;
            SqlCeConnection conny = new SqlCeConnection(connxt);
            SqlCeCommand commy = conny.CreateCommand();
            conny.Open();
            commy.CommandText = "DELETE FROM [" + dt2 + "]";
            dr2 = commy.ExecuteReader();
            dt2.Load(dr2, LoadOption.PreserveChanges);
            refresh_dgv();
            conny.Close();
            commy.Dispose();
        }

        private Color cl_tmp;
        private void dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                try
                {
                    cl_tmp = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                }
                catch (Exception erty) { }
            }
            else { }
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                try
                {
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                }
                catch (Exception erty) { }
            }
            else { }
        }

        private void und_Click(object sender, EventArgs e)
        {
            try
            {
                int x, y;
                x = Convert.ToInt32(aundC[aundC.Count - 1]);
                y = Convert.ToInt32(aundR[aundR.Count - 1]);
                dgv[x, y].Value = aund[aund.Count - 1];

                //remove from undo
                aund.RemoveAt(aund.Count - 1);
                aundC.RemoveAt(aundC.Count - 1);
                aundR.RemoveAt(aundR.Count - 1);
            }
            catch (Exception erty)
            {
                try
                {
                    aund.RemoveAt(aund.Count - 1);
                    aundC.RemoveAt(aundC.Count - 1);
                    aundR.RemoveAt(aundR.Count - 1);
                }
                catch (Exception ett) { }
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            aund.Add(dgv[e.ColumnIndex, e.RowIndex].Value);
            aundC.Add(e.ColumnIndex);
            aundR.Add(e.RowIndex);


            if (dgv.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || dgv.Rows[e.RowIndex].Cells[0].Value == null)
            {
                maxm = maxm + 1;
                dgv.Rows[e.RowIndex].Cells[0].Value = maxm;
                howmany = howmany + 1;
            }
            else
            {

            }
        }

        private void shw_here_Click(object sender, EventArgs e)
        {
            SqlCeDataReader dr2;
            dt2.Clear(); dt2.Columns.Clear();
            SqlCeConnection conny = new SqlCeConnection(connxt);
            SqlCeCommand commy = conny.CreateCommand();
            try
            {
                conny.Open();
                commy.CommandText = "Select * From [" + choosebook.Text + "] Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'"; 
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2, LoadOption.PreserveChanges);
                refresh_dgv();
                conny.Close();
                commy.Dispose();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Opening the Current Book."); }
        }

        private void showAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SqlCeDataReader dr2;
            dt2.Clear(); dt2.Columns.Clear();
            SqlCeConnection conny = new SqlCeConnection(connxt);
            SqlCeCommand commy = conny.CreateCommand();
            try
            {
                conny.Open();
                commy.CommandText = "Select * From [" + choosebook.Text + "]";
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2, LoadOption.PreserveChanges);
                refresh_dgv();
                conny.Close();
                commy.Dispose();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Opening the Current Book."); }
        }

        private void dgvupall_Click(object sender, EventArgs e)
        {
            if (sender.Equals(dgvupall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[0].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty) { }
            }
            else if (sender.Equals(dgvupone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex - 1].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty2) { }
            }
            else if (sender.Equals(dgvleftall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0];
                }
                catch (Exception erty3) { }
            }
            else if (sender.Equals(dgvleftone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex - 1];
                }
                catch (Exception erty4) { }
            }
            else if (sender.Equals(dgvrightone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex + 1];
                }
                catch (Exception erty5) { }
            }
            else if (sender.Equals(dgvrightall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.ColumnCount - 1];
                }
                catch (Exception erty6) { }
            }
            else if (sender.Equals(dgvdownone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index + 1].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty7) { }
            }
            else if (sender.Equals(dgvdownall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.RowCount - 1].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty8) { }
            }
        }

        private string col_string;
        private void Queries(object sender, EventArgs e)
        {
            dt2.Clear(); dt2.Columns.Clear();
            if (sender.Equals(Eqto) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] = '" + toolStripComboBox59.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(ntEqto) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] != '" + toolStripComboBox60.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(btw) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] > '" + toolStripTextBox127.Text + "' AND [" + col_string + "] < '" + toolStripTextBox128.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(gt) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] > '" + toolStripComboBox61.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(lt) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] < '" + toolStripComboBox62.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(lt) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] < '" + toolStripComboBox62.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(stw) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] LIKE '" + toolStripTextBox129.Text + "%'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(enw) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] LIKE '%" + toolStripTextBox130.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(fpw) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select * From [" + tblstr + "] WHERE [" + col_string + "] LIKE '%" + toolStripTextBox131.Text + "%'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
            if (sender.Equals(summedUpToolStripMenuItem) == true)
            {
                SqlCeDataReader dr2;
                dt2.Clear();
                refresh_dgv();
                dt.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select sum([" + col_string + "]) AS [Sum of " + col_string + "] From [" + tblstr + "]";// WHERE [" + col_string + "] = '" + toolStripComboBox59.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                    dgv.DataSource = dt2;
                }
                catch (Exception erty) { }
            }
            if (sender.Equals(averagedToolStripMenuItem) == true)
            {
                SqlCeDataReader dr2;
                dt.Clear();
                refresh_dgv();
                dt2.Clear();
                SqlCeConnection conny = new SqlCeConnection(connxt);
                SqlCeCommand commy = conny.CreateCommand();
                try
                {
                    conny.Open();
                    commy.CommandText = "Select avg([" + col_string + "]) AS [Average of " + col_string + "] From [" + tblstr + "]";// WHERE [" + col_string + "] = '" + toolStripComboBox59.Text + "'";
                    dr2 = commy.ExecuteReader();
                    dt2.Load(dr2, LoadOption.PreserveChanges);
                    refresh_dgv();
                    conny.Close();
                    commy.Dispose();
                    conny.Dispose();
                    dr2.Dispose();
                    dgv.DataSource = dt2;
                }
                catch (Exception erty) { }
            }
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            col_string = dgv.CurrentCell.OwningColumn.HeaderText;
        }

        //movto Win 1       

        private void movtolef_clc(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Dock = DockStyle.Left;
        }

        private void movtor(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Dock = DockStyle.Right;
        }

        private void freesty_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Dock = DockStyle.None;
        }

        private void tobott_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Dock = DockStyle.Bottom;
        }

        private void totop_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Dock = DockStyle.Top;
        }

        private void reszz_ButtonClick(object sender, EventArgs e)
        {
            this.BringToFront();
            dvgpndoc.ToolTipText = "Maximize";
            tmex.Start();
        }

        //simmetrize Win1

        private void simjrnwth_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Size = new Size(this.Size.Width, this.Size.Width);
        }

        private void simjrnhgt_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Size = new Size(this.Size.Height, this.Size.Height);
        }

        private void tmex_Tick(object sender, EventArgs e)
        {
            this.Dock = DockStyle.None;
            this.Size = new Size(Cursor.Position.X - this.Location.X, (Cursor.Position.Y - this.Location.Y) - 20);
        }

        //upd
        private ToolStripTextBox tstb_byrw;
        private void uptxtdgv_TextChanged(object sender, EventArgs e)
        {
            tstb_byrw = (ToolStripTextBox)sender;
            SqlCeDataReader dr2;
            dt2.Clear();
            SqlCeConnection conny = new SqlCeConnection(connxt);
            SqlCeCommand commy = conny.CreateCommand();
            try
            {
                conny.Open();
                commy.CommandText = "Select * From [" + tblstr + "] WHERE [Serial Number] = '" + tstb_byrw.Text + "'";
                dr2 = commy.ExecuteReader();
                dt2.Load(dr2, LoadOption.PreserveChanges);
                refresh_dgv();
                conny.Close();
                commy.Dispose();
                conny.Dispose();
                dr2.Dispose();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.Show();
        }

        private void printDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintDataGrid.PrintDGV.Print_DataGridView(dgv);
        }
    }
}
