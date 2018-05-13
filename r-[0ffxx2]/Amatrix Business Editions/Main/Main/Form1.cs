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
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        public Form1()
        {
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(Form1_Disposed);
            init_db();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        private void init_db()
        {
            try
            {
                bSAMDataSet1.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(signsTableAdapter1.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Signs", conn);// WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    bSAMDataSet1.Signs.Load(dr);
                    dataGridView1.DataSource = bSAMDataSet1.Signs;
                    conn.Close();
                }
                else
                {
                    dr_univ = Quer("SELECT * FROM Signs");
                    bSAMDataSet1.Signs.Load(dr_univ);
                    dataGridView1.DataSource = bSAMDataSet1.Signs;
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A data Error Occured while Loading The Viewing data. Operation Not Aborted, Continued in Error Mode."); }
        }

        void Form1_Disposed(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            bSAMDataSet1.Clear();

            signsTableAdapter1.Connection.Close();

            bSAMDataSet1.Dispose();

            signsTableAdapter1.Dispose();

            signsBindingSource.Dispose();

            button2.Click -= button2_Click;
            okSearchToolStripMenuItem.Click -= okSearchToolStripMenuItem_Click;
            this.save_inv.Click -= this.svebtn_Click;
            this.restr.Click -= this.rstrt_Click;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.deletecell.Click -= this.deletecell_Click;
            this.toolStripMenuItem1.Click -= this.toolStripMenuItem1_Click;
            this.sall.Click -= this.sall_Click;
            this.contentsToolStripMenuItem.Click -= this.contentsToolStripMenuItem_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.button1.Click -= this.button1_Click;
            this.toolStripButton2.Click -= this.toolStripButton2_Click;
            this.dataGridView1.DataError -= this.dataGridView1_DataError;
            this.ts2.MouseEnter -= this.ts2_MouseEnter;
            this.ts2.MouseLeave -= this.ts2_MouseLeave;
            this.pnt.Click -= this.pnt_Click;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.toolStripButton1.ButtonClick -= this.toolStripButton1_Click;
            this.svebtn.Click -= this.svebtn_Click;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.Deactivate -= this.Form1_Deactivate;
            this.Load -= this.Form1_Load;
            this.Activated -= this.Form1_Activated;

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

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Choose_Empl empl = new Choose_Empl();
            empl.Show();
            empl.Disposed += new EventHandler(empl_Disposed);
        }

        void empl_Disposed(object sender, EventArgs e)
        {
            bSAMDataSet1.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(signsTableAdapter1.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Signs", conn);// WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                bSAMDataSet1.Signs.Load(dr);
                dataGridView1.DataSource = bSAMDataSet1.Signs;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM Signs");
                bSAMDataSet1.Signs.Load(dr_univ);
                dataGridView1.DataSource = bSAMDataSet1.Signs;
            }
        }

        private void svebtn_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                try
                {
                    signsTableAdapter1.Update(bSAMDataSet1);
                }
                catch (Exception erty)
                {
                    //Save
                    dataGridView1.CurrentCell = null;
                    DataTable table = new DataTable();
                    table = bSAMDataSet1.Signs;
                    DataTable table2 = new DataTable();

                    using (var con = new SqlCeConnection(signsTableAdapter1.Connection.ConnectionString))
                    using (var adapter = new SqlCeDataAdapter("SELECT * FROM Signs", con))
                    using (new SqlCeCommandBuilder(adapter))
                    {
                        adapter.Fill(table2);
                        con.Open();
                        adapter.Update(table);
                    }
                }
            }
            else
            {
                try
                {
                    asql.Save(bSAMDataSet1.Signs, "Signs", Main.Amatrix.mgt);
                }
                catch (Exception erty)
                {
                    //Save
                    dataGridView1.CurrentCell = null;
                    DataTable table = new DataTable();
                    table = bSAMDataSet1.Signs;
                    DataTable table2 = new DataTable();

                    using (var con = new SqlConnection(Main.Amatrix.mgt))
                    using (var adapter = new SqlDataAdapter("SELECT * FROM Signs", con))
                    using (new SqlCommandBuilder(adapter))
                    {
                        adapter.Fill(table2);
                        con.Open();
                        adapter.Update(table);
                    }
                }
            }
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridView1.SelectedRows)
            {
                try
                {
                    dataGridView1.Rows.Remove(dgvr);
                }
                catch (Exception erty) { }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string ConnString = signsTableAdapter1.Connection.ConnectionString;
            string SqlString = "DELETE FROM " + bSAMDataSet1.Signs.TableName;
            if (Main.Amatrix.mgt == "")
            {
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            bSAMDataSet1.Signs.Load(reader);
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer("DELETE FROM Signs");
                bSAMDataSet1.Signs.Load(dr_univ);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bSAMDataSet1.Signs;
            bSAMDataSet1.Signs.Rows.Clear();
            bSAMDataSet1.Signs.AcceptChanges();
        }

        private void sall_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void clse_ButtonClick(object sender, EventArgs e)
        {
            abtclse.Start();
        }

        private void rstrt_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            f1.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            bSAMDataSet1.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(signsTableAdapter1.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Signs WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                bSAMDataSet1.Signs.Load(dr);
                dataGridView1.DataSource = bSAMDataSet1.Signs;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM Signs WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%'");
                bSAMDataSet1.Signs.Load(dr_univ);
            }
        }

        private void pnt_Click(object sender, EventArgs e)
        {
            PrintDataGrid.PrintDGV.Print_DataGridView(dataGridView1);
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void tbxfned_Enter(object sender, EventArgs e)
        {
            tbxfned.ForeColor = Color.DimGray;
        }

        private void tbxfned_Leave(object sender, EventArgs e)
        {
            tbxfned.ForeColor = Color.LightGray;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("An Error Occured, You may Have Entered an Invalid Value Within a Specified Cell. Press CTRL+Z to Undo or, Enter a Valid Value For the Specified Column.", "Business Studio Error Reporter", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ts2_MouseEnter(object sender, EventArgs e)
        {
            ts2.BackColor = Color.AliceBlue;
        }

        private void ts2_MouseLeave(object sender, EventArgs e)
        {
            ts2.BackColor = Color.Lavender;
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt abt = new app_abt();
            abt.descr(this.Text);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            bSAMDataSet1.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(signsTableAdapter1.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Signs", conn);// WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                bSAMDataSet1.Signs.Load(dr);
                dataGridView1.DataSource = bSAMDataSet1.Signs;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM Signs");
                bSAMDataSet1.Signs.Load(dr_univ);
                dataGridView1.DataSource = bSAMDataSet1.Signs;
            }
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

        private void Form1_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void okSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bSAMDataSet1.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(signsTableAdapter1.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Signs WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' AND datepart(mm, [Employee Time In]) = '" + toolStripComboBox1.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                bSAMDataSet1.Signs.Load(dr);
                dataGridView1.DataSource = bSAMDataSet1.Signs;
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM Signs WHERE [Employee First Name] LIKE '%" + tbxfned.Text + "%' OR [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' OR [Employee First Name] + [Employee Last Name] LIKE '%" + tbxfned.Text + "%' AND datepart(mm, [Employee Time In]) = '" + toolStripComboBox1.Text + "'");
                bSAMDataSet1.Signs.Load(dr_univ);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Attndce.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Could not Find 'Attndce.exe'"); }
        }
    }
}
