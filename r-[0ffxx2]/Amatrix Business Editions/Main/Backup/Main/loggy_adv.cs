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
using System.Linq;
using System.Data.SqlServerCe;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class loggy_adv : Form
    {
        public loggy_adv()
        {
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Disposed += new EventHandler(loggy_adv_Disposed);
            InitializeComponent();

            get_();
            get_2();
            get_3();
            get_AMS_();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
            //MessageBox.Show("Network Server Connections are not available in this Version of Amatrix. Please Contact Astreous to Install Your Network Services.", "Network Server Login Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void loggy_adv_Disposed(object sender, EventArgs e)
        {
            button1.Click -= button1_Click;
            this.Disposed -= loggy_adv_Disposed;
            this.tabPage4.Click -= this.tabPage4_Click;
            this.checkBox2.CheckedChanged -= this.chk_bx_CheckedChanged;
            this.checkBox1.CheckedChanged -= this.chk_bx_CheckedChanged;
            this.chk_bx.CheckedChanged -= this.chk_bx_CheckedChanged;
            this.brw.Click -= this.brw_Click;
            this.dne.Click -= this.dne_Click;
            this.button2.Click -= this.button2_Click;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.Deactivate -= this.loggy_adv_Deactivate;
            this.Load -= this.loggy_adv_Load;
            this.Activated -= this.loggy_adv_Activated;

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

        private void get_3()
        {
            string ss;
            try
            {
                int n = Main.Amatrix.acc.IndexOf("=", 0);
                int n2 = Main.Amatrix.acc.IndexOf("\\", 0);
                ss = Main.Amatrix.acc.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox13.Text = ss;

                n = Main.Amatrix.acc.IndexOf("\\", n);
                n2 = Main.Amatrix.acc.IndexOf("; Initial", n2);
                ss = Main.Amatrix.acc.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox14.Text = ss;

                n = Main.Amatrix.acc.IndexOf("; Initial", n);
                ss = Main.Amatrix.acc;
                try
                {
                    n2 = Main.Amatrix.acc.IndexOf("; uid", n2);
                    ss = Main.Amatrix.acc.Remove(n2);
                }
                catch (Exception erty) { }
                ss = ss.Remove(0, n + 18);
                textBox12.Text = ss;

                try
                {
                    ss = Main.Amatrix.acc;
                    n = Main.Amatrix.acc.IndexOf("; uid", n);
                    n2 = Main.Amatrix.acc.IndexOf("; pwd", n2);
                    ss = Main.Amatrix.acc.Remove(n2);
                    ss = ss.Remove(0, n + 6);
                    textBox11.Text = ss;

                    ss = Main.Amatrix.acc;
                    n = Main.Amatrix.acc.IndexOf("; pwd", n);
                    ss = ss.Remove(0, n + 6);
                    textBox10.Text = ss;
                }
                catch (Exception erty) { }
                checkBox2.Checked = false;
            }
            catch (Exception erty) { }
        }

        private void get_2()
        {
            string ss;
            try
            {
                int n = Main.Amatrix.mgt.IndexOf("=", 0);
                int n2 = Main.Amatrix.mgt.IndexOf("\\", 0);
                ss = Main.Amatrix.mgt.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox8.Text = ss;

                n = Main.Amatrix.mgt.IndexOf("\\", n);
                n2 = Main.Amatrix.mgt.IndexOf("; Initial", n2);
                ss = Main.Amatrix.mgt.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox9.Text = ss;

                n = Main.Amatrix.mgt.IndexOf("; Initial", n);
                ss = Main.Amatrix.mgt;
                try
                {
                    n2 = Main.Amatrix.mgt.IndexOf("; uid", n2);
                    ss = Main.Amatrix.mgt.Remove(n2);
                }
                catch (Exception erty) { }
                ss = ss.Remove(0, n + 18);
                textBox7.Text = ss;

                try
                {
                    ss = Main.Amatrix.mgt;
                    n = Main.Amatrix.mgt.IndexOf("; uid", n);
                    n2 = Main.Amatrix.mgt.IndexOf("; pwd", n2);
                    ss = Main.Amatrix.mgt.Remove(n2);
                    ss = ss.Remove(0, n + 6);
                    textBox6.Text = ss;

                    ss = Main.Amatrix.mgt;
                    n = Main.Amatrix.mgt.IndexOf("; pwd", n);
                    ss = ss.Remove(0, n + 6);
                    textBox5.Text = ss;
                }
                catch (Exception erty) { }
                checkBox1.Checked = false;
            }
            catch (Exception erty) { }
        }

        private void get_()
        {
            string ss;
            try
            {
                int n = Main.Amatrix.doc.IndexOf("=", 0);
                int n2 = Main.Amatrix.doc.IndexOf("\\", 0);
                ss = Main.Amatrix.doc.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox1.Text = ss;

                n = Main.Amatrix.doc.IndexOf("\\", n);
                n2 = Main.Amatrix.doc.IndexOf("; Initial", n2);
                ss = Main.Amatrix.doc.Remove(n2);
                ss = ss.Remove(0, n + 1);
                u_nme.Text = ss;

                n = Main.Amatrix.doc.IndexOf("; Initial", n);
                ss = Main.Amatrix.doc;
                try
                {
                    n2 = Main.Amatrix.doc.IndexOf("; uid", n2);
                    ss = Main.Amatrix.doc.Remove(n2);
                }
                catch (Exception erty) { }
                ss = ss.Remove(0, n + 18);
                textBox2.Text = ss;

                try
                {
                    ss = Main.Amatrix.doc;
                    n = Main.Amatrix.doc.IndexOf("; uid", n);
                    n2 = Main.Amatrix.doc.IndexOf("; pwd", n2);
                    ss = Main.Amatrix.doc.Remove(n2);
                    ss = ss.Remove(0, n + 6);
                    textBox3.Text = ss;

                    ss = Main.Amatrix.doc;
                    n = Main.Amatrix.doc.IndexOf("; pwd", n);
                    ss = ss.Remove(0, n + 6);
                    textBox4.Text = ss;
                }
                catch (Exception erty) { }
                chk_bx.Checked = false;
            }
            catch (Exception erty) { }
        }

        private void get_AMS_()
        {
            string ss;
            try
            {
                int n = Main.Amatrix.AMs.IndexOf("=", 0);
                int n2 = Main.Amatrix.AMs.IndexOf("\\", 0);
                ss = Main.Amatrix.AMs.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox18.Text = ss;

                n = Main.Amatrix.AMs.IndexOf("\\", n);
                n2 = Main.Amatrix.AMs.IndexOf("; Initial", n2);
                ss = Main.Amatrix.AMs.Remove(n2);
                ss = ss.Remove(0, n + 1);
                textBox19.Text = ss;

                n = Main.Amatrix.AMs.IndexOf("; Initial", n);
                ss = Main.Amatrix.AMs;
                try
                {
                    n2 = Main.Amatrix.AMs.IndexOf("; uid", n2);
                    ss = Main.Amatrix.AMs.Remove(n2);
                }
                catch (Exception erty) { }
                ss = ss.Remove(0, n + 18);
                //textBox2.Text = ss;

                try
                {
                    ss = Main.Amatrix.AMs;
                    n = Main.Amatrix.AMs.IndexOf("; uid", n);
                    n2 = Main.Amatrix.AMs.IndexOf("; pwd", n2);
                    ss = Main.Amatrix.AMs.Remove(n2);
                    ss = ss.Remove(0, n + 6);
                    textBox16.Text = ss;

                    ss = Main.Amatrix.AMs;
                    n = Main.Amatrix.AMs.IndexOf("; pwd", n);
                    ss = ss.Remove(0, n + 6);
                    textBox15.Text = ss;
                }
                catch (Exception erty) { }
                checkBox3.Checked = true;
            }
            catch (Exception erty) { }
        }

        private void loggy_adv_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void brw_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            chk_bx.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_bx_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(chk_bx) == true)
            {
                if (chk_bx.Checked == true)
                {
                    foreach (Control c in tabPage1.Controls)
                    {
                        if (c.Name != chk_bx.Name)
                        {
                            c.Enabled = false;
                        }
                    }
                }
                else
                {
                    foreach (Control c in tabPage1.Controls)
                    {
                        c.Enabled = true;
                    }
                }
            }
            if (sender.Equals(checkBox1) == true)
            {
                if (checkBox1.Checked == true)
                {
                    foreach (Control c in tabPage3.Controls)
                    {
                        if (c.Name != checkBox1.Name)
                        {
                            c.Enabled = false;
                        }
                    }
                    checkBox1.Enabled = true;
                }
                else
                {
                    foreach (Control c in tabPage3.Controls)
                    {
                        c.Enabled = true;
                    }
                }
            }
            if (sender.Equals(checkBox2) == true)
            {
                if (checkBox2.Checked == true)
                {
                    foreach (Control c in tabPage4.Controls)
                    {
                        if (c.Name != checkBox2.Name)
                        {
                            c.Enabled = false;
                        }
                    }
                    checkBox1.Enabled = true;
                }
                else
                {
                    foreach (Control c in tabPage4.Controls)
                    {
                        c.Enabled = true;
                    }
                }
            }
        }

        private void dne_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Amatrix will have to be Restarted as a Security Measure. Continue?, if So save all Current Work then Click Yes. To Abort Click No", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                DataTable dtp = new DataTable();
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Connections", conn);
                conn.Open();
                SqlCeDataReader dr= cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();

                if (chk_bx.Checked == true)
                {
                    Main.Amatrix.doc = "";
                    dtp.Rows[3][1] = "";
                }
                else
                {
                    string sql_ = "Data Source=" + textBox1.Text + "\\" + u_nme.Text;
                    if (textBox2.Text != "")
                    {
                        sql_ = sql_ + "; Initial Catalog=" + textBox2.Text;
                    }
                    if (textBox3.Text != "")
                    {
                        sql_ = sql_ + "; uid=" + textBox3.Text;
                    }
                    if (textBox4.Text != "")
                    {
                        sql_ = sql_ + "; pwd=" + textBox4.Text;
                    }
                    Main.Amatrix.doc = sql_;
                    dtp.Rows[3][1] = sql_;
                }
                //2
                if (checkBox1.Checked == true)
                {
                    Main.Amatrix.mgt = "";
                    dtp.Rows[1][1] = "";
                }
                else
                {
                    string sql_ = "Data Source=" + textBox8.Text + "\\" + textBox9.Text;
                    if (textBox7.Text != "")
                    {
                        sql_ = sql_ + "; Initial Catalog=" + textBox7.Text;
                    }
                    if (textBox6.Text != "")
                    {
                        sql_ = sql_ + "; uid=" + textBox6.Text;
                    }
                    if (textBox5.Text != "")
                    {
                        sql_ = sql_ + "; pwd=" + textBox5.Text;
                    }
                    Main.Amatrix.mgt = sql_;
                    dtp.Rows[1][1] = sql_;
                }
                //3
                if (checkBox2.Checked == true)
                {
                    Main.Amatrix.acc = "";
                    dtp.Rows[0][1] = "";
                }
                else
                {
                    string sql_ = "Data Source=" + textBox13.Text + "\\" + textBox14.Text;
                    if (textBox12.Text != "")
                    {
                        sql_ = sql_ + "; Initial Catalog=" + textBox12.Text;
                    }
                    if (textBox11.Text != "")
                    {
                        sql_ = sql_ + "; uid=" + textBox11.Text;
                    }
                    if (textBox10.Text != "")
                    {
                        sql_ = sql_ + "; pwd=" + textBox10.Text;
                    }
                    Main.Amatrix.acc = sql_;
                    dtp.Rows[0][1] = sql_;
                }

                if (checkBox3.Checked == true)
                {
                    string sql_ = "Data Source=" + textBox18.Text + "\\" + textBox19.Text;
                    if (textBox17.Text != "")
                    {
                        sql_ = sql_ + "; Initial Catalog=" + textBox17.Text;
                    }
                    if (textBox16.Text != "")
                    {
                        sql_ = sql_ + "; uid=" + textBox16.Text;
                    }
                    if (textBox15.Text != "")
                    {
                        sql_ = sql_ + "; pwd=" + textBox15.Text;
                    }
                    Main.Amatrix.AMs = sql_;
                    dtp.Rows[4][1] = sql_;
                }
                else
                {
                    Main.Amatrix.AMs = "";
                    dtp.Rows[4][1] = "";
                }
                //Properties.Settings.Default.Save();
            
                DataTable table = new DataTable();
                table = dtp;
                DataTable table2 = new DataTable();

                using (var con = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM Connections", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }

                SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                SqlCeCommand cmd3 = new SqlCeCommand("UPDATE co_nfo SET [usr_nme] = '" + SystemInformation.UserName + "\\AM-USER\\" + SystemInformation.UserDomainName + "'", conn3);
                conn3.Open();
                cmd3.ExecuteNonQuery();
                conn3.Close();

                Application.Restart();
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

        private void loggy_adv_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void loggy_adv_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Astreous_Device_Module.Form1 fmm = new Astreous_Device_Module.Form1();
            fmm.Show();
            fmm.TopMost = true;
            fmm.BringToFront();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox18.Enabled = true;
                textBox19.Enabled = true;
                textBox15.Enabled = true;
                textBox16.Enabled = true;
            }
            else
            {
                textBox18.Enabled = false;
                textBox19.Enabled = false;
                textBox15.Enabled = false;
                textBox16.Enabled = false;
            }
        }
    }
}
