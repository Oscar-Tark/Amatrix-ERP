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
using System.Data.SqlServerCe;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_supch_warranties : Form
    {
        string Main_ = ""; string Sub = ""; string by = "";
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        private DataTable dtp = new DataTable();

        public mgmt_supch_warranties()
        {
            InitializeComponent();
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

        private void init_db()
        {
            dtp.Clear();
            string SQL = "SELECT * FROM prod_warranties WHERE [For Product] = '" + Main_ + "' AND [For Bulk] = '" + Sub + "'";
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.mgt, SQL, "prod_warranties", dtp);
            }
            dgv.DataSource = dtp;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[4].ReadOnly = true;
            dgv.Columns[5].ReadOnly = true;
        }

        private void mgmt_supch_warranties_Load(object sender, EventArgs e)
        {

        }

        public void tx(string main, string sub, string post_by)
        { by = post_by; Main_ = main; Sub = sub; init_db(); this.ShowDialog(); this.Text = "Warranties(" + main.Replace(" ", "") + "/" + sub.Replace(" ", "") + ")"; }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(button1) == true)
                {
                    this.Close();
                }
                else
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        DataTable table = new DataTable();
                        table = (DataTable)dgv.DataSource;
                        DataTable table2 = new DataTable();

                        using (var con = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString))
                        using (var adapter = new SqlCeDataAdapter("SELECT * FROM prod_warranties", con))
                        using (new SqlCeCommandBuilder(adapter))
                        {
                            adapter.Fill(table2);
                            con.Open();
                            adapter.Update(table);
                        }
                    }
                    else
                    {
                        asql.Save(dtp, "prod_warranties", Main.Amatrix.mgt);
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgv[0, dgv.CurrentRow.Index].Value == DBNull.Value)
            { dgv[0, dgv.CurrentRow.Index].Value = "WARR-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString(); }
            dgv[4, dgv.CurrentRow.Index].Value = Main_;
            dgv[5, dgv.CurrentRow.Index].Value = Sub;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("DELETE FROM prod_warranties WHERE [Serial] = '" + dgv[0, dgv.CurrentRow.Index].Value.ToString() + "'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    conn.Close();
                }
                else
                {
                    DataTable dtpy = new DataTable();
                    basql.Execute(Main.Amatrix.mgt, "DELETE FROM prod_warranties WHERE [Serial] = '" + dgv[0, dgv.CurrentRow.Index].Value.ToString() + "'", "prod_warranties", dtpy);
                    dtpy.Clear(); dtpy.Dispose();
                }
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
            catch (Exception erty) { }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv[1, e.RowIndex].Value.ToString() == "Yes")
                {
                    dgv[6, e.RowIndex].Value = by;
                }
            }
            catch (Exception erty) { }
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(dgv[e.ColumnIndex, e.RowIndex].Value);
                    dgv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.WhiteSmoke;
                }
                catch (Exception erty)
                {
                    Am_err ner = new Am_err(); ner.tx("Date Value Not Valid The Syntax Must be : DD/MMM/YY As 01/Jan/91 OR DD-MMM-YY As 01-Jan-91."); dgv[e.ColumnIndex, e.RowIndex].Value = ""; dgv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.DarkOrange;
                    dgv[e.ColumnIndex, e.RowIndex].Selected = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                bkk_Custs.RunWorkerAsync();
                panel1.Controls.Remove(cbx);
            }
            catch (Exception erty) { }
        }

        private DataTable dtp_temp; ComboBox cbx1 = new ComboBox();
        private void bkk_Custs_DoWork(object sender, DoWorkEventArgs e)
        {
            cbx.Items.Clear();
            dtp_temp = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_temp.Load(dr);
            }
            else
            {
                dtp_temp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Customers", "Customers", dtp_temp);
            }

            foreach (DataRow dr in dtp_temp.Rows)
            {
                try
                {
                    cbx.Items.Add(dr.ItemArray[0].ToString() + "[" + dr.ItemArray[1].ToString() + "/" + dr.ItemArray[1].ToString() + "/" + dr.ItemArray[27].ToString() + "]");
                }
                catch (Exception erty) 
                {
                    try
                    {
                        cbx.Items.Add(dr.ItemArray[0].ToString() + "[Content Not Available]");
                    }
                    catch (Exception erty2) { }
                }
            }
        }

        private void bkk_Custs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panel1.Controls.Add(cbx);
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                panel1.Visible = true;
                try
                {
                    bkk_Custs.RunWorkerAsync();
                }
                catch (Exception erty) { }
            }
            else { panel1.Visible = false; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int ndx = cbx.Text.IndexOf("[", 0);
                int ndx2 = cbx.Text.Length - ndx;
                dgv[6, dgv.CurrentRow.Index].Value = cbx.Text.Remove(ndx, ndx2);
                panel1.Visible = false;
            }
            catch (Exception erty) { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
