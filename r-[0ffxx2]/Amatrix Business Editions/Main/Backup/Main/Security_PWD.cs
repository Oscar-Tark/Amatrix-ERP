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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Security_PWD : Form
    {
        bool localmode = false; bool aprooved = false;
        public Security_PWD()
        {
            InitializeComponent();
        }

        private void Security_PWD_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Owner.Close();
            }
            catch (Exception erty) { }
            try
            {
                this.Close();
            }
            catch (Exception erty) { }
        }
        /*SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM PermissionString", conn);
        conn.Open();
        SqlCeDataReader dr = cmd.ExecuteReader();
        DataTable dtp = new DataTable();
        dtp.Load(dr);
        Perms = dtp.Rows[0].ItemArray[0].ToString();
        dtp.Clear(); dtp.Dispose();
        conn.Close();*/

        private string app_ = ""; string connection_;
        public void tx(bool SQL_, String app, String Name_of, string Connection_String)
        {
            connection_ = Connection_String;
            label2.Text = Name_of + " Is Locked";
            app_ = app.ToLower();
            localmode = SQL_;
            try
            {
                //do_it();
                bkk.RunWorkerAsync();
            }
            catch (Exception erty) { do_it(); }
        }

        private string Perms = ""; private string UNMA = ""; bool show = false; string s2 = "";
        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            do_it();
        }

        private void do_it()
        {
            string s = "";
            try
            {
                //dynamic security selection
                int ndx = connection_.ToLower().IndexOf("initial catalog=", 0);
                int ndx2 = connection_.ToLower().IndexOf(";", ndx);

                s = connection_.Remove(ndx2);
                s = s.Remove(0, ndx + 16);

                s2 = connection_.Replace(s, "Security");

                SqlConnection conn = new SqlConnection(s2);
                conn.Open();
                conn.Close();
            }
            catch (Exception erty) { this.Owner.Close(); show = false; button2.Enabled = false; textBox1.Enabled = false; MessageBox.Show("Amatrix Ready Care was unable to find Your Network Permissions Settings. The Application has been Locked Down for Security.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            string uname = "";

            try
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM co_nfo", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                DataTable dtp = new DataTable();
                dtp.Load(dr);
                uname = dtp.Rows[0].ItemArray[5].ToString();
                conn.Close();
                dtp.Clear(); dtp.Dispose();

                SqlConnection conn2 = new SqlConnection(s2);
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM perms WHERE [uname] = '" + uname + "' AND [ForApp] = '" + app_ + "'", conn2);
                conn2.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();
                DataTable dtp2 = new DataTable();
                dtp2.Load(dr2);
                if (dtp2.Rows.Count > 0) { show = true; }
                conn2.Close(); dtp2.Clear(); dtp2.Dispose();
            }
            catch (Exception erty) { MessageBox.Show(erty.StackTrace); }
        }

        private void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (show == true) { this.Show(); this.Owner.Enabled = false; tms.Start(); }
            else { this.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bkk_Authenti.RunWorkerAsync();
            }
            catch (Exception erty) { }
        }

        private void Security_PWD_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private string pwd = "";
        private void bkk_Authenti_DoWork(object sender, DoWorkEventArgs e)
        {
            string uname = "";
            try
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM co_nfo", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                DataTable dtp = new DataTable();
                dtp.Load(dr);
                uname = dtp.Rows[0].ItemArray[5].ToString();
                conn.Close();
                dtp.Clear(); dtp.Dispose();
            }
            catch (Exception erty) { bkk_Authenti.CancelAsync(); Am_err ner = new Am_err(); ner.tx("Authentication Processing Failed"); }
            try
            {
                SqlConnection conn = new SqlConnection(s2);
                SqlCommand cmd = new SqlCommand("SELECT * FROM perms WHERE [uname] = '" + uname + "' AND [ForApp] = '" + app_ + "'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dtp = new DataTable();
                dtp.Load(dr);
                pwd = dtp.Rows[0].ItemArray[1].ToString();
                conn.Close(); dtp.Clear(); dtp.Dispose();
            }
            catch (Exception erty) { bkk_Authenti.CancelAsync(); }
        }

        private void bkk_Authenti_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Am_err ner = new Am_err(); ner.tx("Authentication Processing Failed");
            }
            if (textBox1.Text == pwd && e.Cancelled == false)
            {
                tms.Stop();
                this.Owner.Enabled = true;
                this.Close();
            }
            else { textBox1.BackColor = Color.DarkOrange; textBox1.SelectAll(); }
        }

        private void tms_Tick(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
        }
    }
}
