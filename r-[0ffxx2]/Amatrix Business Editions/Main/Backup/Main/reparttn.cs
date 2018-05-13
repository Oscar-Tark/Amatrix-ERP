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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class reparttn : Form
    {
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public reparttn()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.installericon;
            this.Disposed += new EventHandler(reparttn_Disposed);
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(reparttn_FormClosing);
            clb.SetItemChecked(0, true);
            clb.SetItemChecked(1, true);
            clb.SetItemChecked(2, true);
            clb.SetItemChecked(3, true);
            clb.SetItemChecked(4, true);

            if (Main.Amatrix.acc != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        void reparttn_Disposed(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            dgv2.DataSource = null;
            dgv3.DataSource = null;
            dgv4.DataSource = null;
            dgv5.DataSource = null;

            dtst.Clear();
            dtst2.Clear();
            dtst3.Clear();
            dtst4.Clear();
            dtst5.Clear();

            dtst.Dispose();
            dtst2.Dispose();
            dtst3.Dispose();
            dtst4.Dispose();
            dtst5.Dispose();

            this.button1.Click -= this.button1_Click;
            this.bkk_work.DoWork -= this.bkk_work_DoWork;
            this.bkk_work.RunWorkerCompleted -= this.bkk_work_RunWorkerCompleted;
            this.bkk_work2.DoWork -= this.bkk_work2_DoWork;
            this.bkk_work2.RunWorkerCompleted -= this.bkk_work2_RunWorkerCompleted;
            this.Load -= this.reparttn_Load;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private bool workn_ = false;
        void reparttn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workn_ == true)
            {
                bkk_work.CancelAsync();
                bkk_work2.CancelAsync();
                Application.ExitThread();
                Application.Exit();
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

        private void reparttn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            workn_ = true;
            button1.Enabled = false;
            step_all();
            clb.Enabled = false;
            progressBar1.Visible = true;
        }

        private System.Collections.ArrayList al = new System.Collections.ArrayList();
        private void step_all()
        {
            foreach (String i in clb.Items)
            {
                al.Add(i);
            }
            step1();
        }

        private int max;
        private void step1()
        {
            workn_ = true;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != this.Name)
                {
                    f.Visible = false;
                    f.Enabled = false;
                }
            }
            step2();
        }

        private void step2()
        {
            bkk_work.RunWorkerAsync();
        }

        DataSet dtst = new DataSet();
        DataGridView dgv = new DataGridView();
        DataSet dtst2 = new DataSet();
        DataGridView dgv2 = new DataGridView();
        DataSet dtst3 = new DataSet();
        DataGridView dgv3 = new DataGridView();
        DataSet dtst4 = new DataSet();
        DataGridView dgv4 = new DataGridView();
        DataSet dtst5 = new DataSet();
        DataGridView dgv5 = new DataGridView();
        private void bkk_work_DoWork(object sender, DoWorkEventArgs e)
        {
            string ConnString = Properties.Settings.Default.AmdtbseConnectionString;
            if (Main.Amatrix.acc == "")
            {
                dtst.Clear();
                string SqlString = "Select * From journal ORDER BY [Serial Number] ASC";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            try
                            {
                                dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = dtst.Tables[0];
                            }
                            catch (Exception erty)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                dtst.Clear();
                DataTable dtp = new DataTable();
                dtp = basql.Execute(Main.Amatrix.acc, "Select * From journal ORDER BY [Serial Number] ASC", "journal", dtp);
                dtst.Tables.Add(dtp);
                dgv.DataSource = dtp;
            }
            if (Main.Amatrix.acc == "")
            {
                dtst2.Clear();
                string SqlString2 = "Select * From invoice ORDER BY [Serial Number] ASC";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString2, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            try
                            {
                                dtst2.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv2.DataSource = dtst2.Tables[0];
                            }
                            catch (Exception erty)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                dtst2.Clear();
                DataTable dtp = new DataTable();
                dtp = basql.Execute(Main.Amatrix.acc, "Select * From invoice ORDER BY [Serial Number] ASC", "invoice", dtp);
                dtst2.Tables.Add(dtp);
                dgv2.DataSource = dtp;
            }
            if (Main.Amatrix.acc == "")
            {
                dtst3.Clear();
                string SqlString3 = "Select * From CashBook ORDER BY [Serial Number] ASC";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString3, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            try
                            {
                                dtst3.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                dgv3.DataSource = dtst3.Tables[0];
                            }
                            catch (Exception erty)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                dtst3.Clear();
                DataTable dtp = new DataTable();
                dtp = basql.Execute(Main.Amatrix.acc, "Select * From CashBook ORDER BY [Serial Number] ASC", "CashBook", dtp);
                dtst3.Tables.Add(dtp);
                dgv3.DataSource = dtp;
            }
            if (Main.Amatrix.acc == "")
            {
                dtst4.Clear();
                string SqlString4 = "Select * From SalesBook ORDER BY [Serial Number] ASC";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString4, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            try
                            {
                                dtst4.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                dgv4.DataSource = dtst4.Tables[0];
                            }
                            catch (Exception erty)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                dtst4.Clear();
                DataTable dtp = new DataTable();
                dtp = basql.Execute(Main.Amatrix.acc, "Select * From SalesBook ORDER BY [Serial Number] ASC", "SalesBook", dtp);
                dtst4.Tables.Add(dtp);
                dgv4.DataSource = dtp;
            }
            if (Main.Amatrix.acc == "")
            {
                dtst5.Clear();
                string SqlString5 = "Select * From PurchaseBook ORDER BY [Serial Number] ASC";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString5, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            try
                            {
                                dtst5.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                dgv5.DataSource = dtst5.Tables[0];
                            }
                            catch (Exception erty)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                dtst5.Clear();
                DataTable dtp = new DataTable();
                dtp = basql.Execute(Main.Amatrix.acc, "Select * From PurchaseBook ORDER BY [Serial Number] ASC", "PurchaseBook", dtp);
                dtst5.Tables.Add(dtp);
                dgv5.DataSource = dtp;
            }
        }

        private void bkk_work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Controls.Add(dgv2);
            this.Controls.Add(dgv);
            this.Controls.Add(dgv3);
            this.Controls.Add(dgv4);
            this.Controls.Add(dgv5);
            dgv.Location = new Point(1000,1000);
            dgv2.Location = dgv.Location;
            dgv3.Location = dgv2.Location;
            dgv4.Location = dgv3.Location;
            dgv5.Location = dgv3.Location;
            bkk_work2.RunWorkerAsync();
        }

        private void bkk_work2_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                dgv[0, i].Value = i + 1;
            }
            for (int i = 0; i < dgv2.Rows.Count - 1; i++)
            {
                dgv2[0, i].Value = i + 1;
            }
            for (int i = 0; i < dgv3.Rows.Count - 1; i++)
            {
                dgv3[0, i].Value = i + 1;
            }
            for (int i = 0; i < dgv4.Rows.Count - 1; i++)
            {
                dgv4[0, i].Value = i + 1;
            }
            for (int i = 0; i < dgv5.Rows.Count - 1; i++)
            {
                dgv5[0, i].Value = i + 1;
            }
        }

        private void bkk_work2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                DataTable table = new DataTable();
                table = (DataTable)dgv.DataSource;
                DataTable table2 = new DataTable();

                using (var con = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM journal", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
                //2
                table.Clear();
                table2.Clear();
                table = (DataTable)dgv2.DataSource;

                using (var con = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM invoice", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
                //3
                table.Clear();
                table2.Clear();
                table = (DataTable)dgv3.DataSource;

                using (var con = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM CashBook", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
                //4
                table.Clear();
                table2.Clear();
                table = (DataTable)dgv4.DataSource;

                using (var con = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM SalesBook", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
                //5
                table.Clear();
                table2.Clear();
                table = (DataTable)dgv5.DataSource;

                using (var con = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM PurchaseBook", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
            }
            else
            {
                DataTable table = (DataTable)dgv.DataSource;
                DataTable table2 = (DataTable)dgv2.DataSource;
                DataTable table3 = (DataTable)dgv3.DataSource;
                DataTable table4 = (DataTable)dgv4.DataSource;
                DataTable table5 = (DataTable)dgv5.DataSource;

                asql.Save(table, "journal", Main.Amatrix.acc);
                asql.Save(table2, "invoice", Main.Amatrix.acc);
                asql.Save(table3, "CashBook", Main.Amatrix.acc);
                asql.Save(table4, "SalesBook", Main.Amatrix.acc);
                asql.Save(table5, "PurchaseBook", Main.Amatrix.acc);
            }
            workn_ = true;
            this.Close();
        }
    }
}
