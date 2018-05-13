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
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class acc_linker : Form
    {
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();

        acc_journ acjrn;
        int lnktpe; //0=invoice, 1=customer, 2=product
        string frbkk; string lnktpe_str;
        DataGridViewRow dgvr;
        public acc_linker()
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

        private void acc_linker_Load(object sender, EventArgs e)
        {

        }

        public void tx(int Link_Type, DataGridViewRow Selected_Row, string For_Book, acc_journ ac_ref)
        {
            acjrn = ac_ref;   
            lnktpe = Link_Type;
            if (Link_Type == 0)
            {
                lnktpe_str = "invoice";
            }
            dgvr = Selected_Row;
            frbkk = For_Book;
            start_bkk();
            bkk_ld2.RunWorkerAsync();
            //check defunct
            if (For_Book.ToLower() == "salesbook")
            {
                if (Selected_Row.Cells[4].Value.ToString().Contains("(defunct)"))
                {
                    MessageBox.Show("The Selected Invoice Link is Defunct, You may Replace it, or Keep it so for Public record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (Selected_Row.Cells[3].Value.ToString().Contains("(defunct)"))
                {
                    MessageBox.Show("The Selected Invoice Link is Defunct, You may Replace it, or Keep it so for Public record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void start_bkk()
        {
            groupBox1.Controls.Remove(cbx);
            cbx.Items.Clear();
            bkk.RunWorkerAsync();
        }

        private DataTable dtp = new DataTable();
        private void load()
        {
            dtp.Clear(); dtp.Columns.Clear();
            dtp.Constraints.Clear();
            if (lnktpe == 0)
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM invoice WHERE [Binded to Journal Entry] IS NULL AND [Binded to Journal] IS NULL", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    dtp.Load(dr);
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM invoice WHERE [Binded to Journal Entry] IS NULL AND [Binded to Journal] IS NULL", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dtp.Load(dr);
                    conn.Close();
                }
            }
        }

        private void load_tocbx()
        {
            foreach (DataRow dr in dtp.Rows)
            {
                try
                {
                    cbx.Items.Add(dr.ItemArray[1].ToString());
                }
                catch (Exception erty) { }
            }
        }

        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            load();
            load_tocbx();
        }

        private void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            groupBox1.Controls.Add(cbx);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            start_bkk();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok();
        }

        private void ok()
        {
            if (old == false)
            {
                try
                {
                    loaddb();
                    if (lnktpe == 0 && dgv.RowCount == 0)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO acc_linking VALUES('" + "KEY-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "','" + frbkk + "','" + lnktpe_str + "','" + dgvr.Cells[0].Value.ToString() + "','" + cbx.SelectedItem.ToString() + "')", conn);
                            conn.Open();
                            cmd.ExecuteScalar();
                            conn.Close();

                            SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal Entry] = '" + cbx.Text + "', [Binded to Journal] = '" + frbkk + "' WHERE [Invoice Reference Number (ID)] = '" + cbx.Text + "'", conn2);
                            conn2.Open();
                            cmd2.ExecuteScalar();
                            conn2.Close();
                        }
                        else
                        {
                            basql.Execute(Main.Amatrix.acc, "INSERT INTO acc_linking VALUES('" + "KEY-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "','" + frbkk + "','" + lnktpe_str + "','" + dgvr.Cells[0].Value.ToString() + "','" + cbx.SelectedItem.ToString() + "')", "acc_linking", dtp2);
                            basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal Entry] = '" + cbx.Text + "', [Binded to Journal] = '" + frbkk + "' WHERE [Invoice Reference Number (ID)] = '" + cbx.Text + "'", "invoice", dtp2);
                        }
                        loaddb();
                        if (lnktpe == 0)
                        {
                            if (frbkk == "salesbook")
                            {
                                acjrn.set_linked(0, "salesbook", dgv[4, 0].Value.ToString());
                            }
                            else if (frbkk == "purchasebook")
                            {
                                acjrn.set_linked(0, "purchasebook", dgv[4, 0].Value.ToString());
                            }
                            this.Close();
                        }
                    }
                    else
                    { Am_err ner = new Am_err(); ner.tx("Cannot Enter More than One Invoice Entry per Journal Entry."); }
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Amatrix was Unable to add the Specified Key"); }
            }
            else { this.Close(); }
        }

        private DataTable dtp2 = new DataTable();
        private void bkk_ld2_DoWork(object sender, DoWorkEventArgs e)
        {
            loaddb();
        }

        bool old = false;
        private void loaddb()
        {
            dtp2.Clear();
            dtp2.Columns.Clear();
            dtp2.Constraints.Clear();

            if (Main.Amatrix.acc == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM acc_linking WHERE [For Book] = '" + frbkk + "' AND [For Row] = '" + dgvr.Cells[0].Value.ToString() + "' AND [Type] = '" + lnktpe_str + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp2.Load(dr);
                conn.Close();
            }
            else
            {
                dtp2 = basql.Execute(Main.Amatrix.acc, "SELECT * FROM acc_linking WHERE [For Book] = '" + frbkk + "' AND [For Row] = '" + dgvr.Cells[0].Value.ToString() + "' AND [Type] = '" + lnktpe_str + "'", "acc_linking", dtp2);
            }
            if (dtp2.Rows.Count > 0) { old = true; }
        }

        private void bkk_ld2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv.DataSource = dtp2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            acc_invce vce = new acc_invce();
            vce.tx_out(cbx.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("DELETE FROM acc_linking WHERE [Key] = '" + dgv[0, 0].Value.ToString() + "'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    conn.Close();
                    loaddb();
                    ok();
                }
                else
                {
                    DataTable dtpy = new DataTable();
                    basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [Key] = '" + dgv[0, 0].Value.ToString() + "'", "acc_linking", dtpy);
                    dtpy.Clear();
                    dtpy.Dispose();
                    loaddb();
                    ok();
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Amatrix was Unable to Replace Your Record"); }
        }
    }
}
