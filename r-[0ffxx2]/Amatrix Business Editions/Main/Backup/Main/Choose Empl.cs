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
using System.IO;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Choose_Empl : Form
    {
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public Choose_Empl()
        {
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(Choose_Empl_Disposed);
            Init();
        }

        void Choose_Empl_Disposed(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dgv.DataSource = null;

            bSAMDataSet.Clear();
            bSAMDataSet.Dispose();
            dt.Clear();
            dt.Dispose();

            signsTableAdapter.Connection.Close();
            signsTableAdapter.Dispose();
            signsBindingSource.Dispose();

            this.Disposed -= Choose_Empl_Disposed;
            this.button1.Click -= this.button1_Click;
            this.button2.Click -= this.button2_Click;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.button3.Click -= this.button3_Click;
            this.Deactivate -= this.Choose_Empl_Deactivate;
            this.Load -= this.Choose_Empl_Load;
            this.Activated -= this.Choose_Empl_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
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

        DataTable dt = new DataTable();
        private void Init()
        {
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT [Employee First Name], [Employee Last Name] FROM Employ_payrll", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            else
            {
                dt = basql.Execute(Main.Amatrix.mgt, "SELECT [Employee First Name], [Employee Last Name] FROM Employ_payrll", "Employ_payrll", dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void Choose_Empl_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bSAMDataSet.Signs' table. You can move, or remove it, as needed.
            this.signsTableAdapter.Fill(this.bSAMDataSet.Signs);
        }

        DataTable tb = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
                foreach (DataGridViewRow dgvr in dataGridView1.SelectedRows)
                {
                    DataRow row;
                    row = bSAMDataSet.Signs.NewRow();
                    row[0] = dgvr.Cells[0].Value;
                    row[1] = dgvr.Cells[1].Value;
                    row[2] = DateTime.Now;
                    row[4] = 0;
                    row[5] = "";
                    row[6] = DateTime.Now.Date.ToString() + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString();
                    tb = bSAMDataSet.Signs;
                    try
                    {
                        tb.Rows.Add(row);
                    }
                    catch (Exception erty)
                    {
                        row[6] = DateTime.Now.Date.ToString() + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + (DateTime.Now.Millisecond + 1).ToString();
                        tb.Rows.Add(row);
                    }
                    dgv.DataSource = tb;
                }
                //signsTableAdapter.Update(bSAMDataSet);
                //Save
                dgv.CurrentCell = null;
                DataTable table = new DataTable();
                table = (DataTable)dgv.DataSource;
                DataTable table2 = new DataTable();

                if (Main.Amatrix.mgt == "")
                {
                    using (var con = new SqlCeConnection(signsTableAdapter.Connection.ConnectionString))
                    using (var adapter = new SqlCeDataAdapter("SELECT * FROM Signs", con))
                    using (new SqlCeCommandBuilder(adapter))
                    {
                        adapter.Fill(table2);
                        con.Open();
                        adapter.Update(table);
                    }
                }
                else
                {
                    asql.Save(table, "Signs", Main.Amatrix.mgt);
                }
                this.Close();
        }

        DateTime Dtp1, Res;
        DateTime Dtp2;
        System.TimeSpan tsp = new TimeSpan();
        private void button2_Click(object sender, EventArgs e)
        {
            tb.Clear();
            foreach (DataGridViewRow dgvr in dataGridView1.SelectedRows)
            {
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(signsTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("", conn); 
                    cmd.CommandText = "SELECT * FROM Signs WHERE [Employee First Name] = '" + dgvr.Cells[0].Value + "' AND [Employee Last Name] = '" + dgvr.Cells[1].Value + "' AND DatePart(dd,[Employee Time In]) = DatePart(dd,getdate()) AND DatePart(mm,[Employee Time In]) = DatePart(mm,getdate()) AND DatePart(yy,[Employee Time In]) = DatePart(yy,getdate())";
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    tb.Load(dr);
                }
                else
                { tb = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Signs WHERE [Employee First Name] = '" + dgvr.Cells[0].Value + "' AND [Employee Last Name] = '" + dgvr.Cells[1].Value + "' AND DatePart(dd,[Employee Time In]) = DatePart(dd,getdate()) AND DatePart(mm,[Employee Time In]) = DatePart(mm,getdate()) AND DatePart(yy,[Employee Time In]) = DatePart(yy,getdate())", "Signs", tb); }
                dgv.DataSource = tb;

                foreach (DataGridViewRow dgvrr in dgv.Rows)
                {
                    if (dgvrr.Index != dgv.Rows.Count - 1)
                    {
                        if (dgvrr.Cells[3].Value == DBNull.Value)
                        {
                            dgvrr.Cells[3].Value = DateTime.Now;
                            Dtp1 = (DateTime)dgvrr.Cells[2].Value;
                            Dtp2 = (DateTime)dgvrr.Cells[3].Value;
                            tsp = (Dtp2 - Dtp1);
                            dgvrr.Cells[4].Value = tsp.Hours.ToString() + ":" + tsp.Minutes.ToString();
                        }
                    }
                }

                //Save
                dgv.CurrentCell = dgv[0, dgv.Rows.Count - 1];
                DataTable table = new DataTable();
                table = (DataTable)dgv.DataSource;
                DataTable table2 = new DataTable();

                if (Main.Amatrix.mgt == "")
                {
                    using (var con = new SqlCeConnection(signsTableAdapter.Connection.ConnectionString))
                    using (var adapter = new SqlCeDataAdapter("SELECT * FROM Signs", con))
                    using (new SqlCeCommandBuilder(adapter))
                    {
                        adapter.Fill(table2);
                        con.Open();
                        adapter.Update(table);
                    }
                }
                else
                {
                    asql.Save(table, "Signs", Main.Amatrix.mgt);
                }
            }
            this.Close();
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

        private void Choose_Empl_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Choose_Empl_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mgmt_hr hr = new mgmt_hr();
            hr.Show();
        }
    }
}
