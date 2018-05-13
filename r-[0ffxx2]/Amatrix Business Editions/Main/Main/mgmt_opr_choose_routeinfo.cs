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
    public partial class mgmt_opr_choose_routeinfo : Form
    {
        public mgmt_opr_choose_routeinfo()
        {
            InitializeComponent();
        }

        private void mgmt_opr_choose_routeinfo_Load(object sender, EventArgs e)
        {

        }

        private string s_location = ""; private string CUST = ""; private string CUST_ID = ""; DataTable dtp = new DataTable();
        public void tx(DataTable Prods, String Addr, String Customer, String CUST_ID_, string Ware_house, string Logistical, string Corporate)
        {
            dtp = Prods; s_location = Addr; CUST = Customer; CUST_ID = CUST_ID_;
            dgv_prods.DataSource = dtp;
            tabPage1.Controls.Remove(dgv_prods);
            label2.Text = "0/" + dtp.Rows.Count.ToString();
            bkk_duplicate.RunWorkerAsync();
            this.Show();
        }

        private string our_addr = ""; String Izd;
        private void bkk_route_DoWork(object sender, DoWorkEventArgs e)
        {
            Izd = "AM-LOD-" + DateTime.Now.ToString() + "-ID";
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn_ = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                SqlCeCommand cmd_ = new SqlCeCommand("SELECT * FROM co_nfo", conn_);
                conn_.Open();
                SqlCeDataReader dr_ = cmd_.ExecuteReader();
                DataTable dty = new DataTable(); dty.Load(dr_);
                our_addr = dty.Rows[0].ItemArray[3].ToString();
                conn_.Close();

                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO Logs_mgmt VALUES('" + Izd + "', '" + our_addr + "', '" + s_location + "','','','','','','','','','','','','','','','',NULL,'')", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                conn.Close();

                int max = 0;
                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd2 = new SqlCeCommand("SELECT max([Product Serial Number]) FROM Logs_prod", conn2);
                conn2.Open();
                SqlCeDataReader dr2 = cmd2.ExecuteReader();
                dty.Clear(); dty.Columns.Clear(); dty.Load(dr2);
                try
                {
                    max = Convert.ToInt32(dty.Rows[0].ItemArray[0]);
                    max = max + 1;
                }
                catch (Exception erty) { }
                conn2.Close();
                dty.Clear(); dty.Columns.Clear(); dty.Dispose();

                foreach (DataGridViewRow dgvr in dgv_prods.Rows)
                {
                    try
                    {
                        SqlCeConnection conn1 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd1 = new SqlCeCommand("INSERT INTO Logs_prod VALUES('" + max.ToString() + "','" + dgvr.Cells[1].Value.ToString() + "','" + Izd + "','','" + dgvr.Cells[2].Value.ToString() + "')", conn1);
                        conn1.Open();
                        SqlCeDataReader dr1 = cmd1.ExecuteReader();
                        conn1.Close();
                    }
                    catch (Exception erty) { }
                    max++;
                }
            }
            else
            {

            }
        }

        private void bkk_route_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        //check duplicate
        private void bkk_duplicate_DoWork(object sender, DoWorkEventArgs e)
        {
            check();
        }

        int num_dupl = 0;
        private void check()
        {
            DataTable temp = new DataTable();
            foreach (DataGridViewRow dgvrr in dgv_prods.Rows)
            {
                try
                {
                    temp.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Logs_prod WHERE [Product Reference ID] = '" + dgvrr.Cells[1].Value.ToString() + "' AND [Owning Product ID] = '" + dgvrr.Cells[2].Value.ToString() + "'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        temp.Load(dr);
                        conn.Close();
                        if (temp.Rows.Count > 0)
                        { num_dupl++; }
                    }
                    else
                    { }
                }
                catch (Exception erty) { }
            }
        }

        private void bkk_duplicate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tabPage1.Controls.Add(dgv_prods);
            label2.Text = num_dupl.ToString() + "/" + dtp.Rows.Count.ToString();
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 100;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            { textBox1.Enabled = false; }
            else { textBox1.Enabled = true; }
        }
    }
}
