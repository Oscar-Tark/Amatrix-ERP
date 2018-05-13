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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Ready_Care_Control : Form
    {
        public Ready_Care_Control()
        {
            this.Disposed += new EventHandler(Ready_Care_Control_Disposed);
            InitializeComponent();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
            tabControl1.TabPages.RemoveAt(2);
            tabControl1.TabPages.RemoveAt(0);
        }

        void Ready_Care_Control_Disposed(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                err_dtst.Clear();
                error_logTableAdapter.Connection.Close();

                err_dtst.Dispose();
                errorlogBindingSource.Dispose();
                error_logTableAdapter.Dispose();

                dt_temp.Clear();
                dt_temp2.Clear();
                dt_temp3.Clear();

                dt_temp.Dispose();
                dt_temp2.Dispose();
                dt_temp3.Dispose();
            }
            catch (Exception erty) { }

            this.Disposed -= Ready_Care_Control_Disposed;
            this.bkk_prd.DoWork -= this.bkk_prd_DoWork;
            this.bkk_prd.RunWorkerCompleted -= this.bkk_prd_RunWorkerCompleted;
            this.bkk_check.DoWork -= this.bkk_check_DoWork;
            this.bkk_check.RunWorkerCompleted -= this.bkk_check_RunWorkerCompleted;
            this.restartToolStripMenuItem.Click -= this.restartToolStripMenuItem_Click;
            this.exitToolStripMenuItem.Click -= this.btn_clse_Click;
            this.aboutToolStripMenuItem.Click -= this.aboutToolStripMenuItem_Click;
            this.tmeclsecal.Tick -= this.tmeclsecal_Tick;
            this.dectmecal.Tick -= this.dectmecal_Tick;
            this.Deactivate -= this.Ready_Care_Control_Deactivate;
            this.Load -= this.Ready_Care_Control_Load;
            this.Activated -= this.Ready_Care_Control_Activated;
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

        private void Ready_Care_Control_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmecal.Stop();
            }
            catch (Exception erty)
            { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        DataTable dt_temp, dt_temp2, dt_temp3;
        private void bkk_prd_DoWork(object sender, DoWorkEventArgs e)
        {
            /*SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
            SqlCeCommand cmd = new SqlCeCommand("SELECT [Edition Key] FROM Settings", conn);
            conn.Open();
            SqlCeDataReader dr = cmd.ExecuteReader();
            dt_temp = new DataTable();
            dt_temp.Load(dr);
            conn.Close();*/
        }

        private void bkk_prd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //label3.Text = "Edition Key " + dt_temp.Columns[0].Table.Rows[0].ItemArray[0].ToString();
            dt_temp.Dispose();
        }

        private void bkk_check_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dt_temp2.Columns[0].Table.Rows[0].ItemArray[0].ToString() == "true")
            {
                //bkk_prd.RunWorkerAsync();
            }
            else
            {
                //button3.Visible = true;
            }
            //dt_temp2.Dispose();
        }

        private void bkk_check_DoWork(object sender, DoWorkEventArgs e)
        {
            /*SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
            SqlCeCommand cmd = new SqlCeCommand("SELECT [Is Registered] FROM Settings", conn);
            conn.Open();
            SqlCeDataReader dr = cmd.ExecuteReader();
            dt_temp2 = new DataTable();
            dt_temp2.Load(dr);
            conn.Close();*/
        }

        private void btn_clse_Click(object sender, EventArgs e)
        {
            tmeclsecal.Start();
        }

        string what_;
        private void lv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (lv1.FocusedItem.Text.ToLower() == "processor asl certificate")
            {
                what_ = "Processor ESL Certificate";
            }
            else if (lv1.FocusedItem.Text.ToLower() == "bios asl certificate")
            {
                what_ = "Bios ESL Certificate";
            }
            else if (lv1.FocusedItem.Text.ToLower() == "agreement asl certificate")
            {
                what_ = "Agreement to ESL Certificate";
            }
            else if (lv1.FocusedItem.Text.ToLower() == "user asl certificate")
            {
                what_ = "User ESL Certificate";
            }

            try
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT [" + what_ + "] FROM Certificates", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dt_temp = new DataTable();
                dt_temp.Load(dr);
                conn.Close();
                richTextBox1.Text = dt_temp.Columns[0].Table.Rows[0].ItemArray[0].ToString();
            }
            catch (Exception erty) { richTextBox1.Text = "The Content of This Certificate is Not Available, this Could be due to Certificate Corruption."; }*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ready_Care_Control rcc = new Ready_Care_Control();
            rcc.Show();
            this.Close();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Helper hl = new Helper();
            hl.tx(this.Name);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app_abt abt = new app_abt();
            abt.descr(this.Text);
        }

        private void Ready_Care_Control_Load(object sender, EventArgs e)
        {
        }

        private void tmeclsecal_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void dectmecal_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmecal.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void Ready_Care_Control_Deactivate(object sender, EventArgs e)
        {
            dectmecal.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "09976889-b44")
            {
                textBox2.Visible = true; button1.Enabled = true;
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM PermissionString", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                PS = dtp.Rows[0].ItemArray[0].ToString();
                textBox2.Text = PS;
                conn.Close();
                dtp.Clear();
            }
            else
            { textBox2.Visible = false; button1.Enabled = false; }
        }

        private DataTable dtp = new DataTable(); private string PS = "";
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "09976889-b44")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("UPDATE PermissionString SET [String] = '" + textBox2.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                conn.Close();
                this.Close();
            }
        }

        private void GAC_CHANGE(object sender, EventArgs e)
        {
            if (sender.Equals(radioButton1) == true)
            {
                Properties.Settings.Default.GACTIME = 15000;
            }
            else if (sender.Equals(radioButton3) == true)
            {
                Properties.Settings.Default.GACTIME = 60000;
            }
            else { Properties.Settings.Default.GACTIME = 9999999; }
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            tabControl1.TabPages.Add(tabPage4);
            this.error_logTableAdapter.Fill(this.err_dtst.error_log);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
