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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class logs_pick_prods : Form
    {
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public logs_pick_prods()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.installericon;
            this.Disposed += new EventHandler(logs_pick_prods_Disposed);
            InitializeComponent();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
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

        void logs_pick_prods_Disposed(object sender, EventArgs e)
        {
            logs_prod2.Clear();
            logs_prodTableAdapter.Connection.Close();

            logs_prod2.Dispose();
            logsprodBindingSource.Dispose();
            logs_prodTableAdapter.Dispose();

            dt_temp.Clear();
            dt_temp2.Clear();
            dt_temp.Dispose();
            dt_temp2.Dispose();

            this.Disposed -= logs_pick_prods_Disposed;
            this.dataGridView1.RowEnter -= this.dgv1_re;
            this.dataGridView2.RowEnter -= this.cel_entr;
            this.button1.Click -= this.button1_Click;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.Deactivate -= this.logs_pick_prods_Deactivate;
            this.Load -= this.logs_pick_prods_Load;
            this.Activated -= this.logs_pick_prods_Activated;


            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private DataSet dt_temp = new DataSet();
        private mgmt_opr opr_temp; string ID = "";
        public void tx(mgmt_opr opr, string log_ID)
        {
            ID = log_ID;
            opr_temp = opr;
            this.Show();
            try
            {
                dt_temp.Clear();
                string SqlString = "Select * From Prod_mgmt";
                if (Main.Amatrix.mgt == "")
                {
                    using (SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            dt_temp.Load(reader, LoadOption.PreserveChanges, "Prod_mgmt");
                            dataGridView1.DataSource = dt_temp.Tables[0];
                            conn.Close();
                        }
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(Main.Amatrix.mgt))
                    {
                        using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            dt_temp.Load(reader, LoadOption.PreserveChanges, "Prod_mgmt");
                            dataGridView1.DataSource = dt_temp.Tables[0];
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception erty) {  }
        }

        private DataSet dt_temp2 = new DataSet();
        private void load_bulk(DataGridViewCellEventArgs e)
        {
            try
            {
                dt_temp2.Tables.Clear();
                dt_temp2.Clear();
                string SqlString = "Select * From prod_bulk WHERE [Notes/Information] = '" + dataGridView1[1, e.RowIndex].Value.ToString() + "' AND ([Logistical Batch] = '' OR [Logistical Batch] IS NULL)";
                if (Main.Amatrix.mgt == "")
                {
                    using (SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            dt_temp2.Load(reader, LoadOption.PreserveChanges, "prod_bulk");
                            dataGridView2.DataSource = dt_temp2.Tables[0];
                            conn.Close();
                        }
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(Main.Amatrix.mgt))
                    {
                        using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            dt_temp2.Load(reader, LoadOption.PreserveChanges, "prod_bulk");
                            dataGridView2.DataSource = dt_temp2.Tables[0];
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void logs_pick_prods_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logs_prod2.Logs_prod' table. You can move, or remove it, as needed.
            if (Main.Amatrix.mgt == "")
            {
                this.logs_prodTableAdapter.Fill(this.logs_prod2.Logs_prod);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Main.Amatrix.mgt))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Logs_prod", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        logs_prod2.Load(reader, LoadOption.PreserveChanges, "Logs_prod");
                        dataGridView3.DataSource = logs_prod2.Logs_prod;
                        conn.Close();
                    }
                }
            }
        }

        private void setprods()
        {
            //max
            DataTable dtp = new DataTable();
            int f = 0;
            string SqlString = "Select max([Product Serial Number]) From Logs_prod";
            if (Main.Amatrix.mgt == "")
            {
                using (SqlCeConnection conn = new SqlCeConnection(logs_prodTableAdapter.Connection.ConnectionString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            dtp.Load(reader);
                        }
                        conn.Close();
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Main.Amatrix.mgt))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            dtp.Load(reader);
                        }
                        conn.Close();
                    }
                }
            }
            try
            {
                f = Convert.ToInt32(dtp.Rows[0].ItemArray[0].ToString());
            }
            catch (Exception erty) { f = 0; }

            DataRow dgvrow;
            DataTable dtp_sve = new DataTable();
            foreach (DataGridViewRow dgvr in dataGridView2.SelectedRows)
            {
                try
                {
                    f = f + 1;
                    dgvrow = logs_prod2.Logs_prod.NewRow();
                    dgvrow[0] = f;
                    dgvrow[1] = dgvr.Cells[1].Value.ToString();
                    dgvrow[2] = ID;
                    dgvrow[3] = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    dgvrow[4] = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                    logs_prod2.Logs_prod.Rows.Add(dgvrow);
                    dataGridView3.DataSource = logs_prod2;
                    if (Main.Amatrix.mgt == "")
                    {
                        logs_prodTableAdapter.Update(logs_prod2);
                    }
                    else
                    {
                        asql.Save(logs_prod2.Logs_prod, "Logs_prod", Main.Amatrix.mgt);
                    }
                }
                catch (Exception erty) { break; Am_err ner = new Am_err(); ner.tx("You May not Choose a Mainstream Product Please Pick a Bulk Item or create one For Your Inventory in the Inventory Information tab within Product Managment."); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2[0, 0].Value != DBNull.Value && dataGridView2[0, dataGridView2.CurrentRow.Index].Value != DBNull.Value)
                {
                    setprods();
                    foreach (DataGridViewRow dgvr in dataGridView2.SelectedRows)
                    {
                        try
                        {
                            dgvr.Cells[3].Value = "Shipping (Logistics)";
                            dgvr.Cells[4].Value = ID;
                        }
                        catch (Exception erty) { }
                    }
                    DataTable table = new DataTable();
                    try
                    {
                        dataGridView2.CurrentCell = dataGridView2[0, dataGridView2.CurrentRow.Index + 1];
                    }
                    catch (Exception erty)
                    {
                        dataGridView2.CurrentCell = dataGridView2[0, dataGridView2.CurrentRow.Index - 1];
                    }

                    table = (DataTable)dataGridView2.DataSource;
                    DataTable table2 = new DataTable();
                    if (Main.Amatrix.mgt == "")
                    {
                        using (var con = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString))
                        using (var adapter = new SqlCeDataAdapter("SELECT * FROM prod_bulk", con))
                        using (new SqlCeCommandBuilder(adapter))
                        {
                            adapter.Fill(table2);
                            con.Open();
                            adapter.Update(table);
                        }
                    }
                    else
                    {
                        asql.Save(table, "prod_bulk", Main.Amatrix.mgt);
                    }
                    opr_temp.glue(dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString());
                    abtclse.Start();
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("The Selected Row is Not Valid. Operation Aborted."); }
        }

        private void dgv1_re(object sender, DataGridViewCellEventArgs e)
        {
            load_bulk(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setprods();
        }

        private void cel_entr(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows[e.RowIndex].Selected = true;
        }

        private void dectmeabt_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeabt.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void logs_pick_prods_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void logs_pick_prods_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
